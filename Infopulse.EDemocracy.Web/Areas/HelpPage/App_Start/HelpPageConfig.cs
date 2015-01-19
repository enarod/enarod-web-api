// Uncomment the following to provide samples for PageResult<T>. Must also add the Microsoft.AspNet.WebApi.OData
// package to your project.
////#define Handle_PageResultOfT

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
#if Handle_PageResultOfT
using System.Web.Http.OData;
#endif
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Areas.HelpPage
{
	/// <summary>
	/// Use this class to customize the Help Page.
	/// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
	/// or you can provide the samples for the requests/responses.
	/// </summary>
	public static class HelpPageConfig
	{
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
			MessageId = "Infopulse.EDemocracy.Web.Areas.HelpPage.TextSample.#ctor(System.String)",
			Justification = "End users may choose to merge this string with existing localized resources.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
			MessageId = "bsonspec",
			Justification = "Part of a URI.")]
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
			config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

			// Uncomment the following to use the documentation from XML documentation file.
			config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

			// Uncomment the following to use "sample string" as the sample for all actions that have string as the body parameter or return type.
			// Also, the string arrays will be used for IEnumerable<string>. The sample objects will be serialized into different media type 
			// formats by the available formatters.
			//config.SetSampleObjects(new Dictionary<Type, object>
			//{
			//	{typeof(string), "sample string"},
			//	{typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}},
			//	{
			//		typeof(Model.BusinessEntities.Petition), new Model.BusinessEntities.Petition()
			//		{
			//			ID = 31,
			//			AddressedTo = "not used",
			//			Email = "creator's email",
			//			EffectiveFrom = DateTime.Now.AddDays(-1),
			//			EffectiveTo = DateTime.Now.AddDays(1),
			//			KeyWords = new List<string>() {"tag1", "tag2"},
			//			CreatedDate = DateTime.Now.AddDays(-2),
			//			CreatedBy = new People()
			//			{
			//				ID = 11,
			//				Login = "default user"
			//			},
			//			Limit = 100,
			//			Text = "main petition text",
			//			Requirements = "petition requirements",
			//			Subject = "subject",
			//			VotesCount = 57,
			//			Category = new Model.BusinessEntities.Entity()
			//			{
			//				ID = 4,
			//				Name = "Active",
			//				Description = "Petition is active",
			//				Group = null
			//			},
			//			Level = new Model.BusinessEntities.PetitionLevel()
			//			{
			//				ID = 14,
			//				Name = "City",
			//				Limit = 2000
			//			},
			//			Organization = new Model.BusinessEntities.Organization()
			//			{
							
			//			}
			//		}
			//	}
			//});

			// Extend the following to provide factories for types not handled automatically (those lacking parameterless
			// constructors) or for which you prefer to use non-default property values. Line below provides a fallback
			// since automatic handling will fail and GeneratePageResult handles only a single type.
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

			// Extend the following to use a preset object directly as the sample for all actions that support a media
			// type, regardless of the body parameter or return type. The lines below avoid display of binary content.
			// The BsonMediaTypeFormatter (if available) is not used to serialize the TextSample object.
			config.SetSampleForMediaType(
				new TextSample("Binary JSON content. See http://bsonspec.org for details."),
				new MediaTypeHeaderValue("application/bson"));

			//// Uncomment the following to use "[0]=foo&[1]=bar" directly as the sample for all actions that support form URL encoded format
			//// and have IEnumerable<string> as the body parameter or return type.
			//config.SetSampleForType("[0]=foo&[1]=bar", new MediaTypeHeaderValue("application/x-www-form-urlencoded"), typeof(IEnumerable<string>));

			//// Uncomment the following to use "1234" directly as the request sample for media type "text/plain" on the controller named "Values"
			//// and action named "Put".
			//config.SetSampleRequest("1234", new MediaTypeHeaderValue("text/plain"), "Values", "Put");

			//// Uncomment the following to use the image on "../images/aspNetHome.png" directly as the response sample for media type "image/png"
			//// on the controller named "Values" and action named "Get" with parameter "id".
			//config.SetSampleResponse(new ImageSample("../images/aspNetHome.png"), new MediaTypeHeaderValue("image/png"), "Values", "Get", "id");

			//// Uncomment the following to correct the sample request when the action expects an HttpRequestMessage with ObjectContent<string>.
			//// The sample will be generated as if the controller named "Values" and action named "Get" were having string as the body parameter.
			//config.SetActualRequestType(typeof(string), "Values", "Get");

			//// Uncomment the following to correct the sample response when the action returns an HttpResponseMessage with ObjectContent<string>.
			//// The sample will be generated as if the controller named "Values" and action named "Post" were returning a string.
			//config.SetActualResponseType(typeof(string), "Values", "Post");
		}

#if Handle_PageResultOfT
        private static object GeneratePageResult(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            if (type.IsGenericType)
            {
                Type openGenericType = type.GetGenericTypeDefinition();
                if (openGenericType == typeof(PageResult<>))
                {
                    // Get the T in PageResult<T>
                    Type[] typeParameters = type.GetGenericArguments();
                    Debug.Assert(typeParameters.Length == 1);

                    // Create an enumeration to pass as the first parameter to the PageResult<T> constuctor
                    Type itemsType = typeof(List<>).MakeGenericType(typeParameters);
                    object items = sampleGenerator.GetSampleObject(itemsType);

                    // Fill in the other information needed to invoke the PageResult<T> constuctor
                    Type[] parameterTypes = new Type[] { itemsType, typeof(Uri), typeof(long?), };
                    object[] parameters = new object[] { items, null, (long)ObjectGenerator.DefaultCollectionSize, };

                    // Call PageResult(IEnumerable<T> items, Uri nextPageLink, long? count) constructor
                    ConstructorInfo constructor = type.GetConstructor(parameterTypes);
                    return constructor.Invoke(parameters);
                }
            }

            return null;
        }
#endif
	}
}
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Infopulse.EDemocracy.Email.eNarod.Templates
{
	public abstract class Template
	{
		protected const string TemplateFolder = "~/EmailTemplates/{0}.html";

		public string Subject { get; set; }
		public string Name { get; set; }

		public string Text { get; set; }
		public string FilePath { get; set; }


		protected Template(string subject, string name)
		{
			this.Subject = subject;
			this.Name = name;

			this.FilePath = this.GetFullPathToTemplate();
			this.Text = this.ReadTemplate();
		}


		private string ReadTemplate()
		{
			var text = File.ReadAllText(this.FilePath);
			return text;
		}


		private string GetFullPathToTemplate()
		{
			var relativePath = string.Format(TemplateFolder, this.Name);
			var fullPath = HttpContext.Current.Server.MapPath(relativePath);
			return fullPath;
		}


		public string Fill(Dictionary<string, string> parameters)
		{
			var text = string.Copy(this.Text);

			foreach (var parameter in parameters)
			{
				var parameterName = this.SanitizeParameterName(parameter.Key);
				text = text.Replace(parameterName, parameter.Value);
			}

			return text;
		}


		private string SanitizeParameterName(string parameterName)
		{
			var sanitizedParameterName = parameterName
				.Replace("{", "")
				.Replace("}", "")
				.ToUpper();
			sanitizedParameterName = "{{" + sanitizedParameterName + "}}";
			return sanitizedParameterName;
		}
	}
}
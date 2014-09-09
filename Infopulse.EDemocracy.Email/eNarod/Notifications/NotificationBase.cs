using Infopulse.EDemocracy.Email.eNarod.Templates;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Email.eNarod.Notifications
{
	public abstract class NotificationBase
	{
		public Action Action { get; private set; }

		public string Recipient { get; private set; }

		public string Subject { get { return this.Template.Subject; } }
		public string Text { get; private set; }

		public Template Template { get; private set; }

		public Dictionary<string, string> Parameters { get; private set; }

		public string SuccessfulySentMessage { get; private set; }


		protected NotificationBase(Action action, string recipient, Dictionary<string, string> parameters, string successMesage)
		{
			this.Action = action;
			this.Template = TemplateMap.Map[action];

			this.Parameters = parameters;
			this.SuccessfulySentMessage = successMesage;

			this.Recipient = recipient;
			this.Text = this.Template.Fill(parameters);
		}
	}
}
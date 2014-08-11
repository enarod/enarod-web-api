using Infopulse.EDemocracy.Model.Common;
using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using M = Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionEmailVote : BaseEntity
	{
		public Petition Petition { get; set; }
		public string Email { get; set; }
		public string Hash { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsConfirmed { get; set; }


		public PetitionEmailVote(Petition petition, string email)
		{
			this.Petition = petition;
			this.Email = email;
		}


		M.PetitionEmailVote Map()
		{
			return new M.PetitionEmailVote()
						{
							ID = ID,
							PatitionID = this.Petition.ID,
							email = this.Email,
							hash = this.Hash,
							CreatedDate = CreatedDate,
							IsConfirmed = IsConfirmed
						};
		}


		public void Save()
		{
			this.Prepare();

			if (this.SendEmail())
			{
				using (var db = new EDEntities())
				{
					var item = Map();
					db.PetitionEmailVotes.Add(item);
					db.SaveChanges();
					ID = item.ID;
				}
			}
		}


		private bool SendEmail()
		{
			var text = new StringBuilder("Для підтвердження Вашого голосу необхідно натиснути наступний лінк: ");
			text.Append("https://emarod.org/app/petition/vote?hash=");
			text.Append(this.Hash);

			var message = new MailMessage
			              {
				              Subject = "Petition vote confirmation",
							  Body = text.ToString()
			              };
			message.To.Add(this.Email);

			return EmailService.SendEmail(message);
		}


		private void Prepare()
		{
			var h = new HMACSHA512(Guid.NewGuid().ToByteArray());
			this.Hash = this.ByteArrayToString(h.ComputeHash(Guid.NewGuid().ToByteArray()));

			CreatedDate = DateTime.Now;
			IsConfirmed = false;
		}


		private string ByteArrayToString(byte[] input)
		{
			var result = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				result.Append((char)input[i]);
			}

			return result.ToString();
		}
	}
}
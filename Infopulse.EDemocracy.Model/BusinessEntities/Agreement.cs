using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Agreement : BaseEntity
	{
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		public Candidate Candidate { get; set; }
		public Entity Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public People CreatedBy { get; set; }
		public DateTime? UpdateDate { get; set; }
		public People UpdatedBy { get; set; }

		public int? NumberOfVotes { get; set; }


		public Agreement()
		{
		}


		public Agreement(Model.Agreement agreement)
		{
			this.ID = agreement.ID;
			this.Name = agreement.Name;
			this.ShortDesc = agreement.ShortDesc;
			this.Description = agreement.Description;
			this.Candidate = new Candidate(agreement.Candidate);
			this.Status = new Entity(agreement.Entity);
			this.CreatedDate = agreement.CreatedDate;
			this.CreatedBy = new People(agreement.Person);
			this.UpdateDate = agreement.UpdatedDate;
			this.UpdatedBy = agreement.UpdatedBy != null ? new People(agreement.Person1) : null;
		}


		public static Agreement GetAgreement(long ID)
		{
			using (var db = new Model.EDEntities())
			{
				var result = from p in db.Agreements
							 where p.ID == ID
							 select p;

				var first = result.FirstOrDefault();
				if (first == default(Model.Agreement))
					throw new ApplicationException("Agreement not found {ID = " + ID.ToString() + "}");

				var amount = (from p in db.AgreementVotes
							  where p.AgreementID == first.ID
							  select p).Count();

				Agreement agreement = new Agreement(first);
				return agreement;
			}
		}
	}
}
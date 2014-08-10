using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
    public class PetitionVote : BaseEntity
    {
        public Petition Petition { get; set; }
        public People Person { get; set; }
        public Certificate Certificate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SignedData { get; set; }
        public string SignedHash { get; set; }

        Model.PetitionVote Map()
        {
            return new Model.PetitionVote()
                        {
                            PetitionID = this.Petition.ID,
                            PersonID = this.Person.ID,
                            CertificateID = this.Certificate.ID,
                            CreatedDate = this.CreatedDate,
                            SignedData = this.SignedData,
                            SignedHash = this.SignedHash
                        };
        }

        public void Vote()
        {
            using (var db = new Model.EDEntities())
            {
                var result = (from p in db.PetitionVotes
                              where p.PetitionID == this.Petition.ID
                                 && (p.PersonID == this.Person.ID || p.CertificateID == this.Certificate.ID)
                              select p).Count();

                if (result > 0)
                    throw new ApplicationException("This vote was already counted");

                var vote = this.Map();
                db.PetitionVotes.Add(vote);
                db.SaveChanges();
                this.ID = vote.ID;
            }
        }
    }
}
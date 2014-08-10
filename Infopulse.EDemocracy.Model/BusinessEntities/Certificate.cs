using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
    public class Certificate : BaseEntity
    {
        public Entity CType { get; set; }
        public People Person { get; set; }
        public string SerialNumber { get; set; }


        Model.Certificate Map()
        {
            return new Model.Certificate()
                        {
                            TypeID = this.CType.ID,
                            PersonID = this.Person.ID,
                            SerialNumber = this.SerialNumber
                        };
        }


        public void Save(Model.EDEntities db)
        {
            var result = (from p in db.Certificates
                         where p.SerialNumber == this.SerialNumber
                         select p).Count();

            if (result > 0)
                throw new ApplicationException("Person with this certificate has already been registered");

            var certificate = this.Map();
            db.Certificates.Add(certificate);
            db.SaveChanges();
            this.ID = certificate.ID;
        }
    }
}
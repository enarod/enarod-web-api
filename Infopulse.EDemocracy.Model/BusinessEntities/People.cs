using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
    public class People : BaseEntity
    {
        public string Login { get; set; }

        public People()
        {
        }

        public People(Model.Person person)
        {
            this.ID = person.ID;
            this.Login = person.Login;
        }

        Model.Person Map()
        {
            return new Model.Person()
                        {
                            Login = this.Login
                        };
        }

        public void Save(Model.EDEntities db)
        {
            var result = (from p in db.People
                          where p.Login.ToUpper() == this.Login.ToUpper()
                          select p).Count();

            if (result > 0)
                throw new ApplicationException("Person with this login has already been registered");

            var person = this.Map();
            db.People.Add(person);
            db.SaveChanges();
            this.ID = person.ID;
        }
    }
}
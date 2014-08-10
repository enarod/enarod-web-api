using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Entity : BaseEntity
	{
		public EntityGroup Group { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Entity(Model.Entity entity)
		{
            Map(entity);
		}

        public Entity(string Name)
        {
            using (Model.EDEntities db = new EDEntities())
            {
                var result = from p in db.Entities
                             where p.Name == Name
                             select p;

                var first = result.FirstOrDefault();
                if (first == default(Model.Entity))
                    throw new ApplicationException("Entity not found");

                Map(first);
            }
        }

        public Entity(long ID)
        {
            using (Model.EDEntities db = new EDEntities())
            {
                var result = from p in db.Entities
                             where p.ID == ID
                             select p;

                var first = result.FirstOrDefault();
                if (first == default(Model.Entity))
                    throw new ApplicationException("Entity not found");

                Map(first);
            }
        }

        public bool Equals(string Name)
        {
            return this.Name.ToUpper().Equals(Name.ToUpper());
        }

        void Map(Model.Entity entity)
        {
            this.ID = entity.ID;
            this.Group = new EntityGroup(entity.EntityGroup);
            this.Name = entity.Name;
            this.Description = entity.Description;
        }
	}
}

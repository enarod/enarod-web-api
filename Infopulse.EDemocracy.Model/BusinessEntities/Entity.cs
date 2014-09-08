using System;
using System.Linq;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Entity : BaseEntity
	{
		public EntityGroup Group { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Entity()
		{
		}

		public Entity(Model.Entity entity)
		{
            Map(entity);
		}

        public Entity(string entityName)
        {
            using (var db = new EDEntities())
            {
                var result = from p in db.Entities
                             where p.Name == entityName
                             select p;

                var first = result.FirstOrDefault();
                if (first == default(Model.Entity))
                    throw new ApplicationException("Entity not found");

                Map(first);
            }
        }

        public Entity(long entityID)
        {
            using (var db = new EDEntities())
            {
                var result = from p in db.Entities
                             where p.ID == entityID
                             select p;

                var first = result.FirstOrDefault();
                if (first == default(Model.Entity))
                    throw new ApplicationException("Entity not found");

                Map(first);
            }
        }

        public bool Equals(string entityName)
        {
            return this.Name.ToUpper().Equals(entityName.ToUpper());
        }

        private void Map(Model.Entity entity)
        {
            this.ID = entity.ID;
            this.Group = new EntityGroup(entity.EntityGroup);
            this.Name = entity.Name;
            this.Description = entity.Description;
        }
	}
}

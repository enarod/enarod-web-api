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

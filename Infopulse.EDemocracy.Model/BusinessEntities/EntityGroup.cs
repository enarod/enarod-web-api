namespace Infopulse.EDemocracy.Model.BusinessEntities
{
    public class EntityGroup : BaseEntity
    {
		public EntityGroup Parent { get; set; }
        public string Name { get; set; }

	    public EntityGroup()
	    {
	    }

        public EntityGroup(Model.EntityGroup group)
        {
            this.ID = group.ID;
            this.Parent = group.ParentID.HasValue ? new EntityGroup(group.ParentID.Value) : null;
	        this.Name = group.Name;
        }

        public EntityGroup(long ID)
        {
            this.ID = ID;
        }
    }
}
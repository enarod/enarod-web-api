//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infopulse.EDemocracy.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organization
    {
        public Organization()
        {
            this.Petitions = new HashSet<Petition>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrivateDescription { get; set; }
        public string Logo { get; set; }
        public string AcceptancePolicy { get; set; }
        public Nullable<int> PreliminaryVoteCount { get; set; }
        public Nullable<int> PreliminaryGatheringDays { get; set; }
        public Nullable<int> VoteCount { get; set; }
        public Nullable<int> GatheringDays { get; set; }
    
        public virtual ICollection<Petition> Petitions { get; set; }
    }
}

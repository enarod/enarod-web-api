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
    
    public partial class Petition
    {
        public Petition()
        {
            this.PetitionVotes = new HashSet<PetitionVote>();
            this.PetitionEmailVotes = new HashSet<PetitionEmailVote>();
        }
    
        public long ID { get; set; }
        public long LevelID { get; set; }
        public string AddressedTo { get; set; }
        public string Subject { get; set; }
        public long CategoryID { get; set; }
        public string Text { get; set; }
        public string Requirements { get; set; }
        public string KeyWords { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime EffectiveFrom { get; set; }
        public System.DateTime EffectiveTo { get; set; }
        public Nullable<long> Limit { get; set; }
        public string Email { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public int PetitionStatusID { get; set; }
    
        public virtual Entity Category { get; set; }
        public virtual PetitionLevel PetitionLevel { get; set; }
        public virtual ICollection<PetitionVote> PetitionVotes { get; set; }
        public virtual ICollection<PetitionEmailVote> PetitionEmailVotes { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual User Author { get; set; }
        public virtual User Approver { get; set; }
        public virtual PetitionStatus PetitionStatus { get; set; }
    }
}

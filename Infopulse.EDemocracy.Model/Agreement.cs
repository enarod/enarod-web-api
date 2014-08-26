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
    
    public partial class Agreement
    {
        public Agreement()
        {
            this.AgreementVotes = new HashSet<AgreementVote>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public long CandidateID { get; set; }
        public long StatusID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
    
        public virtual Candidate Candidate { get; set; }
        public virtual Entity Status { get; set; }
        public virtual Person Creator { get; set; }
        public virtual Person Updater { get; set; }
        public virtual ICollection<AgreementVote> AgreementVotes { get; set; }
    }
}

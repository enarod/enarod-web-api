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
    
    public partial class PetitionEmailVote
    {
        public long ID { get; set; }
        public long PetitionID { get; set; }
        public string Hash { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public Nullable<int> PetitionSignerID { get; set; }
    
        public virtual Petition Petition { get; set; }
        public virtual PetitionSigner PetitionSigner { get; set; }
    }
}

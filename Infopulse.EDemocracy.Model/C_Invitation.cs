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
    
    public partial class C_Invitation
    {
        public long ID { get; set; }
        public long InvitationID { get; set; }
        public long ActionID { get; set; }
        public long StatusID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
    
        public virtual Entity Action { get; set; }
        public virtual Entity Status { get; set; }
        public virtual Invitation Invitation { get; set; }
    }
}

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
    
    public partial class RolePeople
    {
        public long ID { get; set; }
        public long GroupID { get; set; }
        public long RoleID { get; set; }
        public long PersonID { get; set; }
        public long StatusID { get; set; }
    
        public virtual Entity Status { get; set; }
        public virtual Group Group { get; set; }
        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
    }
}

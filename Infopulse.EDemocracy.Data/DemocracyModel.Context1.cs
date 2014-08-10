﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infopulse.EDemocracy.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DemocracyEntities : DbContext
    {
        public DemocracyEntities()
            : base("name=DemocracyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_Invitation> C_Invitation { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<AgreementVote> AgreementVotes { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactGroup> ContactGroups { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityGroup> EntityGroups { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<InvitationLink> InvitationLinks { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Petition> Petitions { get; set; }
        public virtual DbSet<PetitionLevel> PetitionLevels { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFunction> RoleFunctions { get; set; }
        public virtual DbSet<RolePeople> RolePeoples { get; set; }
    }
}

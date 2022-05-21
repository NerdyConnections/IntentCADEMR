//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntentCADEMR.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public int id { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> UserIDRequestedBy { get; set; }
        public int UserType { get; set; }
        public Nullable<int> AssignedRole { get; set; }
        public Nullable<int> TerritoryID { get; set; }
        public Nullable<int> RepID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ClinicName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Specialty { get; set; }
        public string SpeakerHonariumRange { get; set; }
        public string ModeratorHonariumRange { get; set; }
        public Nullable<int> TherapeuticID { get; set; }
        public Nullable<int> PrivilegeID { get; set; }
        public Nullable<int> SponsorID { get; set; }
        public string BoneWBSCode { get; set; }
        public string CVWBSCode { get; set; }
        public Nullable<System.DateTime> SubmittedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    
        public virtual User User { get; set; }
        public virtual UserTypeLookUp UserTypeLookUp { get; set; }
    }
}

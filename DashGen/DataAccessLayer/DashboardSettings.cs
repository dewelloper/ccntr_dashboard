//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class DashboardSettings
    {
        public int Id { get; set; }
        public int MaxExpectedWaitingTime { get; set; }
        public int MaxWaitedQueueCount { get; set; }
        public int MaxLongerWaitTime { get; set; }
        public string AcdNos { get; set; }
        public string SkillNos { get; set; }
        public string MailFormat { get; set; }
        public string MailReceipts { get; set; }
        public Nullable<int> MailSendingRepatInMinutes { get; set; }
        public Nullable<int> BoardDataRangeInHours { get; set; }
        public Nullable<int> BoardDataLastIntegralTime { get; set; }
        public Nullable<int> BoardDataIntervalCount { get; set; }
        public System.DateTime ModifiedTime { get; set; }
    }
}

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
    
    public partial class GeneralConfig
    {
        public int Id { get; set; }
        public string ImagesPath { get; set; }
        public Nullable<bool> ScreenPassAllow { get; set; }
        public Nullable<int> ScreenPassTimeInterval { get; set; }
        public Nullable<System.TimeSpan> TimeStartOfTheDay { get; set; }
        public Nullable<System.TimeSpan> TimeAfternoonOfTheDay { get; set; }
        public Nullable<System.TimeSpan> TimeEndOfTheDay { get; set; }
    }
}

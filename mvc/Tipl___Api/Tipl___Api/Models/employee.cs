//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tipl___Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class employee
    {
        public int e_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        [Remote("IsAlreadySigned", "Register", HttpMethod = "POST", ErrorMessage = "Mobile already exists in database.")]
        public string phone_no { get; set; }
        public string position { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<int> salary { get; set; }
    }
}

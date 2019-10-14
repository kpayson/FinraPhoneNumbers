using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PhoneNumberViewModel
    {
        public IList<PhoneNumberVariation> PhoneNumbers { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get {
                return (int)Math.Ceiling(((double)NumVariations) / PageSize); } }
        public int NumVariations { get; set; }
        public string PhoneNumber { get; set; }
    }
}
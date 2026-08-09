using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Models.DTOs
{
    public class CategoryOption
    {
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string? Text { get; set; }
        public string? Value { get; set; }
        public string? IconUrl { get; set; }
    }
}

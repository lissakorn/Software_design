

namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class payment
    {
        public int IdPayment { get; set; }
        public int IdBooking { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual booking booking { get; set; }
    }
}

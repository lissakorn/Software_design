

namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class providedService
    {
        public int IdService { get; set; }
        public int IdBooking { get; set; }
        public int IdProvidedService { get; set; }
    
        public virtual booking booking { get; set; }
        public virtual service service { get; set; }
    }
}

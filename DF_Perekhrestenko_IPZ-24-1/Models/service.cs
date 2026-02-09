
//------------------------------------------------------------------------------

namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public service()
        {
            this.providedServices = new HashSet<providedService>();
        }
    
        public int IdService { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<providedService> providedServices { get; set; }
    }
}

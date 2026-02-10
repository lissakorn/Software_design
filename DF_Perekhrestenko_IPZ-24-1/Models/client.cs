
namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    using System.Collections.Generic;
    // TODO: Issue #1 - Rename class to 'Client' (PascalCase).
    // Currently kept lowercase to match existing Database table names without breaking Entity Framework mapping.
    public partial class client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public client()
        {
            this.bookings = new HashSet<booking>();
        }
    
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }
        public System.DateTime Regestration { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
    }
}

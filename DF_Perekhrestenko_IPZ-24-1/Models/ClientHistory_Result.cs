
namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    
    public partial class ClientHistory_Result
    {
        public string FullName { get; set; }
        public string Number { get; set; }
        public System.DateTime CheckIn { get; set; }
        public System.DateTime CheckOut { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> GeneralExpenses { get; set; }
    }
}

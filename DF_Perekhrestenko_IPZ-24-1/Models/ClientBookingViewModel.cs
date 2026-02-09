using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    public class ClientBookingViewModel
    {
        public string ClientName { get; set; }
        public string Surname { get; set; }
        public string ClientPhone { get; set; }
        public string Passport { get; set; }
         public string Email { get; set; }

        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
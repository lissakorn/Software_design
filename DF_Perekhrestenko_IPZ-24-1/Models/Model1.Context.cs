namespace DF_Perekhrestenko_IPZ_24_1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class hotelEntities : DbContext
    {
        public hotelEntities()
            : base("name=hotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<payment> payments { get; set; }
        public virtual DbSet<room> rooms { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<providedService> providedServices { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<ClientHistory_Result> ClientHistory(string passportData)
        {
            var passportDataParameter = passportData != null ?
                new ObjectParameter("PassportData", passportData) :
                new ObjectParameter("PassportData", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientHistory_Result>("ClientHistory", passportDataParameter);
        }
    
        public virtual ObjectResult<FindFreeRoom_Result> FindFreeRoom(Nullable<System.DateTime> checkIn, Nullable<System.DateTime> checkOut, string roomType)
        {
            var checkInParameter = checkIn.HasValue ?
                new ObjectParameter("CheckIn", checkIn) :
                new ObjectParameter("CheckIn", typeof(System.DateTime));
    
            var checkOutParameter = checkOut.HasValue ?
                new ObjectParameter("CheckOut", checkOut) :
                new ObjectParameter("CheckOut", typeof(System.DateTime));
    
            var roomTypeParameter = roomType != null ?
                new ObjectParameter("RoomType", roomType) :
                new ObjectParameter("RoomType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindFreeRoom_Result>("FindFreeRoom", checkInParameter, checkOutParameter, roomTypeParameter);
        }
    }
}

using DF_Perekhrestenko_IPZ_24_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DF_Perekhrestenko_IPZ_24_1.Controllers
{
    public class ClientsController : Controller
    {
        private hotelEntities db = new hotelEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateWithBooking()
        {
            PrepareRoomList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithBooking(ClientBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new client
                {
                    Name = model.ClientName,
                    Surname = model.Surname,
                    Phone = model.ClientPhone,
                    Passport = model.Passport,
                    Email = model.Email,
                    Regestration = DateTime.Now
                };

                db.clients.Add(client);
                db.SaveChanges();

                var booking = new booking
                {
                    IdClient = client.IdClient,
                    IdRoom = model.RoomId,
                    CheckIn = model.CheckIn,
                    CheckOut = model.CheckOut,
                    Status = "Active"
                };

                db.bookings.Add(booking);
                db.SaveChanges();
            
                return RedirectToAction("Clients", "Home");
            }

            PrepareRoomList();
            return View(model);
        }

        private void PrepareRoomList()
        {
            var busyRoomIds = db.bookings
                                .Where(b => b.Status == "Active")
                                .Select(b => b.IdRoom)
                                .ToList();
            var freeRooms = db.rooms
                              .Where(r => !busyRoomIds.Contains(r.IdRoom))
                              .ToList();
            ViewBag.RoomId = new SelectList(freeRooms, "IdRoom", "Number");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DF_Perekhrestenko_IPZ_24_1.Models;
using System.Data.Entity.Validation;

namespace DF_Perekhrestenko_IPZ_24_1.Controllers
{
    public class HomeController : Controller
    {
        hotelEntities db = new hotelEntities();

        public ActionResult Index()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            var now = DateTime.Now;
            var expiredBookings = db.bookings
                .Where(b => b.Status == "Active" && b.CheckOut < now)
                .ToList();

            if (expiredBookings.Any())
            {
                foreach (var booking in expiredBookings)
                {
                    booking.Status = "completed"; 
                }
                db.SaveChanges();
            }

            if (Session["UserRole"]?.ToString() != "Client")
            {
                ViewBag.TotalRooms = db.rooms.Count();
                ViewBag.ActiveBookings = db.bookings.Count(b => b.Status == "active");
                ViewBag.TotalClients = db.clients.Count();
            }

            return View();
        }
        public ActionResult Clients()
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Account");

            try
            {
                var clientsList = db.clients.ToList();
                return View(clientsList);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

     
        [HttpGet]
        public ActionResult Insert()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public ActionResult Insert(client c)
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Account");
            try
            {
                if (ModelState.IsValid)
                {
                    c.Regestration = DateTime.Now;
                    db.clients.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                return Content("Помилка валідації: " + fullErrorMessage);
            }
            return View(c);
        }
        

        [HttpGet]
        public ActionResult CreateWithBooking()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");

            var busyRoomIds = db.bookings
                .Where(b => b.Status == "Active" && b.CheckOut >= DateTime.Now) 
                .Select(b => b.IdRoom)
                .ToList();
            var freeRooms = db.rooms.Where(r => !busyRoomIds.Contains(r.IdRoom)).ToList();

            ViewBag.RoomId = new SelectList(freeRooms, "IdRoom", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithBooking(ClientBookingViewModel model)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                try
                {
                    var newClient = new client
                    {
                        Name = model.ClientName,
                        Surname = model.Surname,
                        Phone = model.ClientPhone,
                        Passport = model.Passport,
                        Email = model.Email ?? "test@email.com",
                        Regestration = DateTime.Now
                    };

                    var newBooking = new booking
                    {
                        client = newClient,
                        IdRoom = model.RoomId,
                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        Status = "Active"
                    };
                    db.clients.Add(newClient);
                    db.bookings.Add(newBooking);

                    db.SaveChanges();

                    return RedirectToAction("Clients");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

                    var fullErrorMessage = string.Join("<br/> ", errorMessages);

                    return Content("<h3>Помилка валідації:</h3> " + fullErrorMessage);
                }
                catch (Exception ex)
                {
                    return Content("Загальна помилка: " + ex.Message);
                }
            }

 
            var busyRoomIds = db.bookings
                .Where(b => b.Status == "Active" && b.CheckOut >= DateTime.Now) 
                .Select(b => b.IdRoom)
                .ToList();
            ViewBag.RoomId = new SelectList(db.rooms.Where(r => !busyRoomIds.Contains(r.IdRoom)), "IdRoom", "Number", model.RoomId);
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            if (Session["UserRole"]?.ToString() != "HotelAdmin")
            {
                return Content("Доступ заборонено: Тільки Адміністратор може видаляти записи.");
            }

            client c = db.clients.Find(id);
            if (c != null)
            {
                db.clients.Remove(c);
                db.SaveChanges();
            }
            return RedirectToAction("Clients");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            client c = db.clients.Find(id);
            if (c == null) return HttpNotFound();
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(client c)
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Clients");
            }
            return View(c);
        }


        public ActionResult ServicesList()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            var list = db.services.ToList();
            ViewBag.Services = list;
            return View();
        }

        public ActionResult ServiceDelete(int? id)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            service s = db.services.Find(id);
            if (s != null)
            {
                db.services.Remove(s);
                db.SaveChanges();
            }
            return RedirectToAction("ServicesList");
        }

        [HttpGet]
        public ActionResult ServiceInsert()
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public ActionResult ServiceInsert(service s)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            try
            {
                db.services.Add(s);
                db.SaveChanges();
                return RedirectToAction("ServicesList");
            }
            catch (Exception ex)
            {
                return Content("Помилка при збереженні: " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult ServiceEdit(int? id)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return RedirectToAction("ServicesList");
            }
            service s = db.services.Find(id);
            if (s == null) return HttpNotFound();
            return View(s);
        }

        [HttpPost]
        public ActionResult ServiceEdit(service s)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ServicesList");
            }
            return View(s);
        }


        public ActionResult ServiceDetails(int? id)
        {
            if (id == null) return RedirectToAction("ServicesList");

            var service = db.services.Find(id);
            if (service == null) return HttpNotFound();

            var dbZakaz = new HotelFinalContainer();

            var zakazList = dbZakaz.ZakazSet.Where(z => z.Description == service.Name).ToList();

            ViewBag.UsageCount = zakazList.Count;

         
            ViewBag.TotalRevenue = (decimal)zakazList.Count * service.Price;

            ViewBag.RecentOrders = zakazList.OrderByDescending(z => z.Id).Take(5).ToList();

            return View(service);
        }



        public ActionResult OrdersList()
        {
            if (Session["UserName"] == null) return RedirectToAction("Login", "Account");
            var orders = db.providedServices.Include(p => p.booking).Include(p => p.service).ToList();
            return View(orders);
        }

        [HttpGet]
        public ActionResult AddOrder()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            ViewBag.IdBooking = new SelectList(db.bookings, "IdBooking", "IdBooking");
            ViewBag.IdService = new SelectList(db.services, "IdService", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(providedService ps)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.providedServices.Add(ps);
                db.SaveChanges();
                return RedirectToAction("OrdersList");
            }
            return View(ps);
        }

        [HttpGet]
        public ActionResult OrderEdit(int? id)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            var order = db.providedServices.Find(id);
            if (order == null) return HttpNotFound();

            ViewBag.IdBooking = new SelectList(db.bookings, "IdBooking", "IdBooking", order.IdBooking);
            ViewBag.IdService = new SelectList(db.services, "IdService", "Name", order.IdService);

            return View(order);
        }

        [HttpPost]
        public ActionResult OrderEdit(providedService ps)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Entry(ps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrdersList");
            }
            return View(ps);
        }

        public ActionResult OrderDelete(int id)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            var order = db.providedServices.Find(id);
            if (order != null)
            {
                db.providedServices.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("OrdersList");
        }

  
        [HttpGet]
        public ActionResult FindFreeRooms()
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");

            return View(new List<FindFreeRoom_Result>());
        }

        [HttpPost]
        public ActionResult FindFreeRooms(DateTime? start, DateTime? end, string roomType)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            if (start == null || end == null) return View(new List<FindFreeRoom_Result>());

            ViewBag.Start = start;
            ViewBag.End = end;
            ViewBag.RoomType = roomType;

           
            var freeRooms = db.FindFreeRoom(start, end, roomType.ToUpper()).ToList();
            return View(freeRooms);
        }
     
        public ActionResult MyHistory(string passportData)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            bool hasSearchValue = !string.IsNullOrEmpty(passportData);
            ViewBag.SearchPerformed = hasSearchValue;

            var history = hasSearchValue
                          ? db.ClientHistory(passportData).ToList()
                          : new List<DF_Perekhrestenko_IPZ_24_1.Models.ClientHistory_Result>();

            ViewBag.CurrentPassport = passportData;

            return View(history);
        }

        public ActionResult Details(int? id)
        {
           if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");

            var role = Session["UserRole"]?.ToString();
            if (role != "HotelAdmin" && role != "Receptionist") 
            {
                return Content("Доступ заборонено: У вас немає прав для перегляду деталей.");
            }

            var client = db.clients
                           .Include(c => c.bookings.Select(b => b.room)) 
                           .FirstOrDefault(c => c.IdClient == id);

            if (client == null) return HttpNotFound();

            return View(client);
        }

        [HttpGet]
        public ActionResult BookRoom(string roomNum, DateTime? start, DateTime? end)
        {
            if (!IsUserAuthorized()) return RedirectToAction("Login", "Account");
            var room = db.rooms.FirstOrDefault(r => r.Number.Trim() == roomNum.Trim());

            var model = new ClientBookingViewModel
            {
                CheckIn = start ?? DateTime.Now,
                CheckOut = end ?? DateTime.Now.AddDays(1),
                RoomId = room != null ? room.IdRoom : 0,
                Email = Session["UserName"].ToString() 
            };
            ViewBag.RoomId = new SelectList(db.rooms, "IdRoom", "Number", model.RoomId);

            return View("CreateWithBooking", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookRoom(ClientBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newClient = new client
                    {
                        Name = model.ClientName,
                        Surname = model.Surname,
                        Phone = model.ClientPhone,
                        Passport = model.Passport,
                        Email = model.Email,
                        Regestration = DateTime.Now
                    };

                    db.clients.Add(newClient);
                    db.SaveChanges(); 

                    var newBooking = new booking
                    {
                        IdClient = newClient.IdClient,
                        IdRoom = model.RoomId,
                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut,
                        Status = "active" 
                    };

                    db.bookings.Add(newBooking);
                    db.SaveChanges();

                    return RedirectToAction("MyHistory", new { passportData = newClient.Passport });
                }
                catch (Exception ex)
                {
                    var msg = ex.InnerException?.InnerException?.Message ?? ex.Message;
                    ModelState.AddModelError("", "Помилка бази: " + msg);
                }
            }

            ViewBag.RoomId = new SelectList(db.rooms, "IdRoom", "Number", model.RoomId);
            return View(model);
        }
        // Fix for Issue #2 (DRY Violation)
        private bool IsUserAuthorized()
        {
        return Session["UserName"] != null;
        }
    }
}

using System.Web.Mvc;
using System.Linq;
using DF_Perekhrestenko_IPZ_24_1.Models;

namespace DF_Perekhrestenko_IPZ_24_1.Controllers
{
    public class AccountController : Controller
    {
        hotelEntities db = new hotelEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            var user = db.Users.FirstOrDefault(u => u.Login == model.Username && u.Password == model.Password);

            if (user != null)
            {
                Session["UserID"] = user.Id;
                Session["UserName"] = user.Login;
                Session["UserRole"] = user.Role; 

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin"); 
                }
                else if (user.Role == "reception1")
                {
                    return RedirectToAction("Index", "Zakazs");
                }
                else
                {
                    return RedirectToAction("Index", "Home"); 
                }
            }

            ViewBag.Error = "Невірний логін або пароль";
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
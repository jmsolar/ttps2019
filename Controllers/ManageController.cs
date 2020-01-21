using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MercadoVentasTP.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _applicationDbContext;

        public ManageController()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.Titulo = "Configuracion";
            ViewBag.Subtitulo = "Desde acá vas a poder administrar tus publicaciones";
            ViewBag.Parrafo = "Con solo dos clicks podés editar tus preferencias!";
            TempData["Anterior"] = "Manage";

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            ViewBag.ConfPrecioActual = userProp.ConfPrecioActual;
            return View();
        }

        //
        // POST: /Manage/ActualizarConfiguracionDePrecio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarConfiguracionDePrecio()
        {
            var confActual = Request.Form["confPrecio"];
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());

            userProp.ConfPrecioActual = confActual;
            UserManager.Update(userProp);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/IncrementarPorcentajeDePrecios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncrementarPorcentajeDePrecios()
        {
            var incremento = float.Parse(Request.Form["incrementoPrecio"], CultureInfo.InvariantCulture.NumberFormat);
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var publicaciones = _applicationDbContext.Publicaciones.Where(p => p.IdUsuario == userProp.IdUsuario).ToList();
            using (_applicationDbContext)
            {
                foreach (var publicacion in publicaciones)
                {
                    publicacion.PrecioActual += ((publicacion.PrecioActual * incremento) / 100);
                    publicacion.Precio += ((publicacion.Precio * incremento) / 100);
                    publicacion.PrecioMin += ((publicacion.PrecioMin * incremento) / 100);
                    publicacion.PrecioMax += ((publicacion.PrecioMax * incremento) / 100);
                    _applicationDbContext.SaveChanges();
                }
            }
            TempData["Incremento"] = "El incremento de precios fue exitoso";
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DecrementarPorcentajeDePrecios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DecrementarPorcentajeDePrecios()
        {
            var decremento = float.Parse(Request.Form["decrementoPrecio"], CultureInfo.InvariantCulture.NumberFormat);
            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var publicaciones = _applicationDbContext.Publicaciones.Where(p => p.IdUsuario == userProp.IdUsuario).ToList();
            using (_applicationDbContext)
            {
                foreach (var publicacion in publicaciones)
                {
                    publicacion.PrecioActual -= ((publicacion.PrecioActual * decremento) / 100);
                    publicacion.Precio -= ((publicacion.Precio * decremento) / 100);
                    publicacion.PrecioMin -= ((publicacion.PrecioMin * decremento) / 100);
                    publicacion.PrecioMax -= ((publicacion.PrecioMax * decremento) / 100);
                    _applicationDbContext.SaveChanges();
                }
            }
            TempData["Decremento"] = "El decremento de precios fue exitoso";
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}
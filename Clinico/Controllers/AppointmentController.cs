using Microsoft.AspNetCore.Mvc;

namespace Clinico.Controllers {
    public class AppointmentController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Clinico.Controllers {
    public class DoctorController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

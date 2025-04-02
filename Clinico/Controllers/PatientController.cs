using Microsoft.AspNetCore.Mvc;

namespace Clinico.Controllers {
    public class PatientController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

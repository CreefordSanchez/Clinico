using Microsoft.AspNetCore.Mvc;

namespace Clinico.Controllers {
    public class ExamRoomController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

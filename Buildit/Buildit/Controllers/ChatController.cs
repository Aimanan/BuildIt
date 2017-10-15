using System.Web.Mvc;

namespace Buildit.Controllers
{
    public class ChatController : Controller
    {
        public ChatController()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
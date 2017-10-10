using System.Web.Mvc;

namespace Buildit.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
using Buildit.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestsMsTets.HomeControllerTests
{
    [TestClass]
    public class ChatIndexShould
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            var controller = new ChatController();
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}

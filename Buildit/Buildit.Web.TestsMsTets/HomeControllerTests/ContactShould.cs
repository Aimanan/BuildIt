using Buildit.Common.Providers;
using Buildit.Common.Providers.Contracts;
using Buildit.Controllers;
using Buildit.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestsMsTets.HomeControllerTests
{
    [TestClass]
    public class ContactShould
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            var mockedPublService = new Mock<IPublicationService>();
            var mockedCacheProvider = new Mock<ICacheProvider>();
            var mockedMapper = new Mock<IMapperAdapter>();

            var controller = new HomeController(mockedPublService.Object, mockedCacheProvider.Object, mockedMapper.Object);
            controller.WithCallTo(c => c.About()).ShouldRenderDefaultView();
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}

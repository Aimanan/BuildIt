using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Buildit.Web.Models.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.TestsMsTest.PublicationServiceTests
{
    
    [TestClass]
    public class AddPublicationShould
    {
        [TestMethod]
        public void CallAddMethod()
        {
            var publModel = new PublicationViewModelAdmin();
            var mockedData = new Mock<IBuilditData>();
            mockedData.Setup(x => x.Publications.Add(It.IsAny<Publication>())).Verifiable();

            var service = new PublicationService(mockedData.Object);

            service.AddPublication(publModel, "publ");

            mockedData.Verify(x => x.Publications.Add(It.IsAny<Publication>()), Times.Once);
        }

        [TestMethod]
        public void CallDataPublicationsAddMethodWithCorrectProperties()
        {
            var publModel = new PublicationViewModelAdmin()
            {
                Title = "Title",
                Author = "Pesho",
                Description = "Pesho Desc",
                PublicationTypeId = 5,
                Content="Lorem ipsum",
                PublishedOn = new DateTime(2017, 6, 8)
            };
            var pic = "picture.jpg";
            var mockedData = new Mock<IBuilditData>();
            Publication addedPubl = null;
            mockedData.Setup(x => x.Publications.Add(It.IsAny<Publication>()))
                .Callback((Publication book) => addedPubl = book);

            var service = new PublicationService(mockedData.Object);
            service.AddPublication(publModel, pic);

            Assert.AreEqual(publModel.Title, addedPubl.Title);
            Assert.AreEqual(publModel.Author, addedPubl.Author);
            Assert.AreEqual(publModel.Description, addedPubl.Description);
            Assert.AreEqual(publModel.PublishedOn, addedPubl.PublishedOn);
            Assert.AreEqual(publModel.PublicationTypeId, addedPubl.PublicationTypeId);  
            Assert.AreEqual(pic, addedPubl.Picture);
        }

        [TestMethod]
        public void CallDataSaveChanges()
        {
            var mockedData = new Mock<IBuilditData>();
            mockedData.Setup(x => x.Publications.Add(It.IsAny<Publication>())).Verifiable();
            mockedData.Setup(x => x.SaveChanges()).Verifiable();
            var service = new PublicationService(mockedData.Object);
            service.AddPublication(new PublicationViewModelAdmin(), "pic");

            mockedData.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}

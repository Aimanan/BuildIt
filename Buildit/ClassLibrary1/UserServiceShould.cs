using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Buildit.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buildit.Services.TestsMsTest
{
    [TestClass]
    public class UserServiceShould
    {
        [TestMethod]
        public void ThrowExceptioWhenDataIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Buildit.Services.UserService(null, null, null));
        }

        [TestMethod]
        public void NotThrowExceptionIfTheParametersAreCorrect()
        {
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedIEfRepo = new Mock<IEfRepository<User>>();

            var userService = new Buildit.Services.UserService(mockedData.Object, mockedIEfRepo.Object, mockedUnitofWork.Object);

            Assert.IsNotNull(userService);
        }

        [TestMethod]
        public void ReturnTrueWhenUserIsFound()
        {
            var username = "pesho3";
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedIEfRepo = new Mock<IEfRepository<User>>();
            var mockedUser1 = new Mock<User>();
            var mockedUser2 = new Mock<User>();
            var mockedUser3 = new Mock<User>();
            mockedUser1.Setup(x => x.UserName).Returns(username);
            var users = new List<User>()
            {
                mockedUser1.Object,
                mockedUser2.Object,
                mockedUser3.Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Users.All).Returns(users);

            var service = new Buildit.Services.UserService(mockedData.Object, mockedIEfRepo.Object, mockedUnitofWork.Object);

            var result = service.CheckIfUserExists(username);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnNullWhenIdIsNul()
        {
            var username = "pesho3";
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedIEfRepo = new Mock<IEfRepository<User>>();
            var mockedUser1 = new Mock<User>();
            var mockedUser2 = new Mock<User>();
            var mockedUser3 = new Mock<User>();
            mockedUser1.Setup(x => x.UserName).Returns(username);
            var users = new List<User>()
            {
                mockedUser1.Object,
                mockedUser2.Object,
                mockedUser3.Object,
            }.AsQueryable();

            mockedData.Setup(x => x.Users.All).Returns(users);

            var service = new Buildit.Services.UserService(mockedData.Object, mockedIEfRepo.Object, mockedUnitofWork.Object);

            var result = service.CheckIfUserExists(username);

            Assert.IsTrue(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReturnUserWhenIdIsPssed()
        {
            var username = "pesho3";
            var testId = "pesho's ID";
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedUserRepo = new Mock<IEfRepository<User>>();
            var mockedUser = new Mock<User>();
            var mockedUser1 = new Mock<User>();
            mockedUser.Setup(x => x.Id).Returns(testId);
            mockedUser.Setup(x => x.UserName).Returns(username);
            mockedUserRepo.Setup(x => x.GetById(testId)).Returns(new User() { Id = testId });
            var users = new List<User>()
            {
               mockedUser.Object
            }.AsQueryable();

            var service = new UserService(mockedData.Object, mockedUserRepo.Object, mockedUnitofWork.Object);

            var result = service.GetById(testId);

            Assert.IsNotNull(result);    
    }

        [TestMethod]
        public void ReturnNullWhenNulIdIsPssed()
        {
            var username = "pesho3";
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedUserRepo = new Mock<IEfRepository<User>>();
            var mockedUser = new Mock<User>();
            mockedUser.Setup(x => x.UserName).Returns(username);

            var service = new UserService(mockedData.Object, mockedUserRepo.Object, mockedUnitofWork.Object);

            var result = service.GetById(null);
        
            Assert.AreEqual(result, null);
        }
    }
}

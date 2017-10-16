using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buildit.Services.Tests.UserTests
{
    [TestFixture]
    public class UsersShould
    {
        [Test]
        public void ThrowExceptioWhenDataIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, null, null));
        }

        [Test]
        public void ThrowExceptioWhenDataIsNullWithCorrectMessage()
        {
            Assert.That(() => new UserService(null, null, null), Throws.ArgumentNullException.With.Message.Contains("data"));
        }

        [Test]
        public void NotThrowExceptionIfTheParametersAreCorrect()
        {
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedIEfRepo= new Mock<IEfRepository<User>>();
            Assert.DoesNotThrow(() => new UserService(mockedData.Object, mockedIEfRepo.Object, mockedUnitofWork.Object));
        }

        [Test]
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

            var service = new UserService(mockedData.Object, mockedIEfRepo.Object, mockedUnitofWork.Object);

            var result = service.CheckIfUserExists(username);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnUserWhenIdIsPssed()
        {
            var username = "pesho3";
            var testId = "pesho's ID";
            var mockedData = new Mock<IBuilditData>();
            var mockedUnitofWork = new Mock<IUnitOfWork>();
            var mockedUserRepo = new Mock<IEfRepository<User>>();
            var mockedUser = new Mock<User>();
            mockedUser.Setup(x => x.UserName).Returns(username);

            var service = new UserService(mockedData.Object, mockedUserRepo.Object, mockedUnitofWork.Object);

            var result = service.GetById(testId);

            Assert.AreEqual(result, username);
        }

        //public User GetById(string id)
        //{
        //    var user = this.userRepository.GetById(id);

        //    return user;
        //}
    }
}


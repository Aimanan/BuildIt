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
using TestStack.FluentMVCTesting;

namespace Buildit.Web.TestsMsTets.HomeControllerTests
{
    [TestClass]
    public class AboutShould
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

        //[TestMethod]
        //public void Index_ShouldReturnList_With_TaskViewModel()
        //{
        //    var taskServiceMocked = new Mock<ITaskService>();

        //    var taskModelDemo = new TaskModel()
        //    {
        //        Title = "TestTitle",
        //        Content = "TestContent"
        //    };

        //    var taskViewModel = new TaskViewModel()
        //    {
        //        Title = taskModelDemo.Title,
        //        Content = taskModelDemo.Content
        //    };

        //    taskServiceMocked.Setup(x => x.GetAll()).Returns(
        //        new List<TaskModel>()
        //        {
        //           taskModelDemo
        //        });

        //    var controller = new TasksController(taskServiceMocked.Object);

        //    controller.WithCallTo(x => x.Index())
        //        .ShouldRenderDefaultView()
        //        .WithModel<List<TaskViewModel>>(taskModel =>
        //        {
        //            Assert.AreEqual(taskModel.First().Title, taskViewModel.Title);
        //            Assert.AreEqual(taskModel.First().Content, taskViewModel.Content);
        //        });
        //}

        //[TestMethod]
        //public void Index_ShouldCall_GetAll_Once()
        //{
        //    var taskServiceMocked = new Mock<ITaskService>();

        //    var taskModelDemo = new TaskModel()
        //    {
        //        Title = "TestTitle",
        //        Content = "TestContent"
        //    };

        //    var taskViewModel = new TaskViewModel()
        //    {
        //        Title = taskModelDemo.Title,
        //        Content = taskModelDemo.Content
        //    };

        //    taskServiceMocked.Setup(x => x.GetAll()).Returns(
        //        new List<TaskModel>()
        //        {
        //           taskModelDemo
        //        });

        //    var controller = new TasksController(taskServiceMocked.Object);

        //    controller.Index();

        //    taskServiceMocked.Verify(x => x.GetAll(), Times.Once);
        //}

        //[TestMethod]
        //public void Index_ShouldReturn_EmptyList_WhenThereAreNoModels()
        //{
        //    var taskServiceMocked = new Mock<ITaskService>();

        //    var taskViewModel = new TaskViewModel();

        //    taskServiceMocked.Setup(x => x.GetAll()).Returns(
        //        new List<TaskModel>());

        //    var controller = new TasksController(taskServiceMocked.Object);

        //    controller
        //        .WithCallTo(x => x.Index())
        //        .ShouldRenderDefaultView()
        //        .WithModel<List<TaskViewModel>>(viewModel =>
        //        {
        //            Assert.AreEqual(viewModel.Count, 0);
        //        });
        //}
    }
}


using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UserService.Controllers;
using UserService.Entities;
using UserService.Models;

namespace UserService.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        private readonly Mock<IEntityManager<User>> _mock;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            _mock = new Mock<IEntityManager<User>>();
            _controller = new UsersController(_mock.Object);
        }

        [TestMethod]
        public void GetAll_ActionExecutes_ReturnsTaskEnumerableOfUsers()
        {
            var actionResult = _controller.GetAll();
            Assert.IsInstanceOfType(actionResult, typeof(System.Threading.Tasks.Task<ActionResult<IEnumerable<User>>>));
        }

        [TestMethod]
        public void GetAll_ActionExecutes_ReturnsListOfUsers()
        {
            //_mock.Setup(mock => mock.GetAll()).Returns(new System.Threading.Tasks.Task<IEnumerable<User>>(() =>
            //{
            //    return new List<User>()
            //    {
            //        new User
            //        {
            //            ID = new System.Guid(),
            //            Name = "Test"
            //        },
            //        new User
            //        {
            //            ID = new System.Guid(),
            //            Name = "Test2"
            //        }
            //    };
            //}));

            //var result = _controller.GetAll().ConfigureAwait(false).GetAwaiter();
            //IEnumerable<User> user;
            //result.UnsafeOnCompleted(() => user = result.GetResult());
            
        }
    }
}

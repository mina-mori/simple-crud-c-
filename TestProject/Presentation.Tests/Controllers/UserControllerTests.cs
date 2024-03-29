using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DomainEntities.Entities;
using MediatR;
using Application.Handlers;
using Presentation.Controllers;

namespace Presentation.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _loggerMock = new Mock<ILogger<UserController>>();
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            
            var expectedUsers = new List<UserViewModel>();
            expectedUsers.Add(new UserViewModel
            {
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@test.test",
                Phone = "+201901000000"
            });
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUsersQueryHandler>(), default)).ReturnsAsync(expectedUsers);
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.GetUsers();


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUsers = Assert.IsAssignableFrom<List<UserViewModel>>(okResult.Value);
            Assert.Equal(expectedUsers, actualUsers);
        }

        [Fact]
        public async Task GetUser_ReturnsUserById()
        {
            
            var userId = 1;
            var expectedUser = new UserViewModel
            {
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@test.test",
                Phone = "+201901000000"
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQueryHandler>(), default)).ReturnsAsync(expectedUser);
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.GetUser(userId);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUser = Assert.IsType<UserViewModel>(okResult.Value);
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFoundForInvalidId()
        {
            
            var userId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQueryHandler>(), default)).ReturnsAsync((UserViewModel)null);
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.GetUser(userId);


            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddUser_ReturnsOk()
        {
            
            var user = new AddEditUserViewModel { 
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@test.test",
                Phone = "+201901000000"
            };
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.AddUser(user);


            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOk()
        {
            
            var userId = 1;
            var user = new AddEditUserViewModel {
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@test.test",
                Phone = "+201901000000"
            };
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.UpdateUser(userId, user);


            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            
            var userId = 1;
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.DeleteUser(userId);


            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetUsers_ReturnsInternalServerErrorWhenExceptionThrown()
        {
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUsersQueryHandler>(), default)).ThrowsAsync(new Exception("Test Exception"));
            var controller = new UserController(_loggerMock.Object, _mediatorMock.Object);

            
            var result = await controller.GetUsers();


            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error: Test Exception", statusCodeResult.Value);
        }

    }
}

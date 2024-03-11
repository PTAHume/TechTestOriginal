using System;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{
    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void Create_WhenModelStateIsValid_RedirectsToListAction()
    {
        // Arrange
        var controller = CreateController();
        var model = new UserListItemViewModel
        {
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true
        };

        // Act
        var result = controller.Create(model);

        // Assert
        result.Should().BeOfType<RedirectToActionResult>()
            .Which.ActionName.Should().Be("List");
    }

    [Fact]
    public void Create_WhenModelStateIsInvalid_ReturnsViewWithModel()
    {
        // Arrange
        var controller = CreateController();
        var model = new UserListItemViewModel();
        controller.ModelState.AddModelError("Forename", "Forename is required.");

        // Act
        var result = controller.Create(model);

        // Assert
        result.Should().BeOfType<ViewResult>()
            .Which.Model.Should().BeSameAs(model);
    }

    [Fact]
    public void Edit_WhenUserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var controller = CreateController();
        _userService.Setup(s => s.GetById(1));
        var model = new UserListItemViewModel
        {
            Id = 1,
            Forename = "Updated",
            Surname = "User",
            Email = "updated@example.com",
            IsActive = false
        };

        // Act
        var result = controller.Edit(model);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void Edit_WhenUserExistsAndModelStateIsValid_RedirectsToListAction()
    {
        // Arrange
        var controller = CreateController();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true,
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };
        _userService.Setup(s => s.GetById(1)).Returns(user);
        var model = new UserListItemViewModel
        {
            Id = 1,
            Forename = "Updated",
            Surname = "User",
            Email = "updated@example.com",
            IsActive = false,
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var result = controller.Edit(model);

        // Assert
        result.Should().BeOfType<RedirectToActionResult>()
            .Which.ActionName.Should().Be("List");
    }

    [Fact]
    public void Details_WhenUserExists_ReturnsViewWithUserModel()
    {
        // Arrange
        var controller = CreateController();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true,
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };
        _userService.Setup(s => s.GetById(1)).Returns(user);

        // Act
        var result = controller.Details(1);

        // Assert
        result.Should().BeOfType<ViewResult>()
            .Which.Model.Should().BeOfType<UserListItemViewModel>()
            .Which.Id.Should().Be(1);
    }

    [Fact]
    public void Details_WhenUserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var controller = CreateController();
        _userService.Setup(s => s.GetById(1));

        // Act
        var result = controller.Details(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void Delete_WhenUserExists_ReturnsViewWithUserModel()
    {
        // Arrange
        var controller = CreateController();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true,
            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };
        _userService.Setup(s => s.GetById(1)).Returns(user);

        // Act
        var result = controller.Delete(1);

        // Assert
        result.Should().BeOfType<ViewResult>()
           .Which.Model.Should().BeOfType<UserListItemViewModel>()
            .Which.Id.Should().Be(1);
    }

    [Fact]
    public void Delete_WhenUserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var controller = CreateController();
        _userService.Setup(s => s.GetById(1));

        // Act
        var result = controller.Delete(1);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    private User[] SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive
            }
        };

        _userService
            .Setup(s => s.GetAll())
            .Returns(users);

        return users;
    }

    private readonly Mock<IUserService> _userService = new();

    private UsersController CreateController() => new(_userService.Object);
}

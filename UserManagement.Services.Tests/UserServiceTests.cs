using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeSameAs(users);
    }

    [Fact]
    public void FilterUsers_ShouldBeEmpty()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByActive(false);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEmpty();
    }

    [Fact]
    public void UpdateUser_ShouldUpdateUserInDataContext()
    {
        // Arrange
        var service = CreateService();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true
        };

        // Act
        service.Update(user);

        // Assert
        _dataContext.Verify(s => s.Update(user), Times.Once);
    }

    [Fact]
    public void DeleteUser_ShouldDeleteUserFromDataContext()
    {
        // Arrange
        var service = CreateService();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true
        };

        // Act
        service.Delete(user);

        // Assert
        _dataContext.Verify(s => s.Delete(user), Times.Once);
    }

    [Fact]
    public void AddUser_ShouldAddUserToDataContext()
    {
        // Arrange
        var service = CreateService();
        var user = new User
        {
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true
        };

        // Act
        service.Create(user);

        // Assert
        _dataContext.Verify(s => s.Create(user), Times.Once);
    }

    private IQueryable<User> SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
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
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }

    private readonly Mock<IDataContext> _dataContext = new();

    private UserService CreateService() => new(_dataContext.Object);
}

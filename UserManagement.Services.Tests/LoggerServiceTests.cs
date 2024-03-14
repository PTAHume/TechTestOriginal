using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class LoggerServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var logs = SetupLogs(DateTime.UtcNow);

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeSameAs(logs);
    }

    [Fact]
    public void FilterLogs_ShoulHaveCorrectItem()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var logs = SetupLogs(DateTime.UtcNow);

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByUser(1);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().HaveCount(1);
        result.Should().Contain(logs);
    }

    [Fact]
    public void AddLog_ShouldAddLogToDataContext()
    {
        // Arrange
        var service = CreateService();
        var user = new User
        {
            Id = 1,
            Forename = "John",
            Surname = "Doe",
            Email = "johndoe@example.com",
            IsActive = true,
            DateOfBirth = DateTime.UtcNow,
        };

        // Act
        service.LogAction(user, "Test");

        // Assert
        _dataContext.Verify(s => s.Create(It.IsAny<Log>()), Times.Once);
    }

    private IQueryable<Log> SetupLogs(DateTime timeStamp, int userId = 1, string action = "", string details = "")
    {
        var logs = new[]
        {
            new Log
            {
               Id= 2,
               TimeStamp = timeStamp,
               UserId = userId,
               Action = action,
               Details= details
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<Log>())
            .Returns(logs);

        return logs;
    }

    private readonly Mock<IDataContext> _dataContext = new();

    private LoggerService CreateService() => new(_dataContext.Object);
}

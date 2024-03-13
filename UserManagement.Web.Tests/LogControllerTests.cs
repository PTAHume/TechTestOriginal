using System;
using UserManagement.Controllers;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Data.Tests;

public class LogControllerTests
{
    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var logs = SetupLogs(DateTime.UtcNow);

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<LogsListViewModel>()
            .Which.Items.Should().BeEquivalentTo(logs);
    }

    private Log[] SetupLogs(DateTime timeStamp, int userId = 1, string action = "")
    {
        var logs = new[]
        {
            new Log
            {
               Id= 2,
               TimeStamp = timeStamp,
               UserId = userId,
               Action = action,
            }
        };

        _logerService
            .Setup(s => s.GetAll())
            .Returns(logs);

        return logs;
    }

    private readonly Mock<ILoggerService> _logerService = new();

    private LogsController CreateController() => new(_logerService.Object);
}

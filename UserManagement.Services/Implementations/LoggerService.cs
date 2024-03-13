using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class LoggerService(IDataContext dataAccess) : ILoggerService
{
    private readonly IDataContext _dataAccess = dataAccess;

    public IEnumerable<Log> GetAll() => _dataAccess.GetAll<Log>();

    public Log? GetById(long id)
    {
        return _dataAccess.GetAll<Log>().SingleOrDefault(x => x.Id == id);
    }

    public void LogAction(User user, string action)
    {
        var details = JsonSerializer.Serialize(user);
        var log = new Log()
        {
            UserId = user.Id,
            Action = action,
            Details = details,
            TimeStamp = DateTime.UtcNow
        };
        _dataAccess.Create(log);
    }
}

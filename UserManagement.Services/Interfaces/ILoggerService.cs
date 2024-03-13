using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILoggerService
{
    /// <summary>Gets all Logs.</summary>
    /// <returns>
    ///   All Logs.
    /// </returns>
    IEnumerable<Log> GetAll();

    /// <summary>Gets the by identifier.</summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    Log? GetById(long id);

    void LogAction(User user, string action);
}

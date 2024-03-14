using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface ILoggerService
{
    /// <summary>Filters the by user.</summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>
    ///   Filter log for given user id
    /// </returns>
    IEnumerable<Log> FilterByUser(long userId);

    /// <summary>Gets all Logs.</summary>
    /// <returns>
    ///   All Logs.
    /// </returns>
    IEnumerable<Log> GetAll();

    /// <summary>Gets the by identifier.</summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   Log by log id
    /// </returns>
    Log? GetById(long id);

    /// <summary>Logs the action.</summary>
    /// <param name="user">The user.</param>
    /// <param name="action">The action.</param>
    void LogAction(User user, string action);
}

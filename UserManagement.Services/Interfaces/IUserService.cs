using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns>
    /// Filtered Users.
    /// </returns>
    IEnumerable<User> FilterByActive(bool isActive);

    /// <summary>Gets all.</summary>
    /// <returns>
    ///   All users.
    /// </returns>
    IEnumerable<User> GetAll();

    /// <summary>Gets user the by identifier.</summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    ///   Selected User if found otherwise returns null.
    /// </returns>
    User? GetById(long id);

    void Create(User user);

    /// <summary>Updates the specified user.</summary>
    /// <param name="user">The user.</param>
    void Update(User user);

    void Delete(User user);
}

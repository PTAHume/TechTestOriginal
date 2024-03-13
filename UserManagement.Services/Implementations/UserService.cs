using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService(IDataContext dataAccess) : IUserService
{
    private readonly IDataContext _dataAccess = dataAccess;

    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return _dataAccess.GetAll<User>().Where(x => x.IsActive == isActive);
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public User? GetById(long id)
    {
        return _dataAccess.GetAll<User>().SingleOrDefault(x => x.Id == id);
    }

    public void Create(User user)
    {
        _dataAccess.Create(user);
    }

    public void Update(User user)
    {
        _dataAccess.Update(user);
    }

    public void Delete(User user)
    {
        _dataAccess.Delete(user);
    }
}

using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly ILoggerService _loggerService;

    public UsersController(IUserService userService, ILoggerService loggerService)
        => (_userService, _loggerService) = (userService, loggerService);

    [HttpGet]
    public ViewResult List(long filter = -1)
    {
        var results = filter != -1 ? _userService.FilterByActive(Convert.ToBoolean(filter)) : _userService.GetAll();

        var items = results
            .Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    public IActionResult Details(long id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
        });
    }

    public IActionResult Create()
    {
        var model = new UserListItemViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserListItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Forename = model.Forename,
                Surname = model.Surname,
                Email = model.Email,
                IsActive = model.IsActive,
                DateOfBirth = model.DateOfBirth
            };
            _userService.Create(user);
            _loggerService.LogAction(user, "User created");
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Edit(long id)
    {
        return Details(id);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UserListItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetById(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            _loggerService.LogAction(user, "Updated (Details Before)");
            user.Forename = model.Forename;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.DateOfBirth = model.DateOfBirth;
            _userService.Update(user);
            _loggerService.LogAction(user, "Updated (Details After)");
            return RedirectToAction("List");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(long id)
    {
        return Details(id);
    }

    [HttpPost]
    public IActionResult Delete(UserListItemViewModel model)
    {
        var user = _userService.GetById(model.Id);
        if (user == null)
        {
            return NotFound();
        }
        _loggerService.LogAction(user, "Deleted");
        _userService.Delete(user);
        return RedirectToAction("List");
    }
}

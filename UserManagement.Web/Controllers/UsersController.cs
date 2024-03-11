using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public ViewResult List(int filter = -1)
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
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Edit(int id)
    {
        return Details(id);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UserListItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetById((int)model.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.Forename = model.Forename;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.DateOfBirth = model.DateOfBirth;
            _userService.Update(user);
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Details(int id)
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

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        var model = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(UserListItemViewModel model)
    {
        var user = _userService.GetById((int)model.Id);
        if (user == null)
        {
            return NotFound();
        }
        _userService.Delete(user);
        return RedirectToAction("List");
    }
}

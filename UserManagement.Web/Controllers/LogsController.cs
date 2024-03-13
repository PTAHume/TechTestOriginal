using System.Linq;
using System.Text.Json;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;
using UserManagement.Web.Models.Users;

namespace UserManagement.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILoggerService _loggerService;

        public LogsController(ILoggerService loggerService) => (_loggerService) = (loggerService);

        [HttpGet]
        public ViewResult List()
        {
            var logs = _loggerService.GetAll();
            var model = new LogsListViewModel
            {
                Items = logs.Select(x => new LogsListItemViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TimeStamp = x.TimeStamp,
                    Action = x.Action
                }).ToList()
            };

            return View(model);
        }

        public IActionResult Details(int Id)
        {
            var log = _loggerService.GetById(Id);
            if (log == null)
            {
                return NotFound();
            }
            return View(new LogsListItemViewModel
            {
                Id = log.Id,
                UserId = log.UserId,
                TimeStamp = log.TimeStamp,
                Action = log.Action,
                Details = JsonSerializer.Deserialize<UserListItemViewModel>(log.Details) ?? new()
            });
        }
    }
}

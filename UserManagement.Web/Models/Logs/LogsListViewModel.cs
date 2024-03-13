using System;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Models.Logs;

public class LogsListViewModel
{
    public List<LogsListItemViewModel> Items { get; set; } = [];
}

public class LogsListItemViewModel
{
    public long Id { get; set; }

    public DateTime TimeStamp { get; set; }

    public long UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public UserListItemViewModel? Details { get; set; }
}

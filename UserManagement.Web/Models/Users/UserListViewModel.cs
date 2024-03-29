﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = [];
}

public class UserListItemViewModel
{
    public long Id { get; set; }

    [Required]
    [StringLength(256, MinimumLength = 3)]
    public string Forename { get; set; } = string.Empty;

    [Required]
    [StringLength(256, MinimumLength = 3)]
    public string Surname { get; set; } = string.Empty;

    [Required]
    [StringLength(256, MinimumLength = 3)]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter valId e-mail address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DisplayName("Date of Birth")]
    [BindProperty, DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }

    public bool IsActive { get; set; }
}

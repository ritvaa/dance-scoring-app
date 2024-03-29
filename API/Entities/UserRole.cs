﻿using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class UserRole
{
    [Key] public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public int RoleId { get; set; }

    #region Entity Relations

    public User User { get; set; }
    public Role Role { get; set; }

    #endregion
}
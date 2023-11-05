using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Chain_pharmacies.Models;

public partial class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль повинен містити від 4 до 100 символів")]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [JsonIgnore]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<MainAdmin> MainAdmins { get; set; } = new List<MainAdmin>();

    public virtual ICollection<UserCart> UserCarts { get; set; } = new List<UserCart>();

    public virtual ICollection<OrderCart> OrderCarts { get; set; } = new List<OrderCart>();

    public virtual UserRole? Role { get; set; }
}

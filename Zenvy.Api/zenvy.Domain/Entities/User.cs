namespace zenvy.domain.Entities;

public class User
{
    public string UserId { get; set; } = Guid.NewGuid().ToString();

    public int RoleId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Role { get; set; } = string.Empty;
}

namespace zenvy.application.DTOs.Auth;
public class ChangePassRequest
{
    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}
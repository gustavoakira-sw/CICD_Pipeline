namespace FiapApi.Models;

public class AddUserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Key { get; set; }
    public string Role { get; set; }
    public string Password { get; set; } // Nullable field
}
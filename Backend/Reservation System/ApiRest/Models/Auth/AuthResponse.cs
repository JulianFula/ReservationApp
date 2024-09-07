namespace ApiRest.Models.Login;

public class AuthResponse
{
    public bool response { get; set; }
    public string Token { get; set; }
    public string message { get; set; }
}
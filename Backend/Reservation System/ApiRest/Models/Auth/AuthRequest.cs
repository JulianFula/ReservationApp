﻿namespace ApiRest.Models.Login;

public class AuthRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

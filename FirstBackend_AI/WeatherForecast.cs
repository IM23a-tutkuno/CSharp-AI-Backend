namespace FirstBackend_AI
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Passwort { get; set; }

    }

    public class Chat
    {
        public string Role { get; set; }
        public string Content { get; set; }

    }

    public class ChatList
    {
        public List<Chat> Messages { get; set; }
        public string Token { get; set; }
    }

    public class Answer
    {
        public bool success { get; set; }
        public string response { get; set; }
        public string token { get; set; }
    }

    public class LoginToken
    {
        public string Username { get; set; }
        public string Passwort { get; set; }
        public string ApiKey { get; set; }
        public string Ai_name { get; set; }
    }
}

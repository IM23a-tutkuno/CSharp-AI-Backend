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
        public string passwort_input { get; set; }
        public string username_input { get; set; }

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

    public class UserInfo
    {
        public string Username { get; set; }

        public string Passwort { get; set; }

        public string Api_Key { get; set; }

        public string AI_name { get; set; }
    }


    public class UserJSON
    {
        public string Content { get; set; }
    }


    public class SupabaseConfig
    {
        public string Key { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public bool Result { get; set; }

        public bool Token { get; set; }

        public string apiKey { get; set; }
    }

}

    using Microsoft.AspNetCore.Mvc;
    using Claudia;
    using DotNetEnv;
    using System.IdentityModel.Tokens.Jwt;
    using Supabase;
    using System.Text.Json;
    using Microsoft.Extensions.Configuration;
    using Supabase.Interfaces;
    using System.Security.Claims;
    using System.Text;
using Microsoft.IdentityModel.Tokens;

    namespace FirstBackend_AI.Controllers
    {





    [ApiController]
        [Route("api/login")]
        public class LoginController : ControllerBase
        {



        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        private string CreateJwtToken(UserInfo userInfo)
        {



            var _secretKey = _configuration["secret_key"];

            var claims = new[]
            {
                new Claim("username", userInfo.Username),
                new Claim("passwort", userInfo.Passwort),
                new Claim("apiKey", userInfo.Api_Key),
                new Claim("ai_name", userInfo.AI_name)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "CSharpBackend",
                audience: "web-client",
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> Check_Credentials(string username, string password)
        {
            var url = _configuration["Supabase:Url"];
            var key = _configuration["Supabase:Key"];

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            Login credentials = new Login
            {
                passwort_input = password,
                username_input = username
            };


            var check = await supabase.Rpc("check_credentials", credentials );


            return check.Content ;
        }


        private async Task<UserInfo> Get_User(string username, string password)
        {
            var url = _configuration["Supabase:Url"];
            var key = _configuration["Supabase:Key"];

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            Login credentials = new Login
            {
                passwort_input = password,
                username_input = username
            };


            var check = await supabase.Rpc("get_user", credentials);


            var user_arr = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserInfo>>(check.Content);
            UserInfo user = user_arr[0];


            return user;
        }


        private string Login(string username, string password) 
        {

            return "sadfs" +
                "f";


        }





        [HttpPost(Name = "Login")]
            public async Task<LoginResponse> Post([FromBody] Login login)
            {

            string username = login.username_input;
            string password = login.passwort_input;
            


            _logger.LogInformation("Received username: {username_input} and password: {passwort_input}", login.username_input, login.passwort_input);


            string check_cred = await Check_Credentials(username, password);


            if (check_cred == "true")
            {
                UserInfo user = await Get_User(username, password);
                var token = CreateJwtToken(user);
                LoginResponse response = new LoginResponse
                {
                    success = true,
                    message = "Logged in successfully!",
                    result = true,
                    token = token,
                    apiKey = user.Api_Key
                };
                return response;
            }


            else
            {
                LoginResponse response_not_successful = new LoginResponse
                {
                    success = false,
                    message = "Username or password wrong!",
                    result = false,
                };
                return response_not_successful;
            }

            







            }
        }
    }

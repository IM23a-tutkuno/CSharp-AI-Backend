    using Microsoft.AspNetCore.Mvc;
    using Claudia;
    using DotNetEnv;
    using System.IdentityModel.Tokens.Jwt;
    using Supabase;
    using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Supabase.Interfaces;

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
            public async Task<string> Post([FromBody] Login login)
            {

            string username = login.username_input;
            string password = login.passwort_input;
            


            _logger.LogInformation("Received username: {username_input} and password: {passwort_input}", login.username_input, login.passwort_input);


            string check_cred = await Check_Credentials(username, password);


            if (check_cred  == "true")
            {
                UserInfo user = await Get_User(username, password);
                return user.Username;

            }








            return "fdfsd";
            }
        }
    }

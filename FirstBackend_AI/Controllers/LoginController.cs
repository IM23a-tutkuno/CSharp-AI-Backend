    using Microsoft.AspNetCore.Mvc;
    using Claudia;
    using DotNetEnv;
    using System.IdentityModel.Tokens.Jwt;

    namespace FirstBackend_AI.Controllers
    {





    [ApiController]
        [Route("api/login")]
        public class LoginController : ControllerBase
        {


        public static void Login()
        {

        }



        private readonly ILogger<LoginController> _logger;

            public LoginController(ILogger<LoginController> logger)
            {
                _logger = logger;
          
            }




            [HttpPost(Name = "Login")]
            public async Task<IActionResult> Post([FromBody] ChatList chatList)
            {

            _logger.LogInformation("Received token: {Token} and messages: {Messages}", chatList.Token, chatList.Messages);


            var test = "hello";




                

            return Ok(test);
            }
        }
    }

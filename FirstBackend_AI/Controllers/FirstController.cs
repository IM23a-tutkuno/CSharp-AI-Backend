    using Microsoft.AspNetCore.Mvc;

    namespace FirstBackend_AI.Controllers
    {
        [ApiController]
        [Route("api/login")]
        public class FirstController : ControllerBase
        {

            private readonly ILogger<FirstController> _logger;

            public FirstController(ILogger<FirstController> logger)
            {
                _logger = logger;
            }

            [HttpGet(Name = "TestRoute")]
            public IEnumerable<Login> Get(string username, string passwort)
            {
            return new List<Login>()
                {
                    new Login
                    {
                        Username = username,
                        Passwort = passwort
                        
                    }

                };
            }
        }
    }

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



        private readonly ILogger<LoginController> _logger;

            public LoginController(ILogger<LoginController> logger)
            {
                _logger = logger;
          
            }




            [HttpPost(Name = "Login")]
            public async Task<IActionResult> Post([FromBody] ChatList chatList)
            {

            _logger.LogInformation("Received token: {Token} and messages: {Messages}", chatList.Token, chatList.Messages);



        var anthropic = new Anthropic
            {
                ApiKey = "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"
            };

            var anthropicMessages = chatList.Messages.Select(m => new Claudia.Message
            {
                Role = m.Role,     // Map "role" field (e.g., "user" or "assistant")
                Content = m.Content // Map "content" field (the message text)
            }).ToList();

            // Convert the List<Claudia.Message> to Claudia.Message[]
            Claudia.Message[] messageArray = anthropicMessages.ToArray();


            var send_message = await anthropic.Messages.CreateAsync(new MessageRequest
            {
                Model = "claude-3-5-sonnet-20240620", // you can use Claudia.Models.Claude3_5Sonnet string constant
                MaxTokens = 1024,
                Messages = messageArray
            });
                

            return Ok(send_message);
            }
        }
    }

    using Microsoft.AspNetCore.Mvc;
    using Claudia;
    using DotNetEnv;
using System.IdentityModel.Tokens.Jwt;

    namespace FirstBackend_AI.Controllers
    {
        
        

        [ApiController]
        [Route("api/chat")]
        public class ChatController : ControllerBase
        {


        public static JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token;
        }

        public static LoginToken DecodeJwt(JwtSecurityToken token)
        {
            var username = token.Claims.FirstOrDefault(claim => claim.Type == "username")?.Value;
            var password = token.Claims.FirstOrDefault(claim => claim.Type == "passwort")?.Value;
            var apiKey = token.Claims.FirstOrDefault(claim => claim.Type == "apiKey")?.Value;
            var ai_name = token.Claims.FirstOrDefault(claim => claim.Type == "ai_name")?.Value;
            return (new LoginToken {
                Username = username,
                Passwort = password,
                ApiKey = apiKey,
                Ai_name = ai_name
                }
            );
        }

        public async static Task<MessageResponse> SendMessage(Anthropic anthropic, ChatList chatList) 
        {
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




            



        }

        private readonly ILogger<ChatController> _logger;

            public ChatController(ILogger<ChatController> logger)
            {
                _logger = logger;
          
                DotNetEnv.Env.Load();
            }




            [HttpPost(Name = "Chat")]
            public async Task<IActionResult> Post([FromBody] ChatList chatList)
            {
            var sec_token = ConvertJwtStringToJwtSecurityToken(chatList.Token);
            LoginToken decoded = DecodeJwt(sec_token);
            _logger.LogInformation("Received token: {Username}, {Passwort}, {ApiKey} and messages: {Messages}", decoded.Username, decoded.Passwort, decoded.ApiKey, chatList.Messages);



            var anthropic = new Anthropic
            {
                ApiKey = decoded.ApiKey
            };


            var send_response = await SendMessage(anthropic, chatList);


            Answer response = new Answer
            {
                success = true,
                response = send_response.Content[0].Text,
                token = chatList.Token
            };

            Console.WriteLine("fsfadf"); 


            return Ok(response);
            
            }
        }
    }

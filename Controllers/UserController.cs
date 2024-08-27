using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nezter_backend
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private UserService _userService;

        private readonly IConfiguration _configuration;
        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {   _userService = new UserService(_configuration);
            var users = _userService.GetUsers();

        
            return  new JsonResult(User);
        }

        [HttpPost]

        public IActionResult Post(User user){

            if (!ModelState.IsValid){
                var errors = ModelState.Values.SelectMany(err => err.Errors);
                return  new JsonResult(errors);
            }
            var result = _userService.CreateUser(user);
            if (!result){
                return new JsonResult(new { error="Could not create user" });
            }
            return Redirect("/User");

        }




    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nezter_backend
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        // private UserService _userService;
        private UserServiceSqlite _userService;
        private readonly IConfiguration _configuration;
        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = new UserServiceSqlite(_configuration);
        }


        [HttpGet]
        public async Task<IEnumerable<UserView>> Get()
        {

            var users = await _userService.GetUsers();


            return users;
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromForm] User user)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(err => err.Errors);
                return new JsonResult(errors);
            }
            var result = await _userService.CreateUser(user);
            if (!result)
            {
                return new JsonResult(new { error = "Could not create user" });
            }
            return Redirect("/User");

        }

        [HttpPut]
        public async Task<IActionResult> Deactivate()
        {
            var user = await _userService.Deactivate("memo");


            return Ok();


        }

        [HttpGet]
        public IActionResult GetUser(string name)
        {

            var result = _userService.GetUser(name);

            return Redirect("/user");
        }



    }
}

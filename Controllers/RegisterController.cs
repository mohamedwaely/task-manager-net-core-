using Microsoft.AspNetCore.Mvc;
using task_manager.Services;

namespace task_manager.Controllers
{
    [ApiController]
    [Route("/register")]
    public class RegisterController : ControllerBase
    {
        private ILogger _logger;
        private RegisterService _registerService;
        public RegisterController(ILogger<RegisterController> logger, RegisterService registerService)
        {
            _logger = logger;
            _registerService = registerService;
        }

        [HttpPost]
        public ActionResult registerNewUser(registerTemp registerData)
        {
            if(string.IsNullOrWhiteSpace(registerData.username) || string.IsNullOrWhiteSpace(registerData.email) || string.IsNullOrWhiteSpace(registerData.password))
            {
                return BadRequest("All Fields are required");
            }

            var res=_registerService.registerUser(registerData);

            if(!res)
            {
                return Conflict("The Entered Email Already Exists");
            }

            return Ok("User Registed Successfully");

        }



    }
}

using Microsoft.AspNetCore.Mvc;
using task_manager.Services;
using task_manager.Data;

namespace task_manager.Controllers
{
    [ApiController]
    [Route("/login")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;
        private JwtConfig _jwtConfig;
        public LoginController(LoginService loginService, JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _loginService = loginService;
        }
        [HttpPost]
        public ActionResult login(LoginReq req)
        {
            if(string.IsNullOrWhiteSpace(req.email) || string.IsNullOrWhiteSpace(req.password))
            {
                return BadRequest("All Fields are required");
            }

            var getUserRes = _loginService.findUser(req);

            if ((bool)getUserRes["success"])
            {
                var res = new Dictionary<string, object>();
                var user = getUserRes["user"] as User;
                res.Add("success", true);
                res.Add("mess", getUserRes["mess"]);
                res.Add("userEmail", user.Email);
                res.Add("userId", user.Id);
                res.Add("role", "user");

                var token = _jwtConfig.generateToken(user);

                res.Add("token", token);
                res.Add("tokenCreationTime", DateTime.Now);
                res.Add("tokenExpirationTime", DateTime.Now.AddMinutes(60));

                return Ok(res);

            }

            else
            {
                return BadRequest(getUserRes);
            }

        }

    }
}

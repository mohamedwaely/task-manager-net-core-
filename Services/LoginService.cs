using task_manager.Controllers;
using task_manager.Data;

namespace task_manager.Services
{
    public class LoginService
    {
        private AppDBContext _context;
        public LoginService(AppDBContext context) {
            
            _context = context;
        }
        public Dictionary<string,object> findUser(LoginReq req) 
        {

            var user = _context.Users.SingleOrDefault(u => u.Email == req.email);
            var dic = new Dictionary<string, object>();
            
            if (user == null)
            {
                dic.Add("success", false);
                dic.Add("mess", "Email not found");
                return dic;
            }

            else
            {
                dic.Add("success", true);
                dic.Add("mess", "Login Success");
                dic.Add("user", user);
                return dic;

            }

        }
    }
}

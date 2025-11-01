using task_manager.Controllers;
using task_manager.Data;

namespace task_manager.Services
{
    public class RegisterService
    {
        private ILogger _logger;
        private AppDBContext _context;
        public RegisterService(ILogger<RegisterService> logger, AppDBContext context) 
        {
            _logger = logger;
            _context = context;
        
        }

        public bool registerUser(registerTemp userInfo)
        {
            if(_context.Users.Any(u=>u.Email==userInfo.email))
            {
                _logger.LogWarning("Email is already registered before");
                return false;
            }

            var hashedPassword=BCrypt.Net.BCrypt.HashPassword(userInfo.password);
            var user = new User
            {
                userName = userInfo.username,
                Email = userInfo.email,
                hPassword = hashedPassword
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

    
    }
}

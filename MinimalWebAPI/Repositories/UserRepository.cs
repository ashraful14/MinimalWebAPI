using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IConfiguration _configuration;
        private readonly ProductDbContext _context;
        public UserRepository(IConfiguration config, ProductDbContext context)
        {
            _configuration = config;
            _context = context;
        }
        public string GetToken(UserInfo _userData)
        {
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                var user = _context.UserInfos.Where(u => u.UserName == _userData.UserName && u.Password == _userData.Password).SingleOrDefault();

                if (user != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("Password", user.Password),
                    new Claim("Email", user.Email)
                   };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Issuer"],
                        _configuration["Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
               
            }
            return null;    
        }
    }
}

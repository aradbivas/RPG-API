using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotNet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._context = context;

        }
        public async Task<ServiceResponse<string>> Login(string UserName, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserName.ToLower() == UserName.ToLower());
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }
            if (!VerifyPassowrd(password, user.PasswordHased, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password";
                return response;
            }
            response.Data = CreateToken(user);
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = "User Already exists";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHased = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;


            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;

        }

        private void CreatePasswordHash(string password, out byte[] passworedHash, out byte[] passworedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passworedSalt = hmac.Key;
                passworedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassowrd(string password, byte[] passworedHased, byte[] passworedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passworedSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passworedHased);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler =  new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
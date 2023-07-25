using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Repository.Configuration;

namespace DataAccess.Utils
{
    public class HashServices
    {
        private const int _numBytesRequested = 256 / 8;
        private const int _iterationCount = 10000;
        private const KeyDerivationPrf _algorithm = KeyDerivationPrf.HMACSHA256;
        private readonly byte[] _salt;
        private readonly string _encryptedPassword;
        public string EncryptedPassword { get { return _encryptedPassword; } }
        public HashServices(string password, string saltString)
        {
            _salt = Encoding.UTF8.GetBytes(saltString);
            _encryptedPassword = EncryptPassword(password, _salt);
        }
        private static string EncryptPassword(string password, byte[] salt)
        {
            string encryptedPass = GetKeyDerivation(password, salt, _algorithm, _iterationCount, _numBytesRequested).ToBase64String();
            return encryptedPass;
        }
        public bool IsPassed(string storedPassword) => _encryptedPassword == storedPassword;
        private static byte[] GetKeyDerivation(string password, byte[] salt, KeyDerivationPrf algorithm, int iterationCount, int numByteRequested)
        {
            return KeyDerivation.Pbkdf2(
                 password: password,
                 salt: salt,
                 prf: algorithm,
                 iterationCount: iterationCount,
            numBytesRequested: numByteRequested
             );
        }
        public static (string token, DateTime expires) GenerateJwtToken(BusinessObject.Jwt jwtSetting, Repository.Entities.User user)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, jwtSetting.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.MobilePhone, user.Phone),
                        new Claim(ClaimTypes.Role, user.IsAdmin.ToString()),
                        //new Claim(ClaimTypes.Name, user.FullName),
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(jwtSetting.ValidTime);
            var jwtToken = new JwtSecurityToken(
                jwtSetting.Issuer,
                jwtSetting.Audience,
                claims,
                expires: expires,
                signingCredentials: signIn);
            return (new JwtSecurityTokenHandler().WriteToken(jwtToken), expires);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KonceptCSDAPI
{
    public class CommonFunctions
    {
        #region Controller Properties
        private IConfiguration _configuration;
        #endregion Controller Properties

        public CommonFunctions(IConfiguration configuration, IHostingEnvironment env)
        {
            this._configuration = configuration;
        }

        public string GenerateToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWTSetting:Key"]));
            SigningCredentials signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: this._configuration["JWTSetting:Issuer"],
                audience: this._configuration["JWTSetting:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this._configuration["JWTSetting:ExpiryInMins"])),
                claims: claims,
                signingCredentials: signInCred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // Convert To SHA512
        public string ConvertToSHA512(string _string)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                _string = Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(_string)));
            }
            return _string;
        }
    }
}

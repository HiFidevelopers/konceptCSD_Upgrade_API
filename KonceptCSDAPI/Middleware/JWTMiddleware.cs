using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonceptCSDAPI.Models.Authentication;

namespace KonceptCSDAPI.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationManager _iauthenticationManager;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration, IAuthenticationManager iauthenticationManager)
        {
            _next = next;
            _configuration = configuration;
            _iauthenticationManager = iauthenticationManager;
        }

        public async Task Invoke(HttpContext context, SiginModel model)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token, model);

            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token, SiginModel model)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var JWTToken = (JwtSecurityToken)validatedToken;
                var USER_ID = JWTToken.Claims.First(x => x.Type == "USER_ID").Value;

                // attach account to context on successful jwt validation
                context.Items["User"] = _iauthenticationManager.Signin(model);
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}

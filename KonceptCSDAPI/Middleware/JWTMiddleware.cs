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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace KonceptCSDAPI.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly IConfiguration _configuration;
        private string SecretKey = string.Empty;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;

            _jsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _configuration = configuration;
            this.SecretKey = this._configuration["JWTSetting:Key"];
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token);

            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                token = token.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                string authHeader = token;
                authHeader = authHeader.Replace("Bearer ", "");
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

                var User_ID = tokenS.Claims.First(x => x.Type == "User_ID").Value;

                var key = Encoding.ASCII.GetBytes(this.SecretKey);

                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = this._configuration["JWTSetting:Issuer"],
                        ValidAudience = this._configuration["JWTSetting:Audience"],
                        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWTSetting:Key"]))
                    }, out SecurityToken validatedToken);
                }
                catch (Exception ex)
                {
                    
                }
            }
            catch (Exception ex)
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }



    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
    public class CustomMessageException : Exception
    {
        private string _exceptionMessage = string.Empty;

        public string ExceptionMessage { get { return _exceptionMessage; } set { _exceptionMessage = value; } }

        public CustomMessageException() : base() { }

        public CustomMessageException(string exceptionMessage) : base(exceptionMessage)
        {
            _exceptionMessage = exceptionMessage;
        }

        public CustomMessageException(string exceptionMessage, string message) : base(message)
        {
            _exceptionMessage = exceptionMessage;
        }

        public CustomMessageException(string exceptionMessage, string message, Exception innerException) : base(message, innerException)
        {
            _exceptionMessage = exceptionMessage;
        }
    }

    [Serializable]
    public class RulesException : Exception
    {
        private readonly List<ErrorInfo> _errors;

        public RulesException(string propertyName, string errorMessage, string prefix = "")
        {
            _errors = Errors;
            _errors.Add(new ErrorInfo($"{prefix}{propertyName}", errorMessage));
        }

        public RulesException(string propertyName, string errorMessage, object onObject, string prefix = "")
        {
            _errors = Errors;
            _errors.Add(new ErrorInfo($"{prefix}{propertyName}", errorMessage, onObject));
        }

        public RulesException()
        {
            _errors = Errors;
        }

        public RulesException(List<ErrorInfo> errors)
        {
            _errors = errors;
        }

        public List<ErrorInfo> Errors
        {
            get
            {
                return _errors ?? new List<ErrorInfo>();
            }
        }
    }

    public class ErrorInfo
    {
        private readonly string _errorMessage;
        private readonly string _propertyName;
        private readonly object _onObject;

        public ErrorInfo(string propertyName, string errorMessage)
        {
            _propertyName = propertyName;
            _errorMessage = errorMessage;
            _onObject = null;
        }

        public ErrorInfo(string propertyName, string errorMessage, object onObject)
        {
            _propertyName = propertyName;
            _errorMessage = errorMessage;
            _onObject = onObject;
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }
    }

    public static class RulesExceptionExtensions
    {
        public static void AddModelStateErrors(this RulesException ex, ModelStateDictionary modelState)
        {
            foreach (var error in ex.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }

        public static void AddModelStateErrors(this IEnumerable<RulesException> errors, ModelStateDictionary modelState)
        {
            foreach (RulesException ex in errors)
            {
                ex.AddModelStateErrors(modelState);
            }
        }

        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value
                        .Errors
                        .Select(e => e.ErrorMessage).ToArray())
                        .Where(m => m.Value.Count() > 0);
            }
            return null;
        }

        public static IEnumerable Errors(this ModelStateDictionary modelState, bool fixName = false)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => fixName ? kvp.Key.Replace("model.", string.Empty) : kvp.Key,
                    kvp => kvp.Value
                        .Errors
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid value entered." : e.ErrorMessage).ToArray())
                        .Where(m => m.Value.Count() > 0);
            }
            return null;
        }
    }
}

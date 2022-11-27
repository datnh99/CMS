using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CMS_SU21_BE.Common.Authentication
{
    public class JwtManager : ApiController
    {
        private const string Secret = "WVkwuRO9PUDqWcoi0Ux9CILfuA0HY6NN";
        private UserService userService = new UserServiceImpl();
        private UserRoleService userRoleService = new UserRoleServiceImpl();

        public Dictionary<String,Object> GenerateToken(string username,bool basicLogin, int expireMinutes = 180)
        {
            if (!username.EndsWith("@fpt.edu.vn") && !basicLogin)
            {
                throw new Exception("Unsupported domain!!");
            }
            else
            {
                username = username.Replace("@fpt.edu.vn","");
            }
            var mapResult = new Dictionary<String, Object>();
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
            }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            UserResponse userDetail = userService.getUserDetailByAccount(username);
            List<UserRoleResponse> userRoleResponse = userRoleService.getUserRoleByAccount(username);
            ClaimsIdentity identity = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("username", username));
            identity.AddClaim(new Claim("userId", userDetail.userId.ToString()));

            mapResult.Add("token", token);
            mapResult.Add("account", username);
            mapResult.Add("userDetail", userDetail);
            mapResult.Add("roleList", userRoleResponse);
            mapResult.Add("expiredTime", tokenDescriptor.Expires);
            return mapResult;
        }
        public Dictionary<String, Object> GenerateToken(Dictionary<String, Object> profile, bool basicLogin, int expireMinutes = 30)
        {
            string username = profile["email"].ToString();
            if (!username.EndsWith("@fpt.edu.vn") && !basicLogin)
            {
                throw new Exception("Unsupported domain!!");
            }
            else
            {
                username = username.Replace("@fpt.edu.vn", "");
            }
            var mapResult = new Dictionary<String, Object>();
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
            }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            UserResponse userDetail = userService.getUserDetailByAccount(username);
            List<UserRoleResponse> userRoleResponse = userRoleService.getUserRoleByAccount(username);
            /*           ClaimsIdentity identity = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);*/
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim("username", username));
            identity.AddClaim(new Claim("userId", userDetail.userId.ToString()));
            mapResult.Add("token", token);
            mapResult.Add("account", username);
            mapResult.Add("userDetail", userDetail);
            mapResult.Add("roleList", userRoleResponse);
            mapResult.Add("expiredTime", tokenDescriptor.Expires);


            return mapResult;
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception e )
            {
                Console.WriteLine(e.StackTrace.ToString());
                //should write log
                return null;
            }
        }
        public Dictionary<string, Object> getUserNameFromGoogleIDToken(string token)
        {

            string URL = "https://www.googleapis.com/oauth2/v3/tokeninfo";
            string urlParameters = "?id_token=" + token;

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                   var dataObjectsString = response.Content.ReadAsStringAsync().Result;
                   var dataObjects = JsonConvert.DeserializeObject<Dictionary<string, Object>>(dataObjectsString);
                   
                  return dataObjects;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());

                //should write log
                return null;
            }
        }
    }
}
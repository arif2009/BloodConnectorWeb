﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using BloodConnector.Shared.Log;
using BloodConnector.WebAPI.Filters;
using BloodConnector.WebAPI.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Providers;
using BloodConnector.WebAPI.Results;
using BloodConnector.WebAPI.Utilities;
using BloodConnector.WebAPI.ServiceResult;
using BloodConnector.WebAPI.VM;

namespace BloodConnector.WebAPI.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/Account")]
    //[EnableCors("http://localhost:14717,http://localhost:2102", "*", "*")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;
        public ApplicationDbContext Db { get; private set; }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        public AccountController()
        {
            Db = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? new ApplicationRoleManager(new RoleStore<IdentityRole>()); }
            private set { _roleManager = value; } 
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private async Task<ServiceResult<RegisterExternalBindingModel>> ForgotPassword(RegisterExternalBindingModel model, string callbackUrl)
        {
            Contract.Assert(!string.IsNullOrEmpty(callbackUrl), "The 'callbackUrl' cannot be null or empty");

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return FailedResult<RegisterExternalBindingModel>(model, m => m.Email, "[[[Invalid user]]]");
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            string queryString = $"?code={HttpUtility.UrlEncode(code)}&Email={HttpUtility.UrlEncode(user.Email)}";

            var url = callbackUrl + queryString;
            var subject = "Reset your BloodConnector password";
            var body = GetResetBody(url);
            try
            {
                await UserManager.SendEmailAsync(user.Id, subject, body);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return FailedResult<RegisterExternalBindingModel>(model, m => m.Email, "[[[Network Error !]]]");
            }
            
            return SuccessResult<RegisterExternalBindingModel>(model);
        }
        private async Task<ServiceResult<ResetPasswordViewModel>> ChangePassword(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return FailedResult<ResetPasswordViewModel>(model, x => x.Email, "Email was not found]");
            }

            var resetResult = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (!resetResult.Succeeded)
            {
                return FailedResult<ResetPasswordViewModel>(model, GetErrors(resetResult));
            }

            //If the user has not confirmed an email, check it as confirmed now
            if (!user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                var result = await UserManager.UpdateAsync(user);
            }

            return SuccessResult<ResetPasswordViewModel>(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateModelState]
        [Route("resetpassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var result = await this.ChangePassword(model);
            if (!result.Success)
            {
                MapErrorsToModelState(result.ErrorMessages);
                return BadRequest(ModelState);
            }

            return Ok(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateModelState]
        [Route("passwordrecoverybyemail")]
        public async Task<IHttpActionResult> PasswordRecoveryByEmail(RegisterExternalBindingModel model)
        {
            var urlTemplate = Url.Link("Account", new { controller = "Account", action = "ResetPassword" });
            var result = await this.ForgotPassword(model, urlTemplate);
            if (result.Success)
            {
                return Ok<RegisterExternalBindingModel>(model);
            }

            ModelState.AddModelError("Email", "[[[Email was not found]]]");
            return BadRequest(ModelState);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        [AllowAnonymous]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [ValidateModelState]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            try
            {
                if (await this.EmailAlreadyExist(model.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return BadRequest(ModelState);
                }

                /*if (!ModelState.IsValid){}*/

                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NikeName = model.NikeName,
                    BloodGiven = model.BloodGiven,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BloodGroupId = model.BloodGroupId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                var loc = await We.GetUserLocation();
                if (loc != null)
                {
                    var country = await Db.Country.AsNoTracking().FirstOrDefaultAsync(x => x.TowLetterCode == loc.Country);
                    user.CountryId = country?.ID;
                    user.City = ProjectHelper.ConcatTwoString(loc.City, loc.Region);
                    user.LatLong = loc.Loc;
                }

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                var role = await UserManager.AddToRoleAsync(user.Id, Enums.Role.FirstOrDefault(x => x.Value == "3").Key);
                return Ok(role);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                //ModelState.AddModelError("Exception", ex.Message);
                ModelState.AddModelError("Network", "Network problem !");
                return BadRequest(ModelState);
            }
        }

        // POST api/Account/app_register
        [AllowAnonymous]
        [HttpPost]
        [Route("app_register")]
        [ValidateModelState]
        public async Task<IHttpActionResult> AppRegister(AppRegisterBindingModel model)
        {

            try
            {
                var splitedName = model.Name.Split(' ');

                if (await this.EmailAlreadyExist(model.Email))
                {
                    ModelState.AddModelError("model.Email", "Email already in use!");
                    return BadRequest(ModelState);
                }

                var user = new User()
                {
                    FirstName = splitedName.ElementAtOrDefault(0),
                    LastName = splitedName.ElementAtOrDefault(1),
                    NikeName = splitedName.ElementAtOrDefault(2),
                    BloodGiven = model.BloodGiven,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BloodGroupId = model.BloodGroupId,
                    Gender = model.Gender,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                var loc = await We.GetUserLocation();
                if (loc != null)
                {
                    var country = await Db.Country.AsNoTracking().FirstOrDefaultAsync(x => x.TowLetterCode == loc.Country);
                    user.CountryId = country?.ID;
                    user.City = ProjectHelper.ConcatTwoString(loc.City, loc.Region);
                    user.LatLong = loc.Loc;
                }

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                var role = await UserManager.AddToRoleAsync(user.Id, Enums.Role.FirstOrDefault(x => x.Value == "3").Key);
                return Ok(role);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                //ModelState.AddModelError("Exception", ex.Message);
                ModelState.AddModelError("model.Network", "Network problem !");
                return BadRequest(ModelState);
            }
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            User user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                var userDetails = Db.Users.Include(x => x.BloodGroup.Users).FirstOrDefault(x => x.UserId == user.UserId);
                var userDetailsVm = Mapper.Map<UserVM>(userDetails);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(userDetailsVm);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new User() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private void MapErrorsToModelState(List<KeyValuePair<string, string>> errors)
        {
            errors?.ToList().ForEach(p => ModelState.AddModelError(p.Key, p.Value));
        }

        protected ServiceResult<TViewModel> FailedResult<TViewModel>(TViewModel model, List<KeyValuePair<string, string>> errorMessages)
        {
            var result = new ServiceResult<TViewModel>
            {
                Model = model,
                ErrorMessages = errorMessages
            };

            return result;
        }

        private List<KeyValuePair<string, string>> GetErrors(IdentityResult result)
        {
            var errorMessages = new List<KeyValuePair<string, string>>();
            foreach (var error in result.Errors)
            {
                errorMessages.Add(new KeyValuePair<string, string>(string.Empty, error));
            }

            return errorMessages;
        }
        private string GetResetBody(string callbackUrl)
        {
            var body = "Please reset your password by clicking <a href =\"" + callbackUrl + "\">here</a>";
            body += "<br/>";
            body += "<br/>";
            body += "If the link does not work, paste this into your browser. ";
            body += "<br/>";
            body += callbackUrl;

            return body;
        }

        protected ServiceResult<TViewModel> SuccessResult<TViewModel>(TViewModel model)
        {
            return new ServiceResult<TViewModel>
            {
                Model = model,
                ErrorMessages = new List<KeyValuePair<string, string>>()
            };
        }

        protected ServiceResult<TViewModel> FailedResult<TViewModel>(TViewModel model, Expression<Func<TViewModel, object>> properyRef, string errorMessage)
        {
            string propertyName = ProjectHelper.GetName<TViewModel>(properyRef);
            var result = new ServiceResult<TViewModel>
            {
                Model = model,
                ErrorMessages = new List<KeyValuePair<string, string>>()
            };

            result.ErrorMessages.Add(new KeyValuePair<string, string>(string.Format("model.{0}", propertyName), errorMessage));

            return result;
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            Logger.Log("Error from GetErrorResult function");
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        private async Task<bool> EmailAlreadyExist(string email)
        {
            var result = await UserManager.FindByEmailAsync(email);
            return result != null;
        }

        #endregion
    }
}

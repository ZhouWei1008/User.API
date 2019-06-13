using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using User.Identity.Services;
namespace User.Identity.Authentication
{
    public class SmsAuthCodeValidator : IExtensionGrantValidator
    {

        public IAuthCodeService _authCodeService;
        public IUserService _userService;


        public SmsAuthCodeValidator(IAuthCodeService authCodeService, IUserService userService)
        {

            _authCodeService = authCodeService;
            _userService = userService;
        }

        public string GrantType => "sms_auth_code";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw["phone"];
            var auth_code = context.Request.Raw["auth_code"];
            var err = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(auth_code))
            {
                context.Result = err;
                return;
            }

            if (!_authCodeService.Validate(phone, auth_code))
            {
                context.Result = err;
                return;
            }
            var userid = await _userService.CheckOrCreate(phone);
            if (userid <= 0)
            {
                context.Result = err;
                return;
            }

            context.Result = new GrantValidationResult(userid.ToString(), GrantType);

        }
    }
}

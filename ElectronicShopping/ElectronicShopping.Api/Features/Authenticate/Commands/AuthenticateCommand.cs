using ElectronicShopping.Api.Features.Authenticate.Models;
using ElectronicShopping.Api.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ElectronicShopping.Api.Features.Authenticate.Commands
{
    public class AuthenticateCommand : IRequest<TokenModel>
    {
        public AuthenticateCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateCommandHandler : RequestHandler<AuthenticateCommand, TokenModel>
    {
        private readonly AppSettingsModel _appSettingsModel;

        public AuthenticateCommandHandler(IOptions<AppSettingsModel> appSettings)
        {
            _appSettingsModel = appSettings.Value;
        }

        protected override TokenModel Handle(AuthenticateCommand request)
        {
            //var user = _users.SingleOrDefault(u => u.Username == request.UserName && u.Password == request.Password);
            //if (user == null)
            //    throw new BadRequestException("UserName or Password wrong!");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettingsModel.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenModel = new TokenModel();
            tokenModel.access_token = tokenHandler.WriteToken(token);

            return tokenModel;
        }
    }
}

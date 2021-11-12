using ElectronicShopping.Api.Features.Authenticate.Models;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

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

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, TokenModel>
    {
        private readonly AppSettingsModel _appSettingsModel;
        private readonly IUserRepository _userRepository;

        public AuthenticateCommandHandler(IOptions<AppSettingsModel> appSettings, IUserRepository userRepository)
        {
            _appSettingsModel = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<TokenModel> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AuthenticateUser(request.UserName, request.Password, cancellationToken);
            if (user == null)
                throw new BadRequestException("UserName or Password wrong!");

            var token = SecurityHelper.GenerateToken(user, _appSettingsModel.Secret);

            var tokenModel = new TokenModel()
            {
                access_token = token
            };

            return tokenModel;
        }
    }
}

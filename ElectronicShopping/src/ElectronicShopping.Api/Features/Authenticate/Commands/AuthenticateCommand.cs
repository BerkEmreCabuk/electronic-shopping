using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Features.Authenticate.Models;
using ElectronicShopping.Api.Helpers;
using ElectronicShopping.Api.Infrastructure.Cache;
using ElectronicShopping.Api.Models;
using ElectronicShopping.Api.Models.Exceptions;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
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
        private readonly ICacheService _cacheService;
        private readonly ILogger<AuthenticateCommandHandler> _logger;

        public AuthenticateCommandHandler(
            IOptions<AppSettingsModel> appSettings,
            IUserRepository userRepository,
            ICacheService cacheService,
            ILogger<AuthenticateCommandHandler> logger)
        {
            _appSettingsModel = appSettings.Value;
            _userRepository = userRepository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<TokenModel> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AuthenticateUser(request.UserName, request.Password, cancellationToken);
            if (user == null)
                throw new BadRequestException("UserName or Password wrong!");

            var token = SecurityHelper.GenerateToken(user, _appSettingsModel.Secret);

            await _cacheService.Add($"{CacheKeyConstant.USER_INFO}{user.Id}", token, TimeSpan.FromHours(6));
            _logger.LogInformation("token created {@token}", token);

            return new TokenModel(token);
        }
    }
}

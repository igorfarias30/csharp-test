using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using DoroTech.BookStore.Domain.Aggregates;
using OperationResult;

namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class RegisterCommandHandler : BaseCommandHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordEncrypter passwordEncrypter)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordEncrypter = passwordEncrypter;
    }

    public override async Task<Result<AuthenticationResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Result.Error<AuthenticationResponse>(new Exception("error"));

        var encryptedPassword = _passwordEncrypter.CreatePasswordHash(command.Password);

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Salt = encryptedPassword.Salt,
            Hash = encryptedPassword.Hash
        };

        _userRepository.Add(user);

        //Guid userId = Guid.NewGuid();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token
        );
    }
}

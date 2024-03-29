using Infrastrucure.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class DeleteUserCommandHandler : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommandHandler> { 
        private readonly IUserRepository _userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteUserCommandHandler request, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveUserAsync(request.Id);
        }
    }
}

using AutoMapper;
using DomainEntities.Entities;
using Application.ViewModels;
using Infrastrucure.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class AddUserCommandHandler : IRequest
    {
        public AddEditUserViewModel User { get; set; }
    }
    public class AddUserHandler : IRequestHandler<AddUserCommandHandler> { 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task Handle(AddUserCommandHandler request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            await _userRepository.AddUserAsync(user);
        }
    }
}

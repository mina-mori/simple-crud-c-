using AutoMapper;
using DomainEntities.Entities;
using Application.ViewModels;
using Infrastrucure.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class UpdateUserCommandHandler : IRequest
    {
        public int Id { get; set; } 
        public AddEditUserViewModel User { get; set; }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommandHandler> { 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task Handle(UpdateUserCommandHandler request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            await _userRepository.UpdateUserAsync(request.Id, user);
        }
    }
}

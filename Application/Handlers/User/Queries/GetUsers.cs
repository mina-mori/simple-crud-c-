using AutoMapper;
using Application.ViewModels;
using Infrastrucure.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetUsersQueryHandler : IRequest<List<UserViewModel>>
    {
    }
    public class GetUsersHandler : IRequestHandler<GetUsersQueryHandler, List<UserViewModel>> { 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserViewModel>> Handle(GetUsersQueryHandler request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            var usersViewModel = _mapper.Map<List<UserViewModel>>(users);
            return usersViewModel;
        }
    }
}

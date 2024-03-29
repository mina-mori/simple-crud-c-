using Application.Helpers;
using AutoMapper;
using Application.ViewModels;
using Infrastrucure.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetUserByIdQueryHandler : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQueryHandler, UserViewModel> { 
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserViewModel> Handle(GetUserByIdQueryHandler request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }
    }
}

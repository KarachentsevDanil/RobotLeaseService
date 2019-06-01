using AutoMapper;
using RLS.BLL.DTOs.Users;
using RLS.BLL.Services.Contracts.Users;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public UserService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserDto> GetUserByTermAsync(string term, CancellationToken ct = default)
        {
            var item = await _unitOfWork.UserRepository.GetUserTermAsync(term, ct);
            return _mapper.Map<GetUserDto>(item);
        }

        public async Task<GetUserDto> GetUserByIdAsync(string id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.UserRepository.GetAsync(id, ct);
            return _mapper.Map<GetUserDto>(item);
        }

        public async Task AddUserSearchResultAsync(string userId, CancellationToken ct = default)
        {
            var item = await _unitOfWork.UserRepository.GetAsync(userId, ct);

            if (string.IsNullOrWhiteSpace(item.Interests))
            {
                return;
            }

            UserInterestsSearch newItem = new UserInterestsSearch
            {
                CreatedAt = DateTime.Now,
                Interests = item.Interests
            };

            _unitOfWork.UserInterestsSearchRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);
        }
    }
}
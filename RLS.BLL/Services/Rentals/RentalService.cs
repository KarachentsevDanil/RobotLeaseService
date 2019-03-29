﻿using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Models;
using RLS.Domain.Rentals;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Rentals
{
    public class RentalService : IRentalService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RentalService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CollectionResult<GetRentalDto>> GetRentalsByFilterParamsAsync(
            RentalFilterParamsDto filterParams,
            CancellationToken ct = default)
        {
            var rentalFilterParams = _mapper.Map<RentalFilterParams>(filterParams);

            var rentals =
                await _unitOfWork.RentalRepository.GetRentalsByFilterParamsAsync(rentalFilterParams, ct);

            return _mapper.Map<CollectionResult<GetRentalDto>>(rentals);

        }

        public async Task<GetRentalDto> CreateRentalAsync(CreateRentalDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<Rental>(item);

            _unitOfWork.RentalRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalDto>(newItem);
        }
    }
}
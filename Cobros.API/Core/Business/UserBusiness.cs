﻿using AutoMapper;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Exceptions;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(int id)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(id);

            if (existing == null)
                throw new NotFoundException($"User with Id: {id} not found.");

            var result = _mapper.Map<UserDto>(existing);

            return result;
        }
    }
}

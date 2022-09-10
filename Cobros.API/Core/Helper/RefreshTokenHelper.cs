using Cobros.API.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Cobros.API.Core.Helper
{
    public interface IRefreshTokenHelper
    {
        Task RemoveUserRefreshTokens(int userId);
    }

    public class RefreshTokenHelper:IRefreshTokenHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task RemoveUserRefreshTokens(int userId)
        {
            var rTokens = await _unitOfWork.RefreshTokens.GetAllByUserId(userId);

            foreach (var rToken in rTokens)
                _unitOfWork.RefreshTokens.HardDelete(rToken);
        }

        public async Task<string> GetUniqueRefreshTokenValue()
        {
            var value = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var existing = await _unitOfWork.RefreshTokens.GetByValueAsync(value);

            if (existing != null)
                return await GetUniqueRefreshTokenValue();

            return value;
        }


    }
}

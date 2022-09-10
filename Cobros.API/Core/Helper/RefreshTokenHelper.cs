using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Core.Helper
{
    public static class RefreshTokenHelper
    {
        public async static Task RemoveUserRefreshTokens(int userId, IUnitOfWork unitOfWork)
        {
            var rTokens = await unitOfWork.RefreshTokens.GetAllByUserId(userId);

            foreach (var rToken in rTokens)
                unitOfWork.RefreshTokens.HardDelete(rToken);
        }


    }
}

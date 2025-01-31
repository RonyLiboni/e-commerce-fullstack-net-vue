using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Domain.Exceptions;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Security;
public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : BaseCRUDService<RefreshToken, Guid>(refreshTokenRepository, unitOfWork, mapper), IRefreshTokenService
{
    public async Task ThrowExceptionIfTokenIsNotValidAsync(Guid refreshTokenId, string email)
    {
        var isValid = await refreshTokenRepository.DoesRefreshTokenExistByIdAndEmailAsync(refreshTokenId, email);
        if (!isValid)
        {
            throw new InvalidLoginException("Refresh token provided is invalid.");
        }
    }

    public async Task RemoveAllOldRefreshTokensFromUserAsync(string email, Guid? lastRefreshTokenId)
    {
        await refreshTokenRepository.RemoveAllOldRefreshTokensFromUserAsync(email, lastRefreshTokenId);
        await _unitOfWork.SaveChangesAsync();
    }
}

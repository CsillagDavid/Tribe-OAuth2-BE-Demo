using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.Repositories
{
    public interface ISessionRepository
    {
        SessionDto FindByUserId(int userId);
        void Insert(SessionDto dto);
        void ArchiveByToken(string token);
    }
}
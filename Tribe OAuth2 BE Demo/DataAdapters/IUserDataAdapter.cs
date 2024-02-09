using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public interface IUserDataAdapter
    {
        User FindUserByEmail(string email);
        User Insert(User user);
    }
}

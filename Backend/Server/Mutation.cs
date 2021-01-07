using System.Threading.Tasks;
using HotChocolate;
using Server.Auth;

namespace Server
{
    public class Mutation
    {
        public Task<string> GetToken(string email, string password, [Service] IIdentityService identityService) =>
            identityService.Authenticate(email, password);
    }
}
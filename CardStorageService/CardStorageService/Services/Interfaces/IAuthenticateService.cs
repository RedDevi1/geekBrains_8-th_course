using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Models.Responses;

namespace CardStorageService.Services.Interfaces
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);
    }
}

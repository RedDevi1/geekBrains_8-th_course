namespace CardStorageService.Models.Responses
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; }

        public SessionInfo SessionInfo { get; set; }

        public AccountDto Account { get; set; }
    }
}

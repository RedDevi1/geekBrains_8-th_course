namespace CardStorageService.Models.Responses
{
    public class CreateCardResponse
    {
        public string? CardId { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

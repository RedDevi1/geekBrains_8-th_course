﻿namespace CardStorageService.Models.Responses
{
    public class GetCardsResponse
    {
        public IList<CardDto>? Cards { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

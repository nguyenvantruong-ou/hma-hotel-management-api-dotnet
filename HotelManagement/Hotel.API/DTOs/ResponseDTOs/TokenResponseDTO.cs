namespace Hotel.API.DTOs.ResponseDTOs
{
    public class TokenResponseDTO
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

        public TokenResponseDTO(string? token, string? refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}

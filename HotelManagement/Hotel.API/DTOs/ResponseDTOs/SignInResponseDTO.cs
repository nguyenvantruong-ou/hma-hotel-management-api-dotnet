namespace Hotel.API.DTOs.ResponseDTOs
{
    public class SignInResponseDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }

        public SignInResponseDTO(string Email, string Name, string Avatar, string Token)
        {
            this.Email = Email;
            this.Name = Name;
            this.Avatar = Avatar;
            this.Token = Token;
        }
    }
}

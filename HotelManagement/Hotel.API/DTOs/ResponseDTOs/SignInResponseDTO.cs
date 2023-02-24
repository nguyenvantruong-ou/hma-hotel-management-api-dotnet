namespace Hotel.API.DTOs.ResponseDTOs
{
    public class SignInResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsChangedPassword { get; set; }

        public SignInResponseDTO(int id, string email, string name, string avatar, string role, string token, string refreshToken, bool isChangedPassword)
        {
            Id = id;
            Email = email;
            Name = name;
            if (!String.IsNullOrEmpty(avatar))
                Avatar = avatar;
            else
                Avatar = "https://res.cloudinary.com/dykzla512/image/upload/v1672894851/HotelManagement/ng8l2mgr4xaykbzxwtby.jpg";
            Role = role;
            Token = token;
            RefreshToken = refreshToken;
            IsChangedPassword = isChangedPassword;
        }
    }
}

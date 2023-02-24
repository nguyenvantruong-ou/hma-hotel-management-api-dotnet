using Hotel.Domain.Rooms.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class CommentInfoResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public bool? Incognito { get; set; }
        public int? ParentId { get; set; }

        public CommentInfoResponseDTO(Comment comment)
        {
            Id = comment.Id;
            UserId = comment.AccountId;
            Content = comment.Content;
            DateCreated = comment.DateCreated;
            Incognito = comment.Incognito;
            ParentId = comment.ParentId;
            if (comment.Incognito == true)
            {
                Avatar = "https://res.cloudinary.com/dykzla512/image/upload/v1673505162/HotelManagement/Chrome-Incognito-Mode-Icon-256_bnettd.png";
                Username = ReplaceName(comment.Account.LastName + " " + comment.Account.FirstName);
            }
            else
            {
                Avatar = comment.Account.Avatar;
                Username = comment.Account.LastName + " " + comment.Account.FirstName;
            }
        }
        private string ReplaceName(string name)
        {
            string result = name[0].ToString();
            for (int i = 1; i < name.Length - 1; i++)
            {
                if (name[i] != ' ' && name[i-1] != ' ')
                    result += "*";
                else
                    result += name[i];
            }
            result += name[name.Length - 1];
            return result;
        }
    }
}

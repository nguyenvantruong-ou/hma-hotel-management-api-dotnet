using Hotel.Domain.Feedbacks.Entities;

namespace Hotel.API.DTOs.ResponseDTOs
{
    public class FeedbackGeneralResponseDTO
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public int AmountUnread { get; set; }
        public string ContentFeedback { get; set; }
        public string Time { get; set; }

        public FeedbackGeneralResponseDTO(Feedback feeback, int amountUnread)
        {
            UserId = feeback.AccountId;
            UserName = feeback.Account.LastName + " " + feeback.Account.FirstName;
            AmountUnread = amountUnread;
            ContentFeedback = feeback.Content;
            Avatar = feeback.Account.Avatar;

            DateTime now = DateTime.Now;
            TimeSpan time = (TimeSpan)(now - feeback.DateCreated);
            if (time.Days > 0)
            {
                if (time.Days < 31)
                    Time = time.Days + " ngày trước";
                else
                    Time = feeback.DateCreated.ToString().Substring(0, 10);
            }
            else if (time.Hours > 0 )
                Time = time.Hours + " giờ trước";
            else Time = time.Minutes + " phút trước";
        }
    }
}

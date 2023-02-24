using Hotel.Domain.Feedbacks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Feedbacks.DomainServices.Interfaces
{
    public interface IFeedbackService
    {
        Task AddFeedbackAsync(int userId, string content, int rating);
        Task<int> CountUnreadFeedbackAsync();
        IQueryable<Feedback> GetGeneralFeedbacks();
        IQueryable<Feedback> GetFeedbacks(int userId);
    }
}

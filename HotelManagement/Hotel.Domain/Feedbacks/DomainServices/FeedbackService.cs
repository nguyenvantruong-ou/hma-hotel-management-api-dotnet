using Hotel.Domain.Feedbacks.DomainServices.Interfaces;
using Hotel.Domain.Feedbacks.Entities;
using Hotel.Domain.Feedbacks.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Feedbacks.DomainServices
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _repoFeedback;
        public FeedbackService(IFeedbackRepository repoFeedback)
        {
            _repoFeedback = repoFeedback;
        }

        public async Task AddFeedbackAsync(int userId, string content, int rating)
        {
            if (userId < 1 || String.IsNullOrEmpty(content) || rating < 1)
                throw new Exception("Bad Request");

            Feedback feedback = new Feedback();
            feedback.AccountId = userId;
            feedback.Content = content;
            feedback.Rating = rating;
            feedback.IsRead = false;
            await _repoFeedback.AddEntityAsync(feedback);
        }

        public async Task<int> CountUnreadFeedbackAsync()
        {
            var amount =  await _repoFeedback.CountUnreadFeedbackAsync();
            return amount;
        }

        public IQueryable<Feedback> GetFeedbacks(int userId)
        {
            if (userId < 1)
                throw new Exception("userId must be a positive integer!");
            var result =_repoFeedback.GetFeedbacks(userId);
            result.ToList().ForEach(s => s.IsRead = true);
            return result;
        }

        public IQueryable<Feedback> GetGeneralFeedbacks()
        {
            return _repoFeedback.GetGeneralFeedbacks();
        }
    }
}

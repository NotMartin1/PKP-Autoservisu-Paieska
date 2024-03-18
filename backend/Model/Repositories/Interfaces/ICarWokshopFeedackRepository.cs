using Model.Entities.Feedback;
using Model.Entities.Feedback.Request;

namespace Model.Services
{
    public interface ICarWokshopFeedackRepository
    {
        void Create(FeedbackCreateRequest request);
        List<FeedbackDisplayData> GetTotal(int? shopId);
    }
}
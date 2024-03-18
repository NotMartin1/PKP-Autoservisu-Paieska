using Model.Entities;
using Model.Entities.Feedback;
using Model.Entities.Feedback.Request;

namespace Model.Services
{
    public interface ICarWorkshopFeedbackService
    {
        ServiceResult Create(FeedbackCreateRequest request);
        ServiceResult<List<FeedbackDisplayData>> List(int? shopId);
    }
}
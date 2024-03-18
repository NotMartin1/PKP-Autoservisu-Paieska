using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Feedback;
using Model.Entities.Feedback.Request;
using Model.Services;
using Server.Core.Middleware;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Feedback")]
    public class FeedbackController
    {
        public ICarWorkshopFeedbackService _carWorkshopFeedbackService;

        public FeedbackController(ICarWorkshopFeedbackService carWorkshopFeedbackService)
        {
            _carWorkshopFeedbackService = carWorkshopFeedbackService;
        }


        [ServiceFilter(typeof(TokenValidationFilter))]
        [HttpPost("Create")]
        public ServiceResult Create([FromBody] FeedbackCreateRequest request)
        {
            return _carWorkshopFeedbackService.Create(request);
        }

        [HttpGet("List")]
        public ServiceResult<List<FeedbackDisplayData>> List([FromQuery] int? shopId)
        {
            return _carWorkshopFeedbackService.List(shopId);
        }
    }
}

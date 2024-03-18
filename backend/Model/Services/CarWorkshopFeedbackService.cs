using Model.Entities;
using Model.Entities.Feedback;
using Model.Entities.Feedback.Request;
using Model.Services.Interfaces;

namespace Model.Services
{
    public class CarWorkshopFeedbackService : ICarWorkshopFeedbackService
    {
        private readonly ICarWokshopFeedackRepository _carWorkshopFeedbackRepository;
        private readonly ICarWorkshopService _carWorkshopService;
        private readonly IClientService _clientService;

        public CarWorkshopFeedbackService(ICarWokshopFeedackRepository carWokshopFeedackRepository, IClientService clientService, ICarWorkshopService carWorkshopService)
        {
            _carWorkshopFeedbackRepository = carWokshopFeedackRepository;
            _clientService = clientService;
            _carWorkshopService = carWorkshopService;
        }

        public ServiceResult Create(FeedbackCreateRequest request)
        {
            try
            {
                if (!request.ShopId.HasValue)
                    return new(false, "ShopId is not specified");

                if (!request.ClientId.HasValue)
                    return new(false, "ClientId is not specified");

                if (!request.Rating.HasValue)
                    return new(false, "Raiting is not specified");

                if (request.Rating < 0 || request.Rating > 5)
                    return new(false, "Raiting cannot be less than 0 or greater than 5");

                if (string.IsNullOrWhiteSpace(request.Message))
                    return new(false, "Feedback message is not specified");

                if (!_clientService.CheckIfExsitsById(request.ClientId.Value))
                    return new(false, $"Cannot find client by Id: {request.ClientId.Value}");

                if (!_carWorkshopService.CheckIfExsistsById(request.ShopId.Value))
                    return new(false, $"Cannot find workshop by Id: {request.ShopId.Value}");

                _carWorkshopFeedbackRepository.Create(request);

                return new(true);
            }
            catch (Exception ex)
            {
                return new(false, $"Failed to create feedback due to: {ex.Message}");
            }
        }

        public ServiceResult<List<FeedbackDisplayData>> List(int? shopId)
        {
            try
            {
                var list = _carWorkshopFeedbackRepository.GetTotal(shopId);
                return new() { Success = true, Data = list };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = $"Failed to get feedbacks due to: {ex.Message}" };
            }
        }
    }
}

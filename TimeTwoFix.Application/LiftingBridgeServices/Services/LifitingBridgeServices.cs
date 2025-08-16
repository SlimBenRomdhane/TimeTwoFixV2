using AutoMapper;
using TimeTwoFix.Application.Base;
using TimeTwoFix.Application.LiftingBridgeServices.Dtos;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Core.Entities.BridgeManagement;
using TimeTwoFix.Core.Interfaces;

namespace TimeTwoFix.Application.LiftingBridgeServices.Services
{
    public class LiftingBridgeServices : BaseService<LiftingBridge>, ILiftingBridgeServices
    {
        public LiftingBridgeServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ReadLiftingBridgeDto>> GetLiftingBridgesByLoadCapacityAsync(int loadCapacity)
        {
            var liftingBridges = await _unitOfWork.LiftingBridges.GetLiftingBridgesByLoadCapacityAsync(loadCapacity);
            if (liftingBridges == null || !liftingBridges.Any())
            {
                return Enumerable.Empty<ReadLiftingBridgeDto>();
            }
            var liftingBridgeDtos = _mapper.Map<IEnumerable<ReadLiftingBridgeDto>>(liftingBridges);
            return liftingBridgeDtos;


        }

        public async Task<IEnumerable<ReadLiftingBridgeDto>> GetLiftingBridgesByStatusAsync(string status)
        {
            var liftingBridges = await _unitOfWork.LiftingBridges.GetLiftingBridgesByStatusAsync(status);
            if (liftingBridges == null || !liftingBridges.Any())
            {
                return Enumerable.Empty<ReadLiftingBridgeDto>();
            }
            var liftingBridgeDtos = _mapper.Map<IEnumerable<ReadLiftingBridgeDto>>(liftingBridges);
            return liftingBridgeDtos;
        }
    }
}

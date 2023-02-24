using Hotel.API.Areas.Management.DTOs.ResponseDTO;
using Hotel.API.Areas.Management.Services.Interfaces;
using Hotel.Domain.Rooms.Repositories;
using Hotel.Domain.Statistics;
using Hotel.Infrastructure.Data.Statistics;

namespace Hotel.API.Areas.Management.Services
{
    public class StatisticsService : IStatisticsService
    {
        private IStatisticalRoomRepository _repoRoom;
        private IStatisticalServiceRepository _repoService;
        private IStatisticalOrderRepository _repOrder;
        private IRoomRepository _repoR;
        private int _maximumPeople = 5;

        public StatisticsService(IStatisticalRoomRepository repoRoom,
                                IStatisticalServiceRepository repoService,
                                IStatisticalOrderRepository repOrder,
                                IRoomRepository repoR)
        {
            _repoRoom = repoRoom;
            _repoService = repoService;
            _repOrder = repOrder;
            _repoR = repoR;
        }

        public async Task<StatisticalRevenueResponseDTO> StatiscalRevenueAsync(int year)
        {
            List<decimal> Rooms = new List<decimal>();
            List<decimal> Services = new List<decimal>();
            for(int i = 1; i < 13; i++)
            {
                decimal Room = await _repoRoom.GetRevenueRoomAsync(i, year);
                decimal Service = await _repoService.GetRevenueServiceAsync(i, year);
                Rooms.Add(Room);
                Services.Add(Service);

            }
            return new StatisticalRevenueResponseDTO(Rooms, Services);
        }

        public async Task<List<int>> StatisticalVisitorAsync()
        {
            List<int> Results = new List<int>();
            for(int i =1; i <= _maximumPeople; i++)
            {
                Results.Add(await _repOrder.GetCapitaAsync(i));
            }
            return Results;
        }

        public async Task<List<StatisticalAmountPeopleResponseDTO>> StatisticalAmountCustomerAsync()
        {
            int Year = DateTime.Now.Year;
            List <StatisticalAmountPeopleResponseDTO> Results = new List<StatisticalAmountPeopleResponseDTO>();
            for(int i = 2021; i <= Year; i++)
            {
                StatisticalAmountPeopleResponseDTO Result = new StatisticalAmountPeopleResponseDTO();
                Result.Year = i;
                Result.Amount = await _repOrder.GetCountCustomerAsync(i);
                Results.Add(Result);
            }
            return Results;
        }

        public async Task<List<StatisticRevenuePerRoomsResponseDTO>> StatisticalRevenuePerRoomsAsync(DateTime fromDate, DateTime toDate)
        {
            if (toDate < fromDate)
                throw new ArgumentOutOfRangeException("Bad Request");
            var results = _repoR.GetEntityByName("")
                .Select(s => new {
                    RoomId = s.Id,
                    RoomName = s.RoomName,
                    TotalMoney = s.OrderRooms.Where(or=>or.Order.DateCreated > fromDate && or.Order.DateCreated < toDate)
                    .Sum(s =>  s.Price)
                }).ToList();

            return results.Select(_ => new StatisticRevenuePerRoomsResponseDTO(_.RoomId, _.RoomName, _.TotalMoney)).ToList();
        }
    }
}

using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Rooms.DomainServices
{
    public class ReadCommentService : IReadCommentService
    {
        private readonly ICommentRepository _repo;
        public ReadCommentService(ICommentRepository repo)
        {
            _repo = repo;
        }

        public Task<int> CountCommentAsync(int roomId)
        {
            if (roomId < 1 )
                throw new ArgumentException("RoomId must be a positive integer!");
            var count = _repo.ReadComments(roomId).ToList().Count();
            return Task.FromResult(count);
        }

        public Task<List<Comment>> ReadCommentAsync(int roomId, int toIndex)
        {
            if (roomId < 1 || toIndex < 1)
                throw new ArgumentException("RoomId or ToIndex must be a positive integer!");
            var results = _repo.ReadComments(roomId).OrderByDescending(s => s.Id).Take(toIndex).ToList();
            return Task.FromResult(results);
        }

    }
}

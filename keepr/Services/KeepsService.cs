using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            return _repo.CreateKeep(keepData);
        }

        internal List<Keep> GetKeeps(string userId)
        {
            List<Keep> keeps = _repo.GetKeeps();
            keeps = keeps.FindAll(keep => keep.CreatorId == userId);
            return keeps;
        }

        internal Keep GetById(int keepId)
        {
            Keep foundKeep = _repo.GetById(keepId);
            if (foundKeep == null) throw new Exception($"Not a valid Id{keepId}");
            return foundKeep;
        }


    }
}
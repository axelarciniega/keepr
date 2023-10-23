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

            return keeps;
        }

        internal List<Keep> GetProfileKeeps(string profileId)
        {
            List<Keep> keeps = _repo.GetProfileKeeps(profileId);
            return keeps;
        }

        internal Keep GetById(int keepId)
        {
            Keep foundKeep = _repo.GetById(keepId);
            if (foundKeep == null) throw new Exception($"Not a valid Id{keepId}");
            return foundKeep;
        }

        internal Keep Edit(Keep updateData, string userInfo)
        {
            Keep original = this.GetById(updateData.Id);
            if (original.CreatorId != userInfo) throw new Exception("unauthorized to edit");

            original.Name = updateData.Name ?? original.Name;
            original.Description = updateData.Description ?? original.Description;
            original.Img = updateData.Img ?? original.Img;

            Keep keep = _repo.Edit(original);
            return original;

        }

        internal string Delete(int keepId, string userInfo)
        {
            Keep foundKeep = this.GetById(keepId);
            _repo.Delete(keepId);
            if (foundKeep == null) throw new Exception("undefined");
            if (foundKeep.CreatorId != userInfo) throw new Exception("unauthorized to delete");
            return $"{foundKeep.Name} was removed";
        }


    }
}
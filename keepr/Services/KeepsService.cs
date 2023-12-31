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

        internal Keep GetById(int keepId, string userInfo, bool increaseVisits = false)
        {
            Keep foundKeep = _repo.GetById(keepId);
            if (increaseVisits && foundKeep.CreatorId != userInfo)
            {
                this.IncreaseVisits(foundKeep);
            }
            if (foundKeep.Name == null) throw new Exception($"Not a valid Id{keepId}");
            return foundKeep;
        }

        internal Keep Edit(Keep updateData, string userInfo)
        {
            Keep original = this.GetById(updateData.Id, userInfo);
            if (original.CreatorId != userInfo) throw new Exception("unauthorized to edit");

            original.Name = updateData.Name ?? original.Name;
            original.Description = updateData.Description ?? original.Description;
            original.Img = updateData.Img ?? original.Img;

            _repo.Update(original);
            return original;

        }

        internal string DeleteKeep(int keepId, string userInfo)
        {
            Keep foundKeep = this.GetById(keepId, userInfo);
            if (foundKeep.CreatorId != userInfo) throw new Exception("unauthorized to delete");
            _repo.DeleteKeep(keepId);
            return $"{foundKeep.Name} was removed";
        }

        internal void IncreaseVisits(Keep keep)
        {
            keep.Views++;
            _repo.Update(keep);
        }


    }
}
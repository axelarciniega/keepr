using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
        }

        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfile(string profileId)
        {
            try
            {
                Profile profile = _profilesService.GetProfile(profileId);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
        {
            try
            {
                List<Keep> keeps = _keepsService.GetProfileKeeps(profileId);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }






    }
}
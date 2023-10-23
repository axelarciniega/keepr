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

        public ProfilesController(ProfilesService profilesService)
        {
            _profilesService = profilesService;
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






    }
}
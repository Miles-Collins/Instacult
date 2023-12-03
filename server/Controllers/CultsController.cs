using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace Instacult.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CultsController : ControllerBase
    {
        private readonly CultsService _cultsService;
        private readonly Auth0Provider _a0;
        private readonly CultMembersService _cultMembersService;

        public CultsController(CultsService cultsService, Auth0Provider a0, CultMembersService cultMembersService)
        {
            _cultsService = cultsService;
            _a0 = a0;
            _cultMembersService = cultMembersService;
        }

        [HttpGet]
        public ActionResult<List<Cult>> GetCults()
        {
            try
            {
                List<Cult> cults = _cultsService.GetCults();
                return Ok(cults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Cult>> CreateCult([FromBody] Cult cultData)
        {
            try
            {
                Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
                cultData.LeaderId = userInfo.Id;
                Cult cult = _cultsService.CreateCult(cultData);
                return Ok(cult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{cultId}")]
        public ActionResult<Cult> GetCultById(int cultId)
        {
            try
            {
                Cult cult = _cultsService.GetCultById(cultId);
                return Ok(cult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{cultId}/cultMembers")]
        public ActionResult<List<Cultist>> GetCultist(int cultId)
        {
            try
            {
                List<Cultist> cultists = _cultMembersService.GetCultist(cultId);
                return Ok(cultists);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
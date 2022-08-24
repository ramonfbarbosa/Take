using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TakeBlipChallenge.Model;
using TakeBlipChallenge.Service.Interfaces;

namespace TakeBlipChallenge.Controllers
{
    [Route("api/v1/[controller]")]
    public class CarrosselController : Controller
    {
        private IAvatarService AvatarService { get; }
        private IGhubInfoService GhubInfoService { get; }

        public CarrosselController(IAvatarService avatarService, IGhubInfoService ghubInfoService)
        {
            AvatarService = avatarService;
            GhubInfoService = ghubInfoService;
        }

        [HttpGet("carrossel-info")]
        public async Task<IActionResult> GetCarrosselInfo()
        {
            var carrossel = new Carrossel
            {
                Avatar = await AvatarService.GetAvatar(),
                GhubInfo = await GhubInfoService.GetGhubInfo()
            };
            return Ok(JsonConvert.SerializeObject(carrossel));
        }
    }
}

using Newtonsoft.Json.Linq;
using TakeBlipChallenge.Model;

namespace TakeBlipChallenge.Service.Interfaces;

public interface IGhubInfoService
{
    Task<List<GhubInfo>> GetGhubInfo();
}

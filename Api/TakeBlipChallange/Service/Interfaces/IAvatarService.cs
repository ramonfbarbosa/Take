using Newtonsoft.Json.Linq;
using TakeBlipChallenge.Model;

namespace TakeBlipChallenge.Service.Interfaces;

public interface IAvatarService
{
    Task<Avatar> GetAvatar();
}

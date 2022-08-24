using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TakeBlipChallenge.Model;
using TakeBlipChallenge.Service.Interfaces;

namespace TakeBlipChallenge.Service
{
    public class AvatarService : IAvatarService
    {
        private HttpClient HttpClient { get; }
        private static string Path { get; } = "https://api.github.com/users/takenet";

        public AvatarService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<Avatar> GetAvatar()
        {
            var avatar = await GetAsync();
            return new Avatar
            {
                AvatarUrl = avatar["avatar_url"].ToString()
            };
        }

        private async Task<JToken> GetAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Path);
                request.Headers.Add("Accept", "application/json");
                HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TakeChallenge", "1.0"));
                var response = await HttpClient.GetAsync(Path);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JToken.Parse(responseContent);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

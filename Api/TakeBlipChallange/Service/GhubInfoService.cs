using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TakeBlipChallenge.Model;
using TakeBlipChallenge.Service.Interfaces;

namespace TakeBlipChallenge.Service;

public class GhubInfoService : IGhubInfoService
{
    public HttpClient HttpClient { get; }
    private static string Path { get; } = "https://api.github.com/orgs/takenet/repos";

    public GhubInfoService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<List<GhubInfo>> GetGhubInfo()
    {
        var ghubInfo = await GetAsync();
        var lastFiveRepos = ghubInfo
            .Where(obj => obj["language"].Value<string>() is "C#")
            .OrderBy(obj => obj["created_at"])
            .Take(5)
            .ToList();
        return Parse(lastFiveRepos);
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

    private List<GhubInfo> Parse(List<JToken> jTokenList)
    {
        var ghubInfoList = new List<GhubInfo>();
        foreach (var item in jTokenList)
        {
            var obj = new GhubInfo
            {
                FullName = item["full_name"].ToString(),
                Description = item["description"].ToString()
            };
            ghubInfoList.Add(obj);
        }
        return ghubInfoList;
    }
}

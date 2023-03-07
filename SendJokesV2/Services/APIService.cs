using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using SendJokesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SendJokesV2.Services
{
    public interface IAPIService
    {
        Task<JokesModel> GetJokesHttpRequest();
        Task<Uri> GetComicHttpRequest(int comicNumber = 0);
        Task<IEnumerable<RandomFact>> GetRandomFactHttpRequest(int amount = 1);
    }
    public class APIService : IAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<AppSettings> _appSettings;

        public APIService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings;
        }


        public async Task<JokesModel> GetJokesHttpRequest()
        {
            using (var client = _httpClientFactory.CreateClient("MyJokesAPI"))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(_appSettings.Value.BaseURLJokes).Result;

                if (response.IsSuccessStatusCode)
                {
                    JokesModel JsonContent = await response.Content.ReadAsAsync<JokesModel>();
                    return JsonContent;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<IEnumerable<RandomFact>> GetRandomFactHttpRequest(int amount = 1)
        {
            using (var client = _httpClientFactory.CreateClient("RandomFactsAPI"))
            {
                client.DefaultRequestHeaders.Add("X-Api-Key", "VvaTgcE51/bBkBpmpCXyEw==Sish2NGLVuQIMI3y");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"{_appSettings.Value.BaseURLRandomFacts}limit={amount}").Result;

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<RandomFact> JsonContent = await response.Content.ReadAsAsync<IEnumerable<RandomFact>>();
                    return JsonContent;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<Uri> GetComicHttpRequest(int comicNumber = 0)
        {
            string url = _appSettings.Value.BaseURLComics;



            using (var client = _httpClientFactory.CreateClient("ComicsAPI"))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();
                    Uri uriSource = new Uri(comic.Img, UriKind.Absolute);
                    return uriSource;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

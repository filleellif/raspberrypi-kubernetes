using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Scraper
{
    public class FetchCountriesService : BackgroundService
    {
        private const string CountriesUrl = "https://restcountries.eu/rest/v2/all";
        private readonly ILogger<FetchCountriesService> _logger;
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _dbContext;

        public FetchCountriesService(ILogger<FetchCountriesService> logger, 
            IHttpClientFactory httpClientFactory, 
            ApplicationDbContext dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientFactory?.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // only fetch if it's been a while
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching countries at: {time}", DateTimeOffset.Now);
                var request = new HttpRequestMessage(HttpMethod.Get, CountriesUrl);
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();

                    var countries = await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(responseStream);

                    _logger.LogInformation("Fetched {number} countries. Saving to database.", countries.Count());

                    await _dbContext.Countries.AddRangeAsync(countries);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Done saving. Waiting");
                    await Task.Delay(100000);
                }

                await Task.Delay(100000);

                // var countries = await _dbContext.Countries.ToListAsync();
                // foreach (var country in countries)
                // {
                //     System.Console.WriteLine(country.Name);
                // }
            }
        }
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private const int Delay = 1000 * 60 * 60;

        private List<Region> _regions = new List<Region>();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                
                _logger.LogInformation("Worker running at: {time}. Will run again at {time}", currentTime, currentTime.AddMilliseconds(Delay));
                
                await Task.Delay(Delay, stoppingToken);
            }
        }
    }
}

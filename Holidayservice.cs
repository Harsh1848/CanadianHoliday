using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CanadianHoliday
{
    public interface IHolidayService
    {
        Task<List<Holiday>> GetHolidaysAsync(int year, string province);
    }

    public class Holidayservice : IHolidayService
    {
        private const string ApiBaseUrl = "https://canada-holidays.ca/api/v1/holidays";
        private HttpClient _httpClient;

        public Holidayservice()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Holiday>> GetHolidaysAsync(int year, string province)
        {
            var apiUrl = $"{ApiBaseUrl}?year={year}&optional=false";
            if (!string.IsNullOrEmpty(province))
            {
                apiUrl += $"&province={province}";
            }

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var holidays = await response.Content.ReadFromJsonAsync<List<Holiday>>();
            return holidays;
        }
    }
}

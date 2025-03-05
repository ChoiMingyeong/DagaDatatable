using DagaCommon.Models;
using System.Net.Http.Json;

namespace DagaDatatable.Services
{
    public class DBService(HttpClient httpClient)
    {
        private HttpClient _httpClient = httpClient;

        public async Task<AccountInfo?> SigninAsync(SigninInfo info)
        {
            var response = await _httpClient.PostAsJsonAsync("/signin", info);
            if (response.IsSuccessStatusCode)
            {
                var account = await response.Content.ReadFromJsonAsync<AccountInfo>();
                return account;
            }

            return null;
        }
    }
}

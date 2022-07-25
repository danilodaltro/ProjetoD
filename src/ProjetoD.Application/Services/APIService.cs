using System.Net;

namespace ProjetoD.Application.Services
{
    public static class APIService
    {
        private static readonly HttpClientHandler _clientHandler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; },
            AllowAutoRedirect = true
        };

        private static readonly HttpClient _httpClient = new HttpClient(_clientHandler) { Timeout = TimeSpan.FromSeconds(100) };

        public static async Task<string> ExecuteGet(string uri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            string responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK && string.IsNullOrEmpty(responseString))
                throw new Exception(String.Format("{0} - {1}", response.StatusCode, response.ReasonPhrase));

            return responseString;
        }
    }
}
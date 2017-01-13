namespace http_client_sample
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var authUrl = config["authUrl"];            // the url to the authorization token service
            var authScopes = config["authScopes"];      // a list of required scopes will be provided
            var clientId = config["clientId"];          // the id is assigned to individual clients wishing to access the service
            var clientSecret = config["clientSecret"];  // the secret is never be embedded in code, but securely stored on the client machine with access restricted to the application only
            var url = config["testUrl"];

            Console.WriteLine($"Calling authorization service at {authUrl}");

            var authHelper = new AuthHelper(authUrl);
            var token = authHelper.GetAuthToken(clientId, clientSecret, authScopes);

            Console.WriteLine($"Authorization token received.  Token expires in {token.ExpiresIn} seconds.  Sending test request");
            
            using (var client = new HttpClient())
            {
                var content = new StringContent($"Test request data");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                var result = client.PostAsync(url, content).Result;

                var resultMessage = result.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Finished call to test service.  Response...{Environment.NewLine}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(resultMessage);
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}

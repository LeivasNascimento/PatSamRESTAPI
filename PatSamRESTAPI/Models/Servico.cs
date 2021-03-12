using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PatSamRESTAPI.Models
{
    public class Servico
    {
        private static string _urlBase;
        public Servico()
        {

        }

        //retorna o campo AcessToken
        public string ObterTokenServico()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json");
                var config = builder.Build();

                _urlBase = config.GetSection("API_Access:UrlBase").Value;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage respToken = client.PostAsync(
                        _urlBase + "login",

                        new StringContent(
                            JsonConvert.SerializeObject(new
                            {
                                UserID = config.GetSection("API_Access:UserID").Value,
                                AccessKey = config.GetSection("API_Access:AccessKey").Value
                            }), Encoding.UTF8, "application/json")

                        ).Result;

                    string conteudo =
                        respToken.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(conteudo);

                    if (respToken.StatusCode == HttpStatusCode.OK)
                    {
                        Token token = JsonConvert.DeserializeObject<Token>(conteudo);
                        if (token.Authenticated)
                        {
                            return token.AccessToken;

                        }
                        else
                            return string.Empty;
                    }

                }

            }
            catch (Exception ex)
            {
                return string.Empty;
            }

            return string.Empty;
        }
    }
}

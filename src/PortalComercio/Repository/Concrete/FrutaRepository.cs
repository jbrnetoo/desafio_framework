using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PortalComercio.Models;
using PortalComercio.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PortalComercio.Repository.Concrete
{
    public class FrutaRepository : IFrutaRepository
    {
        private readonly IConfiguration _configuration;
        public FrutaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DtoFruta ObterPorId(Guid id)
        {
            using (var httpClient = ObterHttpClient())
            {
                var response = httpClient.GetAsync($"/Fruta/{id}").Result;

                var strResponse = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var dtoFruta = JsonConvert.DeserializeObject<DtoFruta>(strResponse);

                    return dtoFruta;
                }

                return null;
            }
        }

        public IQueryable<DtoFruta> ObterTodos()
        {
            using (var httpClient = ObterHttpClient())
            {
                var response = httpClient.GetAsync("/Fruta").Result;

                var strResponse = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var listDtoFruta = JsonConvert.DeserializeObject<List<DtoFruta>>(strResponse);

                    return listDtoFruta.AsQueryable();
                }

                return new List<DtoFruta>().AsQueryable();
            }
        }

        public bool AtualizarEstoque(DtoAtualizarEstoque dtoAtualizarEstoque)
        {
            using (var httpClient = ObterHttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(dtoAtualizarEstoque);
                var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = httpClient.PutAsync($"/Fruta/Estoque", stringContent).Result;

                return response.IsSuccessStatusCode;
            }
        } 

        public bool AtualizarFruta(DtoFruta fruta)
        {
            using (var httpClient = ObterHttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(fruta);
                var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = httpClient.PutAsync($"/Fruta", stringContent).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public bool InserirFruta(DtoFruta fruta)
        {
            using (var httpClient = ObterHttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(fruta);
                var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync($"/Fruta", stringContent).Result;

                return response.IsSuccessStatusCode;
            }
        }

        public bool RemoverFruta(Guid id)
        {
            using (var httpClient = ObterHttpClient())
            {
                var response = httpClient.DeleteAsync($"/Fruta/{id}").Result;

                return response.IsSuccessStatusCode;
            }
        }

        #region ObterHttpClient
        public HttpClient ObterHttpClient()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(_configuration.GetSection("Api.Compra").Value),
                Timeout = new TimeSpan(0, 0, 30)
            };

            var usuario = _configuration.GetSection("Usuario").Value;
            var senha = _configuration.GetSection("Senha").Value;

            var byteArray = Encoding.ASCII.GetBytes($"{usuario}:{senha}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            return httpClient;
        }
        #endregion
    }
}

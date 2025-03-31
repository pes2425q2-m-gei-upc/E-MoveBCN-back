using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Services.Interface;
using System.Collections.Generic;
using System;

namespace Services
{
    public class TmbService : ITmbService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly string _apiKey;
        private readonly string _appId;
        private readonly string _baseUrl;

        public TmbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            _apiKey = _configuration["TmbApi:ApiKey"];
            _appId = _configuration["TmbApi:AppId"];
            _baseUrl = _configuration["TmbApi:BaseUrl"];

            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_appId) || string.IsNullOrEmpty(_baseUrl))
            {
                throw new Exception("API Key, App ID o Base URL no están configurados en appsettings.json");
            }
        }

        public async Task<List<MetroDto>> GetAllMetrosAsync()
        {
            var metrosEndpoint = _configuration["TmbApi:Metros"];
            var requestUrl = $"{_baseUrl}{metrosEndpoint}?app_id={_appId}&app_key={_apiKey}";

            var metrosResponse = await _httpClient.GetAsync(requestUrl);
            
            if (!metrosResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error en la API: {metrosResponse.StatusCode}");
                return null;
            }

            var metrosContent = await metrosResponse.Content.ReadAsStringAsync();
            
            if (string.IsNullOrWhiteSpace(metrosContent))
            {
                Console.WriteLine("Respuesta vacía de la API de metros.");
                return new List<MetroDto>();
            }

            var jsonObject = JObject.Parse(metrosContent);
            if (!jsonObject.ContainsKey("features"))
            {
                Console.WriteLine("La respuesta no contiene 'features'.");
                return new List<MetroDto>();
            }
            
            var metroStationsJson = jsonObject["features"] as JArray;
            if (metroStationsJson == null || !metroStationsJson.Any())
            {
                Console.WriteLine("No se encontraron datos en la API de metros.");
                return new List<MetroDto>();
            }

            List<MetroDto> metroList = new List<MetroDto>();

            foreach (var metro in metroStationsJson)
            {
                var properties = metro["properties"];
                var geometry = metro["geometry"];

                MetroDto metroStation = new MetroDto
                {
                    IdEstacio = properties?["ID_ESTACIO"]?.Value<int>() ?? 0,
                    CodiEstacio = properties?["CODI_ESTACIO"]?.Value<int>() ?? 0,
                    NomEstacio = properties?["NOM_ESTACIO"]?.Value<string>() ?? "Desconocido",
                    Latitude = geometry?["coordinates"]?[1]?.Value<double>() ?? 0.0,
                    Longitude = geometry?["coordinates"]?[0]?.Value<double>() ?? 0.0
                };

                metroList.Add(metroStation);
            }

            return metroList;
        }

        public async Task<List<BusDto>> GetAllBusAsync()
        {
            var busEndpoint = _configuration["TmbApi:Bus"];
            var requestUrl = $"{_baseUrl}{busEndpoint}?app_id={_appId}&app_key={_apiKey}";

            var busResponse = await _httpClient.GetAsync(requestUrl);
            
            if (!busResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error en la API: {busResponse.StatusCode}");
                return null;
            }

            var busContent = await busResponse.Content.ReadAsStringAsync();
            
            var busStopsJson = JObject.Parse(busContent)["features"] as JArray;
            if (busStopsJson == null)
            {
                Console.WriteLine("No se encontraron datos en la API de buses.");
                return new List<BusDto>();
            }

            List<BusDto> busList = new List<BusDto>();

            foreach (var busStop in busStopsJson)
            {
                var properties = busStop["properties"];
                var geometry = busStop["geometry"];

                BusDto busStation = new BusDto
                {
                    ParadaId = properties?["ID_PARADA"]?.Value<int>() ?? 0,
                    CodiParada = properties?["CODI_PARADA"]?.Value<int>() ?? 0,
                    Name = properties?["NOM_PARADA"]?.Value<string>() ?? "Desconocido",
                    Latitude = geometry?["coordinates"]?[1]?.Value<double>() ?? 0.0,
                    Longitude = geometry?["coordinates"]?[0]?.Value<double>() ?? 0.0
                };

                busList.Add(busStation);
            }

            return busList;
        }
    }
}

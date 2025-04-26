using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Services.Interface;
using System.Collections.Generic;
using System;

namespace Services;
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
                IdEstacioLinia = properties?["ID_ESTACIO_LINIA"]?.Value<int>() ?? 0,
                CodiEstacioLinia = properties?["CODI_ESTACIO_LINIA"]?.Value<int>() ?? 0,
                IdGrupEstacio = properties?["ID_GRUP_ESTACIO"]?.Value<int>() ?? 0,
                CodiGrupEstacio = properties?["CODI_GRUP_ESTACIO"]?.Value<int>() ?? 0,
                IdEstacio = properties?["ID_ESTACIO"]?.Value<int>() ?? 0,
                CodiEstacio = properties?["CODI_ESTACIO"]?.Value<int>() ?? 0,
                NomEstacio = properties?["NOM_ESTACIO"]?.Value<string>() ?? "Desconocido",
                OrdreEstacio = properties?["ORDRE_ESTACIO"]?.Value<int>() ?? 0,
                IdLinia = properties?["ID_LINIA"]?.Value<int>() ?? 0,
                CodiLinia = properties?["CODI_LINIA"]?.Value<int>() ?? 0,
                NomLinia = properties?["NOM_LINIA"]?.Value<string>() ?? "Desconocido",
                DescServei = properties?["DESC_SERVEI"]?.Value<string>() ?? "Desconocido",
                OrigenServei = properties?["ORIGEN_SERVEI"]?.Value<string>() ?? "Desconocido",
                DestiServei = properties?["DESTI_SERVEI"]?.Value<string>() ?? "Desconocido",
                ColorLinia = properties?["COLOR_LINIA"]?.Value<string>() ?? "Desconocido",
                Picto = properties?["PICTO"]?.Value<string>() ?? "Desconocido",
                PictoGrup = properties?["PICTO_GRUP"]?.Value<string>() ?? "Desconocido",
                DataInauguracio = properties?["DATA_INAUGURACIO"]?.Value<string>() ?? "Desconocido",
                Data = properties?["DATA"]?.Value<string>() ?? "Desconocido",
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
                Description = properties?["DESC_PARADA"]?.Value<string>() ?? "Desconocido",
                IntersectionId = properties?["CODI_INTERC"]?.Value<int>() ?? 0,
                IntersectionName = properties?["NOM_INTERC"]?.Value<string>() ?? "Desconocido",
                ParadaTypeName = properties?["NOM_TIPUS_PARADA"]?.Value<string>() ?? "Desconocido",
                ParadaTypeTipification = properties?["TIPIFICACIO_PARADA"]?.Value<string>() ?? "Desconocido",
                Adress = properties?["ADRECA"]?.Value<string>() ?? "Desconocida",
                LocationId = properties?["ID_POBLACIO"]?.Value<string>() ?? "Desconocido",
                LocationName = properties?["NOM_POBLACIO"]?.Value<string>() ?? "Desconocido",
                DistrictId = properties?["ID_DISTRICTE"]?.Value<string>() ?? "Desconocido",
                DistrictName = properties?["NOM_DISTRICTE"]?.Value<string>() ?? "Desconocido",
                Date = properties?["DATA"]?.Value<string>() ?? "Desconocida",
                StreetName = properties?["NOM_VIA"]?.Value<string>() ?? "Desconocida",
                ParadaPoints = properties?["PUNTS_PARADA"]?.Value<string>() ?? "Desconocido",
                Latitude = geometry?["coordinates"]?[1]?.Value<double>() ?? 0.0,
                Longitude = geometry?["coordinates"]?[0]?.Value<double>() ?? 0.0
            };

            busList.Add(busStation);
        }

        return busList;
    }
    public async Task<MetroDto?> GetMetroByIdAsync(int id)
    {
        var allMetros = await GetAllMetrosAsync();
        return allMetros?.FirstOrDefault(m => m.IdEstacio == id);
    }
    public async Task<BusDto?> GetBusByIdAsync(int id)
    {
        var allBuses = await GetAllBusAsync();
        return allBuses?.FirstOrDefault(b => b.ParadaId == id);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Services.Interface;

namespace Services;

    public class TmbService : ITmbService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TmbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<MetroDto>> GetAllMetrosAsync()
        {
            var baseUrl = _configuration["TmbApi:BaseUrl"];
            var metrosEndpoint = _configuration["TmbApi:Metros"];

            var metrosResponse = await _httpClient.GetAsync(baseUrl + metrosEndpoint);

            if (!metrosResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var metrosContent = await metrosResponse.Content.ReadAsStringAsync();
            var metroStationsJson = JObject.Parse(metrosContent)["features"] as JArray;

            List<MetroDto> metroList = new List<MetroDto>();

            foreach (var metro in metroStationsJson)
            {
                var properties = metro["properties"];
                int codiLinia = (int)properties["CODI_GRUP_ESTACIO"];

                var estacionesEndpoint = _configuration["TmbApi:MetrosLineas"].Replace("{codi_linia}", codiLinia.ToString());
                var estacionesResponse = await _httpClient.GetAsync(baseUrl + estacionesEndpoint);

                if (!estacionesResponse.IsSuccessStatusCode)
                {
                    continue;
                }

                var estacionesContent = await estacionesResponse.Content.ReadAsStringAsync();
                var estacionesJson = JObject.Parse(estacionesContent)["features"] as JArray;

                foreach (var item in estacionesJson)
                {
                    var estProperties = item["properties"];
                    var geometry = item["geometry"];

                    MetroDto metroStation = new MetroDto
                    {
                        IdEstacioLinia = (int)estProperties["ID_ESTACIO_LINIA"],
                        CodiEstacioLinia = (int)estProperties["CODI_ESTACIO_LINIA"],
                        IdGrupEstacio = (int)estProperties["ID_GRUP_ESTACIO"],
                        CodiGrupEstacio = (int)estProperties["CODI_GRUP_ESTACIO"],
                        IdEstacio = (int)estProperties["ID_ESTACIO"],
                        CodiEstacio = (int)estProperties["CODI_ESTACIO"],
                        NomEstacio = (string)estProperties["NOM_ESTACIO"],
                        OrdreEstacio = (int)estProperties["ORDRE_ESTACIO"],
                        IdLinia = (int)estProperties["ID_LINIA"],
                        CodiLinia = (int)estProperties["CODI_LINIA"],
                        NomLinia = (string)estProperties["NOM_LINIA"],
                        DescServei = (string)estProperties["DESC_SERVEI"],
                        OrigenServei = (string)estProperties["ORIGEN_SERVEI"],
                        DestiServei = (string)estProperties["DESTI_SERVEI"],
                        ColorLinia = (string)estProperties["COLOR_LINIA"],
                        Picto = (string)estProperties["PICTO"],
                        PictoGrup = (string)estProperties["PICTO_GRUP"],
                        DataInauguracio = (string)estProperties["DATA_INAUGURACIO"],
                        Data = (string)estProperties["DATA"],
                        Latitude = (double)geometry["coordinates"][1],
                        Longitude = (double)geometry["coordinates"][0]
                    };

                    metroList.Add(metroStation);
                }
            }

            return metroList;
        }
    }

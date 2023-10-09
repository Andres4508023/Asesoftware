using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Domain;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsumeService
{
    public class TurnoConsume : ITurnoConsume
    {
        private readonly AppSetting _appSetting;
        public TurnoConsume(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task<List<Servicios>> GetServicios()
        {
            var List = new List<Servicios>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSetting.WebAPILink);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("/api/Turnos/ConsultarServicios");

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    List = JsonConvert.DeserializeObject<List<Servicios>>(readTask);
                    return List;
                }
            }
            return List;
        }

        public async Task<List<TurnoInfo>> GetTurnos()
        {
            var List = new List<TurnoInfo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSetting.WebAPILink);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("/api/Turnos/ConsultarTurnos");

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    List = JsonConvert.DeserializeObject<List<TurnoInfo>>(readTask);
                    return List;
                }
            }
            return List;
        }

        public async Task<bool> SaveTurnos(Turnos model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSetting.WebAPILink);
                client.DefaultRequestHeaders.Clear();
                string jsonData = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/Turnos/GenerarTurnos", content);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<bool>(readTask);

                    return result;
                }
            }
            return false;
        }
    }
}

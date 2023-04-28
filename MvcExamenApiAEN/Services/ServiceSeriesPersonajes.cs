using MvcExamenApiAEN.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcExamenApiAEN.Services
{
    public class ServiceSeriesPersonajes
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceSeriesPersonajes(IConfiguration configuration)
        {
            this.UrlApi =
                configuration.GetValue<string>("ApiUrls:ApiSeriesPersonajes");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response =
                    await client.GetAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        #region GETS

        public async Task<List<Serie>> GetSeries()
        {
            string request = "/api/series";
            return await this.CallApiAsync<List<Serie>>(request);
        }

        public async Task<List<Personaje>> GetPersonajesSerie(int idSerie)
        {
            string request = "/api/personajes/getpersonajesseries/"  + idSerie;
            return await this.CallApiAsync<List<Personaje>>(request);
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "/api/personajes/" + idpersonaje;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }


        #endregion

        #region POST
        public async Task InsertPersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);


                string json = JsonConvert.SerializeObject(personaje);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        #endregion

        #region PUT
        public async Task UpdatePersonajeSerieAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/UpdateSerie";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }
        #endregion

        #region DELETE
        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + idpersonaje;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
        #endregion
    }
}

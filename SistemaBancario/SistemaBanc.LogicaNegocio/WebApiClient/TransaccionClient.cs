using Newtonsoft.Json;
using SistemaBanc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBanc.LogicaNegocio.WebApiClient
{
    public class TransaccionClient
    {
        private const string POST_URI_CREAR_TRANSACCION = "/api/tramites/crearTran";
        private const string GET_URI_TRANSACCIONS_CUENTA = "/api/Transaccions/Cuenta";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;
        public TransaccionClient(string addressURI)
        {
            _addressURI = addressURI;
        }
        public async Task<bool> PostCrearTransaccion(EntityTransaccion entity)
        {
            bool opRealizada = false;
            string mensaje = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.PostAsync(POST_URI_CREAR_TRANSACCION,entity,new JsonMediaTypeFormatter());
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        opRealizada = await response.Content.ReadAsAsync<bool>();
                        break;
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return opRealizada;
        }

        public async Task<List<EntityTransaccion>> GetTransaccionesByCtaAync(int idCuenta)
        {
            string mensaje = string.Empty;
            List<EntityTransaccion> listEntityTrans = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_TRANSACCIONS_CUENTA}/{idCuenta}");
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        listEntityTrans = await response.Content.ReadAsAsync<List<EntityTransaccion>>();
                        break;
                    case HttpStatusCode.NotFound:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data.Message;
                        throw new HttpRequestException(mensaje);
                }
            }

            return listEntityTrans;
        }
    }
}

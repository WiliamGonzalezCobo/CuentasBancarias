using Newtonsoft.Json;
using SistemaBanc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBanc.LogicaNegocio.WebApiClient
{
    public class CuentaClient
    {
        private const string GET_URI_CUENTAS_CLIENTE = "api/Cuentas/Cliente";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        public CuentaClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        public async Task<List<EntityCuenta>> GetCuentasByClienteAsync(int idCliente)
        {
            string mensaje = string.Empty;
            List<EntityCuenta> cuentasCliente = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.GetAsync($"{GET_URI_CUENTAS_CLIENTE}/{idCliente}");
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        cuentasCliente = await response.Content.ReadAsAsync<List<EntityCuenta>>();
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

            return cuentasCliente;
        }
    }
}

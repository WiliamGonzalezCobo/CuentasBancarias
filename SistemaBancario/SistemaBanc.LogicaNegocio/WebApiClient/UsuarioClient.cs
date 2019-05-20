using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaBanc.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Net;
using Newtonsoft.Json;

namespace SistemaBanc.LogicaNegocio.WebApiClient
{
    public class UsuarioClient
    {
        private const string POST_URI_VALIDAR = "/api/Usuarios/validar";

        private const string MEDIA_TYPE_JSON = "application/json";
        private readonly string _addressURI;

        public UsuarioClient(string addressURI)
        {
            _addressURI = addressURI;
        }

        public async Task<EntityUsuario> ValidarAccesoAsync( EntityUsuario entity)
        {
            EntityUsuario usuario = null;
            string mensaje = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_addressURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));

                var response = await client.PostAsync(POST_URI_VALIDAR, entity, new JsonMediaTypeFormatter());
                dynamic data;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        usuario = response.Content.ReadAsAsync<EntityUsuario>().Result;
                        break;
                    case HttpStatusCode.InternalServerError:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;
                        throw new NullReferenceException(mensaje);
                    case HttpStatusCode.BadRequest:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;
                        throw new HttpRequestException(mensaje);
                    case HttpStatusCode.Unauthorized:
                        data = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        mensaje = data;
                        throw new MemberAccessException(mensaje);
                }
            }

            return usuario;
        }

    }
}

using Canela.Service.UserMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace Canela.Service.UserMgmt.Controllers
{
    [ApiController]
    [Route("user")]
    public class CreateController : Controller
    {
        private static HttpClient _client;

        public CreateController()
        {
            if (_client == null)
            {
                _client = new HttpClient();

            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> CreateAccount(User usuario)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:3001/graphql?query=mutation{ createUser( document: \"" + usuario.Documento.ToString() + "\" document_type: " + usuario.tipo_documento.ToString() + " name: \"" + usuario.nombres.ToString() + "\" last_name: \"" + usuario.apellidos.ToString() + "\" birth_date: \"" + usuario.fecha_nacimiento.ToString() + "\" address: \"" + usuario.direccion.ToString() + "\" phone_number: \"" + usuario.telefono.ToString() + "\" email: \"" + usuario.correo.ToString() + "\" ) { document document_type name last_name birth_date address phone_number email }}");
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(_client.BaseAddress, content);

            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.Content.ReadAsStringAsync();

                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }

            if (httpResponse.StatusCode.Equals(500))
            {
                result = await httpResponse.Content.ReadAsStringAsync();

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }
    }
}

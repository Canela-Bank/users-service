using Canela.Service.UserMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using System.Security.Principal;

namespace Canela.Service.UserMgmt.Controllers
{
    
    [ApiController]
    [Route("user")]
    public class DeleteController : ControllerBase
    {


        string url = null;
        private static HttpClient _client;
        
        public DeleteController()
        {
            if (_client == null)
            {
                _client = new HttpClient();
 
            }
        }

        //peticion delete para borrar: user/121315
        [HttpDelete]
        [Route("delete/{document}")]
        public async Task<HttpResponseMessage> DeleteAccount(string document)
        {
            //llamado al integrador de datos para buscar usuario a eliminar
            //TODO verificar conexion intregrador de datos. 
            _client.BaseAddress = new Uri("http://localhost:4000/graphql?query=mutation{deleteAccount(ac1:\"" + document + "\"){dp}");

            var httpResponse = await _client.DeleteAsync(_client.BaseAddress);

            //var result = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
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
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
        [Route("delete-user/{doctype}/{document}")]
        public async Task<IActionResult> DeleteAccount(string docType, string document)
        {
            //llamado al integrador de datos para buscar usuario a eliminar
            //TODO verificar conexion intregrador de datos. 
            
            
            _client.BaseAddress = new Uri("http://localhost:3001/graphql?query=mutation%7B%0A%20%20deleteUser(document%3A%22"+ document +"%22%2C%20document_type%3A%20" + docType +")%7B%0A%20%20%20%20message%2C%0A%20%20%20%20data%7B%0A%20%20%20%20%20%20document%2C%0A%20%20%20%20%20%20document_type%2C%20%0A%20%20%20%20%20%20name%2C%0A%20%20%20%20%20%20last_name%2C%0A%20%20%20%20%20%20birth_date%2C%0A%20%20%20%20%20%20phone_number%2C%0A%20%20%20%20%20%20email%0A%20%20%20%20%7D%0A%20%20%7D%0A%0A%7D");

            
            

            var httpResponse = await _client.DeleteAsync(_client.BaseAddress);

            //var result = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.Content.ReadAsStringAsync();

                return StatusCode(StatusCodes.Status202Accepted, "Eliminado");
            }

            if (httpResponse.IsSuccessStatusCode != true)
            {
                result = await httpResponse.Content.ReadAsStringAsync();

                

                return StatusCode(StatusCodes.Status500InternalServerError, "Error eliminando cuenta");

            }


            return StatusCode(StatusCodes.Status404NotFound, "Cuenta no encontrada");

        }
    }
}
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
            _client.BaseAddress = new Uri("http://localhost:4000/graphql?query=mutation{deleteUser(document:\"" + document + "\",document_type:" + docType + "){message}");

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
using Canela.Service.UserMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
        [Route("Delete")]
        public async Task<String> DeleteAccount(User usuario)
        {
            //llamado al integrador de datos para buscar usuario a eliminar
            //TODO 
            _client.BaseAddress = new Uri("http://localhost:4000/graphql?query=mutation{deleteAccount(ac1:\"" + usuario + "\"){id}");

            var httpResponse = await _client.DeleteAsync(_client.BaseAddress);

            var result = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
            {
                 result = await httpResponse.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Canela.Service.UserMgmt.Models;
using Microsoft.VisualBasic;
using System.Security.Principal;
using System.Net.Http;
using Api.Graphql;

namespace Canela.Service.UserMgmt.Controllers
{
    [ApiController]
    [Route("user")]
    public class UpdateController : Controller
    {
        string url = null;
        private static HttpClient _client;

        public UpdateController()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                
            }
        }


        

        [HttpPut]
        [Route("update/{document}")]
        [ProducesResponseType(200)]

        public async Task<string> Post(string document, string docType)
        {
            _client.BaseAddress = new Uri("http://localhost:4000/graphql?query=mutation{deleteUser(document:\"" + document + "\",document_type:" + docType + "){message}");

            HttpContent content = null;

            var httpResponse = await _client.PutAsync(_client.BaseAddress,content);

            //TODO Send http put to data integrator.

            //TODO Build body to send to data integrator.

            return document;
        }

        

    }
}

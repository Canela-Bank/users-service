using Canela.Service.UserMgmt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Canela.Service.UserMgmt.Controllers
{
    [ApiController]
    [Route("user")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public dynamic CrearCuenta(User usuario)
        {
            //llamado al integrador de datos para solicitar la creacion del usuario
            return new
            {
                status = 201,
                message= "User create",
                result = usuario
            };
        }
    }
}

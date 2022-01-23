using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midia_Indoo.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Midia_Indoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAL _usuarioDAL;
        public UsuarioController()
        {
            _usuarioDAL = new UsuarioDAL();
        }

        [HttpGet]
        public IActionResult Get(string email, string senha)
        {
            var user = _usuarioDAL.Get(email, senha);
          //404 ?

            return user != null ? Ok(user) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }
    }
}

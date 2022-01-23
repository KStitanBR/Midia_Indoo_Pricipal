using Domain.Models;
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
    public class MensagemController : Controller
    {
        public readonly MensagemDAL _MensagemDAL;
        public MensagemController()
        {
            _MensagemDAL = new MensagemDAL();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPlayers = this._MensagemDAL.GetAll();

            return allPlayers != null ? Ok(allPlayers) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByCodigo(int id)
        {
            var allPlayers = this._MensagemDAL.GetAllByCodigo(id);

            return allPlayers != null ? Ok(allPlayers) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Mensagem _msg)
        {
            if (_MensagemDAL.RecordPlayer(_msg) == "OK")
                return this.StatusCode(StatusCodes.Status200OK);
            else
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Houve problemas internos.");
        }
    }
}

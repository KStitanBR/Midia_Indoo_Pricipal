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
    public class PlayerController : Controller
    {
        public readonly PlayerDAL _playerDAL;
        public PlayerController()
        {
            _playerDAL = new PlayerDAL();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPlayers = this._playerDAL.GetAll();

            return allPlayers != null ? Ok(allPlayers) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByCodigo(int id)
        {
            var allPlayers = this._playerDAL.GetAllByCodigo(id);

            return allPlayers != null ? Ok(allPlayers) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Player _Player)
        {
            if (_playerDAL.RecordPlayer(_Player) == "OK")
                return this.StatusCode(StatusCodes.Status200OK);
            else
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Houve problemas internos.");
        }
    }
}

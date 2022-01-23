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
    public class VideoController : Controller
    {
        private readonly VideoDAL _videoDAL;
        public VideoController()
        {
            _videoDAL = new VideoDAL();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allvideos = this._videoDAL.GetAll();

            return allvideos != null ? Ok(allvideos) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpGet("{id}")]
        public IActionResult GetAllByCodigo(int id)
        {
            var allVideos = this._videoDAL.GetAllByCodigo(id);

            return allVideos != null ? Ok(allVideos) : this.StatusCode(StatusCodes.Status404NotFound, "Registro(s) não encontrado(s).");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Videos _video )
        {
            if (_videoDAL.RecordVideo(_video) == "OK")
                return this.StatusCode(StatusCodes.Status200OK);
            else
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Houve problemas internos.");
        }
    }
}

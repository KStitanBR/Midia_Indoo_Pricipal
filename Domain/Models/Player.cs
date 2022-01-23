using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Player
    {
        public int Codigo { get; set; }
        public int UsuarioID { get; set; }
        public string Nome { get; set; }
        public Guid PlayerGuid { get; set; }

        public Player()
        {

        }


    }
}

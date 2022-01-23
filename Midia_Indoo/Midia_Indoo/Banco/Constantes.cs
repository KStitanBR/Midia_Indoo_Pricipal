using Midia_Indoo.Helps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Midia_Indoo.Banco
{
    public static class Constantes
    {
        public const string NomeDoArquivo = "MediaIndoo.db3";

        public static string CaminhoDoBanco
        {
            get
            {
                var caminhoBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                return Path.Combine(caminhoBase, NomeDoArquivo);
            }
        }
    }
}

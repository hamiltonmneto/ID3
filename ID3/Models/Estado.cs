using System.Collections.Generic;

namespace ID3.Models
{
    public class Estado
    {
        public Estado()
        {
            Ramo = new List<No> { };
            Particao = new List<DadosInputModel> { };
        }
        public List<DadosInputModel> Particao { get; set; }
        public List<No> Ramo { get; set; }
        public No Arvore { get; set; }
    }
}

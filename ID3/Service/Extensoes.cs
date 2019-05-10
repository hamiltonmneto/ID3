using ID3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID3.Service
{
    public class Extensoes
    {
        public IList<string> GerarPropriedades(IList<DadosInputModel> dados)
        {
            var propriedades = new List<string>();
            var props = dados.First().GetType().GetProperties();
            foreach (var p in props)
            {
                string name = p.Name;
                propriedades.Add(name);
            }
            return propriedades;
        }
    }
}

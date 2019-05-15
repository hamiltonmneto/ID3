using ID3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ID3.Service
{
    public class Extensoes
    {
        public IList<string> GerarPropriedades(IList<DadosInputModel> dados, string criterio)
        {
            var propriedades = new List<string>();
            var props = dados.First().GetType().GetProperties();
            foreach (var p in props)
            {
                string name = p.Name;
                if(name != "Id" && name != criterio)
                    propriedades.Add(name);
            }
            return propriedades;
        }

        public List<string> GetValoresPropriedades(List<DadosInputModel> dados, string propriedadeSelecionada)
        {
            var valores = new List<string>();

            foreach (var d in dados)
            {
                var value = d.GetType().GetProperty(propriedadeSelecionada).GetValue(d, null);
                valores.Add(value.ToString());
            }
            return valores.Distinct().ToList();
        }
    }
}

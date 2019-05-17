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

        public List<DadosInputModel> GetValoresParticao(string ValorDaPropriedade, List<DadosInputModel> dados)
        {
            var ValoresParticao = new List<DadosInputModel>();
            var props = dados.First().GetType().GetProperties();

            foreach (var d in props)
            {
                var ValoresDaLista = dados.Where(x => d.GetValue(x).Equals(ValorDaPropriedade)).ToList();
                if (ValoresDaLista.Any())
                    ValoresParticao = ValoresDaLista;
            }

            return ValoresParticao;
        }
    }
}

using ID3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID3.Service
{
    public class Processar
    {
        public No InduzirArvore(List<DadosInputModel> dados, IList<string> propriedades, string Classe)
        {

            var ArvoreDeDecisao = new No();
            if (dados.Select(x => x.Risco).Distinct().Count() == 1)
                return new No { Classe = dados.FirstOrDefault().Risco };
            if (!propriedades.Any())
                return new No {Classe = string.Join(" OU ", dados.Select(x => x.Risco).Distinct()) };
            var propriedadeSelecionada = SelecionarPropriedadeERemoverDaListaDePropriedades(propriedades);

            ArvoreDeDecisao.Propriedade = propriedadeSelecionada;
            var valoresDaPropriedade = new Extensoes().GetValoresPropriedades(dados, propriedadeSelecionada);
            for (int i = 0; i < valoresDaPropriedade.Count(); i++)
            {
                if(i == 0)
                    ArvoreDeDecisao.Filhos = new List<No> { new No { Rotulo = valoresDaPropriedade[0] } };
                else
                    ArvoreDeDecisao.Filhos.Add(new No { Rotulo = valoresDaPropriedade[i] } );
            }

            return ArvoreDeDecisao;
        }

        private static string SelecionarPropriedadeERemoverDaListaDePropriedades(IList<string> propriedades)
        {
            var random = new Random();
            int index = random.Next(propriedades.Count);
            var propriedadeEscolhida = propriedades.ElementAt(index);
            propriedades.Remove(propriedadeEscolhida);
            return propriedadeEscolhida;
        }
    }
}

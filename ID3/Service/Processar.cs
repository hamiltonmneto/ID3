using ID3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ID3.Service
{
    public class Processar
    {
        public No ArvoreDeDecisao = new No();
        public Arvore<No> arvore = new Arvore<No>();
        public No InduzirArvore( List<DadosInputModel> dados,  IList<string> propriedades, string Classe)
        {
            if (dados.Select(x => x.Risco).Distinct().Count() == 1)
                return new No { Classe = dados.FirstOrDefault().Risco };
            if (!propriedades.Any())
                return new No {Classe = string.Join(" OU ", dados.Select(x => x.Risco).Distinct()) };
            var PropriedadeSelecionada = SelecionarPropriedadeERemoverDaListaDePropriedades(propriedades);

            if (arvore.Root == null)
            {
                arvore.Root = new TreeNode<No>() { Data = new No {Propriedade = PropriedadeSelecionada} };
            }
            var ValoresDaPropriedade = new Extensoes().GetValoresPropriedades(dados, PropriedadeSelecionada);

            if (arvore.Root.Children == null)
                for (int i = 0; i < ValoresDaPropriedade.Count(); i++)
                {
                    arvore.Root.Children.Add(new TreeNode<No>() { Data = new No { Rotulo = ValoresDaPropriedade[i] }, Parent = arvore.Root });
                }


                var particao = CriarNovaParticao(ValoresDaPropriedade[i], dados);
                var classe = InduzirArvore(particao, propriedades, Classe);
                //var ramo = new No { Rotulo = ValoresDaPropriedade[i], Filhos = new List<No> { } };
                //ArvoreDeDecisao.Filhos.Add(ramo);
                //var particao = CriarNovaParticao(ValoresDaPropriedade[i],dados);
                //var filho = InduzirArvore( particao,  propriedades, Classe);

            return ArvoreDeDecisao;
        }

        private List<DadosInputModel> CriarNovaParticao(string Valor, List<DadosInputModel> dados)
            => new Extensoes().GetValoresParticao(Valor, dados);

        private static string SelecionarPropriedadeERemoverDaListaDePropriedades(IList<string> propriedades)
        {
            var random = new Random();
            int index = random.Next(propriedades.Count);
            //var propriedadeEscolhida = propriedades.ElementAt(index);
            var propriedadeEscolhida = propriedades.ElementAt(0);
            propriedades.Remove(propriedadeEscolhida);
            return propriedadeEscolhida;
        }
    }
}

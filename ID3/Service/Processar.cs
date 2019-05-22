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
        public List<Estado> ListaDeEstados = new List<Estado>();
        public No InduzirArvore( List<DadosInputModel> dados,  IList<string> propriedades, string Classe)
        {
            var ramo = new No { };
            if (dados.Select(x => x.Risco).Distinct().Count() == 1)
                return new No { Classe = dados.FirstOrDefault().Risco };
            if (!propriedades.Any())
                return new No {Classe = string.Join(" OU ", dados.Select(x => x.Risco).Distinct()) };
            var PropriedadeSelecionada = SelecionarPropriedadeERemoverDaListaDePropriedades(propriedades);

            if (string.IsNullOrEmpty(ArvoreDeDecisao.Propriedade))
            {
                ArvoreDeDecisao.Propriedade = PropriedadeSelecionada;
                ArvoreDeDecisao.Filhos = new List<No> { };
            }
            var ValoresDaPropriedade = new Extensoes().GetValoresPropriedades(dados, PropriedadeSelecionada);

            for (int i = 0; i < ValoresDaPropriedade.Count(); i++)
            {
                if(ListaDeEstados.Any())
                    if (ListaDeEstados.LastOrDefault().Ramo.Select(x => x.Rotulo).Any())
                        ramo = new No { Propriedade = PropriedadeSelecionada, Filhos = new List<No> { } };
                    else
                        ramo = new No { Rotulo = ValoresDaPropriedade[i], Filhos = new List<No> { } };
                else
                    ramo = new No { Rotulo = ValoresDaPropriedade[i], Filhos = new List<No> { } };
                var particao = CriarNovaParticao(ValoresDaPropriedade[i],dados);
                var estado = new Estado { Arvore = ArvoreDeDecisao, Particao = particao };
                estado.Ramo.Add(ramo);
                ListaDeEstados.Add(estado);
                var classe = InduzirArvore( particao,  propriedades, Classe);
                var ramoCompleto = CompletarRamo(classe);
                ArvoreDeDecisao.Filhos.Add(ramoCompleto);
                ListaDeEstados.Clear();
            }

            return ArvoreDeDecisao;
        }

        private No CompletarRamo(No classe)
        {
            for(int i = 0; i < ListaDeEstados.Count(); i++)
            {
                ListaDeEstados[i].Ramo.FirstOrDefault().Filhos.Add(ListaDeEstados[i + 1].Ramo.FirstOrDefault());
                ListaDeEstados.RemoveAt(i + 1);
            }
            var ramoCompleto = ListaDeEstados.FirstOrDefault().Ramo;
            ramoCompleto.Select(x => x.Filhos.Last().Filhos).FirstOrDefault().Add(classe);
            return ramoCompleto.FirstOrDefault();
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

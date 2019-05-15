using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ID3.Models;
using ID3.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ID3.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ProcessamentoController : ControllerBase
    {
        [HttpPost]
        [Route("processar")]
        public void ProcessarDados([FromBody]List<DadosInputModel> Dados)
        {
            var classe = "Risco";
            var Propriedades = new Extensoes().GerarPropriedades(Dados, classe);
            var Arvore = new Processar().InduzirArvore(Dados, Propriedades, classe);
            Ok(Arvore);
        }

    }
}
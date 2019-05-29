using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IntegracaoPjeService;
using Integracao_Juridico.Models;
using Newtonsoft.Json;

namespace Integracao_Juridico.Controllers
{
    public class ConsultaTestePjePush
    {
        private const string UrlServico = @"https://wwwh.cnj.jus.br/pjemni-2x/intercomunicacao?wsdl";
        private const string UserName = "75293730215";
        private const string Password = "admin123";

        public string Consultar()
        {
            var response = RetornarResponse();
            return JsonConvert.SerializeObject(response.Result);
        }

        public string Consultar(string idStr)
        {
            long id = Convert.ToInt64(idStr);
            var response = RetornarResponse();

            var retorno = response.Result;

            return string.Format("Consulta processo - mensagem: " + retorno.mensagem + "{0}" +
                                 "Sucesso? - " + (retorno.mensagem.Contains("sucesso") ? "Sim" : "Não") + "{0}" +
                                 "Dados básicos - numero: " + retorno.processo.dadosBasicos.numero + "{0}" +
                                 "Data ajuizamento: " + retorno.processo.dadosBasicos.dataAjuizamento + "{0}" +
                                 "Valor causa: " + retorno.processo.dadosBasicos.valorCausa, 
                                 Environment.NewLine); 
        }

        private Task<consultarProcessoResponse> RetornarResponse()
        {
            var bindingConexao = new BasicHttpBinding
            {
                MaxReceivedMessageSize = 2147483647,
                SendTimeout = TimeSpan.MaxValue
            };
            bindingConexao.Security.Mode = BasicHttpSecurityMode.None;
            bindingConexao.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            var endpoint = new EndpointAddress(new Uri(UrlServico));
            var client = new servicointercomunicacao222Client(bindingConexao, endpoint);

            Task<consultarProcessoResponse> response = null;

            var request = GetRequest();
            response = client.consultarProcessoAsync(request);
            response.Start();

            while (!response.IsCompleted)
                response.Wait();

            if (!response.IsCompletedSuccessfully)
                throw new OperationCanceledException("Erro na consulta do processo: " + response.Status + response.Exception);

            if (response.Result == null)
                throw new OperationCanceledException("response nulo");

            return response;
        }

        private consultarProcesso GetRequest()
        {
            return new consultarProcesso
            {
                idConsultante = UserName,
                senhaConsultante = Password,
                numeroProcesso = "0000003-34.2018.2.00.0200"
            };
        }
        
        private static List<DadosProcessosLayout> GetDadosTeste()
        {
            return new List<DadosProcessosLayout>
            {
                new DadosProcessosLayout
                {
                    IdProcesso = 1,
                    ResponseProcesso =  new consultarProcessoResponse()
                    {
                        mensagem = "Consulta processo 1 efetuada com sucesso",
                        sucesso = true,
                        processo = new tipoProcessoJudicial
                        {
                            dadosBasicos = new tipoCabecalhoProcesso
                            {
                                numero = "0599794-97.1991.8.02.0008",
                                dataAjuizamento = "20/03/2019",
                                valorCausa = 555d
                            }
                        }
                    }
                },

                new DadosProcessosLayout
                {
                    IdProcesso = 2,
                    ResponseProcesso = new consultarProcessoResponse()
                    {
                        mensagem = "Consulta processo 2 efetuada com sucesso",
                        sucesso = true,
                        processo = new tipoProcessoJudicial
                        {
                            dadosBasicos = new tipoCabecalhoProcesso
                            {
                                numero = "0000001-11.2222.3.44.5555",
                                dataAjuizamento = "25/03/2019",
                                valorCausa = 1000d
                            }
                        }
                    }
                }
            };
        }
    }
}

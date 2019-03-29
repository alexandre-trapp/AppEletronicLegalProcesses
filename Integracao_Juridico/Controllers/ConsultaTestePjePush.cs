using System.Collections.Generic;
using System.Text;
using ConsultaPushPjeService;
using Newtonsoft.Json;

namespace Integracao_Juridico.Controllers
{
    public class ConsultaTestePjePush
    {
        private const string UrlServico = @"https://wwwh.cnj.jus.br/pjemni-2x/intercomunicacao?wsdl";
        private const string UserName = "alexandre.trapp@hotmail.com";
        private const string Password = "15303@le371030";

        public string Consultar()
        {
            //response.Start();

            //while (!response.IsCompleted)
            //    response.Wait();

            //if (!response.IsCompletedSuccessfully)
            //    throw new Exception("Erro na consulta do processo: " + response.Status + response.Exception);

            var response = RetornarResponse();
            return JsonConvert.SerializeObject(response);
        }

        private List<consultarProcessoResponse> RetornarResponse()
        {
            //var bindingConexao = new BasicHttpBinding
            //{
            //    MaxReceivedMessageSize = 2147483647,
            //    SendTimeout = TimeSpan.MaxValue
            //};
            //bindingConexao.Security.Mode = BasicHttpSecurityMode.None;
            //bindingConexao.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            //var endpoint = new EndpointAddress(new Uri(UrlServico));
            //var client = new servicointercomunicacao222Client(bindingConexao, endpoint);

            //client.ClientCredentials.UserName.UserName = UserName;
            //client.ClientCredentials.UserName.Password = Password;

            //using (new OperationContextScope(client.InnerChannel))
            //{
            //    var request = GetRequest();
            //    var response = client.consultarProcessoAsync(request);
            //    if (response == null)
            //        throw new Exception("response nulo");

            //    return response;
            //}

            var response = new List<consultarProcessoResponse>
            {
                new consultarProcessoResponse()
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
                },
                new consultarProcessoResponse()
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
                },
            };

            return response;
        }
    }
}

using System;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsultaPushPjeService;

namespace Integracao_Juridico.Controllers
{
    public class ConsultaProcessosPjePush
    {
        private const string UrlServico = @"https://wwwh.cnj.jus.br/pjemni-2x/intercomunicacao?wsdl";
        private const string UserName = "alexandre.trapp@hotmail.com";
        private const string Password = "15303@le371030";

        public void Consultar()
        {
            var response = ConnectService();
            response.Start();

            while (!response.IsCompleted)
                response.Wait();

            if (!response.IsCompletedSuccessfully)
                throw new Exception("Erro na consulta do processo: " + response.Status + response.Exception);

            TratarRetornoResponse(response);
        }

        private Task<consultarProcessoResponse> ConnectService()
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
            
            client.ClientCredentials.UserName.UserName = UserName;
            client.ClientCredentials.UserName.Password = Password;

            using (new OperationContextScope(client.InnerChannel))
            {
                var request = GetRequest();
                var response = client.consultarProcessoAsync(request);
                if (response == null)
                    throw new Exception("response nulo");

                return response;
            }
        }

        private static void TratarRetornoResponse(System.Threading.Tasks.Task<consultarProcessoResponse> response)
        {
            var retornoSb = new StringBuilder();
            retornoSb.AppendLine("Consulta processo - mensagem: " + response.Result.mensagem);
            retornoSb.AppendLine("Sucesso? - " + response.Result.sucesso);
            retornoSb.AppendLine("Dados básicos - numero: " + response.Result.processo.dadosBasicos.numero);
            retornoSb.AppendLine("Data ajuizamento: " + response.Result.processo.dadosBasicos.dataAjuizamento);
            retornoSb.AppendLine("Valor causa: " + response.Result.processo.dadosBasicos.valorCausa);

            var file = new StreamWriter(@"C:\Trapp\processo.txt");
            file.WriteLine(retornoSb.ToString());
        }

        private consultarProcesso GetRequest()
        {
            return new consultarProcesso
            {
                idConsultante = UserName,
                senhaConsultante = Password,
                numeroProcesso = "0002934-09.2019.8.26.0278"
            };
        }
    }
}

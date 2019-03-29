var dadoProcessosJson = null;

$(document).ready(function () {

    var url = "/Home/DadosProcesso";
    $.get(url, null, function (data) {

        if (data === null || data === undefined)
            return;

        CreateRowsProcessos(data);
        clicarNaLinha();
    });
    
});

function CreateRowsProcessos(data) {

    dadoProcessosJson = JSON.parse(data);
    var table = document.getElementById("tabelaProcessos");

    for (var i = dadoProcessosJson.length -1; i >= 0; i--) {

        var row = table.insertRow(1);
        row.id = "linhaGrid";

        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);

        cell1.innerHTML = dadoProcessosJson[i].IdProcesso;
        cell2.innerHTML = dadoProcessosJson[i].ResponseProcesso.processo.dadosBasicos.dataAjuizamento;
        cell3.innerHTML = dadoProcessosJson[i].ResponseProcesso.processo.dadosBasicos.numero;

        /*cell3.innerHTML = "Consulta processo - mensagem: " + dados[i].mensagem + "\n" +
                          "Sucesso? - " + (dados[i].sucesso ? "Sim" : "Não") + "\n" +
                          "Dados básicos - numero: " + dados[i].processo.dadosBasicos.numero + "\n" +
                          "Data ajuizamento: " + dados[i].processo.dadosBasicos.dataAjuizamento + "\n" +
                          "Valor causa: " + dados[i].processo.dadosBasicos.valorCausa;*/
    }
}

function clicarNaLinha() {



    $("#tabelaProcessos tr").click(function () {
        var tabela = document.getElementById("tabelaProcessos");
        var linhas = tabela.getElementsByTagName("tr");

        for (var i = 0; i < linhas.length; i++) {

            selLinha(this, false);
        }

        var selecionados = tabela.getElementsByClassName("selecionado");
        if (selecionados.length < 0) {
            return false;
        }

        var dados = "";

        for (var i = 0; i < selecionados.length; i++) {

            var selecionado = selecionados[i];
            selecionado = selecionado.getElementsByTagName("td");

            var idProcesso = selecionado[0].innerHTML.trim();
            var index = -1;

            var filteredObj = dadoProcessosJson.find(function (item, i) {
                if (item.name === idProcesso) {
                    index = i;
                    return i;
                }
            });

            dados = filteredObj;
            //dados = "Data de distribuição: " + selecionado[0].innerHTML.trim() + "\nNúmero do processo: " + selecionado[1].innerHTML.trim();
        }

        $("#dadosBasicos").html(dados);
    });

    function selLinha(linha, multiplos) {

        if (!multiplos) {
            var linhas = linha.parentElement.getElementsByTagName("tr");
            for (var i = 0; i < linhas.length; i++) {
                var linha_ = linhas[i];
                linha_.classList.remove("selecionado");
            }
        }
        linha.classList.toggle("selecionado");
    }
}
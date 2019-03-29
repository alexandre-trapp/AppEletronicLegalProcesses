$(document).ready(function () {

    var url = "/Home/DadosProcesso";
    $.get(url, null, function (data) {

        if (data === null || data === undefined)
            return;

        CreateRowsProcessos(data)
    });
    
});

function CreateRowsProcessos(data) {

    var dados = JSON.parse(data);
    var table = document.getElementById("tabelaProcessos");

    for (var i = 0; i < dados.length; i++) {

        var row = table.insertRow(1);
        row.id = "linhaGrid";

        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(3);

        cell1.innerHTML = dados[i].processo.dadosBasicos.dataAjuizamento;
        cell2.innerHTML = dados[i].processo.dadosBasicos.numero;
        cell3.innerHTML = "Consulta processo - mensagem: " + dados[i].mensagem + "\n" +
                          "Sucesso? - " + dados[i].sucesso + "\n" +
                          "Dados básicos - numero: " + dados[i].processo.dadosBasicos.numero + "\n" +
                          "Data ajuizamento: " + dados[i].processo.dadosBasicos.dataAjuizamento + "\n" +
                          "Valor causa: " + dados[i].processo.dadosBasicos.valorCausa;
    }
}


/*

    $("#tabelaProcessos tr").click(function ()
    {
        var tabela = document.getElementById("tabelaProcessos");
        var linhas = tabela.getElementsByTagName("tr");

        for (var i = 0; i < linhas.length; i++) {
            var linha = linhas[i];

            selLinha(this, false);
        }

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

        var selecionados = tabela.getElementsByClassName("selecionado");
        if (selecionados.length < 0) {
            return false;
        }

        var dados = "";

        for (var i = 0; i < selecionados.length; i++)
        {
            var selecionado = selecionados[i];
            selecionado = selecionado.getElementsByTagName("td");
            dados = "Data de distribuição: " + selecionado[0].innerHTML.trim() + "\nNúmero do processo: " + selecionado[1].innerHTML.trim();
        }

        $("#dadosBasicos").html(dados);

    }); */
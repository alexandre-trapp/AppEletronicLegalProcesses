$(document).ready(function () {

    var url = "/Home/DadosProcesso";
    $.get(url, null, function (data) {

        if (data === null || data === undefined)
            return;

        CreateRowsProcessos(data);
    });

    var clicarLinha = clicarNaLinha();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(clicarLinha);
});

function CreateRowsProcessos(data) {

    var dadoProcessosJson = JSON.parse(data);
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

        /*cell3.innerHTML = */
    }
}

function clicarNaLinha() {


    var tabela = document.getElementById("tabelaProcessos");
    var linhas = tabela.getElementsByTagName("tr");

    for (i = 0; i < rows.length; i++)
    {
        var currentRow = table.rows[i];
        var createClickHandler =
            function (row)
            {
                return function () {
                    var cell = row.getElementsByTagName("td")[0];
                    var id = cell.innerHTML;

                    alert("id:" + id);
                };
            }

        currentRow.onclick = createClickHandler(currentRow);
    }

    var selecionados = tabela.getElementsByClassName("selecionado");
    if (selecionados.length < 0) {
        return false;
    }

    var dados = "";

    for (var i = 0; i < selecionados.length; i++) {

        var selecionado = selecionados[i];
        selecionado = selecionado.getElementsByTagName("td");

        
    }

    $("#dadosBasicos").html(data);
}

function ObterDados(idProcesso) {

    var url = "/Home/ObterDetalhesProcesso";
    $.get(
        url,
        idProcesso,

        function (data) {
            if (data === null || data === undefined || data.length === 0)
                return;

            console.log(data);
        });
}

}
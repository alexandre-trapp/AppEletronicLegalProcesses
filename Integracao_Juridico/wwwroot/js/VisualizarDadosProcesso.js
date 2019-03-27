$(document).ready(function () {

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

    });
});
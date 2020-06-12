$(document).ready(function(){
    $("#Valor").mask("000,00", {reverse: true});
    document.getElementById("upload").addEventListener("change", function(){
        document.getElementById("uploadLabel").innerHTML = this.files[0].name;
    });
});
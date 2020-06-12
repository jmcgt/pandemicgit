$("#listaPedidos").on("show.bs.collapse", function(){
	document.getElementById("icon_pedidos").innerHTML = "expand_less";
});
$("#listaPedidos").on("hide.bs.collapse", function(){
	document.getElementById("icon_pedidos").innerHTML = "expand_more";
});
$("#listaClientes").on("show.bs.collapse", function(){
	document.getElementById("icon_clientes").innerHTML = "expand_less";
});
$("#listaClientes").on("hide.bs.collapse", function(){
	document.getElementById("icon_clientes").innerHTML = "expand_more";
});
$("#listaProdutos").on("show.bs.collapse", function(){
	document.getElementById("icon_produtos").innerHTML = "expand_less";
});
$("#listaProdutos").on("hide.bs.collapse", function(){
	document.getElementById("icon_produtos").innerHTML = "expand_more";
});
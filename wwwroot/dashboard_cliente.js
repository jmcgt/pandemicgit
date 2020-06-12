document.getElementById("ouro").addEventListener("change", function(){
	if(this.checked) document.getElementById("linkCompra").setAttribute("href", "/comprar-voucher/"+this.value);
});
document.getElementById("prata").addEventListener("change", function(){
	if(this.checked) document.getElementById("linkCompra").setAttribute("href", "/comprar-voucher/"+this.value);
});
document.getElementById("bronze").addEventListener("change", function(){
	if(this.checked) document.getElementById("linkCompra").setAttribute("href", "/comprar-voucher/"+this.value);
});
document.getElementById("platina").addEventListener("change", function(){
	if(this.checked) document.getElementById("linkCompra").setAttribute("href", "/comprar-voucher/"+this.value);
});
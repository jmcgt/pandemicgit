namespace PandemicWeb.Models
{
    public class ItemPedido
    {
        public Pedido Pedido {get; set;}

        public Produto Produto {get; set;}

        public int Quantidade {get; set;}

        public double Valor {get; set;}

        public ItemPedido(Produto produto)
        {
            Produto = produto;
            Valor = produto.Valor;
        }

    }
}
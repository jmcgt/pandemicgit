using System.Collections.Generic;
using System.Data.SqlClient;
using PandemicWeb.Models;

namespace PandemicWeb.Data
{
    public class ItemPedidoData : EntityData
    {
        public void Create(ItemPedido item)
        {
            command = new SqlCommand("insert into item_pedido values (@pedido_id, @produto_id, @quantidade, @valor)", connection);
            command.Parameters.AddWithValue("@pedido_id", item.Pedido.Id);
            command.Parameters.AddWithValue("@produto_id", item.Produto.Id);
            command.Parameters.AddWithValue("@quantidade", item.Quantidade);
            command.Parameters.AddWithValue("@valor", item.Valor);
            command.ExecuteNonQuery();
        }

        public List<ItemPedido> Read(Pedido pedido)
        {
            List<ItemPedido> lista = null;
            command = new SqlCommand("select * from item_pedido where pedido_id = @pedido_id", connection);
            command.Parameters.AddWithValue("@pedido_id", pedido.Id);
            reader = command.ExecuteReader();
            if(reader.HasRows) lista = new List<ItemPedido>();
            while(reader.Read())
            {
                ItemPedido item;
                using(ProdutoData data = new ProdutoData()) item = new ItemPedido(data.Read(reader.GetInt32(1)));
                item.Pedido = new Pedido(reader.GetInt32(0));
                item.Quantidade = reader.GetInt32(2);
                item.Valor = reader.GetDouble(3);
                lista.Add(item);
            }
            return lista;
        }
    }
}
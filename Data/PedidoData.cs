using System.Collections.Generic;
using System.Data.SqlClient;
using PandemicWeb.Models;

namespace PandemicWeb.Data
{
    public class PedidoData : EntityData
    {
        public void Create(Pedido pedido)
        {
            command = new SqlCommand(@"insert into pedido (data_hora, cliente_id, valor_total, status) output inserted.id values (default, @cliente_id,
            @valor_total, default)", connection);
            command.Parameters.AddWithValue("@cliente_id", pedido.Cliente.Id);
            command.Parameters.AddWithValue("@valor_total", pedido.ValorTotal);
            int idGerado = (int) command.ExecuteScalar();
            foreach(ItemPedido item in pedido.Itens)
            {
                item.Pedido = new Pedido(idGerado);
                using(ItemPedidoData data = new ItemPedidoData()) data.Create(item);
            }
        }

        public void Update(Pedido pedido)
        {
            command = new SqlCommand("update pedido set status = @status where id = @id", connection);
            command.Parameters.AddWithValue("@status",  pedido.Status);
            command.Parameters.AddWithValue("@id", pedido.Id);
            command.ExecuteNonQuery();
        }

        public List<Pedido> Read()
        {
            List<Pedido> lista = null;
            command = new SqlCommand(@"select pedido.id, data_hora, usuario.nome, cliente.endereco, valor_total, status from pedido, cliente, usuario 
            where pedido.cliente_id = cliente.id and cliente.usuario_id = usuario.id order by data_hora desc", connection);
            reader = command.ExecuteReader();
            if(reader.HasRows) lista = new List<Pedido>();
            while(reader.Read())
            {
                Pedido pedido = new Pedido(reader.GetInt32(0));
                pedido.DataHora = reader.GetDateTime(1);
                pedido.Cliente = new Cliente();                
                pedido.Cliente.Usuario.Nome = reader.GetString(2);
                pedido.Cliente.Endereco = reader.GetString(3);
                pedido.ValorTotal = reader.GetDouble(4);
                switch(reader.GetInt32(5))
                {
                    case 1:
                        pedido.Status = StatusPedido.Aberto;
                        break;
                    case 2:
                        pedido.Status = StatusPedido.Preparando;
                        break;
                    case 3:
                        pedido.Status = StatusPedido.EmTransporte;
                        break;
                    case 4:
                        pedido.Status = StatusPedido.Entregue;
                        break;
                    default:
                        pedido.Status = StatusPedido.Cancelado;
                        break;
                }
                lista.Add(pedido);
            }
            return lista;
        }

        public List<Pedido> Read(Cliente cliente)
        {
            List<Pedido> lista = null;
            command = new SqlCommand(@"select * from pedido where cliente_id = @cliente_id order by data_hora desc", connection);
            command.Parameters.AddWithValue("@cliente_id", cliente.Id);
            reader = command.ExecuteReader();
            if(reader.HasRows) lista = new List<Pedido>();
            while(reader.Read())
            {
                Pedido pedido = new Pedido(reader.GetInt32(0));
                pedido.DataHora = reader.GetDateTime(1);
                pedido.Cliente = new Cliente(reader.GetInt32(2));
                pedido.ValorTotal = reader.GetDouble(3);
                switch(reader.GetInt32(4))
                {
                    case 1:
                        pedido.Status = StatusPedido.Aberto;
                        break;
                    case 2:
                        pedido.Status = StatusPedido.Preparando;
                        break;
                    case 3:
                        pedido.Status = StatusPedido.EmTransporte;
                        break;
                    case 4:
                        pedido.Status = StatusPedido.Entregue;
                        break;
                    default:
                        pedido.Status = StatusPedido.Cancelado;
                        break;
                }
                lista.Add(pedido);
            }
            return lista;
        }
    }
}
using System;
using System.Collections.Generic;

namespace PandemicWeb.Models
{
    public class Pedido
    {
        public int Id {get; set;}
        public DateTime DataHora {get; set;}
        public double ValorTotal {get; set;}
        public Cliente Cliente {get; set;}
        public StatusPedido Status {get; set;}
        public List<ItemPedido> Itens {get; set;}


        public Pedido(){Status = StatusPedido.Aberto;}
        public Pedido(int id){Id = id;}
    }
}
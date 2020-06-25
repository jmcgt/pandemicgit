using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PandemicWeb.Models{
    public class Cliente{
        public int Id {get; set;}        
        
        [Required(ErrorMessage="CPF obrigatório")]
        [MaxLength(14)]
        [MinLength(14, ErrorMessage="CPF inválido")]
        public string CPF {get; set;}
        
        [Required(ErrorMessage="Campo obrigatório")]
        [MinLength(14, ErrorMessage="O telefone deve ter o mínimo de 14 caracteres")]        
        public string Telefone {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]
        [MinLength(3, ErrorMessage="O endereço deve ter o mínimo de três caracteres")]
        public string Endereco {get; set;}

        public double Credito {get; set;}

        public Usuario Usuario {get; set;}

        public List<Pedido> Pedidos {get; set;}

        public Voucher Voucher {get; set;}

        public Cliente(){
            Usuario = new Usuario();
            Voucher = new Voucher();
        }
        public Cliente(int id){Id = id;}
    }
}
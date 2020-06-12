using System;
using System.ComponentModel.DataAnnotations;
using PandemicWeb.Data;

namespace PandemicWeb.Models
{
    public class Voucher
    {
        public int Id {get; set;}        
        public Cliente Cliente {get; set;}
        public double Credito{get; set;}                
        public StatusVoucher Status {get; set;} = StatusVoucher.Inativo;        

        public Voucher(){if(new ConfigData().Ativacao()==ValorConfig.VouchersAtivados) Status = StatusVoucher.Ativo;}
        public Voucher(int id){Id = id;}
    }
}
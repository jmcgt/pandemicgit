using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PandemicWeb.Models;

namespace PandemicWeb.Data
{
    public class VoucherData : EntityData
    {
        public void Create(Voucher voucher)
        {
            command = new SqlCommand("insert into voucher (cliente_id, credito, status) values (@cliente_id, default, @status)", 
            connection);
            command.Parameters.AddWithValue("@cliente_id", voucher.Cliente.Id);            
            command.Parameters.AddWithValue("@status", voucher.Status);
            command.ExecuteNonQuery();
        }

        public void Update(Voucher voucher)
        {
            if(voucher.Credito==0) command = new SqlCommand("update voucher set credito = @credito, status = default where id = @id", connection);
            else command = new SqlCommand("update voucher set credito = @credito where id = @id", connection);
            command.Parameters.AddWithValue("@credito", voucher.Credito);
            command.Parameters.AddWithValue("@id", voucher.Id);
            command.ExecuteNonQuery();
        }

        public Voucher Read(Cliente cliente)
        {
            Voucher voucher = null;
            command = new SqlCommand("select * from voucher where cliente_id = @cliente_id", connection);
            command.Parameters.AddWithValue("@cliente_id", cliente.Id);
            reader = command.ExecuteReader();                    
            if(reader.Read())
            {
                voucher = new Voucher(reader.GetInt32(0));
                voucher.Cliente = new Cliente(reader.GetInt32(1));
                voucher.Credito = (double)reader.GetDouble(2);
                voucher.Status = (reader.GetBoolean(3)) ? StatusVoucher.Ativo : StatusVoucher.Inativo;   
            }
            return voucher;
        }

        public void SetStatusVouchers(StatusVoucher status)
        {
            command = new SqlCommand("update voucher set status = @status", connection);
            command.Parameters.AddWithValue("@status", status);
            command.ExecuteNonQuery();
        }
    }
}
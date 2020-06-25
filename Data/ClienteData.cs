using System.Collections.Generic;
using PandemicWeb.Models;
using System.Data.SqlClient;

namespace PandemicWeb.Data
{
  public class ClienteData : EntityData
  { 
    public void Create(Cliente cliente)
    {                        
      using(UsuarioData data = new UsuarioData()) cliente.Usuario = data.Create(cliente.Usuario);
      command = new SqlCommand(@"insert into cliente (cpf, telefone, endereco, usuario_id) output inserted.id values (@cpf, @telefone, @endereco, 
      @usuario_id)", connection);
      command.Parameters.AddWithValue("@cpf", cliente.CPF);
      command.Parameters.AddWithValue("@telefone", cliente.Telefone);
      command.Parameters.AddWithValue("@endereco", cliente.Endereco);
      command.Parameters.AddWithValue("@usuario_id", cliente.Usuario.Id);
      cliente.Voucher.Cliente = new Cliente((int)command.ExecuteScalar());
      using(VoucherData data = new VoucherData()) data.Create(cliente.Voucher); 
    }

    public Cliente Update(Cliente cliente)
    {
      using(UsuarioData data = new UsuarioData()) data.Update(cliente.Usuario);      
      command = new SqlCommand("update cliente set cpf = @cpf, telefone = @telefone, endereco = @endereco where id = @id", connection);
      command.Parameters.AddWithValue("@id", cliente.Id);      
      command.Parameters.AddWithValue("@cpf", cliente.CPF);
      command.Parameters.AddWithValue("@telefone", cliente.Telefone);
      command.Parameters.AddWithValue("@endereco", cliente.Endereco);
      if(command.ExecuteNonQuery()>0) return cliente;
      else return null;
    }
    
    public List<Cliente> Read()
    {
      List<Cliente> lista = null;
      command = new SqlCommand(@"select cliente.id, cpf, telefone, endereco, usuario_id, usuario.nome, usuario.email from cliente, usuario 
      where usuario.id = cliente.usuario_id order by usuario.nome", connection);
      reader = command.ExecuteReader();
      if(reader.HasRows) lista = new List<Cliente>(); 
      while(reader.Read())
      {
        Cliente cliente = new Cliente(reader.GetInt32(0));        
        cliente.CPF = reader.GetString(1);
        cliente.Telefone = reader.GetString(2);
        cliente.Endereco = reader.GetString(3);
        using(VoucherData data = new VoucherData()) cliente.Voucher = data.Read(cliente);
        cliente.Usuario = new Usuario(reader.GetInt32(4));
        cliente.Usuario.Nome = reader.GetString(5);
        cliente.Usuario.Email = reader.GetString(6);
        lista.Add(cliente);
      }
      return lista;
    }
    
    public Cliente Read(Usuario usuario)
    {
      Cliente cliente = null;
      command = new SqlCommand("select * from cliente where usuario_id = @usuario_id", connection);
      command.Parameters.AddWithValue("@usuario_id", usuario.Id);
      reader = command.ExecuteReader();
      if(reader.Read())
      {
        cliente = new Cliente(reader.GetInt32(0));        
        cliente.CPF = reader.GetString(1);
        cliente.Telefone = reader.GetString(2);
        cliente.Endereco = reader.GetString(3);
        cliente.Usuario = usuario;        
      }
      reader.Close();
      if(cliente!=null){
        using(PedidoData data = new PedidoData()) cliente.Pedidos = data.Read(cliente);
        using(VoucherData data = new VoucherData()) cliente.Voucher = data.Read(cliente);
      }      
      return cliente;
    }
  }
}
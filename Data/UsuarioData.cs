using System;
using PandemicWeb.Models;
using System.Data.SqlClient;

namespace PandemicWeb.Data
{
  public class UsuarioData : EntityData
  {
    public Usuario Create(Usuario usuario)
    { 
      command = new SqlCommand("insert into usuario (nome, email, senha) output inserted.id values (@nome, @email, @senha)", connection);
      command.Parameters.AddWithValue("@nome", usuario.Nome);
      command.Parameters.AddWithValue("@email", usuario.Email);
      command.Parameters.AddWithValue("@senha", usuario.Senha);
      int idGerado = (int)command.ExecuteScalar();
      if(idGerado>0)
      {
        Usuario user = new Usuario(idGerado);
        user.Nome = usuario.Nome;
        user.Email = usuario.Email;
        user.Senha = usuario.Senha;
        user.Tipo = usuario.Tipo;
        return user;
      }else return null;
    }

    public void Update(Usuario usuario)
    {
      command = new SqlCommand("update usuario set nome = @nome, email = @email where id = @id", connection);
      command.Parameters.AddWithValue("@nome", usuario.Nome);
      command.Parameters.AddWithValue("@email", usuario.Email);      
      command.Parameters.AddWithValue("@id", usuario.Id);
      command.ExecuteNonQuery();
    }

    public void UpdateSenha(Usuario usuario)
    {
      command = new SqlCommand("update usuario set senha = @senha where id = @id", connection);
      command.Parameters.AddWithValue("@senha", usuario.Senha);
      command.Parameters.AddWithValue("@id", usuario.Id);
      command.ExecuteNonQuery();
    }
    
    public Usuario Read(string email, string senha)
    {
      command = new SqlCommand("select * from usuario where email = @email and senha = @senha", connection);
      command.Parameters.AddWithValue("@email", email);
      command.Parameters.AddWithValue("@senha", senha);
      reader = command.ExecuteReader();
      if(reader.Read())
      {
        Usuario usuario = new Usuario(reader.GetInt32(0));
        usuario.Nome = reader.GetString(1);
        usuario.Email = reader.GetString(2);
        usuario.Senha = reader.GetString(3);
        usuario.Tipo = (reader.GetBoolean(4)) ? TipoUsuario.Administrador : TipoUsuario.Cliente;
        return usuario;            
      }else return null;
    }
  }
}
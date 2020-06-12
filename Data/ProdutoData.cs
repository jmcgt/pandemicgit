using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Http;
using PandemicWeb.Models;

namespace PandemicWeb.Data
{
    public class ProdutoData : EntityData
    {
        public void Create(Produto produto)
        {
            command = new SqlCommand(@"insert into produto (nome, descricao, tempo_preparo, valor, imagem, status) values (@nome, @descricao, @tempo_preparo, 
            @valor, @imagem, default)", connection);
            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@tempo_preparo", produto.TempoPreparo);
            command.Parameters.AddWithValue("@valor", produto.Valor);
            command.Parameters.AddWithValue("@imagem", produto.ImagePath);
            command.ExecuteNonQuery();
        }

        public void Update(Produto produto)
        { 
            command = new SqlCommand(@"update produto set nome = @nome, descricao = @descricao, tempo_preparo = @tempo_preparo, valor = @valor, 
            imagem = @imagem, status = @status where id = @id", connection);
            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@tempo_preparo", produto.TempoPreparo);
            command.Parameters.AddWithValue("@valor", produto.Valor);
            command.Parameters.AddWithValue("@imagem", produto.ImagePath);
            command.Parameters.AddWithValue("@status", produto.Status);
            command.Parameters.AddWithValue("@id", produto.Id);            
            command.ExecuteNonQuery();
        }

        public List<Produto> Read()
        {
            List<Produto> lista = null;
            command = new SqlCommand("select * from produto order by nome", connection);
            reader = command.ExecuteReader();
            if(reader.HasRows) lista = new List<Produto>();
            while(reader.Read())
            {
                Produto produto = new Produto(reader.GetInt32(0));
                produto.Nome = reader.GetString(1);
                produto.Descricao = reader.GetString(2);
                if(!reader.IsDBNull(3)) produto.TempoPreparo = reader.GetTimeSpan(3);
                produto.Valor = (float)reader.GetDouble(4);
                produto.ImagePath = reader.GetString(5);
                switch(reader.GetInt32(6))
                {
                    case 0:
                        produto.Status = StatusProduto.Indisponível;
                        break;
                    default:
                        produto.Status = StatusProduto.Disponível;
                        break;
                }
                lista.Add(produto);
            }
            return lista;
        }

        public List<Produto> Catalogo()
        {
            List<Produto> lista = null;
            command = new SqlCommand("select * from produto where status = 1 order by nome", connection);
            reader = command.ExecuteReader();
            if(reader.HasRows) lista = new List<Produto>();
            while(reader.Read())
            {
                Produto produto = new Produto(reader.GetInt32(0));
                produto.Nome = reader.GetString(1);
                produto.Descricao = reader.GetString(2);
                if(!reader.IsDBNull(3)) produto.TempoPreparo = reader.GetTimeSpan(3);
                produto.Valor = (float)reader.GetDouble(4);
                produto.ImagePath = reader.GetString(5);
                switch(reader.GetInt32(6))
                {
                    case 0:
                        produto.Status = StatusProduto.Indisponível;
                        break;
                    default:
                        produto.Status = StatusProduto.Disponível;
                        break;
                }
                lista.Add(produto);
            }
            return lista;
        }

        public List<Produto> Search(string busca)
        {
            List<Produto> lista = null;
            command = new SqlCommand("select * from produto where nome like @busca and status = 1 order by nome", connection);
            command.Parameters.AddWithValue("@busca", "%"+busca+"%");
            reader = command.ExecuteReader();
            
            if(reader.HasRows) lista = new List<Produto>();
            
            while(reader.Read())
            {
                Produto produto = new Produto(reader.GetInt32(0));
                produto.Nome = reader.GetString(1);
                produto.Descricao = reader.GetString(2);
                if(!reader.IsDBNull(3)) produto.TempoPreparo = reader.GetTimeSpan(3);
                produto.Valor = (float)reader.GetDouble(4);
                produto.ImagePath = reader.GetString(5);
                switch(reader.GetInt32(6))
                {
                    case 0:
                        produto.Status = StatusProduto.Indisponível;
                        break;
                    default:
                        produto.Status = StatusProduto.Disponível;
                        break;
                }

                lista.Add(produto);
            }

            return lista;
        }

        public Produto Read(int id)
        {
            Produto produto = null;
            command = new SqlCommand("select * from produto where id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                produto = new Produto(reader.GetInt32(0));
                produto.Nome = reader.GetString(1);
                produto.Descricao = reader.GetString(2);
                if(!reader.IsDBNull(3)) produto.TempoPreparo = reader.GetTimeSpan(3);
                produto.Valor = (float)reader.GetDouble(4);
                produto.ImagePath = reader.GetString(5);
                switch(reader.GetInt32(6))
                {
                    case 0:
                        produto.Status = StatusProduto.Indisponível;
                        break;
                    default:
                        produto.Status = StatusProduto.Disponível;
                        break;
                }
            }
            return produto;
        }

        public void Delete(int id)
        {      
            string sql = "DELETE FROM produtos WHERE id = @id";

            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
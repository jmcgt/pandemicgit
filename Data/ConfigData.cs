using System.Data.SqlClient;
using PandemicWeb.Models;

namespace PandemicWeb.Data
{
    public class ConfigData : EntityData
    {
        public void Iniciar()
        {
            if(!ConfigIniciada()) new SqlCommand("insert into config values (default)", connection).ExecuteNonQuery();
        }

        public bool ConfigIniciada()
        {
            command = new SqlCommand("select * from config", connection);
            reader = command.ExecuteReader();
            bool result = reader.HasRows;
            reader.Close();
            return result;
        }
        
        public ValorConfig Ativacao()
        {
            Iniciar();
            command = new SqlCommand("select * from config", connection);
            reader = command.ExecuteReader();
            reader.Read();            
            return (reader.GetBoolean(0)) ? ValorConfig.VouchersAtivados : ValorConfig.VouchersDesativados;
        }

        public void Salvar(Config config)
        {
            command = new SqlCommand("update config set ativacao = @ativacao", connection);
            command.Parameters.AddWithValue("@ativacao", config.Value);
            command.ExecuteNonQuery();
        }
    }
}
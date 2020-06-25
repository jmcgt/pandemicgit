using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PandemicWeb.Models{
    public class Usuario{        
        public int Id {get; set;}

        [Required(ErrorMessage="Campo obrigatório")]
        [MinLength(3, ErrorMessage="O nome deve ter o mínimo de três caracteres")]
        public string Nome {get; set;}
        
        [Required(ErrorMessage="Campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage="E-mail inválido")]
        [MinLength(7, ErrorMessage="O e-mail deve ter mínimo de sete caracteres")]
        [MaxLength(50)]
        public string Email {get; set;}
        
        [Required(ErrorMessage="Campo obrigatório")]
        [StringLength(20, MinimumLength = 8, ErrorMessage="A senha deve ter entre 8 e 20 caracteres")]
        [DataType(DataType.Password)]
        public string Senha {get; set;}

        [DataType(DataType.Password)]
        public string NovaSenha {get; set;}
        
        public TipoUsuario Tipo {get; set;}

        public Usuario(){Tipo = TipoUsuario.Cliente;}
        public Usuario(int id){Id = id;}
    }
}
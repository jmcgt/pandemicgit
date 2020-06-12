using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PandemicWeb.Models
{
    public class Produto
    {
        public int Id {get; set;}
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O nome do produto deve ter o mínimo de três caracteres")]
        public string Nome {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "A descrição do produto deve ter o mínimo de três caracteres")]
        [MaxLength(100)]
        public string Descricao {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Time, ErrorMessage = "Tempo inválido")]
        public TimeSpan TempoPreparo {get; set;}

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Currency, ErrorMessage = "Valor inválido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do produto não pode ser zero")]
        [DisplayFormat(DataFormatString="{0:C}", ApplyFormatInEditMode = true)]
        public float Valor {get; set;}

        [DataType(DataType.Upload)]
        public IFormFile Imagem{get; set;}
        
        public string ImagePath{get; set;}

        public StatusProduto Status{get; set;}

        public Produto(){}
        public Produto(int id){Id = id;}
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CadastroDeClientes.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Display(Name ="Id")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name ="Nome")]
        [StringLength(100, MinimumLength = 2)]
        [Column("Nome")]
        public string? Nome { get; set; }

        [Display(Name ="CNPJ")]
        [Column ("CNPJ")]
        [StringLength(14, MinimumLength = 14)]
        public string? Cnpj { get; set; }
        
        [Display(Name ="Segmento")]
        [Column("Segmento")]
        public string? Segmento { get; set; }
        
        [Display(Name = "CEP")]
        [Column("Cep")]
        [StringLength(8, MinimumLength = 8)]
        public string? Cep { get; set; }
        
        [Display(Name = "Cidade")]
        [Column("Cidade")]
        public string? Cidade { get; set; }
        [StringLength(255, MinimumLength = 4)]
        [Display(Name ="Rua")]
        [Column("Rua")]
        public string? Rua { get; set; }
        
        [StringLength(255, MinimumLength = 4)]
        [Display(Name ="Bairro")]
        [Column("Bairro")]
        public string? Bairro { get; set; }
        
        [Display(Name ="Uf")]
        [Column("Uf")]
        public string? Uf { get; set; }
        
        [StringLength(255, MinimumLength = 4)]
        [Display(Name ="Ibge")]
        [Column("Ibge")]
        public string? Ibge { get; set; }
        
        [Display(Name = "Imagem")]
        public string? ImagemCaminho { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPIServer.Models
{
    [Table("Filmes")]
    public class Filmes
    {
        [Column("Id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Column("Titulo")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Column("Desc")]
        [Display(Name = "Descrição")]
        public string Desc { get; set; }

        [Column("Ano")]
        [Display(Name = "Lançamento")]
        public int Ano { get; set; }

        [Column("Capa")]
        [Display(Name = "Capa")]
        public string CapaUrl { get; set; }
    }
}

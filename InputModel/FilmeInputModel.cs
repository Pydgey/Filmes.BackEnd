using System.ComponentModel.DataAnnotations;

namespace FilmesAPIServer.InputModel
{
    public class FilmeInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Título do Filme deve ter entre 3 e 100 caracteres")]
        public string Title { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "A descrição deve conter entre 3 e 200 caracteres")]
        public string Desc { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O Ano do filme deve conter 4 caracteres")]
        public int Ano { get; set; }

        [Required]
        public string CapaUrl { get; set; }
    }
}

namespace FilmesAPIServer.Exceptions
{
    public class FilmeNaoCadastradoException : Exception
    {
        public FilmeNaoCadastradoException()
            : base("Não foi possivel cadastrar o filme")
        { }
    }
}

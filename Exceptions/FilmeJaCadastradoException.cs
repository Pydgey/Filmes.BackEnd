namespace FilmesAPIServer.Exceptions
{
    public class FilmeJaCadastradoException : Exception
    {
        public FilmeJaCadastradoException()
            : base("Este Filme já esta cadastrado")
        { }
    }
}

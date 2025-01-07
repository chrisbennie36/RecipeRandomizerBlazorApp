namespace RecipeRandomizerBlazorApp.Exceptions;

public class PageTypeNotImplementedException : Exception
{
    public PageTypeNotImplementedException() : base()
    {

    }

    public PageTypeNotImplementedException(string message) : base(message)
    {

    }

    public PageTypeNotImplementedException(string message, Exception innerException) : base(message, innerException)
    {}
}

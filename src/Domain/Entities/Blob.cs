namespace Domain.Entities;

public class Blob
{
    public string Name { get; set; } = null!;
    public Stream Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    
    public Blob() {}
    
    public Blob(string name, Stream content, string contentType)
    {
        Name = name;
        Content = content;
        ContentType = contentType;
    }
}

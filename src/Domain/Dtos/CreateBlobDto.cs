namespace Domain.Dtos;

public class CreateBlobDto
{
    public Stream Content { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    
    public CreateBlobDto() {}
    
    public CreateBlobDto(Stream content, string contentType)
    {
        Content = content;
        ContentType = contentType;
    }
}

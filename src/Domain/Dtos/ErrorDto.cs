namespace Domain.Dtos;

public class ErrorDto
{
    public int Code { get; set; }
    public string Message { get; set; } = null!;

    public ErrorDto() {}

    public ErrorDto(int code, string message)
    {
        Code = code;
        Message = message;
    }
}

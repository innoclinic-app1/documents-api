namespace Domain.Exceptions;

public class NotFoundException(string fileName) : InvalidOperationException($"File {fileName} not found");

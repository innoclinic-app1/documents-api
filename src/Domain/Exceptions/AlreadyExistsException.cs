namespace Domain.Exceptions;

public class AlreadyExistsException(string fileName) : Exception($"File {fileName} already exists.");

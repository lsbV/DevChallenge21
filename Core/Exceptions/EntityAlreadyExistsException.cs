namespace Core.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public EntityAlreadyExistsException()
    {
    }

    public EntityAlreadyExistsException(string message) : base(message)
    {
    }

    public EntityAlreadyExistsException(string entityName, string propertyName, string value) : base($"Entity {entityName} with {propertyName} = {value} already exists.")
    {
    }


}
namespace Core.Exceptions;

public class EntityNotExistException : Exception
{
    public EntityNotExistException() : base()
    {
    }

    public EntityNotExistException(string message) : base(message)
    {
    }

    public EntityNotExistException(string entityTypeName, object identifier) : base($"Entity {entityTypeName} with Value {identifier} not found")
    {
    }
}
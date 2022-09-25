namespace Ordering.Domain.Exeptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string entity, object key) :
           base($"Entity \"{entity}\" ({key}) was not found.")
        {
        }
    }
}
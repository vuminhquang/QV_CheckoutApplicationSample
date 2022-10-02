using FreeBot.Crosscutting.Exceptions;
using PosApplication.Shared.Constants;

namespace PosApplication.Shared.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string detail, string entityName, string errorKey) : base(ErrorConstants.EntityNotFoundType, detail, entityName, errorKey)
        {
        }
    }
}

namespace Cobros.API.Core.Model.Exceptions
{
    public class AccessForbiddenException:Exception
    {
        public AccessForbiddenException()
        {

        }

        public AccessForbiddenException(string message):base(message)
        {

        }
    }
}

namespace Eddo.Runtime.Session
{
    public static class EddoSessionExtensions
    {
        public static long GetUserId(this IEddoSession eddoSession)
        {
            if (!eddoSession.UserId.HasValue)
            {
                throw new EddoException("Session.UserId is null");
            }
            return eddoSession.UserId.Value;
        }
    }
}

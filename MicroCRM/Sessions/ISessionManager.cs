using MicroCRM.Entities;

namespace MicroCRM.Sessions
{
    public interface ISessionManager
    {
        /// <summary>
        /// Gets the is authenticated flag.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        User CurrentUser { get; }
    }
}

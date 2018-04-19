using MicroCRM.Auth;
using MicroCRM.Data;
using Microsoft.AspNetCore.Mvc;

namespace MicroCRM
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public abstract class RestController : Controller
    {
        #region Protected members

        protected readonly AuthContext _authContext;
        protected readonly DataContext _dataContext;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="authContext">The authentication context.</param>
        /// <param name="dataContext">The database context.</param>
        public RestController(AuthContext authContext, DataContext dataContext)
        {
            _authContext = authContext;
            _dataContext = dataContext;
        }
    }
}

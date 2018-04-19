using System;
using System.Linq;
using System.Security.Claims;
using MicroCRM.Data;
using MicroCRM.Entities;
using Microsoft.AspNetCore.Http;

namespace MicroCRM.Auth
{
    public class AuthContext
    {
        #region Private members

        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public User CurrentUser
        {
            get
            {
                var idClaim = _httpContext.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier);
                if (idClaim == null)
                {
                    return null;
                }

                if (!Guid.TryParse(idClaim.Value, out var id))
                {
                    return null;
                }

                return _dataContext.Users.FirstOrDefault(us => us.Id == id);
            }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataContext">The database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public AuthContext(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContext = httpContextAccessor.HttpContext;
        }
    }
}

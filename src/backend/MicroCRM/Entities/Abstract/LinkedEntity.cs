using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCRM.Entities.Abstract
{
    /// <summary>
    /// The linked entity class.
    /// </summary>
    public abstract class LinkedEntity : AbstractEntity
    {
        #region Public properties

        [ForeignKey("CreatedById")]
        public Guid CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        [ForeignKey("UpdatedById")]
        public Guid UpdatedById { get; set; }

        public virtual User UpdatedBy { get; set; }

        #endregion
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroCRM.Entities.Abstract
{
    /// <summary>
    /// The abstract entity class.
    /// </summary>
    public abstract class AbstractEntity
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the created at timestamp in UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the updated at timestamp in UTC.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; } = true;

        #endregion
    }
}

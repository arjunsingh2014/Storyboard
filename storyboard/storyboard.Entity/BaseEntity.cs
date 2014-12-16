using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace storyboard.Entity
{
    /// <summary>
    /// This Base Entity use to define common properties, which will be exist in all the database table
    /// This must be inherited by other entity objects
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        #region Public Properties

        public int ID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Active { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace storyboard.Entity
{
    [Serializable]
    public class User : BaseEntity
    {
        #region Public Properties

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        #endregion
    }
}

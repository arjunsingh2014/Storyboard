using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace storyboard.Utility
{
    /// <summary>
    /// This class is used to store all the session names at common place
    /// All the variables containing object name must be declared as 'constant'
    /// Use only variable names during calls
    /// </summary>
    public static class SessionKeys
    {
        public const string UserSessionObject = "UserSession";
    }

    public class Message
    {
        public const string SuccessSavedMessage = "Saved successfully";
        public const string SuccessDeleteMessage = "Deleted successfully";
        public const string NotSuccessSavedMessage = "Not saved successfully";
        public const string NotSuccessDeleteMessage = "Not deleted successfully";
        public const string RecordFoundMessage = "Records Found";
        public const string NotRecordFoundMessage = "No Records Found";
    }

}


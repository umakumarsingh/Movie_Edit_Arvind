using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePreFSEmaster.Tests.Exceptions
{
   public class UserNotFoundException : Exception
    {
        public string Messages;

        public UserNotFoundException()
        {
            Messages = "User Not Found";
        }
        public UserNotFoundException(string message)
        {
            Messages = message;
        }
    }
}

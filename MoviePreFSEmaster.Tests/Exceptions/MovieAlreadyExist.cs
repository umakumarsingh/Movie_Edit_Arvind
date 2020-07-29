using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePreFSEmaster.Tests.Exceptions
{
 public   class MovieAlreadyExist :Exception
    {
        public string Messages;

        public MovieAlreadyExist()
        {
            Messages = "Movie Already Exist";
        }
        public MovieAlreadyExist(string message)
        {
            Messages = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return "This " + Message + " wasn't found";
        }
    }
    /// <summary>
    /// if the value already exist throw this exception
    /// </summary>
    [Serializable]
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() : base() { }
        public AlreadyExistsException(string message) : base(message) { }
        public AlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return "This " + Message + " already exists";
        }
    }


    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }
    }

}


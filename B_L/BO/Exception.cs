using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// if the value not found throw this exception
    /// </summary>
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

    /// <summary>
    /// if the amount is empty throw this exception
    /// </summary>
    [Serializable]
    public class EmptyAmountExeption : Exception
    {
        public EmptyAmountExeption() : base() { }
        public EmptyAmountExeption(string message) : base(message) { }
        public EmptyAmountExeption(string message, Exception inner) : base(message, inner) { }
        protected EmptyAmountExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return "the " + Message + " is empty";
        }
    }

    /// <summary>
    /// if battery calculations are wrong throw this exception
    /// </summary>
    [Serializable]
    public class InOrderList : Exception
    {
        public InOrderList() : base() { }
        public InOrderList(string message) : base(message) { }
        public InOrderList(string message, Exception inner) : base(message, inner) { }
        protected InOrderList(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return "This " + Message + " is in the Order list, you cannot delete it";
        }
    }
    /// <summary>
    /// if the input is invalid throw this exception
    /// </summary>
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return "Invalid input: " + Message;
        }
    }
    /// <summary>
    /// if the status of the drone not suitable for the action throw this exception
    /// </summary>
    [Serializable]
    public class CarStatusException : Exception
    {
        public CarStatusException() : base() { }
        public CarStatusException(string message) : base(message) { }
        public CarStatusException(string message, Exception inner) : base(message, inner) { }
        protected CarStatusException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// if the list is empty throw this exception
    /// </summary>
    [Serializable]
    public class EmptyListException : Exception
    {
        public EmptyListException() : base() { }
        public EmptyListException(string message) : base(message) { }
        public EmptyListException(string message, Exception inner) : base(message, inner) { }
        protected EmptyListException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString()
        {
            return Message;
        }
    }

    [Serializable]
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

        public override string ToString()
        {
            return base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
        }
    }
}

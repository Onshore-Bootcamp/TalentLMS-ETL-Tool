namespace Updater.Custom.Exceptions
{
    using System;

    public class OutOfTicketsException : Exception
    {
        public OutOfTicketsException() { }
        public OutOfTicketsException(string message)
        {
            this.overrideMessage = message;
        }

        private string overrideMessage;
        public override string Message
        {
            get
            {
                if(overrideMessage != null)
                {
                    return overrideMessage;
                }
                return "The API called ran out of tickets available";
            }
        }
    }
}

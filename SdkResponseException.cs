using System;
using System.Net.Http;

namespace APIs.Library.Sdk
{
    public sealed class SdkResponseException : Exception
    {
        /// <summary>
        /// Response message.
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="responseMessage"></param>
        /// <param name="innerException"></param>
        public SdkResponseException(
              string message
            , HttpResponseMessage responseMessage
            , Exception innerException = null) : base(message, innerException)
        {
            ResponseMessage = responseMessage;
            Data.Add("ResponseMessage", responseMessage);
        }
    }
}
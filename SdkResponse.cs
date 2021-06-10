using System.Net;
using System.Net.Http;

namespace APIs.Library.Sdk
{
    public sealed class SdkResponse<T>
    {
        /// <summary>
        /// Response data.
        /// </summary>
        public T ResponseData { get; private set; }

        /// <summary>
        /// Http response message.
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; }

        /// <summary>
        /// Http response status code.
        /// </summary>
        public HttpStatusCode ResponseStatusCode { get; }

        /// <summary>
        /// Whether or not the response is a no content response.
        /// </summary>
        public bool IsNoContent
            => ResponseStatusCode == HttpStatusCode.NoContent;

        /// <summary>
        /// Whether or not the response has data.
        /// </summary>
        public bool HasResponseData
            => ResponseData is not null;

        /// <summary>
        /// SDK response exception on failure.
        /// </summary>
        public SdkResponseException SdkResponseException { get; private set; }


        /// <summary>
        /// SDK Response.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <param name="data"></param>
        /// <param name="sdkResponseException"></param>
        public SdkResponse(
              HttpResponseMessage responseMessage
            , T data = default
            , SdkResponseException sdkResponseException = null)
        {
            ResponseMessage = responseMessage;
            ResponseStatusCode = responseMessage.StatusCode;
            ResponseData = data;
            SdkResponseException = sdkResponseException;
        }
    }
}
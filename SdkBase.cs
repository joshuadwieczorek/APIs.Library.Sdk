using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIs.Library.Sdk
{
    public abstract class SdkBase : IDisposable
    {
        protected HttpClient HttpClient { get; private set; }


        /// <summary>
        /// SDK base with http client.
        /// </summary>
        /// <param name="httpClient"></param>
        protected SdkBase([NotNull] HttpClient httpClient)
        {
            HttpClient = httpClient;
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        /// <summary>
        /// SDK base with base url.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="defaultMediaType"></param>
        protected SdkBase([NotNull] string baseUrl, string defaultMediaType = "application/json")
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri($"{baseUrl.TrimEnd('/')}/")
            };
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Accept", defaultMediaType);
        }


        /// <summary>
        /// Generate a SDK Response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected SdkResponse<T> ResponseOrFail<T>(HttpResponseMessage response)
        {
            if (response is null)
                return new SdkResponse<T>(null, sdkResponseException: new SdkResponseException("HttpResponseMessage is null!", null));

            try
            {
                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
                    return new SdkResponse<T>(response, DeserializeResponse<T>(response));

                if (response.StatusCode == HttpStatusCode.NoContent)
                    return new SdkResponse<T>(response);

                var responseMessage = response.Content.ReadAsStringAsync().Result;
                return new SdkResponse<T>(response,
                    sdkResponseException: new SdkResponseException(responseMessage, response));
            }
            catch (Exception e)
            {
                return new SdkResponse<T>(response,
                    sdkResponseException: new SdkResponseException(e.Message, response, e));
            }
        }


        /// <summary>
        /// Generate a SDK Response async.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected async Task<SdkResponse<T>> ResponseOrFailAsync<T>(HttpResponseMessage response)
        {
            if (response is null)
                return new SdkResponse<T>(null, sdkResponseException: new SdkResponseException("HttpResponseMessage is null!", null));

            try
            {
                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
                    return new SdkResponse<T>(response, await DeserializeResponseAsync<T>(response));

                if (response.StatusCode == HttpStatusCode.NoContent)
                    return new SdkResponse<T>(response);

                var responseMessage = await response.Content.ReadAsStringAsync();
                return new SdkResponse<T>(response,
                    sdkResponseException: new SdkResponseException(responseMessage, response));
            }
            catch (Exception e)
            {
                return new SdkResponse<T>(response,
                    sdkResponseException: new SdkResponseException(e.Message, response));
            }
        }


        /// <summary>
        /// Deserialize data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected T DeserializeResponse<T>(HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);


        /// <summary>
        /// Deserialize data async.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());


        /// <summary>
        /// Serialize payload.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="payload"></param>
        /// <returns></returns>
        protected string Serialize<T>(T payload)
            => JsonConvert.SerializeObject(payload);


        /// <summary>
        /// Dispose of sdk.
        /// </summary>
        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}
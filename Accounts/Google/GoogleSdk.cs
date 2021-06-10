using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using APIs.Library.Contracts;

namespace APIs.Library.Sdk.Accounts.Google
{
    public class GoogleSdk : SdkBase
    {
        /// <summary>
        /// SDK with provided http client.
        /// </summary>
        /// <param name="httpClient"></param>
        public GoogleSdk(HttpClient httpClient) : base(httpClient) { }


        /// <summary>
        /// SDK with base url.
        /// </summary>
        /// <param name="baseUrl"></param>
        public GoogleSdk(string baseUrl) : base(baseUrl) { }


        /// <summary>
        /// Get List of Google Accounts.
        /// </summary>
        /// <returns></returns>
        public SdkResponse<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>> Get()
        {
            try
            {
                // Make HTTP call to our apis.
                var response = HttpClient.GetAsync("google").Result;
                
                // Construct response object based on http response message.
                return ResponseOrFail<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>>(response);
            }
            catch (Exception e)
            {
                return new SdkResponse<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>>(null,
                    sdkResponseException: new SdkResponseException(e.Message, null, e));
            }
        }


        /// <summary>
        /// Get List of Google Accounts Async.
        /// </summary>
        /// <returns></returns>
        public async Task<SdkResponse<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>>> GetAsync()
        {
            try
            {
                // Make HTTP call to our apis.
                var response = await HttpClient.GetAsync("google");

                // Construct response object based on http response message.
                return await ResponseOrFailAsync<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>>(response);
            }
            catch (Exception e)
            {
                return new SdkResponse<ApiResponse<List<Database.Accounts.Domain.accounts.Google>>>(null,
                    sdkResponseException: new SdkResponseException(e.Message, null, e));
            }
        }
        

        /// <summary>
        /// Get Single Google Account.
        /// </summary>
        /// <returns></returns>
        public SdkResponse<ApiResponse<Database.Accounts.Domain.accounts.Google>> Get(int googleId)
        {
            try
            {
                // Make HTTP call to our apis.
                var response = HttpClient.GetAsync($"google/{googleId}").Result;

                // Construct response object based on http response message.
                return ResponseOrFail<ApiResponse<Database.Accounts.Domain.accounts.Google>>(response);
            }
            catch (Exception e)
            {
                return new SdkResponse<ApiResponse<Database.Accounts.Domain.accounts.Google>>(null,
                    sdkResponseException: new SdkResponseException(e.Message, null, e));
            }
        }


        /// <summary>
        /// Get Single Google Account Async.
        /// </summary>
        /// <returns></returns>
        public async Task<SdkResponse<ApiResponse<Database.Accounts.Domain.accounts.Google>>> GetAsync(int googleId)
        {
            try
            {
                // Make HTTP call to our apis.
                var response = await HttpClient.GetAsync($"google/{googleId}");

                // Construct response object based on http response message.
                return await ResponseOrFailAsync<ApiResponse<Database.Accounts.Domain.accounts.Google>>(response);
            }
            catch (Exception e)
            {
                return new SdkResponse<ApiResponse<Database.Accounts.Domain.accounts.Google>>(null,
                    sdkResponseException: new SdkResponseException(e.Message, null, e));
            }
        }
    }
}
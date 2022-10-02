﻿using Newtonsoft.Json;

namespace PosApplication.Test.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage responseMessage)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);

            return result;
        }
    }
}

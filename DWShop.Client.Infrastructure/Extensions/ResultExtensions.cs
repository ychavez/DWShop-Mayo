﻿using DWShop.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DWShop.Client.Infrastructure.Extensions
{
    public static class ResultExtensions
    {
        public static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString,

                new JsonSerializerOptions()
                {

                    PropertyNameCaseInsensitive = true
                });
            return responseObject!;
        }

        public static async Task<IResult> ToResult(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Result>(responseAsString,

                new JsonSerializerOptions()
                {

                    PropertyNameCaseInsensitive = true
                });
            return responseObject!;
        }
    }
}

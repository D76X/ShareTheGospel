﻿using BlazorApp.Client.Abstractions.Services;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetItemAsync<T>(string key, T item)
        {
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                key,
                JsonSerializer.Serialize(item));
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            var serializedValue = await _jsRuntime.InvokeAsync<string>(
                "localStorage.getItem",
                key);

            return string.IsNullOrEmpty(serializedValue)
                    ? default
                    : JsonSerializer.Deserialize<T>(serializedValue);
        }
    }
}

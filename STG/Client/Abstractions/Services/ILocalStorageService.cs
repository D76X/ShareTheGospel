﻿using System.Threading.Tasks;

namespace BlazorApp.Client.Abstractions.Services
{
    public interface ILocalStorageService
    {
        Task SetItemAsync<T>(string key, T item);
        Task<T?> GetItemAsync<T>(string key);
    }
}

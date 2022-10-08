﻿using System;
namespace Discount.API.Data
{
    public interface IDbService
    {
        Task<T> GetAsync<T> (string command, object parms);
        Task<List<T>> GetAll<T> (string command, object parms);
        Task<int> EditData(string command, object parms);
    }
}

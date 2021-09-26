using Marimo.Manabiya.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Marimo.Manabiya.Client
{
    public class Web通信
    {
        HttpClient http;

        public Web通信() : this(new()) { }
        public Web通信(HttpClient httpClient)
        {
            http = httpClient;
        }
         
        public virtual async Task<IEnumerable<勉強会テーマ>> Get勉強会テーマAsync()
            => await http.GetFromJsonAsync<勉強会テーマ[]>("勉強会テーマ");

        public virtual async Task Post勉強会テーマAsync(勉強会テーマ 新規テーマ)
            => await http.PostAsJsonAsync("勉強会テーマ", 新規テーマ);
    }
}

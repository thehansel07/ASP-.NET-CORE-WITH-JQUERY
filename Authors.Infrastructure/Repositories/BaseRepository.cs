using Authors.Core.Entities;
using Authors.Core.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Authors.Core.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            string result = string.Empty;
            var response = await _httpClient.GetAsync("https://fakerestapi.azurewebsites.net/api/v1/Authors");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();

                }
                else 
                {
                    result = "There are an error getting from Authors";
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"{result}", ex.InnerException);
            }
            return JsonConvert.DeserializeObject<List<T>>(result).AsQueryable();
        }
    }
}

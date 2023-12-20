using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models.Repo
{
    public  interface ICitySrvice
    {
        Task<IEnumerable<string>> GetCitiesAsync();
        Task UpdateCitiesAsync(IEnumerable<string> cities);
    }
}

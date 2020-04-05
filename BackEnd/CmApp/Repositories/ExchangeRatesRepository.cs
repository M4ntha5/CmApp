﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CmApp.Domains;
using CmApp.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CmApp.Repositories
{
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        //base currency allways EUR
        private readonly string Url = "https://api.exchangeratesapi.io/latest";
        private async Task<ExchangeRate> GetLatestForeignExchanges()
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(Url).Result;
      
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExchangeRate>(responseData);
                client.Dispose();
                return result;
            }
            else
            {
                client.Dispose();
                return null;
            }
        }
        public async Task<ExchangeRate> GetSelectedExchangeRate(string name)
        {
            var url = Url + "?base=" + name.ToUpper();
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExchangeRate>(responseData);
                client.Dispose();
                return result;
            }
            else
            {
                client.Dispose();
                return null;
            }
        }

        public async Task<double> CalculateResult(ExchangeInput input)
        {
            if (input.From == "" || input.To == "" || input.Amount < 1)
                throw new BusinessException("Input data was in incorect format!");

            var rates = await GetSelectedExchangeRate(input.From);
            double result = 0;
            if (rates.Rates.ContainsKey(input.To))
            {
                var rate = double.Parse(rates.Rates[input.To]);

                result = Math.Round(input.Amount * rate, 2);
            }
            return result;
        }

        public async Task<List<string>> GetAvailableCurrencies()
        {
            var rates = await GetLatestForeignExchanges();
            List<string> names = new List<string>();
            foreach (var rate in rates.Rates)
            {
                names.Add(rate.Key);
            }
            names.Remove("USD"); names.Remove("GBP");

            names = names.OrderByDescending(x => x).ToList();
            //adding thees to the begining of the list
            names.Add("USD"); names.Add("GBP"); names.Add("EUR");
            names.Reverse();

            return names;
        }

        public async Task<List<string>> GetAllCountries()
        {
            var url = "https://restcountries.eu/rest/v2/all";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseData);
                var countries = new List<string>();
                foreach(var res in result)
                    countries.Add(Convert.ToString(res.name));
                
                client.Dispose();
                return countries;
            }
            else
            {
                client.Dispose();
                return null;
            }
        }

    }
}

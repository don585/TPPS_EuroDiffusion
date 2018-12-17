using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class EuroDiffusion
    {
        const int MAX_NUMBER_OF_DAYS = 20000;
        const int MAX_NUMBER_OF_COUNTRIES = 20;
        const int MAX_X = 10;
        const int MAX_Y = 10;

        private int numberOfCountries;
        private CoinDistribution[] countries;
        private bool[,] matrixOfEUCountries;

        public EuroDiffusion(CoinDistribution[] countries)
        {
            numberOfCountries = countries.Length;
            this.countries = countries;
            CheckInputData();
            FillMatrixOfEUCountries();
            SetMatrixOfEUCountries();
        }

        private void CheckInputData()
        {
            if (numberOfCountries > MAX_NUMBER_OF_COUNTRIES)
                Console.WriteLine("Number of counries should be less than " + MAX_NUMBER_OF_COUNTRIES);
        }

        private void FillMatrixOfEUCountries()
        {
            matrixOfEUCountries = new bool[MAX_X, MAX_Y];
            foreach (CoinDistribution country in countries)
            {
                for (int x = country.GetXl(); x <= country.GetXh(); x++)
                    for (int y = country.GetYl(); y <= country.GetYh(); y++)
                        matrixOfEUCountries[x, y] = true;
            }
        }

        private void SetMatrixOfEUCountries()
        {
            foreach (CoinDistribution country in countries)
                country.SetMatrixOfEUCountries(matrixOfEUCountries);
        }

        public void EuroDiffusionSimulation()
        {
            int day = 0;
            while (!IsEnd())
            {
                day++;
                for (int i = 0; i < numberOfCountries; i++)
                {
                    countries[i].NextDay();
                }
                if (day > MAX_NUMBER_OF_DAYS)
                    Console.WriteLine("Countries should be connected");
            }
        }

        private bool IsEnd()
        {
            bool result = true;
            for (int i = 0; i < numberOfCountries; i++)
            {
                if (!countries[i].IsComplete() && !CheckCountryComplete(countries[i]))
                    result = false;
            }
            return result;
        }

        private bool CheckCountryComplete(CoinDistribution country)
        {
            for (int x = country.GetXl(); x <= country.GetXh(); x++)
            {
                for (int y = country.GetYl(); y <= country.GetYh(); y++)
                {
                    for (int j = 0; j < numberOfCountries; j++)
                    {
                        if (countries[j].GetCityCoins(x, y) == 0)
                            return false;
                    }
                }
            }
            country.SetComplete(true);
            return true;
        }

        public String GetResults()
        {
            var sortedCountries = countries.OrderBy(c => c.numberOfDays).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < numberOfCountries; i++)
            {
                stringBuilder.Append(sortedCountries[i].GetName())
                        .Append(" ")
                        .Append(sortedCountries[i].GetNumberOfDays())
                        .Append("\n");
            }
            return stringBuilder.ToString();
        }

        public int getNumberOfCountries()
        {
            return numberOfCountries;
        }
    }
}
   


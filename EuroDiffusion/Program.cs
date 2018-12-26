using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class Program
    {
        const string FILE_NOT_FOUND = "File not found.";
        const string INCORRECT_FILE_FORMAT = "Incorrect file format.";
        const string CASE_NUMBER = "Case Number";
        const string NUMBER_OF_COUNTRY_SHOULD_BE_POSITIVE = "Number of country should be positive";

        static void Main(string[] args)
        {

            List<EuroDiffusion> euroDiffusionSimulations = parseInputFile("input.txt");

            foreach (EuroDiffusion euroDiffusionSimulation in euroDiffusionSimulations)
                euroDiffusionSimulation.EuroDiffusionSimulation();
            Console.WriteLine(BuildResult(euroDiffusionSimulations));
            Console.ReadKey();
        }

        private static List<EuroDiffusion> parseInputFile(string input)
        {
            List<EuroDiffusion> result = new List<EuroDiffusion>();
            int numberOfCountry = 0;
            using (StreamReader sr = new StreamReader(input))
            {
                while (!sr.EndOfStream)
                {
                    numberOfCountry = Convert.ToInt32(sr.ReadLine());
                    if (numberOfCountry < 0)
                        throw new Exception(NUMBER_OF_COUNTRY_SHOULD_BE_POSITIVE);
                    CoinDistribution[] countries = new CoinDistribution[numberOfCountry];
                    for (int i = 0; i < numberOfCountry; i++)
                    {
                        string[] splitLine = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        countries[i] = new CoinDistribution(splitLine[0], Convert.ToInt32(splitLine[1]), Convert.ToInt32(splitLine[2]), Convert.ToInt32(splitLine[3]), Convert.ToInt32(splitLine[4]));
                    }
                    EuroDiffusion euroDiffusionSimulation = new EuroDiffusion(countries);
                    result.Add(euroDiffusionSimulation);
                }
                
            }
            return result;
        }

        private static string BuildResult(List<EuroDiffusion> euroDiffusionSimulations)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int i = 0;
            foreach (EuroDiffusion euroDiffusionSimulation in euroDiffusionSimulations)
            {
                i++;
                if (euroDiffusionSimulation.getNumberOfCountries() == 0)
                    continue;
                stringBuilder.Append(CASE_NUMBER)
                        .Append(" ")
                        .Append(i)
                        .Append("\n")
                        .Append(euroDiffusionSimulation.GetResults());
            }

            return stringBuilder.ToString();
        }
            
    }
}

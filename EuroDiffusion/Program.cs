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
        const string CASE_NUMBER = "Case Number";

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
                    CoinDistribution[] countries = new CoinDistribution[numberOfCountry];
                    for (int i = 0; i < numberOfCountry; i++)
                    {
                        if (sr.EndOfStream)
                            Console.WriteLine("Incorrect input format");
                        else
                        {
                            string[] splitLine = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                             countries[i] = new CoinDistribution(splitLine[0], Convert.ToInt32(splitLine[1]), Convert.ToInt32(splitLine[2]), Convert.ToInt32(splitLine[3]), Convert.ToInt32(splitLine[4]));
                        }
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

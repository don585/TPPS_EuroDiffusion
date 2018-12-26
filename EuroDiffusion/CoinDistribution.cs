using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    class CoinDistribution
    {
        const string NAME_LENGTH_ERROR = "Name should contains not more than 25 characters.";
        const string COORDINATES_SHOULD_BE_POSITIVE = "Coordinates should be positive.";
        const string COORDINATES_SHOULD_BE_LESS_THAN_MAX = "Coordinates should be less than max value.";
        const int LENGTH_NAME = 25;
        const int MAX_Y = 10, MAX_X = 10;
        const int MIN_X = 0, MIN_Y = 0;
        const int PER_AMOUNT = 1000;
        const int INITIAL_CITY_MOTIF = 1000000;

        public string countryName;
        private int[,] currentMatrix;
        private bool[,] matrixOfEUCountries;
        private int xl;
        private int yl;
        private int xh;
        private int yh;
        public int numberOfDays = 0;
        private bool isComplete;

        public CoinDistribution(String name, int xl, int yl, int xh, int yh)
        {
            this.countryName = name;
            this.xl = xl - 1;
            this.yl = yl - 1;
            this.xh = xh - 1;
            this.yh = yh - 1;
            if (!CheckInputData())
                return;
            currentMatrix = InitMatrix();
        }

        private bool CheckInputData()
        {
            if (countryName.Length > LENGTH_NAME)
                throw new Exception(NAME_LENGTH_ERROR);
            if (xl < MIN_X || yl < MIN_Y || xh < MIN_X || yh < MIN_Y)
            {
                throw new Exception(COORDINATES_SHOULD_BE_POSITIVE);
            }
            if (xl >= MAX_X || yl >= MAX_Y || xh >= MAX_X || yh >= MAX_Y)
                throw new Exception(COORDINATES_SHOULD_BE_LESS_THAN_MAX);
            return true;
        }

        public void NextDay()
        {
            int[,] result = new int[MAX_X, MAX_Y];
            for (int x = 0; x < MAX_X; x++)
            {
                for (int y = 0; y < MAX_Y; y++)
                {
                    int amountToTransport = currentMatrix[x, y] / PER_AMOUNT;
                    int numberOfSuccessfulTransportation =
                            TransportToNeighbors(result, x, y, amountToTransport);
                    result[x, y] += currentMatrix[x, y] - numberOfSuccessfulTransportation * amountToTransport;
                }
            }
            if (!IsComplete())
                numberOfDays++;
            currentMatrix = result;
        }

        private int TransportToNeighbors(int[,] matrix, int x, int y, int amountToTransport)
        {
            int numberOfSuccessfulTransportation = 0;
            if (amountToTransport <= 0)
                return numberOfSuccessfulTransportation;
            if (UpdateNeighborCoins(matrix, x - 1, y, amountToTransport))
                numberOfSuccessfulTransportation++;
            if (UpdateNeighborCoins(matrix, x, y - 1, amountToTransport))
                numberOfSuccessfulTransportation++;
            if (UpdateNeighborCoins(matrix, x + 1, y, amountToTransport))
                numberOfSuccessfulTransportation++;
            if (UpdateNeighborCoins(matrix, x, y + 1, amountToTransport))
                numberOfSuccessfulTransportation++;
            return numberOfSuccessfulTransportation;
        }

        private bool UpdateNeighborCoins(int[,] matrix, int x, int y, int amountToTransport)
        {
            if (!CheckIsCityAvailable(x, y))
                return false;
            matrix[x, y] += amountToTransport;
            return true;
        }

        private int[,] InitMatrix()
        {
            int[,] result = new int[MAX_X, MAX_Y];

            for (int x = xl; x <= xh; x++)
                for (int y = yl; y <= yh; y++)
                    result[x, y] = INITIAL_CITY_MOTIF;
            return result;
        }

        private bool CheckIsCityAvailable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MAX_X || y >= MAX_Y)
                return false;
            return matrixOfEUCountries[x, y];
        }

        public int CompareTo(Object o)
        {
            if (o is CoinDistribution)
            {
                CoinDistribution country = (CoinDistribution)o;
                if (numberOfDays > country.numberOfDays)
                    return 1;
                if (numberOfDays < country.numberOfDays)
                    return -1;
                return (countryName.CompareTo(country.countryName));
            }
            else
                return -1;
        }

        public int GetCityCoins(int x, int y)
        {
            return currentMatrix[x, y];
        }

        public String GetName()
        {
            return countryName;
        }

        public int GetXl()
        {
            return xl;
        }

        public int GetYl()
        {
            return yl;
        }

        public int GetXh()
        {
            return xh;
        }

        public int GetYh()
        {
            return yh;
        }

        public int GetNumberOfDays()
        {
            return numberOfDays;
        }

        public bool IsComplete()
        {
            return isComplete;
        }

        public void SetComplete(bool complete)
        {
            isComplete = complete;
        }

        public void SetMatrixOfEUCountries(bool[,] matrixOfEUCountries)
        {
            this.matrixOfEUCountries = matrixOfEUCountries;
        }
    }
}
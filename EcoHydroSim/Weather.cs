using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHydroSim
{
    public interface IWeather
    {
        protected static int Humidity { get; set; }
        IWeather CalculateWeatherForPlain(Plain plain);
        IWeather CalculateWeatherForGrassland(Grassland grassland);
        IWeather CalculateWeatherForLakesRegion(LakesRegion lakesRegion);

        public static IWeather weatherCalculate(int Humidity)
        {
            if (Humidity > 70)
            {
                Humidity = 30;
                return new RainyWeather(Humidity);
                
            }
            else if (Humidity >= 40 && Humidity <= 70)
            {
                int chanceOfRain = (int)(Humidity - 40) * 33 / 10;
                if (new Random().Next(100) < chanceOfRain)
                {
                    return new RainyWeather(Humidity);
                }
                else
                {
                    return new CloudyWeather(Humidity);
                }
            }
            else
            {
                return new SunnyWeather(Humidity);
            }
        }

    }


       public class SunnyWeather : IWeather
      {
        public IWeather CalculateWeatherForPlain(Plain plain)
        {
            plain.WaterStored -= 3;
            Humidity += 5;
            if (plain.WaterStored > 15)
            {
                plain.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForGrassland(Grassland grassland)
        {
            grassland.WaterStored -= 6;
            Humidity += 10;
            if (grassland.WaterStored < 16)
            {
                grassland.Type = "P";
            }
            else if (grassland.WaterStored > 50)
            {
                grassland.Type = "L";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForLakesRegion(LakesRegion lakesRegion)
        {
            lakesRegion.WaterStored -= 10;
            Humidity += 15;
            if (lakesRegion.WaterStored > 51)
            {
                lakesRegion.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        private static SunnyWeather instance = null;
        public static SunnyWeather Instance()
        {
            if (instance == null)
            {
                instance = new SunnyWeather(Humidity);
            }
            return instance;
        }
        public static int Humidity { get; set; }
        public SunnyWeather(int humiditys)
        {
            Humidity = humiditys;
        }
    }

    public class CloudyWeather : IWeather
    {
        public IWeather CalculateWeatherForPlain(Plain plain)
        {
            plain.WaterStored -= 1;
            Humidity += 5;
            if (plain.WaterStored > 15)
            {
                plain.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForGrassland(Grassland grassland)
        {
            grassland.WaterStored -= 2;
            Humidity += 10;
            if (grassland.WaterStored < 16)
            {
                grassland.Type = "P";
            }
            else if (grassland.WaterStored > 50)
            {
                grassland.Type = "L";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForLakesRegion(LakesRegion lakesRegion)
        {
            lakesRegion.WaterStored -= 3;
            Humidity += 15;
            if (lakesRegion.WaterStored > 51)
            {
                lakesRegion.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }


        private static CloudyWeather instance = null;
        public static CloudyWeather Instance()
        {
            if (instance == null)
            {
                instance = new CloudyWeather(Humidity);
            }
            return instance;
        }
        public static int Humidity { get; set; }
        public CloudyWeather(int humiditys)
        {
            Humidity = humiditys;
        }
    }

     public class RainyWeather : IWeather
    {
        public IWeather CalculateWeatherForPlain(Plain plain)
        {
            plain.WaterStored += 20;
            Humidity+= 5;  

            if (plain.WaterStored > 15)
            {
                plain.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForGrassland(Grassland grassland)
        {
            grassland.WaterStored += 15;
            Humidity += 10;
            if (grassland.WaterStored < 16)
            {
                grassland.Type = "P";
            }
            else if (grassland.WaterStored > 50)
            {
                grassland.Type = "L";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        public IWeather CalculateWeatherForLakesRegion(LakesRegion lakesRegion)
        {
            lakesRegion.WaterStored += 20;
            Humidity += 15;
            if (lakesRegion.WaterStored > 51)
            {
                lakesRegion.Type = "G";
            }
            return IWeather.weatherCalculate(Humidity);
        }

        private static RainyWeather instance = null;
        public static RainyWeather Instance()
        {
            if (instance == null)
            {
                instance = new RainyWeather(Humidity);
            }
            return instance;
        }
        public static int Humidity { get; set; }
        public RainyWeather(int humiditys)
        {
            Humidity = humiditys;
        }
    }
}

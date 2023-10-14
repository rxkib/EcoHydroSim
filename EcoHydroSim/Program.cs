using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using TextFile;
namespace EcoHydroSim
{
    public class Program
    {
        static void Main()
        {
            TextFileReader reader = new TextFileReader(fileName());


            List<Area> areas = ReadAreasFromFile(reader);
            int humidity = ReadHumidityFromFile(reader);

            List<Area> allAreas = new List<Area>();
            allAreas.AddRange(areas);

            IWeather weather = IWeather.weatherCalculate(humidity);

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < allAreas.Count; j++)
                {
                    allAreas[j].UpdateWeather(ref weather);
                    switch (allAreas[j].Type)
                    {
                        case "G":
                            allAreas[j] = new Grassland(allAreas[j].Owner, allAreas[j].WaterStored);
                            break;
                        case "P":
                            allAreas[j] = new Plain(allAreas[j].Owner, allAreas[j].WaterStored);
                            break;
                        case "L":
                            allAreas[j] = new LakesRegion(allAreas[j].Owner, allAreas[j].WaterStored);
                            break;
                    }
                }
                Console.WriteLine("Round " + i);
                PrintAreas(allAreas);
                Console.WriteLine(weather);
            }

            Area areaWithMaxWater = FindAreaWithMaxWater(allAreas);
            Console.WriteLine($"Owner of area with the maximum water: {areaWithMaxWater.Owner}, Water stored: {areaWithMaxWater.WaterStored} km3");
        }

        public static List<Area> ReadAreasFromFile(TextFileReader reader)
        {
            List<Area> areas = new List<Area>();

            if (reader.ReadLine(out string line))
            {
                int n = int.Parse(line);

                for (int i = 0; i < n; i++)
                {
                    if (reader.ReadLine(out line))
                    {
                        string[] tokens = line.Split(' ');

                        string owner = string.Join(" ", tokens[0], tokens[1]);
                        string type = tokens[2];
                        double waterStored = double.Parse(tokens[3]);

                        switch (type)
                        {
                            case "P":
                                areas.Add(new Plain(owner, waterStored));
                                break;
                            case "G":
                                areas.Add(new Grassland(owner, waterStored));
                                break;
                            case "L":
                                areas.Add(new LakesRegion(owner, waterStored));
                                break;
                        }
                    }
                }
            }

            return areas;
        }

        public static string fileName()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the file: ");
                    string fileName = Console.ReadLine();
                    TextFileReader reader = new(fileName);
                    return fileName;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File does not exist");
                }
            }
        }
        public static int ReadHumidityFromFile(TextFileReader reader)
        {
            if (reader.ReadLine(out string line))
            {
                return int.Parse(line);
            }

            return 0;
        }

        static void PrintAreas(List<Area> areas)
        {
            foreach (Area area in areas)
            {
                Console.WriteLine(area);
            }
        }

        public static Area FindAreaWithMaxWater(List<Area> areas)
        {
            Area maxWaterArea = null;
            double maxWater = 0;

            foreach (Area area in areas)
            {
                if (area.WaterStored > maxWater)
                {
                    maxWater = area.WaterStored;
                    maxWaterArea = area;
                }
            }

            return maxWaterArea;
        }
    }
}
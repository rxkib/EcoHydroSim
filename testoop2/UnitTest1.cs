using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OOPassign2;
using TextFile;

namespace testoop2
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void updateWeather_forPlain()
        {
            var plain = new Plain("Mr. A", 50);
            IWeather weather = new SunnyWeather(80);

            plain.UpdateWeather(ref weather);

            Assert.AreEqual(47, plain.WaterStored);
            Assert.AreEqual(85, SunnyWeather.Humidity);
            Assert.AreEqual("G", plain.Type);
        }

        [TestMethod]
        public void updateWeather_forGrass()
        {
            var grassland = new Grassland("Mr. B", 30);
            IWeather weather = new CloudyWeather(60);

            grassland.UpdateWeather(ref weather);

            Assert.AreEqual(28, grassland.WaterStored);
            Assert.AreEqual(70, CloudyWeather.Humidity);
            Assert.AreEqual("G", grassland.Type);
        }

        [TestMethod]
        public void updateWeather_forLake()
        {
            var lakesRegion = new LakesRegion("Mr. C", 70);
            IWeather weather = new RainyWeather(90);

            lakesRegion.UpdateWeather(ref weather);

            Assert.AreEqual(90, lakesRegion.WaterStored);
            Assert.AreEqual(30, RainyWeather.Humidity);
            Assert.AreEqual("G", lakesRegion.Type);

        }

        [TestMethod]
        public void areaWithMaxWater()
        {
            var areas = new List<Area>
            {
                new Plain("Mr. X", 20),
                new Grassland("Mr. Y", 35),
                new LakesRegion("Mr. Z", 50)
            };

            Area maxWaterArea = OOPassign2.Program.FindAreaWithMaxWater(areas);
            Assert.IsNotNull(maxWaterArea);
            Assert.AreEqual("Mr. Z", maxWaterArea.Owner);
            Assert.AreEqual(50, maxWaterArea.WaterStored);

        }

        [TestMethod]

        public void readAreasFromFile_returnList()
        {
            var reader = new TextFileReader("input.txt");
            List<Area> areas = OOPassign2.Program.ReadAreasFromFile(reader);

            Assert.IsNotNull(areas);
            Assert.AreEqual(4, areas.Count);
        }
    }
}
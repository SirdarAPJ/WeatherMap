namespace WeatherMap.Infraestructure.DataTransfer
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Coord Coord { get; set; }
    }

    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }
}

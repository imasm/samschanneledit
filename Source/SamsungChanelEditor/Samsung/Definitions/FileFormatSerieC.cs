namespace SamsChannelEditor.Samsung.Definitions
{
    internal class FileFormatSerieC : FileFormat
    {
        public FileFormatSerieC() : base("Serie-C")
        {
            Favorites = 4;
            MapAirD = new ChannelFormat292();
        }
    }
}

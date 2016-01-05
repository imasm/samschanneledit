using SamsChannelEditor.Samsung.Definitions;

namespace SamsChannelEditor.Samsung
{
    internal class FileFormatSerieC : FileFormat
    {
        public FileFormatSerieC() : base("Serie-C")
        {
            Favorites = 4;
            MapAirD = new ChannelFormat292();
        }
    }


    internal abstract class FileFormat
    {
        public string Id { get; protected set; }
        public int Favorites { get; protected set; }
        public ChannelFormat MapAirD { get; protected set; }

        protected FileFormat(string id)
        {
            Id = id;
        }
    }
}

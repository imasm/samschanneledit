namespace SamsChannelEditor.Samsung.Definitions
{
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
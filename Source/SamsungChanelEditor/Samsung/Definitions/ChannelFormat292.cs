namespace SamsChannelEditor.Samsung.Definitions
{
    internal class ChannelFormat292 : ChannelFormat
    {
        public ChannelFormat292() : base()
        {
            Length = 292;
            ProgramNumberOffset = 0;
            VideoPidOffset = 2;
            PcrPidOffset = 4;
            ServiceIdOffset = 6;
            DeletedOffset = 8;
            DeletedMask = 0x01;
            SignalSourceOffset = 10;
            QamOffset = 12;
            BandwidthOffset = 14;
            ServiceTypeOffset = 15;
            CodecOffset = 16;
            HResOffset = 20;
            VResOffset = 22;
            EncryptedOffset = 24;
            EncryptedMask = 0x01;
            FrameRateOffset = 25;
            SymbolRateOffset = 28;
            LockOffset = 31;
            LockMask = 0x01;
            OriginalNetworkIdOffset = 32;
            NetworkIdOffset = 34;
            ServiceProviderIdOffset = 40;
            ChannelTransponderOffset = 42;
            LogicalProgramNrOffset = 44;
            TransportStreamIdOffset = 48;
            NameOffset = 64;
            NameLength = 100;
            ShortNameOffset = 264;
            ShortNameLength = 18;
            VideoFormatOffset = 282;
            FavoritesOffset = 290;
            ChecksumOffset = 291;
        }
    }
}
#region Copyright (C) 2011-2017 Ivan Masmitjà

// Copyright (C) 2011-2017 Ivan Masmitjà
// 
// SamsChannelEditor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SamsChannelEditor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SamsChannelEditor. If not, see <http://www.gnu.org/licenses/>.

#endregion

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
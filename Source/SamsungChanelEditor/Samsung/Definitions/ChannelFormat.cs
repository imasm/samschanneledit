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

using System;
using System.Text;

namespace SamsChannelEditor.Samsung.Definitions
{
    internal class ChannelFormat
    {
        private byte[] data;

        public int Length { get; set; }
        public int ProgramNumberOffset { get; set; }
        public int VideoPidOffset { get; set; }
        public int PcrPidOffset { get; set; }
        public int ServiceIdOffset { get; set; }
        public int DeletedOffset { get; set; }
        public int DeletedMask { get; set; }
        public int SignalSourceOffset { get; set; }
        public int QamOffset { get; set; }
        public int BandwidthOffset { get; set; }
        public int ServiceTypeOffset { get; set; }
        public int CodecOffset { get; set; }
        public int HResOffset { get; set; }
        public int VResOffset { get; set; }
        public int EncryptedOffset { get; set; }
        public int EncryptedMask { get; set; }
        public int FrameRateOffset { get; set; }
        public int SymbolRateOffset { get; set; }
        public int LockOffset { get; set; }
        public int LockMask { get; set; }
        public int OriginalNetworkIdOffset { get; set; }
        public int NetworkIdOffset { get; set; }
        public int ServiceProviderIdOffset { get; set; }
        public int ChannelTransponderOffset { get; set; }
        public int LogicalProgramNrOffset { get; set; }
        public int TransportStreamIdOffset { get; set; }
        public int NameOffset { get; set; }
        public int NameLength { get; set; }
        public int ShortNameOffset { get; set; }
        public int ShortNameLength { get; set; }
        public int VideoFormatOffset { get; set; }
        public int FavoritesOffset { get; set; }
        public int ChecksumOffset { get; set; }


        protected ChannelFormat()
        {


            Length = -1;
            ProgramNumberOffset = -1;
            VideoPidOffset = -1;
            PcrPidOffset = -1;
            ServiceIdOffset = -1;
            DeletedOffset = -1;
            DeletedMask = 0x01;
            SignalSourceOffset = -1;
            QamOffset = -1;
            BandwidthOffset = -1;
            ServiceTypeOffset = -1;
            CodecOffset = -1;
            HResOffset = -1;
            VResOffset = -1;
            EncryptedOffset = -1;
            EncryptedMask = 0x01;
            FrameRateOffset = -1;
            SymbolRateOffset = -1;
            LockOffset = -1;
            LockMask = 0x01;
            OriginalNetworkIdOffset = -1;
            NetworkIdOffset = -1;
            ServiceProviderIdOffset = -1;
            ChannelTransponderOffset = -1;
            LogicalProgramNrOffset = -1;
            TransportStreamIdOffset = -1;
            NameOffset = -1;
            NameLength = -1;
            ShortNameOffset = -1;
            ShortNameLength = -1;
            VideoFormatOffset = -1;
            FavoritesOffset = -1;
            ChecksumOffset = -1;
        }



    }
}
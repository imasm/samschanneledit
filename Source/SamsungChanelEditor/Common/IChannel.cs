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

namespace SamsChannelEditor.Common
{
    internal interface IChannel
    {
        bool IsOk();
        int FilePosition { get; }
        byte[] Data { get; }

        short Number { get; set; }
        string Name { get; set; }
        string ChannelType { get; }
        bool IsEncrypted { get; }
        long Frequency { get; }
        ushort ServiceID { get; }
        ushort Multiplex_TSID { get; }
        ushort Multiplex_ONID { get; }
        ushort Network { get; }
        bool Deleted { get; set; }
        bool Active { get; set; }

        bool FavoriteList1 { get; set; }
        bool FavoriteList2 { get; set; }
        bool FavoriteList3 { get; set; }
        bool FavoriteList4 { get; set; }

        bool Locked { get; set; }

        byte CalcChecksum(bool save);
    }
}

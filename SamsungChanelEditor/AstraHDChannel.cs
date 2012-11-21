#region Copyright (C) 2011 Ivan Masmitja

// Copyright (C) 2011 Ivan Masmitja
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
using System.Collections.Generic;
using System.Text;

namespace SamsChannelEditor
{
  internal class AstraHDChannel : MapChannel
  {
    public AstraHDChannel(int pos, byte[] buffer)
      : base(pos, buffer)
    {
    }

    public override string Name
    {
      get
      {
        return Encoding.Unicode.GetString(base.Data, 0x31, 100);
      }
    }

    public override string ChannelType
    {
      get
      {
        return ((MapChannelType)base.Data[14]).ToString();
      }
    }

    public override ushort Multiplex_ONID
    {
        get { return BitConverter.ToUInt16(base.Data, 32); }
    }

    public override ushort Multiplex_TSID
    {
        get { return BitConverter.ToUInt16(base.Data, 36); }
    }

    public override ushort Network
    {
      get { return 0; }
    }

    public override ushort ServiceID
    {
        get { return BitConverter.ToUInt16(base.Data, 16); }
    }

    //TODO: Discover
    public override bool IsEncrypted
    {
      get { return (base.Data[180] == 1); }
    }
  }
}

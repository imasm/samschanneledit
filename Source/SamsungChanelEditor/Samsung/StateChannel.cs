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

using System.Text;

namespace SamsChannelEditor.Samsung
{
  internal class StateChannel : MapChannel
  {
    public StateChannel(int pos, byte[] buffer)
      : base(pos, buffer)
    {
    }

    public override string Name
    {
      get { return Encoding.Unicode.GetString(Data, 0x25, 100); }
    }

    public override string ChannelType
    {
      get { return ((MapChannelType) Data[0x0E]).ToString(); }
    }

    public override bool IsEncrypted
    {
      get { return false; } // return (data[24] == 1); }
    }
  }
}

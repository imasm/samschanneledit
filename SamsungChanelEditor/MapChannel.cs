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
using System.Text;

namespace SamsChannelEditor
{
  enum MapChannelType : byte
  {
    None = 0x00,
    TV = 0x01,
    Radio = 0x02,
    Data = 0x0C,
    HD = 0x19,
    Other = 0xFF, // wildcard for new models not supported
  }

  internal class MapChannel : IChannel
  {
    bool _isDeleted;

    public int FilePosition { get; set; }
    public bool Deleted { get { return _isDeleted; } set { _isDeleted = value; } }
    public bool Active { get { return !_isDeleted; } set { _isDeleted = !value; } }

    public byte[] Data { get; private set; }

    public MapChannel(int pos, byte[] buffer)
    {
      _isDeleted = false;
      FilePosition = pos;
      Data = (byte[])buffer.Clone();
    }

    public short Number
    {
      get
      {
        return BitConverter.ToInt16(Data, 0);
      }

      set
      {
        Int16 sh = value;
        byte[] b = BitConverter.GetBytes(sh);

        Data[0] = b[0];
        Data[1] = b[1];
      }
    }


    public virtual string Name
    {
      get
      {
        return StringUtils.RemoveNulls(Encoding.BigEndianUnicode.GetString(Data, 64, 100));
      }
      set
      {
        byte[] newName = Encoding.BigEndianUnicode.GetBytes(value);
        newName.CopyTo(Data, 64);
      }
    }


    public virtual string ChannelType
    {
      get
      {
        MapChannelType ct = GetMapChannelType(Data[15]);
        return ct != MapChannelType.Other ? ct.ToString() : "";
      }
    }

    public virtual bool IsEncrypted
    {
      get { return (Data[24] == 1); }
    }

    public virtual long Frequency
    {
      //TODO: Search Freq. field
      get { return 0; }
    }

    public virtual ushort Network
    {
      get { return BitConverter.ToUInt16(Data, 34); }
    }

    public virtual ushort ServiceID
    {
      get { return BitConverter.ToUInt16(Data, 6); }
    }

    public virtual ushort Multiplex_TSID
    {
      get { return BitConverter.ToUInt16(Data, 48); }
    }

    public virtual ushort Multiplex_ONID
    {
      get { return BitConverter.ToUInt16(Data, 32); }
    }

    public virtual bool FavoriteList1
    {
      get
      {
        if (Data.Length > 290)
          return ((Data[290] & 0x01) > 0);
        return false;
      }
      set
      {
        if (Data.Length <= 290)
          return;

        if (value)
        {
          Data[290] |= 0x01;
        }
        else
        {
          Data[290] &= 0xFE;
        }
      }
    }

    public virtual bool FavoriteList2
    {
      get
      {
        if (Data.Length > 290)
          return ((Data[290] & 0x02) > 0);
        return false;
      }
      set
      {
        if (Data.Length <= 290)
          return;

        if (value)
        {
          Data[290] |= 0x02;
        }
        else
        {
          Data[290] &= 0xFD;
        }
      }
    }

    public virtual bool FavoriteList3
    {
      get
      {
        if (Data.Length > 290)
          return ((Data[290] & 0x04) > 0);
        return false;
      }
      set
      {
        if (Data.Length <= 290)
          return;

        if (value)
        {
          Data[290] |= 0x04;
        }
        else
        {
          Data[290] &= 0xFB;
        }
      }
    }

    public virtual bool FavoriteList4
    {
      get
      {
        if (Data.Length > 290)
          return ((Data[290] & 0x08) > 0);
        return false;
      }
      set
      {
        if (Data.Length <= 290)
          return;

        if (value)
        {
          Data[290] |= 0x08;
        }
        else
        {
          Data[290] &= 0xF7;
        }
      }
    }

    public virtual bool Locked
    {
      get
      {
        return Data[31] == 0x01;
      }
      set
      {
        if (value)
        {
          Data[31] = 0x01;
        }
        else
        {
          Data[31] = 0x00;
        }
      }
    }

    public virtual bool IsOk()
    {
      return (BitConverter.ToInt16(Data, 0) > 0);
    }

    public byte CalcChecksum()
    {
      return CalcChecksum(false);
    }

    public virtual byte CalcChecksum(bool saveindata)
    {
      byte ck = 0;
      for (int i = 0; i < Data.Length - 1; i++)
        ck += Data[i];

      if (saveindata)
        Data[Data.Length - 1] = ck;

      return ck;
    }

    protected MapChannelType GetMapChannelType(byte b)
    {
      try
      {
        return (MapChannelType)b;
      }
      catch
      {
        return MapChannelType.Other;
      }
    }
  }
}

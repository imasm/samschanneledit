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
  enum MapChannelType : byte
  {
    None = 0x00,
    TV = 0x01,
    Radio = 0x02,
    Data = 0x0C,
    HD = 0x19,
    Other = 0xFF, // wildcard for new models not supported
  }

  internal class MapChannel: IChannel
  {
    bool _isDeleted;

    public int FilePosition { get; set; }    
    public bool Deleted { get { return _isDeleted;} set {_isDeleted = value ;} }
    public bool Active { get { return !_isDeleted; } set { _isDeleted = !value; } }
  
    private byte[] data;

    public byte[] Data
    {
      get { return data; }
    }

    public MapChannel(int pos, byte[] buffer)
    {
      this._isDeleted = false;
      this.FilePosition = pos;
      this.data = (byte[])buffer.Clone();
    }

    public short Number
    {
      get
      {
        return BitConverter.ToInt16(data, 0);
      }

      set
      {
        Int16 sh = (Int16)value;
        byte[] b = BitConverter.GetBytes(sh);

        data[0] = b[0];
        data[1] = b[1];
      }
    }

    public virtual string Name
    {
      get
      {
        return StringUtils.RemoveNulls(Encoding.Unicode.GetString(data, 65, 100));
      }
    }

    public virtual string ChannelType
    {
      get
      {
          MapChannelType ct = GetMapChannelType(data[15]);
          if (ct != MapChannelType.Other)
              return ct.ToString();
          else
              return "";
        
      }
    }

    public virtual bool IsEncrypted
    {
      get { return (data[24] == 1); }
    }

    public virtual long Frequency
    {
      //TODO: Search Freq. field
      get { return 0; }
    }

    public virtual ushort Network
    {
      get { return BitConverter.ToUInt16(data, 34); }
    }

    public virtual ushort ServiceID
    {
      get { return BitConverter.ToUInt16(data, 6); }
    }

    public virtual ushort Multiplex_TSID
    {
      get { return BitConverter.ToUInt16(data, 48); }
    }

    public virtual ushort Multiplex_ONID
    {
      get { return BitConverter.ToUInt16(data, 32); }
    }

    public virtual  bool IsOk()
    {
      return (BitConverter.ToInt16(data, 0) > 0);
    }

    public byte CalcChecksum()
    {
      return CalcChecksum(false);
    }

    public virtual byte CalcChecksum(bool saveindata)
    {
      byte ck = 0;
      for (int i = 0; i < data.Length - 1; i++)
        ck += data[i];

      if (saveindata)
        data[data.Length - 1] = ck;

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

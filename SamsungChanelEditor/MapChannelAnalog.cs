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
  internal class MapChannelAnalog: IChannel
  {
    bool _isDeleted;
    short _number;

    public int FilePosition { get; set; }    
    public bool Deleted { get { return _isDeleted;} set {_isDeleted = value ;} }
    public bool Active { get { return !_isDeleted; } set { _isDeleted = !value; } }
  
    private byte[] data;

    public byte[] Data
    {
      get { return data; }
    }

    public MapChannelAnalog(int pos, byte[] buffer)
    {
      this._isDeleted = false;
      this._number = (short) pos;      
      this.FilePosition = pos;
      this.data = (byte[])buffer.Clone();
    }

    public short Number
    {
      get
      { return this._number; }
      set
      { this._number = value; }
    }

    public virtual string Name
    {
      get
      {
        return Encoding.Unicode.GetString(data, 21, 10);
      }
    }

    public virtual string ChannelType
    {
      get
      {
        return MapChannelType.TV.ToString(); ;
      }
    }

    public virtual bool IsEncrypted
    {
      get { return false; }
    }

    public virtual long Frequency
    {
      //TODO: Search Freq. field
      get { return 0; }
    }

    public virtual ushort Network
    {
      get { return 0; }
    }

    public virtual ushort ServiceID
    {
      get { return 0; }
    }

    public virtual ushort Multiplex_TSID
    {
      get { return 0; }
    }

    public virtual ushort Multiplex_ONID
    {
      get { return 0; }
    }

    public virtual bool FavoriteList1
    {
        get { return false; }
    }
    
    public virtual bool FavoriteList2
    {
        get { return false; }
    }
    
    public virtual bool FavoriteList3
    {
        get { return false; }
    }
    
    public virtual bool FavoriteList4
    {
        get { return false; }
    }
    
    public virtual bool IsOk()
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
  }  
}

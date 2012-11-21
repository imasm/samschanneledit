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
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SamsChannelEditor
{
  internal class ChannelList : List<IChannel>
  {
    private static bool ChannelIsOk(IChannel ch)
    {
      return (ch != null) && (ch.IsOk());
    }

    private static int CompareChanelByNumber(IChannel x, IChannel y)
    {
      if ((!ChannelIsOk(x)) || (!ChannelIsOk(y)))
      {
        if (!ChannelIsOk(x))
        {
          if (!ChannelIsOk(y))
            return 0;
          else
            return 1;
        }
        else
          return -1;
      }
      else
      {
        return x.Number.CompareTo(y.Number);
      }
    }

    private static int CompareChanelByFilePos(IChannel x, IChannel y)
    {
      if ((!ChannelIsOk(x)) || (!ChannelIsOk(y)))
      {
        if (!ChannelIsOk(x))
        {
          if (!ChannelIsOk(y))
            return 0;
          else
            return 1;
        }
        else
          return -1;
      }
      else
      {
        return x.FilePosition.CompareTo(y.FilePosition);
      }
    }


    private static int CompareChanelByName(IChannel x, IChannel y)
    {
      
      if ((!ChannelIsOk(x)) || (!ChannelIsOk(y)))
      {
        if (!ChannelIsOk(x))
        {
          if (!ChannelIsOk(y))
            return 0;
          else
            return 1;
        }
        else
          return -1;
      }
      else
      {
        return x.Name.CompareTo(y.Name);
      }
    }

    public void SortByChanelNum()
    {
      this.Sort(CompareChanelByNumber);
    }

    public void SortByFilePosition()
    {
      this.Sort(CompareChanelByFilePos);
    }

    public void SortByName()
    {
      this.Sort(CompareChanelByName);
    }

    public void SwapChannels(int idx1, int idx2)
    {
      IChannel ch1 = this[idx1];
      this[idx1] = this[idx2];
      this[idx2] = ch1;
    }

    internal void SaveOrderTo(string filename)
    {
      using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Unicode))
      {
        sw.Write("Number".PadRight(10));
        sw.Write("Name".PadRight(100));
        sw.Write("TSID ");
        sw.Write("ONID ");
        sw.WriteLine();

        this.SortByChanelNum();
        foreach (IChannel ch in this)
        {
          if (ch.IsOk() && (!ch.Deleted))
          {
            sw.Write(ch.Number.ToString().PadRight(10));
            sw.Write(ch.Name.ToString().PadRight(100));
            sw.Write(ch.Multiplex_TSID.ToString("X4") + " ");
            sw.Write(ch.Multiplex_ONID.ToString("X4") + " ");
            sw.WriteLine();
          }
        }

        sw.Flush();
        sw.Close();
      }      
    }

    internal int SetOrderFrom(string filename)
    {
      string linia;
      const short notusedix = 16383;
      short curix = notusedix;
      foreach (IChannel ch in this)
          ch.Number = curix++;

      curix = 0;
        
      char[] charsToTrim = {' ', '\t', ',', ';'};

      using (StreamReader sr = new StreamReader(filename, Encoding.Unicode))
      {
          if (sr.ReadLine() != null) // ignore first line
          {
              while ((linia = sr.ReadLine()) != null)
              {
                  int num = StringUtils.TryToInt32(StringUtils.Copy(linia, 0, 10), -1);
                  string nom = StringUtils.Copy(linia, 10, 100);
                  nom = nom.Trim(charsToTrim);

                  foreach (IChannel ch in this)
                      if (ch.Number >= notusedix && ch.Name.Equals(nom))
                      {
                          ch.Number = curix++;
                          break;
                      }
              }
          }
          sr.Close();
      }
      SortByChanelNum();
      return curix;
    }
  }
}

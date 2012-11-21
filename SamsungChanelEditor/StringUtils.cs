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
  internal static class StringUtils
  {
    public static string Reverse(string s)
    {
      char[] arr = s.ToCharArray();
      Array.Reverse(arr);
      return new string(arr);
    }

    public static string RemoveNulls(string s)
    {
      int pos = s.IndexOf('\0');

      if (pos > 0)
        return s.Substring(0, pos);
      else
        return s;
    }

    public static string Copy(string s, int start, int count)
    {
      if (start < 0)
        start = 0;

      if (start >= s.Length)
      {
        return "";
      }
      if ((start + count) > s.Length)
      {
        return s.Substring(start);
      }
      return s.Substring(start, count);
    }

    internal static int TryToInt32(string s, int defaultnum)
    {
      int nout = 0;
      if (Int32.TryParse(s, out nout))        
        return nout;
      else
        return defaultnum;
    }
  }
}

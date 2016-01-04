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

namespace SamsChannelEditor
{
  internal static class StringUtils
  {
    public static string Reverse(string s)
    {
      var arr = s.ToCharArray();
      Array.Reverse(arr);
      return new string(arr);
    }

    public static string RemoveNulls(string s)
    {
      var pos = s.IndexOf('\0');
      return pos > 0 ? s.Substring(0, pos) : s;
    }

    public static string Copy(string s, int start, int count)
    {
      if (start < 0)
        start = 0;

      if (start >= s.Length)
        return "";

      return (start + count) > s.Length ? s.Substring(start) : s.Substring(start, count);
    }

    internal static int TryToInt32(string s, int defaultnum)
    {
      int nout;
      return Int32.TryParse(s, out nout) ? nout : defaultnum;
    }
  }
}

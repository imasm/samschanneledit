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

using System.IO;

namespace SamsChannelEditor.Utils
{
  internal class FileUtils
  {
    public static long GetFileSize(string filename)
    {
      var fi = new FileInfo(filename);
      return fi.Length;
    }

    public static string GetTempDirectory()
    {
      var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
      while (Directory.Exists(path))
        path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

      Directory.CreateDirectory(path);
      return path;
    }
  }
}

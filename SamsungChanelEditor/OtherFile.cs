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
using System.Data;
using System.IO;
using log4net;


namespace SamsChannelEditor
{
  internal abstract class OtherFile
  {
    static ILog log = LogManager.GetLogger("OtherFile");

    public string FileName { get; set; }
    public SCMFileContentType MapType { get; private set; }
    public bool Changed { get; set; }

    public DataTable DataTable { get; private set; }

    public OtherFile(string filename, SCMFileContentType maptype)
    {
      MapType = maptype;
      Changed = false;
      FileName = filename;
      DataTable = CreateDataTable();
    }

    #region Abstract Methods
    public abstract bool ReadFile(string fullPathFileName);
    public abstract DataTable CreateDataTable();
    #endregion

    public void Clear()
    {
      DataTable.Rows.Clear();
    }

    public bool ReadFrom(string directory)
    {
      Changed = false;
      Clear();

      string fullPathFileName = Path.Combine(directory, this.FileName);

      if (log.IsDebugEnabled)
        log.Debug("Read MapFile " + fullPathFileName);

      if (!File.Exists(fullPathFileName))
        throw new FileLoadException(string.Format("File {0} not found", fullPathFileName), "Open file error");
      
      return ReadFile(fullPathFileName);
    }


  }
}

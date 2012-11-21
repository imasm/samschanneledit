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
using System.Data;
using System.IO;
using System.Text;

namespace SamsChannelEditor
{
  internal class SatDataBaseFile: OtherFile
  {
    byte[] _regtmp = new  byte[145];

    public SatDataBaseFile(string filename)
      :base(filename, SCMFileContentType.SatDataBase)
    {
    }

    public override DataTable CreateDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("#", typeof(int));
      dt.Columns.Add("Satellite", typeof(String));
      //dt.Columns.Add("Id", typeof(int));
      return dt;
    }

    public override bool ReadFile(string fullPathFileName)
    {
      int idx = 0;

      using (FileStream fs = File.Open(fullPathFileName, FileMode.Open))
      {
        while (fs.Read(_regtmp, 0, _regtmp.Length) == _regtmp.Length)
        {
          idx++;
          DataRow dr = DataTable.NewRow();
          dr[0] = idx;
          dr[1] = Encoding.Unicode.GetString(_regtmp, 0x0D, 50);
          //dr[2] = BitConverter.ToInt16(_regtmp, 0x05);
          DataTable.Rows.Add(dr);
        }
      }
      return true;
    }
  }
}

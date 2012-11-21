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
  internal class CloneInfoFile: OtherFile
  {
    byte[] _regtmp = new  byte[68];

    public CloneInfoFile(string filename, SCMFileContentType maptype)
      :base(filename, maptype)
    {
    }

    public override DataTable CreateDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("Country ID", typeof(String));
      dt.Columns.Add("TV Model", typeof(String));
      return dt;
    }

    public override bool ReadFile(string fullPathFileName)
    {
      string pais;
      string model;
      using (FileStream fs = File.Open(fullPathFileName, FileMode.Open))
      {
        int readed = fs.Read(_regtmp, 0, _regtmp.Length);
        if (readed > 0)
        {
          pais = StringUtils.Reverse(Encoding.ASCII.GetString(_regtmp, 0x00, 3));
          model = Encoding.ASCII.GetString(_regtmp, 0x04, 15);

          DataRow dr = DataTable.NewRow();
          dr[0] = pais;
          dr[1] = model;
          DataTable.Rows.Add(dr);
        }
      }
      return true;
    }
  }
}

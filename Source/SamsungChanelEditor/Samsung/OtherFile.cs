#region Copyright (C) 2011-2017 Ivan Masmitjà

// Copyright (C) 2011-2017 Ivan Masmitjà
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

using System.Data;
using System.IO;
using log4net;

namespace SamsChannelEditor.Samsung
{
    internal abstract class OtherFile
    {
        static readonly ILog LOG = LogManager.GetLogger("OtherFile");

        public string FileName { get; set; }
        public SCMFileContentType MapType { get; private set; }
        public bool Changed { get; set; }

        public DataTable DataTable { get; private set; }

        protected OtherFile(string filename, SCMFileContentType maptype)
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

            string fullPathFileName = Path.Combine(directory, FileName);

            if (LOG.IsDebugEnabled)
                LOG.Debug("Read MapFile " + fullPathFileName);

            if (!File.Exists(fullPathFileName))
                throw new FileLoadException(string.Format("File {0} not found", fullPathFileName), "Open file error");

            return ReadFile(fullPathFileName);
        }


    }
}

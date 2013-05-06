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
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using log4net;

namespace SamsChannelEditor
{
  internal class MyZipFile
  {
    static ILog log = LogManager.GetLogger("MyZipFile");

    string _zipfilename = "";
    ZipFile _zipfile = null;

    public string FileName
    {
      get { return _zipfilename; }
      set { _zipfilename = value; }
    }

    public MyZipFile(string filename)
    {
      FileName = filename;
      _zipfile = new ZipFile(filename);
    }

    public void Close()
    {
      if (_zipfile != null)
      {
        _zipfile.Close();
        _zipfile = null;
      }
      _zipfilename = "";
    }

    public string ExtractFile(string fileinzip)
    {
      string fout = "";

      if (log.IsDebugEnabled)
        log.Debug("Extract file " + fileinzip + " from " + this.FileName);

      ZipEntry zentry = _zipfile.GetEntry(fileinzip);

      if (zentry == null)
        return "";

      string directoryName = Path.GetDirectoryName(zentry.Name);
      string fileName = Path.GetFileName(zentry.Name);

      string temppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      // create directory
      if (directoryName != "")
        directoryName = Path.Combine(temppath, directoryName);
      else
        directoryName = temppath;

      if (directoryName != "" && (!Directory.Exists(directoryName)))
        Directory.CreateDirectory(directoryName);
      
      if (fileName != String.Empty)
      {
        fout = Path.Combine(directoryName, fileName);
        byte[] buffer = new byte[4096];
        Stream zipStream = _zipfile.GetInputStream(zentry);
        using (FileStream streamWriter = File.Create(fout))
        {
          StreamUtils.Copy(zipStream, streamWriter, buffer);
        }

        if (log.IsDebugEnabled)
          log.Debug("File extracted: " + fout);
      }

      return fout;
    }

    public string ExtractFiles()
    {
      string fout = "";
      //string temppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      string temppath = FileUtils.GetTempDirectory();


      if (log.IsDebugEnabled)
        log.Debug("Extract files from " + this.FileName);

      foreach (ZipEntry zentry in _zipfile)
      {

        if (zentry == null)
          return "";

        string directoryName = Path.GetDirectoryName(zentry.Name);
        string fileName = Path.GetFileName(zentry.Name);

        // create directory
        if (directoryName != "")
          directoryName = Path.Combine(temppath, directoryName);
        else
          directoryName = temppath;

        if (directoryName != "" && (!Directory.Exists(directoryName)))
          Directory.CreateDirectory(directoryName);

        if (fileName != String.Empty)
        {
          fout = Path.Combine(directoryName, fileName);
          byte[] buffer = new byte[4096];
          Stream zipStream = _zipfile.GetInputStream(zentry);
          using (FileStream streamWriter = File.Create(fout))
          {
            StreamUtils.Copy(zipStream, streamWriter, buffer);
          }

          if (log.IsDebugEnabled)
            log.Debug("File extracted: " + fout);
        }
      }

      return temppath;
    }

    public void AddFile(string fileinzip)
    {
      //ZipFile zipFile = new ZipFile(_zipfilename);

      // Must call BeginUpdate to start, and CommitUpdate at the end.
      _zipfile.BeginUpdate();
      // The "Add()" method will add or overwrite as necessary.
      // When the optional entryName parameter is omitted, the entry will be named
      // with the full folder path and without the drive e.g. "temp/folder/test1.txt".
      _zipfile.Add(Path.GetFileName(fileinzip));

      // Both CommitUpdate and Close must be called.
      _zipfile.CommitUpdate();

      if (log.IsDebugEnabled)
        log.Debug(fileinzip + " added to " + this.FileName);
    }

    public List<string> ListAllFiles()
    {
      ZipFile zf = new ZipFile(_zipfilename);
      List<string> list = new List<string>();
      foreach (ZipEntry z in zf)
      {
        if (z.IsFile)
          list.Add(z.Name);
      }
      zf.Close();
      return list;
    }
  }
}

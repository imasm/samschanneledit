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
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using log4net;

namespace SamsChannelEditor
{
  internal class MyZipFile
  {
    static readonly ILog LOG = LogManager.GetLogger("MyZipFile");

    string _zipfilename = "";
    ZipFile _zipfile;

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

      if (LOG.IsDebugEnabled)
        LOG.Debug("Extract file " + fileinzip + " from " + FileName);

      var zentry = _zipfile.GetEntry(fileinzip);
      if (zentry == null)
        return "";

      var directoryName = Path.GetDirectoryName(zentry.Name);
      var fileName = Path.GetFileName(zentry.Name);
      var temppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      
      // create directory
      if (string.IsNullOrEmpty(directoryName))
        directoryName = temppath;
      else
        directoryName = Path.Combine(temppath, directoryName);

      if (directoryName != "" && (!Directory.Exists(directoryName)))
        Directory.CreateDirectory(directoryName);
      
      if (!string.IsNullOrEmpty(fileName))
      {
        fout = Path.Combine(directoryName, fileName);
        var buffer = new byte[4096];
        var zipStream = _zipfile.GetInputStream(zentry);
        using (var streamWriter = File.Create(fout))
        {
          StreamUtils.Copy(zipStream, streamWriter, buffer);
        }

        if (LOG.IsDebugEnabled)
          LOG.Debug("File extracted: " + fout);
      }

      return fout;
    }

    public string ExtractFiles()
    {
      //string temppath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      var temppath = FileUtils.GetTempDirectory();

      if (LOG.IsDebugEnabled)
        LOG.Debug("Extract files from " + FileName);

      foreach (ZipEntry zentry in _zipfile)
      {
        if (zentry == null)
          return "";

        var directoryName = Path.GetDirectoryName(zentry.Name);
        var fileName = Path.GetFileName(zentry.Name);

        // create directory
        if (string.IsNullOrEmpty(directoryName))
          directoryName = temppath;
        else
          directoryName = Path.Combine(temppath, directoryName);

        if (directoryName != "" && (!Directory.Exists(directoryName)))
          Directory.CreateDirectory(directoryName);

        if (!string.IsNullOrEmpty(fileName))
        {
          var fout = Path.Combine(directoryName, fileName);
          var buffer = new byte[4096];
          
          var zipStream = _zipfile.GetInputStream(zentry);
          using (var streamWriter = File.Create(fout))
          {
            StreamUtils.Copy(zipStream, streamWriter, buffer);
          }

          if (LOG.IsDebugEnabled)
            LOG.Debug("File extracted: " + fout);
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
      _zipfile.Add(fileinzip, Path.GetFileName(fileinzip));

      // Both CommitUpdate and Close must be called.
      _zipfile.CommitUpdate();

      if (LOG.IsDebugEnabled)
        LOG.Debug(fileinzip + " added to " + FileName);
    }

    public List<string> ListAllFiles()
    {
      var zf = new ZipFile(_zipfilename);
      var list = new List<string>();
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

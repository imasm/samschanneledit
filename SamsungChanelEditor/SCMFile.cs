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
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using log4net;

namespace SamsChannelEditor
{
  public enum SCMFileContentType
  {
    unknown = -1,
    mapAirA,
    mapAirD,
    mapCableA,
    mapCableD,
    mapCDTVVD,
    mapSateD,
    mapAstraHDPlusD,
    CloneInfo,
    SatDataBase,
  }

  internal class SCMFileContentInfo
  {
    public string Filename { get; private set; }
    public SCMFileContentType ContentType { get; private set; }
    public bool IsChannelListFile { get; private set; }

    public SCMFileContentInfo(string filename, SCMFileContentType contenttype, bool ischannellist)
    {
      Filename = filename;
      ContentType = contenttype;
      IsChannelListFile = ischannellist;
    }
  }

  internal class SCMFile
  {
    static ILog log = LogManager.GetLogger("SCMFile");

    #region Static methods
    static Dictionary<string, SCMFileContentInfo> SupportedFiles = new Dictionary<string, SCMFileContentInfo>()
    {
      {"cloneinfo", new SCMFileContentInfo("cloneinfo", SCMFileContentType.CloneInfo, false) },
      {"map-aird", new SCMFileContentInfo("map-aird", SCMFileContentType.mapAirD, true) },
      {"map-aira", new SCMFileContentInfo("map-aira", SCMFileContentType.mapAirA, true) },
      {"map-cablea", new SCMFileContentInfo("map-cablea", SCMFileContentType.mapCableA, true) },
      {"map-cabled", new SCMFileContentInfo("map-cabled", SCMFileContentType.mapCableD, true) },
      {"map-cdtvvd", new SCMFileContentInfo("map-cdtvvd", SCMFileContentType.mapCDTVVD, true) },
      {"map-sated", new SCMFileContentInfo("map-sated", SCMFileContentType.mapSateD, true) },
      {"map-astrahdplusd", new SCMFileContentInfo("map-astrahdplusd", SCMFileContentType.mapAstraHDPlusD, true) },
      {"satdatabase.dat", new SCMFileContentInfo("satdatabase.dat", SCMFileContentType.SatDataBase, false) },      
    };

    internal static bool IsChannelMapFile(string filename)
    {
      string f = Path.GetFileName(filename).ToLower();

      if (SupportedFiles.ContainsKey(f))
        return SupportedFiles[f].IsChannelListFile;
      else
        return false;
    }
   
    public static bool IsSupportedFile(string filename)
    {
      return (GetFileType(filename) != SCMFileContentType.unknown);
    }

    public static SCMFileContentType GetFileType(string filename)
    {
      string f = Path.GetFileName(filename).ToLower();

      if (SupportedFiles.ContainsKey(f))
        return SupportedFiles[f].ContentType;
      else
        return SCMFileContentType.unknown;
    }

    public static bool IsSCMFile(string filename)
    {
      return (Path.GetExtension(filename).ToLower() == ".scm");
    }
    #endregion

    string _filename;
    string _tempDirectory;
    MyZipFile _zipfile;
    string[] _filesInside;
    Dictionary<SCMFileContentType, MapFile> _mapfiles;
    Dictionary<SCMFileContentType, OtherFile> _otherfiles;

    public string FileName { get { return _filename; } }
    
    public SCMFile(string filename)
    {
      if (log.IsDebugEnabled)
        log.Debug("Open SCM File " + filename);

      _filename = filename;
      _zipfile = new MyZipFile(filename);
      _tempDirectory = _zipfile.ExtractFiles();
      _filesInside = GetContentFiles();
      _mapfiles = new Dictionary<SCMFileContentType, MapFile>();
      _otherfiles = new Dictionary<SCMFileContentType, OtherFile>();
    }

    private string[] GetContentFiles()
    {
      string[] files = Directory.GetFiles(_tempDirectory);
      for (int i = 0; i < files.Length; i++)
        files[i] = Path.GetFileName(files[i]);

      return files;
    }

    public void Close()
    {
      _filename = "";
      _tempDirectory = "";
      _zipfile.Close();      
    }

    public string[] GetAllFiles()
    {
      return (string[]) _filesInside.Clone();
    }

    public string[] GetSupportedFiles()
    {
      List<string> l = new List<string>();
      foreach (string f in _filesInside)
      {
        if (SCMFile.IsSupportedFile(f))
          l.Add(f);
      }

      return l.ToArray();
    }

    public MapFile GetMapFile(string filename)
    {
      MapFile mapFile = null;

      SCMFileContentType filetype = SCMFile.GetFileType(filename);

      if (log.IsInfoEnabled)
        log.Debug("Get MapFile " + filetype);

      if (_mapfiles.ContainsKey(filetype))
        mapFile = _mapfiles[filetype];
      else
      {
        if (filetype != SCMFileContentType.unknown)
          mapFile = new MapFile(filename, filetype);

        if (mapFile != null)
        {
          mapFile.ReadFrom(_tempDirectory);
          _mapfiles.Add(filetype, mapFile);
        }
      }

      return mapFile;
    }

    internal OtherFile GetOtherFile(string filename)
    {
      OtherFile otherFile = null;

      SCMFileContentType filetype = SCMFile.GetFileType(filename);

      if (log.IsInfoEnabled)
        log.Debug("Get Other File " + filetype);

      if (_otherfiles.ContainsKey(filetype))
        otherFile = _otherfiles[filetype];
      else
      {
        switch (filetype)
        {
          case SCMFileContentType.CloneInfo:
            otherFile = new CloneInfoFile(filename, filetype);
            break;
          case SCMFileContentType.SatDataBase:
            otherFile = new SatDataBaseFile(filename);
            break;
        }

        if (otherFile != null)
        {
          otherFile.ReadFrom(_tempDirectory);
          _otherfiles.Add(filetype, otherFile);
        }
      }

      return otherFile;
    }

    public void Save()
    {
      if (log.IsInfoEnabled)
        log.Info("Saving smv file");

      foreach (KeyValuePair<SCMFileContentType, MapFile> kvp in _mapfiles)
      {
        if (kvp.Value.Changed)
        {
          kvp.Value.SaveTo(_tempDirectory);
          _zipfile.AddFile(Path.Combine(_tempDirectory, kvp.Value.FileName));
        }
      }
    }

    
  }
}

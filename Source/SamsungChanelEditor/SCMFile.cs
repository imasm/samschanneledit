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

using System.Collections.Generic;
using System.IO;
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
    cloneInfo,
    satDataBase,
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
    static readonly ILog LOG = LogManager.GetLogger("SCMFile");

    #region Static methods
    static readonly Dictionary<string, SCMFileContentInfo> SUPPORTED_FILES = new Dictionary<string, SCMFileContentInfo>
    {
      {"cloneinfo", new SCMFileContentInfo("cloneinfo", SCMFileContentType.cloneInfo, false) },
      {"map-aird", new SCMFileContentInfo("map-aird", SCMFileContentType.mapAirD, true) },
      {"map-aira", new SCMFileContentInfo("map-aira", SCMFileContentType.mapAirA, true) },
      {"map-cablea", new SCMFileContentInfo("map-cablea", SCMFileContentType.mapCableA, true) },
      {"map-cabled", new SCMFileContentInfo("map-cabled", SCMFileContentType.mapCableD, true) },
      {"map-cdtvvd", new SCMFileContentInfo("map-cdtvvd", SCMFileContentType.mapCDTVVD, true) },
      {"map-sated", new SCMFileContentInfo("map-sated", SCMFileContentType.mapSateD, true) },
      {"map-astrahdplusd", new SCMFileContentInfo("map-astrahdplusd", SCMFileContentType.mapAstraHDPlusD, true) },
      {"satdatabase.dat", new SCMFileContentInfo("satdatabase.dat", SCMFileContentType.satDataBase, false) },      
    };

    internal static bool IsChannelMapFile(string filename)
    {
      if (string.IsNullOrEmpty(filename))
        return false;

      filename = Path.GetFileName(filename);
      if (string.IsNullOrEmpty(filename))
        return false;

      var f = filename.ToLower();

      return SUPPORTED_FILES.ContainsKey(f) && 
        SUPPORTED_FILES[f].IsChannelListFile;
    }

    public static bool IsSupportedFile(string filename)
    {
      return (GetFileType(filename) != SCMFileContentType.unknown);
    }

    public static SCMFileContentType GetFileType(string filename)
    {
      if (string.IsNullOrEmpty(filename))
        return SCMFileContentType.unknown;

      filename = Path.GetFileName(filename);
      if (string.IsNullOrEmpty(filename))
        return SCMFileContentType.unknown; 

      var f = filename.ToLower();

      return SUPPORTED_FILES.ContainsKey(f) ? SUPPORTED_FILES[f].ContentType : SCMFileContentType.unknown;
    }

    public static bool IsSCMFile(string filename)
    {
      if (string.IsNullOrEmpty(filename))
        return false;

      var extension = Path.GetExtension(filename);
      if (string.IsNullOrEmpty(extension))
        return false;

      return (extension.ToLower() == ".scm");
    }
    #endregion

    string _tempDirectory;
    readonly MyZipFile _zipfile;
    readonly string[] _filesInside;

    readonly Dictionary<SCMFileContentType, MapFile> _mapfiles;
    readonly Dictionary<SCMFileContentType, OtherFile> _otherfiles;

    public string FileName { get; private set; }

    public SCMFile(string filename)
    {
      if (LOG.IsDebugEnabled)
        LOG.Debug("Open SCM File " + filename);

      FileName = filename;
      _zipfile = new MyZipFile(filename);
      _tempDirectory = _zipfile.ExtractFiles();
      _filesInside = GetContentFiles();
      _mapfiles = new Dictionary<SCMFileContentType, MapFile>();
      _otherfiles = new Dictionary<SCMFileContentType, OtherFile>();
    }

    private string[] GetContentFiles()
    {
      var files = Directory.GetFiles(_tempDirectory);
      for (var i = 0; i < files.Length; i++)
        files[i] = Path.GetFileName(files[i]);

      return files;
    }

    public void Close()
    {
      FileName = "";
      _tempDirectory = "";
      _zipfile.Close();      
    }

    public string[] GetAllFiles()
    {
      return (string[]) _filesInside.Clone();
    }

    public string[] GetSupportedFiles()
    {
      var l = new List<string>();
      foreach (var f in _filesInside)
      {
        if (IsSupportedFile(f))
          l.Add(f);
      }

      return l.ToArray();
    }

    public MapFile GetMapFile(string filename)
    {
      MapFile mapFile = null;

      var filetype = GetFileType(filename);

      if (LOG.IsInfoEnabled)
        LOG.Debug("Get MapFile " + filetype);

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

      var filetype = GetFileType(filename);

      if (LOG.IsInfoEnabled)
        LOG.Debug("Get Other File " + filetype);

      if (_otherfiles.ContainsKey(filetype))
        otherFile = _otherfiles[filetype];
      else
      {
        switch (filetype)
        {
          case SCMFileContentType.cloneInfo:
            otherFile = new CloneInfoFile(filename, filetype);
            break;
          case SCMFileContentType.satDataBase:
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
      if (LOG.IsInfoEnabled)
        LOG.Info("Saving scm file");

      foreach (var kvp in _mapfiles)
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

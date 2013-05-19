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
using System.IO;
using log4net;
using System.Configuration;
using System.Collections.Specialized;

namespace SamsChannelEditor
{
  internal class MapFile: IChannelFile
  {
    static readonly ILog LOG = LogManager.GetLogger("MapFile");
    
    public int RegSize {get; private set; }
    public int RegCount {get; private set; }

    protected byte[] Regtmp { get; private set; }

    public ChannelList Channels { get; private set; }
    public string FileName { get; set; }
    public SCMFileContentType MapType { get; private set; }
    public bool Changed { get; set; }

    public int FileSize { get { return RegSize * RegCount; } }

    public MapFile(string filename, SCMFileContentType maptype)
    {
      MapType = maptype;
      Changed = false;
      FileName = filename;
      RegSize = 0;
      RegCount = 1000; // nº of register in a file

      Regtmp = null;
      Channels = new ChannelList();
    }

    public void Clear()
    {
      Channels.Clear();
    }

    public void ConvertToTxt(string filename)
    {
      using (var sw = new StreamWriter(filename))
      {
        foreach (IChannel ch in Channels)
        {
          sw.WriteLine(BitConverter.ToString(ch.Data, 0, ch.Data.Length));
          sw.WriteLine(ConvertToAscii(ch.Data, 0, ch.Data.Length));
        }
        sw.Close(); 
      }
    }

    private static string ConvertToAscii(byte[] bytes, int start, int length)
    {
      var sb = new StringBuilder();

      char[] chars = Encoding.ASCII.GetChars(bytes, start, length);
      for (int i = start; i < start + length; i++)
      {
        if (i != start)
          sb.Append("-");

        sb.Append(" ");
        if ((bytes[i] >= 32) && (bytes[i] < 254))
          sb.Append(chars[i - start]);
        else
          sb.Append(" ");
      }

      return sb.ToString();
    }

    private static int[] GetSizeInSettings(SCMFileContentType maptype)
    {
      int[] sizes = null;
      string key;

      if (maptype != SCMFileContentType.unknown)
         key = "fs_" + maptype;
      else
         key = "fs_default";

      try
      {
        NameValueCollection appSettings =
           ConfigurationManager.AppSettings;

        string settings = appSettings.Get(key);
        if (settings != null)
        {
          string[] strsizes = settings.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
          var valors = new List<int>();
          for (var i = 0; i < strsizes.Length; i++)
          {
            int size;
            if (int.TryParse(strsizes[i].Trim(), out size))
              valors.Add(size);
            else
              if (LOG.IsErrorEnabled)
                LOG.Error("Bad file size in settings [" + key + "] : " + strsizes[i]);
          }

          if (valors.Count > 0)
            sizes = valors.ToArray();
        }
        // Get the AppSettings section elements.
      }
      catch (ConfigurationErrorsException e)
      {
        sizes = null;
        if (LOG.IsErrorEnabled)
          LOG.Error("Error reading settings [" + key + "] : ", e);
      }
      
      return sizes;
    }

    private int getRecordSize(SCMFileContentType maptype, long filelen)
    {
      int[] sizes = GetSizeInSettings(maptype);
      if (sizes != null)
      {
        foreach (var recordsize in sizes)
          if (filelen % (recordsize * 1000) == 0)
            return recordsize;
      }

      sizes = GetSizeInSettings(SCMFileContentType.unknown);
      if (sizes != null)
      {
        foreach (var recordsize in sizes)
          if (filelen % (recordsize * 1000) == 0)
            return recordsize;
      }

      return 0;
    }

    public bool ReadFrom(string directory)
    {
      Changed = false;

      string fullPathFileName = Path.Combine(directory, FileName);

      if (LOG.IsDebugEnabled)
        LOG.Debug("Read MapFile " + fullPathFileName);

      int index = 0;

      if (!File.Exists(fullPathFileName))
        throw new FileLoadException(string.Format("File {0} not found", fullPathFileName), "Open file error");

      long filelen = FileUtils.GetFileSize(fullPathFileName);
      RegSize = getRecordSize(MapType, filelen);

      /*
      switch (MapType)
      {
        case SCMFileContentType.mapAirA:
        case SCMFileContentType.mapCableA:
          recordsize = 40; // C Series
          if (filelen % (recordsize * 1000) == 0)
          {
            RegSize = recordsize;
            RegCount = (int)(filelen / recordsize);
          }
          else
          {
            recordsize = 64; // D Series
            if (filelen % (recordsize * 1000) == 0)
            {
              RegSize = recordsize;
              RegCount = (int)(filelen / recordsize);
            }
          }
          break;

        case SCMFileContentType.mapSateD:
          recordsize = 172; // 172 multiple
          if (filelen % (recordsize * 1000) == 0) 
          {
            RegSize = recordsize;
            RegCount = (int)(filelen / recordsize);
          }
          else
          {
            recordsize = 144; // LE37C670 ??
            if (filelen % (recordsize * 1000) == 0)
            {
              RegSize = recordsize;
              RegCount = (int)(filelen / recordsize);
            }
          }
          break;

        case SCMFileContentType.mapAstraHDPlusD:
          recordsize = 212; // 212 multiple
          if (filelen % (recordsize * 1000) == 0) 
          {
            RegSize = recordsize;
            RegCount = (int)(filelen / recordsize);
          }
          break;
        
        default:
          recordsize = 292; // C Series
          if (filelen % (recordsize * 1000) == 0) 
          {
            RegSize = recordsize;
            RegCount = (int)(filelen / recordsize);
          }
          else
          {
            recordsize = 320; // D Series
            if (filelen % (recordsize * 1000) == 0) 
            {
              RegSize = recordsize;
              RegCount = (int)(filelen / recordsize);
            }
          }
          break;
      }*/

      if (RegSize == 0)
        throw new FileLoadException(string.Format("Bad file size ({0}).", filelen), "Open file error");

      RegCount = (int)(filelen / RegSize);

      Regtmp = new byte[RegSize];

      Channels.Clear();
      using (FileStream fs = File.Open(fullPathFileName, FileMode.Open))
      {
        int readed = fs.Read(Regtmp, 0, Regtmp.Length);
        while (readed > 0)
        {
          IChannel ch;

          switch (MapType)
          {
            case SCMFileContentType.mapSateD:
              ch = new StateChannel(index++, Regtmp);
              break;

            case SCMFileContentType.mapAstraHDPlusD:
              ch = new AstraHDChannel(index++, Regtmp);
              break;

            case SCMFileContentType.mapAirA:
            case SCMFileContentType.mapCableA:
              ch = new MapChannelAnalog(index++, Regtmp);
              break;

            default:
              ch = new MapChannel(index++, Regtmp);
              break;
          }

#if DEBUG
          // Check if current checksums are Ok 
          byte bcalculated = ch.CalcChecksum(false);
          byte breal = ch.Data[RegSize - 1];
          if (bcalculated != breal)
            throw new InvalidDataException(string.Format("Checksum error: Expected {0:x2} and current is {1:x2}", bcalculated, breal));
#endif
          if (ch.IsOk())
            Channels.Add(ch);

          readed = fs.Read(Regtmp, 0, Regtmp.Length);
        }
        fs.Close();
      }
      return true;
    }

    public bool SaveTo(string directory)
    {
      if (RegSize == 0)
        throw new NotSupportedException("Regsize is 0, Load a file before save it");
      
      string fullPathFileName = Path.Combine(directory, FileName);

      if (LOG.IsDebugEnabled)
        LOG.Debug("Saving " + fullPathFileName);


      if ((MapType == SCMFileContentType.mapAirA) || (MapType == SCMFileContentType.mapCableA))
      {
        // In Analog files write channels in the specified order
        Channels.SortByChanelNum();
      }
      else
      {
        // Sort de channels as it appear in original file. 
        Channels.SortByFilePosition();
      }

      //Save (Overwrite if it exists)
      int idx = 0;
      using (var fs = File.Open(fullPathFileName, FileMode.Create))
      {
        foreach (IChannel ch in Channels)
        {
          if (ch.Deleted) 
            continue;

          fs.Write(ch.Data, 0, ch.Data.Length);
          idx++;
        }

        // Fill the file with NULL registers
        for (var i = 0; i < Regtmp.Length; i++)
          Regtmp[i] = 0;

        for (var i = idx; i < RegCount; i++)
          fs.Write(Regtmp, 0, Regtmp.Length);

        fs.Close();
      }

      if (LOG.IsDebugEnabled)
        LOG.Debug(Regtmp.Length + "channels saved.");

      return true;
    }


  }
}

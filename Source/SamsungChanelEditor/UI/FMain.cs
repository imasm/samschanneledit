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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using log4net;

namespace SamsChannelEditor.UI
{
  public partial class FMain : Form
  {
    static readonly ILog LOG = LogManager.GetLogger("Main");

    const string SCM_FILTER = "scm file (*.scm)|*.scm";
    const string ALL_FILTER = "All files (*.*)|*.*";

    MapFile _mapFile;
    OtherFile _otherFile;
    string _currentFilename = "";
    SCMFile _scmFile;

    public FMain()
    {
      InitializeComponent();

#if !DEBUG
      mnDebug.Visible = false;
#endif

      statusLabel.Text = "";      
    }

    private void mnLoad_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog
        {
          Filter = SCM_FILTER + "|" + ALL_FILTER, 
          FilterIndex = 0, 
          Multiselect = false
        };

      if (ofd.ShowDialog() != DialogResult.OK) 
        return;

      CloseAll();

      _currentFilename = ofd.FileName;
      ucSingleEdit1.Clear();

      WriteStatus("Loading file...");

      if (SCMFile.IsSCMFile(ofd.FileName))
      {
        try
        {
          Cursor.Current = Cursors.WaitCursor;
          _scmFile = new SCMFile(ofd.FileName);
          RefreshTreeview();
          WriteStatus(ofd.FileName);
        }
        catch (Exception ex)
        {
          Cursor.Current = Cursors.Default;
          ShowError(ex, "Open file");
          CloseAll();
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }
      }
      else
        ShowInfo("Incorrect File extension : " + _currentFilename, "Open file");
    }

    private void RefreshTreeview()
    {
      treeView1.Nodes.Clear();
      if (_scmFile == null)
        return;

      TreeNode parentTreeNode = treeView1.Nodes.Add(Path.GetFileName(_scmFile.FileName));
      string[] allFiles = _scmFile.GetAllFiles();

      foreach (string f in allFiles)
      {
        TreeNode treeNode = parentTreeNode.Nodes.Add(f);
        if (!SCMFile.IsSupportedFile(f))
          treeNode.ForeColor = Color.LightGray;
      }

      treeView1.ExpandAll();
      if (parentTreeNode.Nodes.Count > 0)
        treeView1.SelectedNode = parentTreeNode.Nodes[0];
      else
        CloseAll();
    }

    private void CloseAll()
    {
      if (_scmFile != null)
      {
        _scmFile.Close();
        _scmFile = null;
      }
      _currentFilename = "";
      statusLabel.Text = "";
    }

    private void mnAbout_Click(object sender, EventArgs e)
    {
      FAboutBox.Show();
    }

    private void WriteChanels()
    {

      if (_mapFile == null)
        return;

      ucSingleEdit1.WriteChannelsMapFile(_mapFile);
      WriteStatus(string.Format("{0} : {1} channels", _mapFile.FileName, _mapFile.Channels.Count));
    }

    private void WriteOtherFile()
    {      
      if (_otherFile == null)
        return;
      ucSingleEdit1.WriteOtherFile(_otherFile);
      
      WriteStatus(string.Format("{0} ", _otherFile.FileName));
    }

    private bool OpenMapFile(string filename)
    {
      try
      {
        _mapFile = _scmFile.GetMapFile(filename);
      }
      catch (Exception ex)
      {
        ShowError(ex, "Open Map file");
        return false;
      }

      return (_mapFile != null);
    }

    private bool OpenOtherFile(string filename)
    {
      try
      {
        _otherFile = _scmFile.GetOtherFile(filename);
      }
      catch (Exception ex)
      {
        ShowError(ex, "Open file");
        return false;
      }

      return (_otherFile != null);
    }     
    
    private void mnRefresh_Click(object sender, EventArgs e)
    {
      WriteChanels();
    }

    private void mnSave_Click(object sender, EventArgs e)
    {
      if (_mapFile == null) return;

      if (_mapFile.Channels.Count == 0)
      {
        ShowError("Channel list is empty", "Error");
        return;
      }

      if ((_scmFile == null) || (_scmFile.FileName == "")) 
        return;

      if (Save())
      {
        if (statusLabel.Text.StartsWith("*"))
          statusLabel.Text = statusLabel.Text.Substring(1);
      }
    }
     
    private bool Save()
    {
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        ucSingleEdit1.SaveState();
        _scmFile.Save();
      }
      catch (Exception ex)
      {
        Cursor.Current = Cursors.Default;
        ShowError(ex, "Save file error");
        return false;
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }

      return true;
    }

    private void mnExit_Click(object sender, EventArgs e)
    {
      CloseAll();
      Application.Exit();
    }

    private void FMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      CloseAll();
    }   

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if ((treeView1.Nodes.Count <= 0) || (e.Node == treeView1.Nodes[0])) 
        return;

      _currentFilename = e.Node.Text;

      try
      {
        Cursor.Current = Cursors.WaitCursor;

        // Reorder current map file if its loaded
        if (_mapFile != null)
          ucSingleEdit1.SaveState();

        ucSingleEdit1.Clear();

        _mapFile = null;
        _otherFile = null;

        if (SCMFile.IsSupportedFile(_currentFilename))
        {
          if (SCMFile.IsChannelMapFile(_currentFilename))
          {
            WriteStatus("Reading channels...");
            if (OpenMapFile(_currentFilename))
              WriteChanels();
          }
          else
          {
            WriteStatus("Reading file...");
            if (OpenOtherFile(_currentFilename))
              WriteOtherFile();
          }
        }
        else
          WriteStatus("File not supported.");
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }  

   

    #region ErrorMessages
    private void ShowError(string message, string caption)
    {
      if (LOG.IsErrorEnabled)
        LOG.Error(caption + ": " + message);

      MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ShowError(Exception ex, string caption)
    {
      if (LOG.IsErrorEnabled)
        LOG.Error(caption, ex);

      MessageBox.Show(this, ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ShowInfo(string message, string caption)
    {
      if (LOG.IsInfoEnabled)
        LOG.Info(caption + ": " + message);

      MessageBox.Show(this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    #endregion

    #region Debug Functions
    private void mnConvertToTxt_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      if (ofd.ShowDialog() == DialogResult.OK)
      {
        MapFile mapfile = new MapFile(Path.GetFileName(ofd.FileName), SCMFileContentType.unknown);
        mapfile.ReadFrom(Path.GetDirectoryName(ofd.FileName));
        mapfile.ConvertToTxt(Path.ChangeExtension(ofd.FileName, ".txt"));
      }
    }

    #endregion
    
    public void WriteStatus(string mesg)
    {
      if (!InvokeRequired)
      {
        statusLabel.Text = mesg;
        statusStrip1.Refresh();
      }
      else
      {
        Action<string> req = WriteStatus;
        req.Invoke(mesg);
      }
    }

    private void FMain_KeyDown(object sender, KeyEventArgs e)
    {
      if ((e.KeyCode == Keys.F3) && (_mapFile != null))
      {
        switch (e.Modifiers)
        {
          case Keys.Control:
            var f = new FInputBox();
            if (f.Demana("Entre search text:", "Search :", ucSingleEdit1.GetLastSearchText()) == DialogResult.OK)
            {
              ucSingleEdit1.JumpToNext(f.Value);
              ucSingleEdit1.Focus();
            }
            break;

          case Keys.None:
            ucSingleEdit1.JumpToNext();
            break;
        }
      }
    }  
  }
}

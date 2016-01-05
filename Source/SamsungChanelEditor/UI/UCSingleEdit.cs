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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using SamsChannelEditor.Common;
using SamsChannelEditor.Samsung;

namespace SamsChannelEditor.UI
{
  internal partial class UCSingleEdit : UserControl
  {
    private readonly Font _deletedItemFont;
    private readonly Font _normalItemFont;
    private MapFile _currentMapFile;
    private bool _isChannelFile;

    public UCSingleEdit()
    {
      InitializeComponent();

      listView1.AllowColumnReorder = false;

      _normalItemFont = (Font)listView1.Font.Clone();
      _deletedItemFont = new Font(_normalItemFont.FontFamily, _normalItemFont.Size, FontStyle.Strikeout);
    }

    private void UCSingleEdit_Resize(object sender, EventArgs e)
    {
      if (listView1.Columns.Count > 0)
        listView1.Columns[listView1.Columns.Count - 1].Width = -2;
    }

    #region search

    internal string GetLastSearchText()
    {
      return jumpToToolStripMenuItem.Text;
    }

    private void NextToolStripMenuItemClick(object sender, EventArgs e)
    {
      JumpToNext(jumpToToolStripMenuItem.Text);
    }

    public void JumpToNext()
    {
      JumpToNext(jumpToToolStripMenuItem.Text);
    }

    public void JumpToNext(String name)
    {
      jumpToToolStripMenuItem.Text = name;

      var startPos = 0;
      if (listView1.SelectedIndices.Count >= 1)
        startPos = listView1.SelectedIndices[0] + 1;

      var found = false;
      for (var i = startPos; i <= listView1.Items.Count - 1; i++)
      {
        var lvi = listView1.Items[i];

        if (!lvi.Name.ToLower().Contains(name.ToLower()))
          continue;

        listView1.SelectedItems.Clear();
        listView1.Items[i].Selected = true;
        listView1.Items[i].EnsureVisible();
        found = true;
        break;
      }

      if (found || startPos == 0)
        return;

      for (var i = 0; i <= listView1.Items.Count - 1; i++)
      {
        var lvi = listView1.Items[i];
        if (!lvi.Name.ToLower().Contains(name.ToLower()))
          continue;

        listView1.SelectedItems.Clear();
        listView1.Items[i].Selected = true;
        listView1.Items[i].EnsureVisible();
        break;
      }
    }

    #endregion

    public void Clear()
    {
      listView1.Items.Clear();
      _currentMapFile = null;
      _isChannelFile = false;
    }

    public void SaveState()
    {
      if (_currentMapFile == null)
        return;

      short idx = 1;
      //Set channel number in sequential order
      foreach (ListViewItem lvi in listView1.Items)
      {
        var ch = (IChannel)lvi.Tag;

        if (ch == null)
          continue;

        if (ch.Deleted)
          continue;

        ch.Number = idx++;
        ch.CalcChecksum(true);
      }

      _currentMapFile.Changed = true;
    }

    #region Fill ListView

    /// <summary>
    /// Fill listview with Map file channels.
    /// Enable to Drag and Drop List View items and the connext menu
    /// </summary>
    /// <param name="mapFile">Map file</param>
    public void WriteChannelsMapFile(MapFile mapFile)
    {
      const string NUMBER_FORMAT = "X4";

      _currentMapFile = mapFile;
      var channels = mapFile.Channels;

      var showExtra = ((mapFile.MapType != SCMFileContentType.mapAirA) &&
                       (mapFile.MapType != SCMFileContentType.mapCableA));
      channels.SortByChanelNum();

      try
      {
        listView1.BeginUpdate();
        CreateChannelListView(showExtra);
        _isChannelFile = true;

        for (var i = 0; i < mapFile.Channels.Count; i++)
        {
          var ch = channels[i];

          if (!ch.IsOk())
            continue;

          ListViewItem lvi = new ListViewItem(ch.Number.ToString(CultureInfo.InvariantCulture)); // #
          lvi.Name = ch.Name;
          lvi.SubItems.Add(ch.Name); // Name
          lvi.SubItems.Add(ch.ChannelType); // Type
          lvi.SubItems.Add(ch.IsEncrypted ? "S" : ""); // Enc
          if (showExtra)
          {
            lvi.SubItems.Add(ch.Locked ? "1" : string.Empty);
            lvi.SubItems.Add(ch.Frequency.ToString(CultureInfo.InvariantCulture)); // Freq
            lvi.SubItems.Add(ch.ServiceID.ToString(NUMBER_FORMAT)); // sid
            lvi.SubItems.Add(ch.Multiplex_TSID.ToString(NUMBER_FORMAT)); // tsid
            lvi.SubItems.Add(ch.Multiplex_ONID.ToString(NUMBER_FORMAT)); // onid
            lvi.SubItems.Add(ch.Network.ToString(NUMBER_FORMAT)); // net
            lvi.SubItems.Add(ch.FavoriteList1 ? "1" : string.Empty);
            lvi.SubItems.Add(ch.FavoriteList2 ? "1" : string.Empty);
            lvi.SubItems.Add(ch.FavoriteList3 ? "1" : string.Empty);
            lvi.SubItems.Add(ch.FavoriteList4 ? "1" : string.Empty);
          }

#if DEBUG
          lvi.SubItems.Add(ch.FilePosition.ToString(CultureInfo.InvariantCulture)); // Position inside channel file
#endif

          lvi.Checked = !ch.Deleted;

          lvi.Tag = ch; // Store channel object in each item

          lvi.Font = ch.Deleted ? _deletedItemFont : _normalItemFont;

          listView1.Items.Add(lvi);

        }
      }
      finally
      {
        listView1.EndUpdate();
      }

      channels.SortByFilePosition();
    }

    /// <summary>
    /// Fill listview with data of a non channel file
    /// Disable Drag and Drop and the connext menu
    /// </summary>
    /// <param name="otherFile">Other file</param>
    public void WriteOtherFile(OtherFile otherFile)
    {
      _currentMapFile = null;
      try
      {
        listView1.BeginUpdate();
        CreateOtherFileListView(otherFile);
        _isChannelFile = false;

        DataTable dt = otherFile.DataTable;
        foreach (DataRow dr in dt.Rows)
        {
          var lvi = new ListViewItem(dr[0].ToString());
          for (var i = 1; i < dt.Columns.Count; i++)
            lvi.SubItems.Add(dr[i].ToString());

          listView1.Items.Add(lvi);
        }
      }
      finally
      {
        listView1.EndUpdate();
      }
    }

    #endregion

    #region Create ListViews

    private void CreateOtherFileListView(OtherFile otherfile)
    {
      listView1.Clear();
      DataTable dt = otherfile.DataTable;
      foreach (DataColumn dc in dt.Columns)
        listView1.Columns.Add(dc.ColumnName, 100);

      if (listView1.Columns.Count > 0)
        listView1.Columns[listView1.Columns.Count - 1].Width = -2;

      listView1.CheckBoxes = false;
      listView1.DragAndDropEnabled = false;
    }

    private void CreateChannelListView(bool viewExtra)
    {
      listView1.Clear();
      listView1.Columns.Add(typeof(MapChannel).GetProperty("Number").Name, "#", 80);
      listView1.Columns.Add(typeof(MapChannel).GetProperty("Name").Name, "Name", 150);
      listView1.Columns.Add(typeof(MapChannel).GetProperty("ChannelType").Name, "Type", 50);
      listView1.Columns.Add(typeof(MapChannel).GetProperty("IsEncrypted").Name, "Enc.", 50);

      if (viewExtra)
      {
        listView1.Columns.Add(typeof(MapChannel).GetProperty("Locked").Name, "Locked", 20);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("Frequency").Name, "Freq.", 50);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("ServiceID").Name, "ServiceId", 80);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("Multiplex_TSID").Name, "tsId", 60);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("Multiplex_ONID").Name, "onid", 60);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("Network").Name, "Network", 80);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("FavoriteList1").Name, "Fav1", 50);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("FavoriteList2").Name, "Fav2", 50);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("FavoriteList3").Name, "Fav3", 50);
        listView1.Columns.Add(typeof(MapChannel).GetProperty("FavoriteList4").Name, "Fav4", 50);
      }
#if DEBUG
      listView1.Columns.Add(typeof(MapChannel).GetProperty("FilePosition").Name, "Pos", 50);
#endif

      listView1.Columns[listView1.Columns.Count - 1].Width = -2;
      listView1.CheckBoxes = true;
      listView1.DragAndDropEnabled = true;
      _isChannelFile = true;
    }

    #endregion


    /// <summary>
    /// Renumber all channels in the listview
    /// </summary>
    private void RenumberChannels()
    {
      RenumberChannels(0, listView1.Items.Count);
    }

    /// <summary>
    /// Renumber all channels in the listview and if set remove encrypted 
    /// </summary>
    private void RenumberChannels(bool removeEncrypted)
    {
      RenumberChannels(0, listView1.Items.Count, removeEncrypted);
    }


    /// <summary>
    /// Renumber all channels from min to max and if set remove encrypted channels
    /// </summary>
    private void RenumberChannels(int min, int max, bool removeEncrypted = false)
    {
      var start = 0;
      var end = listView1.Items.Count - 1;

      if (min > max) // Swap
      {
        var temp = min;
        min = max;
        max = temp;
      }

      if ((min >= 0) && (max < listView1.Items.Count))
        start = min;

      if ((max >= 0) && (max < listView1.Items.Count))
        end = max;

      var idx = start + 1;
      //Set channel number in sequential order
      for (var i = start; i <= end; i++)
      {
        var lvi = listView1.Items[i];
        if (removeEncrypted)
        {
          var mc = lvi.Tag as MapChannel;
          if (mc != null)
          {
            if (mc.IsEncrypted) lvi.Checked = false;
          }
        }

        if (lvi.Checked)
        {
          lvi.Text = idx.ToString(CultureInfo.InvariantCulture);
          idx++;
        }
      }
    }

    #region ListView Drag and Drop Events

    private void listView1_AfterDragAndDrop(object sender, ListViewDragAndDrop.AfterDragAndDropEventArgs e)
    {
      RenumberChannels(e.MinIndex, e.MaxIndex);
    }

    #endregion

    private bool _isFirstChange = true;

    private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (listView1.IsUpdating)
        return;

      if ((ModifierKeys == Keys.Control) || (ModifierKeys == Keys.Shift))
      {
        if (_isFirstChange)
        {
          _isFirstChange = false;
          e.Item.Checked = !e.Item.Checked;
        }
        else
          _isFirstChange = true;

        return;
      }

      if (e.Item.Tag != null)
      {
        var ch = (IChannel)e.Item.Tag;
        ch.Deleted = !e.Item.Checked;
        e.Item.Font = (ch.Deleted ? _deletedItemFont : _normalItemFont);
      }

      //Set channel number in sequential order
      short idx = 1;
      foreach (ListViewItem lvi in listView1.Items)
      {
        if (lvi.Checked)
        {
          lvi.Text = idx.ToString(CultureInfo.InvariantCulture);
          idx++;
        }
        else
          lvi.Text = "";
      }
    }

    #region Context Menu

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      mnMoveTo.Enabled =
        mnRenumber.Enabled =
        mnSaveOrder.Enabled =
        mnRemoveEncrypted.Enabled =
        mnReorderFrom.Enabled = _isChannelFile;
    }

    private void mnMoveTo_Click(object sender, EventArgs e)
    {
      var f = new FInputBox();
      if (f.Demana("Position", "New position :", "0") != DialogResult.OK)
        return;

      int newpos;
      if (!int.TryParse(f.Value, out newpos))
      {
        MessageBox.Show("Invalid number");
        return;
      }

      if (newpos <= 0)
        newpos = 1;

      if (newpos > listView1.Items.Count)
        newpos = listView1.Items.Count;

      var minIndex = -1;
      var maxIndex = -1;
      var dragIndex = newpos - 1;

      var sel = new ListViewItem[listView1.SelectedItems.Count];
      for (var i = 0; i <= listView1.SelectedItems.Count - 1; i++)
        sel[i] = listView1.SelectedItems[i];

      for (var i = 0; i < sel.GetLength(0); i++)
      {
        var dragItem = sel[i];
        var itemIndex = dragIndex;

        if (dragItem.Index < itemIndex)
          itemIndex++;
        else
          itemIndex = dragIndex + i;

        var insertItem = (ListViewItem)dragItem.Clone();

        var oldidx = dragItem.Index;
        var newidx = itemIndex;

        if ((minIndex == -1) || (Math.Min(oldidx, newidx) < minIndex))
          minIndex = Math.Min(oldidx, newidx);

        if ((maxIndex == -1) || (Math.Max(oldidx, newidx) > maxIndex))
          maxIndex = Math.Max(oldidx, newidx);

        if (newpos >= listView1.Items.Count) // Add to end
          listView1.Items.Add(insertItem);
        else
          listView1.Items.Insert(itemIndex, insertItem); // insert into position

        listView1.Items.Remove(dragItem);
      }

      RenumberChannels(minIndex, maxIndex);
    }

    private void mnRenumber_Click(object sender, EventArgs e)
    {
      RenumberChannels();
    }

    private void mnSaveOrder_Click(object sender, EventArgs e)
    {
      if (_currentMapFile == null)
        return;

      var sfd = new SaveFileDialog
        {
          Filter = "Text file (*.txt)|*.txt",
          AddExtension = true
        };

      if (sfd.ShowDialog() == DialogResult.OK)
      {
        SaveState();
        _currentMapFile.Channels.SaveOrderTo(sfd.FileName);
      }
    }

    private void reorderFromToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (_currentMapFile == null)
        return;

      var sfd = new OpenFileDialog
        {
          Filter = "Text file (*.txt)|*.txt",
          AddExtension = true
        };

      if (sfd.ShowDialog() == DialogResult.OK)
      {
        var items = _currentMapFile.Channels.SetOrderFrom(sfd.FileName);
        SortListviewByItemNumbers();
        MessageBox.Show(String.Format("The first {0} item(s) were resorted.", items));
      }
    }

    private class ListViewItemComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        var chX = (IChannel)(((ListViewItem)x).Tag);
        var chY = (IChannel)(((ListViewItem)y).Tag);
        return chX.Number.CompareTo(chY.Number);
      }
    }

    private void SortListviewByItemNumbers()
    {
      IComparer old = listView1.ListViewItemSorter;
      listView1.ListViewItemSorter = new ListViewItemComparer();
      listView1.Sort();
      listView1.ListViewItemSorter = old;
      short idx = 1;
      //Set channel number in sequential order
      foreach (ListViewItem lvi in listView1.Items)
      {
        if (lvi.Checked)
        {
          lvi.Text = idx.ToString(CultureInfo.InvariantCulture);
          idx++;
        }
      }
    }

    private void mnRemoveEncrypted_Click(object sender, EventArgs e)
    {
      const string CAPTION = "Shure?";
      const string TEXT = "Do you really want to remove all encrypted Channels?" +
                          "\r\n \tAll Channels will be reordered";

      if (MessageBox.Show(TEXT, CAPTION, MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        RenumberChannels(true);
      }
    }

    #endregion

    #region Edit channel data
    private void EditListViewItem(ListViewHitTestInfo item)
    {
      if (item == null || item.SubItem == null)
      {
        return;
      }

      TextBox tbxEdit = new TextBox();
      tbxEdit.Parent = listView1;
      tbxEdit.Tag = item;	// Store clicked item
      tbxEdit.Location = new Point(item.SubItem.Bounds.Location.X, item.SubItem.Bounds.Location.Y - 1);
      tbxEdit.AutoSize = false;
      tbxEdit.Height = item.Item.Bounds.Height + 1;
      tbxEdit.Width = item.SubItem.Bounds.Width + 1;
      tbxEdit.BorderStyle = BorderStyle.FixedSingle;
      tbxEdit.KeyDown += new KeyEventHandler(tbxEdit_KeyDown);
      tbxEdit.LostFocus += new EventHandler(tbxEdit_LostFocus);
      tbxEdit.Text = item.SubItem.Text;
      tbxEdit.CreateControl();
      tbxEdit.Focus();
    }

    void tbxEdit_LostFocus(object sender, EventArgs e)
    {
      tbxEdit_KeyDown(sender as TextBox, new KeyEventArgs(Keys.Escape));
    }

    void tbxEdit_KeyDown(object sender, KeyEventArgs e)
    {
      TextBox tbxEdit = sender as TextBox;

      if (tbxEdit == null)
      {
        return;
      }

      if (e.KeyCode == Keys.Enter)
      {
        ListViewHitTestInfo infoitem = (tbxEdit.Tag as ListViewHitTestInfo);
        if ((infoitem == null) || (infoitem.Item == null))
        {
          return;
        }
        
        ListView lview = tbxEdit.Parent as ListView;
        if (lview == null)
        {
          return;
        }

        IChannel ch = infoitem.Item.Tag as IChannel;
        if (ch == null)
        {
          return;
        }

        
        int sidx = infoitem.Item.SubItems.IndexOf(infoitem.SubItem);	// get subitem index
        string lvcolumnKey = listView1.Columns[sidx].Name;

        try
        {
          lview.BeginUpdate();
          object newObj = null;
          switch (ch.GetType().GetProperty(lvcolumnKey).PropertyType.Name)
          {
            case "Boolean":
              if (tbxEdit.Text == "1")
              {
                newObj = true;
                tbxEdit.Text = "1";
              }
              else
              {
                newObj = false;
                tbxEdit.Text = "";
              }
              break;

            default:
              newObj = tbxEdit.Text;
              break;
          }
          infoitem.SubItem.Text = tbxEdit.Text;		// update edited subitem text
          lview.EndUpdate();

          ch.GetType().GetProperty(lvcolumnKey).SetValue(ch, newObj, null);
        }
        catch (ArgumentException ex)
        {
          MessageBox.Show(Properties.Resources.READONLYFIELD_EXCEPTION, Properties.Resources.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

#if DEBUG
        catch (System.Exception ex)
        {
          MessageBox.Show(ex.ToString());
        }
#endif
        finally
        {
          tbxEdit.LostFocus -= tbxEdit_LostFocus;
          tbxEdit.Dispose();
        }
      }
      else if (e.KeyCode == Keys.Escape)
      {
        tbxEdit.Dispose();
      }
    }

    private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      ListViewHitTestInfo info = listView1.HitTest(e.Location);

      Point p = listView1.PointToClient(Cursor.Position);

      if (p.X <= listView1.Columns[0].Width)
      {
        return;
      }

      // Only edit name and Fav. lists
      int sidx = info.Item.SubItems.IndexOf(info.SubItem);	// get subitem index
      string lvcolumnKey = listView1.Columns[sidx].Name;
      IChannel ch = info.Item.Tag as IChannel;
      if (ch != null)
      {
        if (IsEditable(ch.GetType().GetProperty(lvcolumnKey)))
          EditListViewItem(info);
      }
    }

    // Check for Editable attribute in property
    private static bool IsEditable(PropertyInfo property)
    {
      foreach (object attribute in property.GetCustomAttributes(true))
      {
        if (attribute is EditableAttribute)
        {
          return true;
        }
      }
      return false;
    }

    #endregion

    /// <summary>
    /// Change checked state only if item was clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if(listView1.IsUpdating)
        return;
        
      Point p = listView1.PointToClient(Cursor.Position);

      if (p.X > listView1.Columns[0].Width)
      {
        e.NewValue = e.CurrentValue;
      }
    }

  }
}

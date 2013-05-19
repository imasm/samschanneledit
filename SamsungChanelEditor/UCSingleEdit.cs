using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SamsChannelEditor
{
  internal partial class UCSingleEdit : UserControl
  {
    Font _deletedItemFont = null;
    Font _normalItemFont = null;
    MapFile _currentMapFile = null;
    bool _isChannelFile = false;

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

    void NextToolStripMenuItemClick(object sender, EventArgs e)
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

      int startPos = 0;
      if (listView1.SelectedIndices.Count >= 1)
      {
        startPos = listView1.SelectedIndices[0] + 1;
      }

      bool found = false;
      for (int i = startPos; i <= listView1.Items.Count - 1; i++)
      {
        ListViewItem lvi = listView1.Items[i];
        if (lvi.Name.ToLower().Contains(name.ToLower()))
        {
          listView1.SelectedItems.Clear();
          listView1.Items[i].Selected = true;
          listView1.Items[i].EnsureVisible();
          found = true;
          break;
        }
      }

      if (!found && startPos != 0)
      {
        for (int i = 0; i <= listView1.Items.Count - 1; i++)
        {
          ListViewItem lvi = listView1.Items[i];
          if (lvi.Name.ToLower().Contains(name.ToLower()))
          {
            listView1.SelectedItems.Clear();
            listView1.Items[i].Selected = true;
            listView1.Items[i].EnsureVisible();
            found = true;
            break;
          }
        }
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
        IChannel ch = (IChannel)lvi.Tag;
        if (ch == null)
          continue;

        if (!ch.Deleted)
        {
          ch.Number = idx++;
          ch.CalcChecksum(true);
        }
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
      string numberFormat = "X4";

#if DEBUG
      numberFormat = "";
#endif

      _currentMapFile = mapFile;
      ChannelList channels = mapFile.Channels;

      bool showExtra = ((mapFile.MapType != SCMFileContentType.mapAirA) && (mapFile.MapType != SCMFileContentType.mapCableA));
      channels.SortByChanelNum();

      try
      {
        listView1.BeginUpdate();
        CreateChannelListView(showExtra);
        _isChannelFile = true;

        for (int i = 0; i < mapFile.Channels.Count; i++)
        {
          IChannel ch = channels[i];
          if (ch.IsOk())
          {
            ListViewItem lvi = new ListViewItem(ch.Number.ToString()); // #
            lvi.Name = ch.Name;
            lvi.SubItems.Add(ch.Name); // Name
            lvi.SubItems.Add(ch.ChannelType); // Type
            lvi.SubItems.Add(ch.IsEncrypted ? "S" : ""); // Enc
            if (showExtra)
            {
			  lvi.SubItems.Add(ch.Locked ? "1" : string.Empty);
              lvi.SubItems.Add(ch.Frequency.ToString()); // Freq
              lvi.SubItems.Add(ch.ServiceID.ToString(numberFormat)); // sid
              lvi.SubItems.Add(ch.Multiplex_TSID.ToString(numberFormat)); // tsid
              lvi.SubItems.Add(ch.Multiplex_ONID.ToString(numberFormat)); // onid
              lvi.SubItems.Add(ch.Network.ToString(numberFormat)); // net
              lvi.SubItems.Add(ch.FavoriteList1 ? "1" : string.Empty);
              lvi.SubItems.Add(ch.FavoriteList2 ? "1" : string.Empty);
              lvi.SubItems.Add(ch.FavoriteList3 ? "1" : string.Empty);
              lvi.SubItems.Add(ch.FavoriteList4 ? "1" : string.Empty);
            }

#if DEBUG
            lvi.SubItems.Add(ch.FilePosition.ToString()); // Position inside channel file
#endif

            lvi.Checked = !ch.Deleted;

            lvi.Tag = ch; // Store channel object in each item

            if (ch.Deleted)
              lvi.Font = _deletedItemFont;
            else
              lvi.Font = _normalItemFont;

            listView1.Items.Add(lvi);
          }
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
    /// <param name="mapFile">Other file</param>
    public void WriteOtherFile(OtherFile _otherFile)
    {
      _currentMapFile = null;
      try
      {
        listView1.BeginUpdate();
        CreateOtherFileListView(_otherFile);
        _isChannelFile = false;

        DataTable dt = _otherFile.DataTable;
        foreach (DataRow dr in dt.Rows)
        {
          ListViewItem lvi = new ListViewItem(dr[0].ToString());
          for (int i = 1; i < dt.Columns.Count; i++)
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

    private void CreateChannelListView()
    {
      CreateChannelListView(true);
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
    /// Renumber all channels from min to max
    /// </summary>
    private void RenumberChannels(int min, int max)
    {
      RenumberChannels(min, max, false);
    }

    /// <summary>
    /// Renumber all channels from min to max and if set remove encrypted channels
    /// </summary>
    private void RenumberChannels(int min, int max, bool removeEncrypted)
    {
      int start = 0;
      int end = listView1.Items.Count - 1;

      if (min > max) // Swap
      {
        int temp = min;
        min = max;
        max = temp;
      }

      if ((min >= 0) && (max < listView1.Items.Count))
        start = min;

      if ((max >= 0) && (max < listView1.Items.Count))
        end = max;

      int idx = start + 1;
      //Set channel number in sequential order
      for (int i = start; i <= end; i++)
      {
        ListViewItem lvi = listView1.Items[i];
        if (removeEncrypted)
        {
            MapChannel mc = lvi.Tag as MapChannel;
            if (mc != null)
            {
                if (mc.IsEncrypted) lvi.Checked = false;
            }
        }

        if (lvi.Checked)
        {
          lvi.Text = idx.ToString();
          idx++;
        }
      }
    }

    #region ListView Drag and Drop Events
    private void listView1_DragDrop(object sender, DragEventArgs e)
    {
      //if (!statusLabel.Text.StartsWith("*"))
      //  statusLabel.Text = "*" + statusLabel.Text;
    }

    void listView1_AfterDragAndDrop(object sender, EventArgs e)
    {
      short idx = 1;
      //Set channel number in sequential order
      foreach (ListViewItem lvi in listView1.Items)
      {
        if (lvi.Checked)
        {
          lvi.Text = idx.ToString();
          idx++;
        }
      }
    }

    private void listView1_AfterDragAndDrop(object sender, ListViewDragAndDrop.AfterDragAndDropEventArgs e)
    {
      RenumberChannels(e.MinIndex, e.MaxIndex);
    }

    private void listView1_AfterDragAndDropItem(object sender, ListViewDragAndDrop.AfterDragAndDropItemEventArgs e)
    {
      int minidx = e.NewItemIndex;
      int maxidx = e.OldItemIndex;

      if (e.NewItemIndex > e.OldItemIndex)
      {
        minidx = e.OldItemIndex;
        maxidx = e.NewItemIndex;
      }

      for (int i = minidx; i <= maxidx; i++)
      {
        listView1.Items[i].Text = (i + 1).ToString();
      }
    }
    #endregion


    bool _isFirstChange = true;
    private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (listView1.IsUpdating)
        return;

      if ((Control.ModifierKeys == Keys.Control) || (Control.ModifierKeys == Keys.Shift))
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
        IChannel ch = (IChannel)e.Item.Tag;
        ch.Deleted = !e.Item.Checked;

        e.Item.Font = (ch.Deleted ? _deletedItemFont : _normalItemFont);
      }

      //Set channel number in sequential order
      short idx = 1;
      foreach (ListViewItem lvi in listView1.Items)
      {
        if (lvi.Checked)
        {
          lvi.Text = idx.ToString();
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
      FInputBox f = new FInputBox();
      if (f.Demana("Position", "New position :", "0") == DialogResult.OK)
      {
        int newpos = 0;
        if (!int.TryParse(f.Value, out newpos))
        {
          MessageBox.Show("Invalid number");
          return;
        }

        if (newpos <= 0)
          newpos = 1;

        if (newpos > listView1.Items.Count)
          newpos = listView1.Items.Count;

        int minIndex = -1;
        int maxIndex = -1;

        int dragIndex = newpos - 1;
        ListViewItem[] sel = new ListViewItem[listView1.SelectedItems.Count];
        for (int i = 0; i <= listView1.SelectedItems.Count - 1; i++)
          sel[i] = listView1.SelectedItems[i];

        for (int i = 0; i < sel.GetLength(0); i++)
        {
          ListViewItem dragItem = sel[i];
          int itemIndex = dragIndex;

          if (dragItem.Index < itemIndex)
            itemIndex++;
          else
            itemIndex = dragIndex + i;

          ListViewItem insertItem = (ListViewItem)dragItem.Clone();

          int oldidx = dragItem.Index;
          int newidx = itemIndex;

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
    }

    private void mnRenumber_Click(object sender, EventArgs e)
    {
      RenumberChannels();
    }

    private void mnSaveOrder_Click(object sender, EventArgs e)
    {
      if (_currentMapFile == null)
        return;

      SaveFileDialog sfd = new SaveFileDialog();
      sfd.Filter = "Text file (*.txt)|*.txt";
      sfd.AddExtension = true;
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

        OpenFileDialog sfd = new OpenFileDialog();
        sfd.Filter = "Text file (*.txt)|*.txt";
        sfd.AddExtension = true;
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            int items = _currentMapFile.Channels.SetOrderFrom(sfd.FileName);
            SortListviewByItemNumbers();
            MessageBox.Show(String.Format("The first {0} item(s) were resorted.", items));
        }
    }
    class ListViewItemComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            IChannel chX = (IChannel)(((ListViewItem)x).Tag);
            IChannel chY = (IChannel)(((ListViewItem)y).Tag);
            return chX.Number.CompareTo(chY.Number);
        }
    }
    private void SortListviewByItemNumbers()
    {
        IComparer old;
        old = this.listView1.ListViewItemSorter;
        this.listView1.ListViewItemSorter = new ListViewItemComparer();
        listView1.Sort();
        this.listView1.ListViewItemSorter = old;
        short idx = 1;
        //Set channel number in sequential order
        foreach (ListViewItem lvi in listView1.Items)
        {
            if (lvi.Checked)
            {
                lvi.Text = idx.ToString();
                idx++;
            }
        }
    }


    private void mnRemoveEncrypted_Click(object sender, EventArgs e)
    {
      String caption = "Shure?";
      String text = "Do you really want to remove all encrypted Channels? \r\n \tAll Channels will be reordered";

      if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo) == DialogResult.Yes)
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

		TextBox tbxEdit = null;
		tbxEdit = new TextBox();
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

			ListView lview = tbxEdit.Parent as ListView;
			

			IChannel ch = infoitem.Item.Tag as IChannel;

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
			catch (System.ArgumentException ex)
			{
				MessageBox.Show(SamsChannelEditor.Properties.Resources.READONLYFIELD_EXCEPTION, SamsChannelEditor.Properties.Resources.ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		EditListViewItem(info);
	}
	#endregion

	/// <summary>
	/// Change checked state only if item was clicked
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		Point p = listView1.PointToClient(Cursor.Position);

		if (p.X > listView1.Columns[0].Width)
		{
			e.NewValue = e.CurrentValue;
		}
	}

  }
}

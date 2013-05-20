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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SamsChannelEditor
{
  internal sealed class ListViewDragAndDrop : ListView
  {
    public class AfterDragAndDropEventArgs : EventArgs
    {
      public int MinIndex;
      public int MaxIndex;

      public AfterDragAndDropEventArgs(int minidx, int maxidx)
      {
        MinIndex = minidx;
        MaxIndex = maxidx;
      }
    }

    public class AfterDragAndDropItemEventArgs : EventArgs
    {
      public int OldItemIndex;
      public int NewItemIndex;
      public ListViewItem Item;

      public AfterDragAndDropItemEventArgs(ListViewItem item, int oldidx, int newidx)
      {
        OldItemIndex = oldidx;
        NewItemIndex = newidx;
        Item = item;
      }
    }

    public bool DragAndDropEnabled { get; set; }

    public delegate void AfterDragAndDropItemEventHandler(object sender, AfterDragAndDropItemEventArgs e);

    public delegate void AfterDragAndDropEventHandler(object sender, AfterDragAndDropEventArgs e);

    public event AfterDragAndDropItemEventHandler AfterDragAndDropItem;
    public event AfterDragAndDropEventHandler AfterDragAndDrop;

    public bool IsUpdating { get; private set; }

    private int _scrollDirection;

    private readonly Timer _tmrLvScroll;

    public ListViewDragAndDrop()
    {
      DragAndDropEnabled = true;
      IsUpdating = false;
      DoubleBuffered = true;
      OwnerDraw = true;
      DragOver += new DragEventHandler(ListViewDragAndDrop_DragOver);
      _tmrLvScroll = new Timer();
      _tmrLvScroll.Tick += new EventHandler(tmrLVScroll_Tick);
    }

    public new void BeginUpdate()
    {
      IsUpdating = true;
      base.BeginUpdate();
    }

    public new void EndUpdate()
    {
      IsUpdating = false;
      base.EndUpdate();
    }

    protected override void OnItemDrag(ItemDragEventArgs e)
    {
      base.OnItemDrag(e);

      if (DragAndDropEnabled)
        DoDragDrop(SelectedItems, DragDropEffects.Move);
    }

    protected override void OnDragEnter(DragEventArgs drgevent)
    {
      base.OnDragEnter(drgevent);

      if (DragAndDropEnabled)
      {
        int len = drgevent.Data.GetFormats().Length - 1;
        int i;
        for (i = 0; i <= len; i++)
          if (drgevent.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
            drgevent.Effect = DragDropEffects.Move;
      }
    }

    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);

      if (!DragAndDropEnabled)
        return;

      if (SelectedItems.Count == 0)
        return;

      var cp = PointToClient(new Point(drgevent.X, drgevent.Y));

      var dragToItem = GetItemAt(cp.X, cp.Y);
      if (dragToItem == null)
        return;

      int dragIndex = dragToItem.Index;

      ListViewItem[] sel = new ListViewItem[SelectedItems.Count];
      for (var i = 0; i <= SelectedItems.Count - 1; i++)
        sel[i] = SelectedItems[i];

      int min = -1;
      int max = -1;

      for (var i = 0; i < sel.GetLength(0); i++)
      {
        ListViewItem dragItem = sel[i];

        dragItem.Selected = false;
        int itemIndex = dragIndex;

        if (itemIndex == dragItem.Index)
          return;

        if (dragItem.Index < itemIndex)
          itemIndex++;
        else
          itemIndex = dragIndex + i;

        var insertItem = (ListViewItem) dragItem.Clone();
        insertItem.Selected = true;

        int oldidx = dragItem.Index;
        int newidx = itemIndex;

        Items.Insert(itemIndex, insertItem);
        Items.Remove(dragItem);

        if ((min == -1) || (Math.Min(oldidx, newidx) < min))
          min = Math.Min(oldidx, newidx);

        if ((max == -1) || (Math.Max(oldidx, newidx) > max))
          max = Math.Max(oldidx, newidx);

        if (AfterDragAndDropItem != null)
          AfterDragAndDropItem(this, new AfterDragAndDropItemEventArgs(insertItem, oldidx, newidx));
      }

      if (AfterDragAndDrop != null)
        AfterDragAndDrop(this, new AfterDragAndDropEventArgs(min, max));
    }

    private void ListViewDragAndDrop_DragOver(object sender, DragEventArgs e)
    {
      var position = new Point(e.X, e.Y);
      position = PointToClient(position);
      
      if (position.Y <= Font.Height/2)
      {
        // getting close to top, ensure previous item is visible
        _scrollDirection = 0;
        _tmrLvScroll.Enabled = true;
      }
      else if (position.Y >= ClientSize.Height - Font.Height/2)
      {
        // getting close to bottom, ensure next item is visible
        _scrollDirection = 1;
        _tmrLvScroll.Enabled = true;
      }
      else
      {
        _tmrLvScroll.Enabled = false;
      }

      e.Effect = DragDropEffects.Move;

      position.X = e.X;
      position.Y = e.Y;
    }

    protected override void OnDrawItem(DrawListViewItemEventArgs e)
    {
      e.DrawDefault = true;
      e.Item.BackColor = e.Item.Index%2 == 0 ? Color.White : Color.FromArgb(240, 240, 240);
      base.OnDrawItem(e);
    }

    protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
    {
      e.DrawDefault = true;
      base.OnDrawColumnHeader(e);
    }

    protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
    {
      e.SubItem.BackColor = e.Item.Index%2 == 0 ? Color.White : Color.FromArgb(240, 240, 240);
      e.DrawDefault = true;
      base.OnDrawSubItem(e);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);


    private void tmrLVScroll_Tick(object sender, EventArgs e)
    {
      ScrollControl(this, ref _scrollDirection);
    }

    private static void ScrollControl(Control objControl, ref int intDirection)
    {
      // This function enables a control (e.g. TreeView or ListView) to scroll
      // during a drag-and-drop operation.
      // For lngDirection, a value of 0 scrolls up; a value of 1 scrolls down.

      const UInt32 WM_SCROLL = 0x0115;
      SendMessage(objControl.Handle, WM_SCROLL, new IntPtr(intDirection), IntPtr.Zero);
    }
  }
}
  


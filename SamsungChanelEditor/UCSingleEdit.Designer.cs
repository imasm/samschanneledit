namespace SamsChannelEditor
{
  partial class UCSingleEdit
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnMoveTo = new System.Windows.Forms.ToolStripMenuItem();
      this.mnRenumber = new System.Windows.Forms.ToolStripMenuItem();
      this.mnRemoveEncrypted = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnSaveOrder = new System.Windows.Forms.ToolStripMenuItem();
      this.mnReorderFrom = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.jumpToToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
      this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.listView1 = new SamsChannelEditor.ListViewDragAndDrop();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnMoveTo,
            this.mnRenumber,
            this.mnRemoveEncrypted,
            this.toolStripSeparator1,
            this.mnSaveOrder,
            this.mnReorderFrom,
            this.toolStripMenuItem1,
            this.jumpToToolStripMenuItem,
            this.nextToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(216, 195);
      this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
      // 
      // mnMoveTo
      // 
      this.mnMoveTo.Name = "mnMoveTo";
      this.mnMoveTo.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.mnMoveTo.Size = new System.Drawing.Size(215, 22);
      this.mnMoveTo.Text = "Move to...";
      this.mnMoveTo.Click += new System.EventHandler(this.mnMoveTo_Click);
      // 
      // mnRenumber
      // 
      this.mnRenumber.Name = "mnRenumber";
      this.mnRenumber.Size = new System.Drawing.Size(215, 22);
      this.mnRenumber.Text = "Renumber All";
      this.mnRenumber.Click += new System.EventHandler(this.mnRenumber_Click);
      // 
      // mnRemoveEncrypted
      // 
      this.mnRemoveEncrypted.Name = "mnRemoveEncrypted";
      this.mnRemoveEncrypted.Size = new System.Drawing.Size(215, 22);
      this.mnRemoveEncrypted.Text = "Remove Encrypted";
      this.mnRemoveEncrypted.Click += new System.EventHandler(this.mnRemoveEncrypted_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
      this.toolStripSeparator1.Visible = false;
      // 
      // mnSaveOrder
      // 
      this.mnSaveOrder.Name = "mnSaveOrder";
      this.mnSaveOrder.Size = new System.Drawing.Size(215, 22);
      this.mnSaveOrder.Text = "Save order to ...";
      this.mnSaveOrder.Click += new System.EventHandler(this.mnSaveOrder_Click);
      // 
      // mnReorderFrom
      // 
      this.mnReorderFrom.Name = "mnReorderFrom";
      this.mnReorderFrom.Size = new System.Drawing.Size(215, 22);
      this.mnReorderFrom.Text = "Reorder from...";
      this.mnReorderFrom.Click += new System.EventHandler(this.reorderFromToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
      // 
      // jumpToToolStripMenuItem
      // 
      this.jumpToToolStripMenuItem.Name = "jumpToToolStripMenuItem";
      this.jumpToToolStripMenuItem.Size = new System.Drawing.Size(155, 23);
      this.jumpToToolStripMenuItem.Text = "JumpTo";
      // 
      // nextToolStripMenuItem
      // 
      this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
      this.nextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
      this.nextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
      this.nextToolStripMenuItem.Text = "Next";
      this.nextToolStripMenuItem.Click += new System.EventHandler(this.NextToolStripMenuItemClick);
      // 
      // listView1
      // 
      this.listView1.AllowColumnReorder = true;
      this.listView1.AllowDrop = true;
      this.listView1.CheckBoxes = true;
      this.listView1.ContextMenuStrip = this.contextMenuStrip1;
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.DragAndDropEnabled = true;
      this.listView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listView1.FullRowSelect = true;
      this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.listView1.HideSelection = false;
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.listView1.Name = "listView1";
      this.listView1.OwnerDraw = true;
      this.listView1.ShowGroups = false;
      this.listView1.Size = new System.Drawing.Size(627, 573);
      this.listView1.TabIndex = 1;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = System.Windows.Forms.View.Details;
      this.listView1.AfterDragAndDrop += new SamsChannelEditor.ListViewDragAndDrop.AfterDragAndDropEventHandler(this.listView1_AfterDragAndDrop);
      this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
      this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
      // 
      // UCSingleEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.listView1);
      this.Name = "UCSingleEdit";
      this.Size = new System.Drawing.Size(627, 573);
      this.Resize += new System.EventHandler(this.UCSingleEdit_Resize);
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }
    private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripTextBox jumpToToolStripMenuItem;

    #endregion

    private ListViewDragAndDrop listView1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnMoveTo;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnSaveOrder;
    private System.Windows.Forms.ToolStripMenuItem mnReorderFrom;
    private System.Windows.Forms.ToolStripMenuItem mnRenumber;
    private System.Windows.Forms.ToolStripMenuItem mnRemoveEncrypted;
  }
}

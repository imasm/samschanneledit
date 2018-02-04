#region Copyright (C) 2011-2017 Ivan Masmitjà

// Copyright (C) 2011-2017 Ivan Masmitjà
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
using System.Windows.Forms;

namespace SamsChannelEditor.UI
{
    public partial class FInputBox : Form
    {
        public string Value
        {
            get { return txtValor.Text; }
            set { txtValor.Text = value; }
        }

        public char PasswordChar
        {
            get { return txtValor.PasswordChar; }
            set { txtValor.PasswordChar = value; }
        }

        public FInputBox()
        {
            InitializeComponent();
            Value = "";
        }

        public DialogResult Demana(string caption, string missatge, string defaulttext)
        {
            Text = caption;
            label1.Text = missatge;
            Value = defaulttext;

            return ShowDialog();
        }

        private void FInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Accept();
            }
            else
                if (e.KeyCode == Keys.Escape)
            {
                Cancel();
            }
        }

        private void Accept()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void FInputBox_Load(object sender, EventArgs e)
        {
            txtValor.Focus();
        }

        private void FInputBox_Activated(object sender, EventArgs e)
        {
            txtValor.Focus();
        }

        /*
        public DialogResult DemanaDecimal()
        {
            
            return ShowDialog();
        }*/

        /*
        #region Validacions
        private bool ValidarDecimal(object sender, InputBoxValidateEventArgs e)
        {
            bool bret = true;

            try
            {
                e.Valor = decimal.Parse(e.UserText);
            }
            catch
            {
                bret = false;
            }
            return bret;
        }
        #endregion
         * */
    }
}
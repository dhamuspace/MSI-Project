using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;


namespace DataGridNameSpace
{
    public class MyDataGrid : DataGridView
    {
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.Enter)
            //{
            //    base.ProcessTabKey(Keys.Tab );
            //    return true;
            //}
         
            //return base.ProcessDialogKey(keyData);
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key.  
            if (key == Keys.Enter)
            {
               
                return this.ProcessTabKey(keyData);
            }
            else if (key == Keys.Down)
            {
                return false;
            }
            else if (key == Keys.Up)
            {
                return false;
            }

            return base.ProcessDialogKey(keyData);
        }
       
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key.  
            if (e.KeyCode == Keys.Enter)
          
            {
              //  return this.ProcessTabKey(e.KeyData);
            }
            else if (e.KeyCode == Keys.Down)
            {
               
                return false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                return false;
            }
            return base.ProcessDataGridViewKey(e);
        }
    }
}

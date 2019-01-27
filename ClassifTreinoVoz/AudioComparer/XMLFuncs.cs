using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AudioComparer
{
    /// <summary>Useful XML Functions</summary>
    public static class XMLFuncs
    {

        /// <summary>Textbos</summary>
        public static TextBox txtSaldo = new TextBox();
        /// <summary>Creates new table</summary>
        /// <param name="TableName">Name</param>
        /// <param name="Fields">Table fields. String. Start with dbl to make doubles.</param>
        public static DataTable CreateNewTable(string TableName, string[] Fields)
        {
            DataTable t = new DataTable(TableName);
            DataColumn[] key = new DataColumn[1];
            key[0] = new DataColumn();
            key[0].DataType = System.Type.GetType("System.Int32");
            key[0].ColumnName = "Count";
            key[0].AutoIncrement = true;
            key[0].ReadOnly = true;

            t.Columns.Add(key[0]);
            t.PrimaryKey = key;

            for (int i = 0; i < Fields.Length; i++)
            {
                DataColumn c;
                if (Fields[i].StartsWith("dbl")) c = new DataColumn(Fields[i], System.Type.GetType("System.Double"));
                else if (Fields[i].StartsWith("int")) c = new DataColumn(Fields[i], System.Type.GetType("System.Int32"));
                else c = new DataColumn(Fields[i], System.Type.GetType("System.String"));

                t.Columns.Add(c);
                c.Dispose();
            }

            return t;
        }


        /// <summary>Creates a column in a datatable if it does not exist and binds it to the control. 
        /// Added controls: Control name starts with txtInt: integer. Control name starts with "txt" - TextBox bound to Double. If textbox ends with "Name" - string.
        /// Control name starts with "radio" - Radio button. Boolean.
        /// Control name starts with "chk" - Check box. Boolean.
        /// Control name starts with "cmb" - Combo box. String.</summary>
        /// <param name="t">Datatable to analyze.</param>
        /// <param name="c">Control to create column from.</param>
        public static void CreateColumn(DataTable t, Control c)
        {
            if (t.Rows.Count == 0)
            {
                DataRow r = t.NewRow();
                t.Rows.Add(r);
            }

            if (c.Name.StartsWith("txt"))
            {
                TextBox tb = (TextBox)c;
                if (t.Columns.IndexOf(c.Name) < 0)
                {
                    DataColumn col = new DataColumn(c.Name);

                    //Coloca nomes como aceitando strings
                    if (c.Name.EndsWith("Name")) col.DataType = System.Type.GetType("System.String");
                    else if (c.Name.StartsWith("txtInt")) col.DataType = System.Type.GetType("System.Int32");
                    //Os demais aceitam apenas numeros
                    else col.DataType = System.Type.GetType("System.Double");

                    t.Columns.Add(col);

                    DataRow r = t.Rows[0];
                    if (tb.Text.Trim() != "") r[c.Name] = tb.Text;
                }

                tb.DataBindings.Clear();
                tb.DataBindings.Add("Text", t, c.Name, true, DataSourceUpdateMode.OnPropertyChanged);

            }
            else if (c.Name.StartsWith("radio"))
            {
                if (t.Columns.IndexOf(c.Name) < 0)
                {
                    DataColumn col = new DataColumn(c.Name);
                    col.DataType = System.Type.GetType("System.Boolean");

                    t.Columns.Add(col);
                }
                RadioButton rb = (RadioButton)c;
                rb.DataBindings.Clear();

                rb.DataBindings.Add("Checked", t, c.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            }
            else if (c.Name.StartsWith("chk"))
            {
                if (t.Columns.IndexOf(c.Name) < 0)
                {
                    DataColumn col = new DataColumn(c.Name);
                    col.DataType = System.Type.GetType("System.Boolean");

                    t.Columns.Add(col);
                }
                CheckBox cb = (CheckBox)c;
                cb.DataBindings.Clear();

                cb.DataBindings.Add("Checked", t, c.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            }
            else if (c.Name.StartsWith("cmb"))
            {
                if (t.Columns.IndexOf(c.Name) < 0)
                {
                    DataColumn col = new DataColumn(c.Name);
                    col.DataType = System.Type.GetType("System.String");

                    t.Columns.Add(col);
                }
                ComboBox cmb = (ComboBox)c;
                cmb.DataBindings.Clear();

                cmb.DataBindings.Add("Text", t, c.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            }
        }

        /// <summary>Creates a table containing a grid's structure. Sets data type based on column name: if it starts with: int -> Integer data type;
        /// string -> String data type; else -> Double.</summary>
        /// <param name="grid">DataGridView to read info from.</param>
        /// <param name="TableName">Name of table being created.</param>
        /// <param name="data">DataSet to store table.</param>
        public static DataTable MakeTableFromDataGrid(DataGridView grid, string TableName, DataSet data)
        {
            //data.Tables.Remove(TableName);

            DataTable t;
            if (data.Tables.IndexOf(TableName) < 0)
            {
                t = new DataTable(TableName);
                data.Tables.Add(t);
            }
            else t = data.Tables[TableName];

            if (t.Columns.IndexOf("Count") < 0)
            {
                DataColumn[] key = new DataColumn[1];
                key[0] = new DataColumn();
                key[0].DataType = System.Type.GetType("System.Int32");
                key[0].ColumnName = "Count";
                key[0].AutoIncrement = true;
                key[0].ReadOnly = true;

                t.Columns.Add(key[0]);
                t.PrimaryKey = key;
            }

            foreach (DataGridViewColumn c in grid.Columns)
            {
                DataColumn col = new DataColumn(c.Name);

                if (col.ColumnName.StartsWith("int")) col.DataType = System.Type.GetType("System.Int32");
                else if (col.ColumnName.StartsWith("string")) col.DataType = System.Type.GetType("System.String");
                else col.DataType = System.Type.GetType("System.Double");

                if (t.Columns.IndexOf(c.Name) < 0)
                {
                    t.Columns.Add(col);
                }
                t.Columns[c.Name].Caption = c.HeaderText;
            }

            if (grid.DataSource == null) grid.Columns.Clear();

            grid.DataSource = t;
            grid.Columns["Count"].Visible = false;

            foreach (DataGridViewColumn c in grid.Columns)
                c.HeaderText = t.Columns[c.Name].Caption;

            return t;
        }

        /// <summary>Class to read text files.</summary>
        public static class FileReader
        {
            /// <summary>Reads a text file and stores its lines in string arrays.</summary>
            /// <param name="FileName">File to read</param>
            public static List<string[]> ReadFile(string FileName)
            {
                List<string[]> resp = new List<string[]>();

                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(FileName);
                    string linha;

                    //Le dados
                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine();
                        linha = FileReader.Trata(linha);

                        string[] texto = linha.Split();
                        resp.Add(texto);
                    }

                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

                return resp;

            }

            private static string Trata(string linha)
            {
                string sepDec = (1.5).ToString().Substring(1, 1);

                linha = linha.Replace(".", sepDec);
                linha = linha.Replace(",", sepDec);
                linha = linha.Trim().Replace("     ", " ");
                linha = linha.Replace("    ", " ");
                linha = linha.Replace("   ", " ");
                linha = linha.Replace("  ", " ");
                return linha;
            }

        }

        #region Editable data Form creation and management
        /// <summary>Creates a multiple container for varible sizes fields.
        /// Example: multiple objectives stored in a single field.</summary>
        public class MultiField
        {
            /// <summary>MultiField configuration</summary>
            public static class Config
            {
                /// <summary>Vertical spacing</summary>
                public static int VerticalSpacing = 10;
                /// <summary>Horizontal spacing</summary>
                public static int HorizSpacing = 10;
                /// <summary>"Add button" title</summary>
                public static string AddButtonTitle = "Adicionar";
                /// <summary>"Remove button" title</summary>
                public static string RemButtonTitle = "Remover";
                /// <summary>Items list box height</summary>
                public static int ItemsListBoxHeight = 150;
                /// <summary>Buttons width</summary>
                public static int BtnsWidth = 80;
                /// <summary>Editable textbox height</summary>
                public static int TxtEditHeight = 80;
            }

            #region Controls (Textbox, buttons, listbox)
            /// <summary>Text box bound to data source</summary>
            private TextBox txtBinded;

            /// <summary>List box that will contain the items</summary>
            private ListBox lstItems;

            /// <summary>Button to add a new item</summary>
            private Button btnAdd;

            /// <summary>Button to remove an item</summary>
            private Button btnRemove;

            /// <summary>Text box to edit items</summary>
            private TextBox txtEditItem;
            #endregion

            #region Edit management
            /// <summary>Is this class editing the bound textbox?</summary>
            private bool IsEditing = false;
            /// <summary>Fields to be added</summary>
            private List<string> Fields = new List<string>();
            #endregion

            /// <summary>Constructor.</summary>
            /// <param name="BindedTextBox">TextBox bound to the data source</param>
            /// <param name="ParentControl">Control to use to add the necessary items</param>
            public MultiField(TextBox BindedTextBox, Control ParentControl)
            {
                txtBinded = BindedTextBox;
                txtBinded.Enabled = false;
                txtBinded.TextChanged += new EventHandler(bindedTextBox_TextChanged);

                #region Creates controls
                txtEditItem = new TextBox();
                txtEditItem.Top = txtBinded.Top; // +txtBinded.Height + Config.VerticalSpacing;
                txtEditItem.Width = txtBinded.Width;
                txtEditItem.Left = txtBinded.Left;
                txtEditItem.Multiline = true;
                txtEditItem.Height = Config.TxtEditHeight;
                txtEditItem.ScrollBars = ScrollBars.Vertical;

                btnAdd = new Button();
                btnAdd.Width = Config.BtnsWidth;
                btnAdd.Text = Config.AddButtonTitle;
                btnAdd.Top = txtEditItem.Top; btnAdd.Height = txtBinded.Height;
                btnAdd.Left = txtEditItem.Left + txtEditItem.Width + Config.HorizSpacing;
                btnAdd.Click += new EventHandler(AddButton_Click);

                btnRemove = new Button();
                btnRemove.Width = Config.BtnsWidth;
                btnRemove.Text = Config.RemButtonTitle;
                btnRemove.Top = btnAdd.Top + btnAdd.Height + Config.VerticalSpacing;
                btnRemove.Height = btnAdd.Height;
                btnRemove.Left = btnAdd.Left;
                btnRemove.Click += new EventHandler(btnRemove_Click);

                lstItems = new ListBox();
                lstItems.HorizontalScrollbar = true;
                lstItems.Width = txtEditItem.Width;
                lstItems.Top = txtEditItem.Top + txtEditItem.Height + Config.VerticalSpacing;
                lstItems.Left = txtEditItem.Left;
                lstItems.Height = Config.ItemsListBoxHeight;
                lstItems.SelectedIndexChanged += new EventHandler(lstItems_SelectedIndexChanged);
                #endregion

                //Add controls
                ParentControl.Controls.Add(txtEditItem);
                ParentControl.Controls.Add(btnAdd);
                ParentControl.Controls.Add(btnRemove);
                ParentControl.Controls.Add(lstItems);

            }


            /// <summary>Gets next Top position to place controls.</summary>
            public int GetNextTop()
            {
                if (lstItems != null) return lstItems.Top + lstItems.Height;
                else return -1;
            }

            #region Private methods
            void lstItems_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (lstItems.SelectedIndex >= 0)
                    txtEditItem.Text = Fields[lstItems.SelectedIndex];
            }

            void btnRemove_Click(object sender, EventArgs e)
            {
                if (lstItems.SelectedIndex >= 0)
                {
                    IsEditing = true;

                    Fields.RemoveAt(lstItems.SelectedIndex);
                    UpdateListBox();

                    IsEditing = false;
                }
            }

            void AddButton_Click(object sender, EventArgs e)
            {
                IsEditing = true;

                Fields.Add(txtEditItem.Text);
                UpdateListBox();

                IsEditing = false;
            }

            void bindedTextBox_TextChanged(object sender, EventArgs e)
            {
                if (!IsEditing)
                {
                    //Reads fields
                    string[] s = txtBinded.Text.Split('|');
                    Fields.Clear();

                    foreach (string ss in s)
                    {
                        Fields.Add(ss);
                    }

                    UpdateListBox();
                }
            }

            /// <summary>Uses Fields to update listbox</summary>
            private void UpdateListBox()
            {
                string boundTxt = "";
                lstItems.Items.Clear();
                foreach (string s in Fields)
                {
                    lstItems.Items.Add(s);
                    if (boundTxt != "") boundTxt += "|";
                    boundTxt += s;
                }

                txtBinded.Text = boundTxt;
            }
            #endregion


        }

        /// <summary>Creates a form containing editable data</summary>
        /// <param name="BindSrc">Binding source to use</param>
        /// <param name="ParentControl">Control where to add items. A new panel is created there.</param>
        /// <param name="MultiLineCols">Columns containig multiline data</param>
        /// <param name="MultiFieldCols">Columns containing multiple data such as more than 1 objective, etc.</param>
        /// <param name="ComboboxCols">Columns which should be combo boxes</param>
        /// <param name="ComboboxItems">Data to populate each combobox</param>
        public static void CreateForm(BindingSource BindSrc, Control ParentControl, List<string> MultiLineCols, 
            List<string> MultiFieldCols, List<string> ComboboxCols, List<List<string>> ComboboxItems)
        {
            //Creates a panel to hold everything
            Panel panel = new Panel();
            panel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            panel.Top = 0; panel.Left = 0;
            panel.Width = ParentControl.Width; panel.Height = ParentControl.Height;
            panel.AutoScroll = true;
            ParentControl.Controls.Add(panel);

            DataTable t = (DataTable)BindSrc.DataSource;

            int top = 10;
            int left = 40;
            foreach (DataColumn c in t.Columns)
            {
                if (c.ColumnName != "Count" && c.ColumnName != "Ações/Documentos Pendentes")
                {
                    Label lbl = new Label();
                    lbl.AutoSize = true;
                    lbl.Text = c.ColumnName;
                    lbl.Refresh();
                    lbl.Top = top + 3;
                    lbl.Left = left;

                    if (ComboboxCols != null && ComboboxCols.IndexOf(c.ColumnName) < 0)
                    {
                        if (c.ColumnName == "Saldo")
                        {
                            if (txtSaldo.IsDisposed)
                                txtSaldo = new TextBox();
                            txtSaldo.Name = "txtSaldo";
                            txtSaldo.ReadOnly = true;
                            txtSaldo.Top = top;
                            txtSaldo.Left = lbl.Left + lbl.Width + 220;
                            txtSaldo.Width = panel.Width / 2;
                            txtSaldo.DataBindings.Add("Text", BindSrc, c.ColumnName, false, DataSourceUpdateMode.OnPropertyChanged);
                            top = txtSaldo.Top + txtSaldo.Height + 10;
                            panel.Controls.Add(txtSaldo);
                        }
                        else
                        {
                            TextBox txt = new TextBox();
                            txt.Top = top;
                            txt.Left = lbl.Left + lbl.Width + 220;
                            txt.Width = panel.Width / 2;
                            txt.DataBindings.Add("Text", BindSrc, c.ColumnName, false, DataSourceUpdateMode.OnPropertyChanged);
                            top = txt.Top + txt.Height + 10;

                            if (MultiLineCols.IndexOf(c.ColumnName) >= 0)
                            {
                                txt.Multiline = true;
                                txt.ScrollBars = ScrollBars.Vertical;
                                txt.Height = 120;
                                top = txt.Top + txt.Height + 10;
                            }
                            else if (MultiFieldCols.IndexOf(c.ColumnName) >= 0)
                            {
                                XMLFuncs.MultiField mf = new XMLFuncs.MultiField(txt, panel);
                                top = mf.GetNextTop() + 10;
                            }
                            panel.Controls.Add(txt);
                        }
                    }
                    else
                    {
                        //Combobox
                        ComboBox cmb = new ComboBox();
                        cmb.Top = top;
                        cmb.Left = lbl.Left + lbl.Width + 220;
                        cmb.Width = panel.Width / 2;
                        cmb.DataBindings.Add("Text", BindSrc, c.ColumnName, false, DataSourceUpdateMode.OnPropertyChanged);
                        top = cmb.Top + cmb.Height + 10;
                        cmb.Font = new Font(cmb.Font.FontFamily, 10);
                        
                       //cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        
                        foreach (string s in ComboboxItems[ComboboxCols.IndexOf(c.ColumnName)])
                            cmb.Items.Add(s);
                        
                        panel.Controls.Add(cmb);
                    }

                    panel.Controls.Add(lbl);
                }
            }
        }

        #endregion


        #region Retrieve data controls in a field

        /// <summary>Retrieves all text boxes, combo boxes, radioButtons and numeric updowns in a given control. Goes down tabControls and groupboxes</summary>
        /// <param name="C">Control to search</param>
        public static List<Control> RetrieveAllFillableFields(Control C)
        {
            List<Control> FillableFields = new List<Control>();

            RetrieveAllFillableFields(C, FillableFields);

            return FillableFields;
        }

        private static void RetrieveAllFillableFields(Control C, List<Control> FillableFields)
        {
            foreach (Control c in C.Controls)
            {
                if (c is TextBox || c is MaskedTextBox || c is CheckBox || c is RadioButton || c is NumericUpDown || c is ComboBox)
                {
                    FillableFields.Add(c);
                }
                else if (c is GroupBox) RetrieveAllFillableFields(c, FillableFields);
                else if (c is SplitContainer)
                {
                    SplitContainer sc = (SplitContainer)c;
                    RetrieveAllFillableFields(sc.Panel1, FillableFields);
                    RetrieveAllFillableFields(sc.Panel2, FillableFields);
                }
                else if (c is TabControl)
                {
                    TabControl tb = (TabControl)c;
                    foreach (TabPage p in tb.TabPages)
                        RetrieveAllFillableFields(p, FillableFields);
                }
            }
        }

        /// <summary>Saves contents of controls into a datatable</summary>
        /// <param name="C">Control to save to</param>
        /// <param name="tbl">Datatable to use. Gets created if null</param>
        public static void SaveToTable(List<Control> C, ref DataTable tbl)
        {
            DataRow r = null;
            if (tbl.Rows.Count > 0) r = tbl.Rows[0];
            SaveToTable(C, ref tbl, ref r);
        }

        /// <summary>Saves contents of controls into a datatable</summary>
        /// <param name="C">Control to save to</param>
        /// <param name="tbl">Datatable to use. Gets created if null</param>
        /// <param name="r">Datarow to use. Gets created if null</param>
        public static void SaveToTable(List<Control> C, ref DataTable tbl, ref DataRow r)
        {
            //Creates table if necessary
            if (tbl == null) tbl = CreateNewTable("tblFields", new string[0]);

            if (tbl.Columns.Count == 0) tbl = CreateNewTable(tbl.TableName, new string[0]);

            //Creates row if null
            if (r == null)
            {
                r = tbl.NewRow();
                tbl.Rows.Add(r);
            }

            foreach (Control c in C)
            {
                CheckExistence(c, ref tbl, ref r);

                if (c is TextBox || c is MaskedTextBox)
                {
                    r[c.Name] = c.Text;
                }
                else if (c is NumericUpDown)
                {
                    r[c.Name] = ((NumericUpDown)c).Value;
                }
                else if (c is RadioButton)
                {
                    r[c.Name] = ((RadioButton)c).Checked;
                }
                else if (c is CheckBox)
                {
                    r[c.Name] = ((CheckBox)c).Checked;
                }
                else if (c is ComboBox)
                {
                    r[c.Name] = ((ComboBox)c).SelectedIndex;
                }

            }
        }

        /// <summary>Reads contents of controls from a datatable</summary>
        /// <param name="C">Control to save to</param>
        /// <param name="tbl">Datatable to use.</param>
        public static void ReadFromTable(List<Control> C, ref DataTable tbl)
        {
            DataRow r = tbl.Rows[0];
            ReadFromTable(C, ref tbl, ref r);
        }

        /// <summary>Checks if a control c exists in the datatable and creates it if it doesn't</summary>
        private static void CheckExistence(Control c, ref DataTable tbl, ref DataRow r)
        {
            if (tbl.Columns.IndexOf(c.Name) < 0)
            {
                if (c is TextBox) tbl.Columns.Add(c.Name, System.Type.GetType("System.String"));
                else if (c is MaskedTextBox) tbl.Columns.Add(c.Name, System.Type.GetType("System.String"));
                else if (c is NumericUpDown) tbl.Columns.Add(c.Name, System.Type.GetType("System.Decimal"));
                else if (c is RadioButton || c is CheckBox) tbl.Columns.Add(c.Name, System.Type.GetType("System.Boolean"));
                else if (c is ComboBox) tbl.Columns.Add(c.Name, System.Type.GetType("System.Int32"));
            }
        }


        /// <summary>Reads contents of controls from a datatable</summary>
        /// <param name="C">Control to save to</param>
        /// <param name="tbl">Datatable to use.</param>
        /// <param name="r">Datarow to use.</param>
        public static void ReadFromTable(List<Control> C, ref DataTable tbl, ref DataRow r)
        {
            if (r.RowState == DataRowState.Detached || r.RowState == DataRowState.Deleted) return;

            foreach (Control c in C)
            {
                CheckExistence(c, ref tbl, ref r);

                if (c is TextBox || c is MaskedTextBox)
                {
                    c.Text = r[c.Name].ToString();
                }
                else if (c is NumericUpDown)
                {
                    try
                    {
                        if (r[c.Name].ToString() != "") ((NumericUpDown)c).Value = (decimal)r[c.Name];
                    }
                    catch { ((NumericUpDown)c).Value = ((NumericUpDown)c).Minimum; }
                }
                else if (c is RadioButton)
                {
                    if (r[c.Name].ToString() != "") ((RadioButton)c).Checked = (bool)r[c.Name];
                }
                else if (c is CheckBox)
                {
                    if (r[c.Name].ToString() != "") ((CheckBox)c).Checked = (bool)r[c.Name];
                }
                else if (c is ComboBox)
                {
                    int n;
                    int.TryParse(r[c.Name].ToString(), out n);
                    ComboBox cmb = (ComboBox)c;
                    if (cmb.Items.Count > 0) cmb.SelectedIndex = Math.Min(n, cmb.Items.Count - 1);
                }

            }
        }

        #endregion

    }
}

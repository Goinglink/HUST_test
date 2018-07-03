namespace HUST_Aux
{
    partial class XmlDataSetViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnAddSortCondition = new System.Windows.Forms.Button();
            this.btnOpenXml = new System.Windows.Forms.Button();
            this.filterexpression = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.filename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.service_condition = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Itemand = new DevComponents.Editors.ComboItem();
            this.Itemor = new DevComponents.Editors.ComboItem();
            this.comboBoxSortType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItemAsc = new DevComponents.Editors.ComboItem();
            this.comboItemDesc = new DevComponents.Editors.ComboItem();
            this.Optr = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.Itemlow = new DevComponents.Editors.ComboItem();
            this.Itemhigh = new DevComponents.Editors.ComboItem();
            this.Itemequal = new DevComponents.Editors.ComboItem();
            this.Itemlowequal = new DevComponents.Editors.ComboItem();
            this.Itemhighequal = new DevComponents.Editors.ComboItem();
            this.Item1like = new DevComponents.Editors.ComboItem();
            this.Item1notlike = new DevComponents.Editors.ComboItem();
            this.sortField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.fields = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.sortexpression = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.condition = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.openXmlFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.expandablePanel1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel1.Controls.Add(this.treeView1);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(81, 433);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 7;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "可用表";
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 26);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(81, 407);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnAddSortCondition);
            this.groupPanel1.Controls.Add(this.btnOpenXml);
            this.groupPanel1.Controls.Add(this.filterexpression);
            this.groupPanel1.Controls.Add(this.filename);
            this.groupPanel1.Controls.Add(this.service_condition);
            this.groupPanel1.Controls.Add(this.comboBoxSortType);
            this.groupPanel1.Controls.Add(this.Optr);
            this.groupPanel1.Controls.Add(this.sortField);
            this.groupPanel1.Controls.Add(this.fields);
            this.groupPanel1.Controls.Add(this.sortexpression);
            this.groupPanel1.Controls.Add(this.condition);
            this.groupPanel1.Controls.Add(this.label6);
            this.groupPanel1.Controls.Add(this.label4);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.Controls.Add(this.label3);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.panel1);
            this.groupPanel1.Controls.Add(this.panel2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(832, 116);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 0;
            // 
            // btnAddSortCondition
            // 
            this.btnAddSortCondition.Location = new System.Drawing.Point(757, 51);
            this.btnAddSortCondition.Name = "btnAddSortCondition";
            this.btnAddSortCondition.Size = new System.Drawing.Size(47, 19);
            this.btnAddSortCondition.TabIndex = 18;
            this.btnAddSortCondition.Text = "添加";
            this.btnAddSortCondition.UseVisualStyleBackColor = true;
            this.btnAddSortCondition.Click += new System.EventHandler(this.btnAddSortCondition_Click);
            // 
            // btnOpenXml
            // 
            this.btnOpenXml.Location = new System.Drawing.Point(681, 11);
            this.btnOpenXml.Name = "btnOpenXml";
            this.btnOpenXml.Size = new System.Drawing.Size(62, 23);
            this.btnOpenXml.TabIndex = 17;
            this.btnOpenXml.Text = "浏览";
            this.btnOpenXml.UseVisualStyleBackColor = true;
            this.btnOpenXml.Click += new System.EventHandler(this.btnOpenXml_Click);
            // 
            // filterexpression
            // 
            // 
            // 
            // 
            this.filterexpression.Border.Class = "TextBoxBorder";
            this.filterexpression.Location = new System.Drawing.Point(95, 79);
            this.filterexpression.Name = "filterexpression";
            this.filterexpression.ReadOnly = true;
            this.filterexpression.Size = new System.Drawing.Size(331, 21);
            this.filterexpression.TabIndex = 15;
            this.filterexpression.TextChanged += new System.EventHandler(this.filterexpression_TextChanged);
            this.filterexpression.Enter += new System.EventHandler(this.filterexpression_Enter);
            this.filterexpression.Leave += new System.EventHandler(this.filterexpression_Leave);
            // 
            // filename
            // 
            // 
            // 
            // 
            this.filename.Border.Class = "TextBoxBorder";
            this.filename.Location = new System.Drawing.Point(114, 12);
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            this.filename.Size = new System.Drawing.Size(558, 21);
            this.filename.TabIndex = 10;
            // 
            // service_condition
            // 
            this.service_condition.DisplayMember = "Text";
            this.service_condition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.service_condition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.service_condition.FormattingEnabled = true;
            this.service_condition.ItemHeight = 15;
            this.service_condition.Items.AddRange(new object[] {
            this.Itemand,
            this.Itemor});
            this.service_condition.Location = new System.Drawing.Point(374, 50);
            this.service_condition.Name = "service_condition";
            this.service_condition.Size = new System.Drawing.Size(52, 21);
            this.service_condition.TabIndex = 14;
            this.service_condition.SelectedIndexChanged += new System.EventHandler(this.service_condition_SelectedIndexChanged);
            // 
            // Itemand
            // 
            this.Itemand.Text = "and";
            // 
            // Itemor
            // 
            this.Itemor.FontStyle = System.Drawing.FontStyle.Strikeout;
            this.Itemor.Text = "or";
            // 
            // comboBoxSortType
            // 
            this.comboBoxSortType.DisplayMember = "Text";
            this.comboBoxSortType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortType.FormattingEnabled = true;
            this.comboBoxSortType.ItemHeight = 15;
            this.comboBoxSortType.Items.AddRange(new object[] {
            this.comboItemAsc,
            this.comboItemDesc});
            this.comboBoxSortType.Location = new System.Drawing.Point(678, 50);
            this.comboBoxSortType.Name = "comboBoxSortType";
            this.comboBoxSortType.Size = new System.Drawing.Size(73, 21);
            this.comboBoxSortType.TabIndex = 12;
            this.comboBoxSortType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSortType_SelectedIndexChanged);
            // 
            // comboItemAsc
            // 
            this.comboItemAsc.Text = "升序";
            // 
            // comboItemDesc
            // 
            this.comboItemDesc.Text = "降序";
            // 
            // Optr
            // 
            this.Optr.DisplayMember = "Text";
            this.Optr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Optr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Optr.FormattingEnabled = true;
            this.Optr.ItemHeight = 15;
            this.Optr.Items.AddRange(new object[] {
            this.Itemlow,
            this.Itemhigh,
            this.Itemequal,
            this.Itemlowequal,
            this.Itemhighequal,
            this.Item1like,
            this.Item1notlike});
            this.Optr.Location = new System.Drawing.Point(224, 50);
            this.Optr.Name = "Optr";
            this.Optr.Size = new System.Drawing.Size(73, 21);
            this.Optr.TabIndex = 12;
            this.Optr.SelectedIndexChanged += new System.EventHandler(this.Optr_SelectedIndexChanged);
            // 
            // Itemlow
            // 
            this.Itemlow.Text = "<";
            // 
            // Itemhigh
            // 
            this.Itemhigh.Text = ">";
            // 
            // Itemequal
            // 
            this.Itemequal.Text = "=";
            // 
            // Itemlowequal
            // 
            this.Itemlowequal.Text = "<=";
            // 
            // Itemhighequal
            // 
            this.Itemhighequal.Text = ">=";
            // 
            // Item1like
            // 
            this.Item1like.Text = "like";
            // 
            // Item1notlike
            // 
            this.Item1notlike.Text = "not like";
            // 
            // sortField
            // 
            this.sortField.DisplayMember = "Text";
            this.sortField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.sortField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortField.FormattingEnabled = true;
            this.sortField.ItemHeight = 15;
            this.sortField.Location = new System.Drawing.Point(548, 50);
            this.sortField.Name = "sortField";
            this.sortField.Size = new System.Drawing.Size(120, 21);
            this.sortField.TabIndex = 11;
            this.sortField.SelectedIndexChanged += new System.EventHandler(this.sortField_SelectedIndexChanged);
            // 
            // fields
            // 
            this.fields.DisplayMember = "Text";
            this.fields.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fields.FormattingEnabled = true;
            this.fields.ItemHeight = 15;
            this.fields.Location = new System.Drawing.Point(95, 50);
            this.fields.Name = "fields";
            this.fields.Size = new System.Drawing.Size(122, 21);
            this.fields.TabIndex = 11;
            this.fields.SelectedIndexChanged += new System.EventHandler(this.fields_SelectedIndexChanged);
            // 
            // sortexpression
            // 
            // 
            // 
            // 
            this.sortexpression.Border.Class = "TextBoxBorder";
            this.sortexpression.Location = new System.Drawing.Point(548, 79);
            this.sortexpression.Name = "sortexpression";
            this.sortexpression.ReadOnly = true;
            this.sortexpression.Size = new System.Drawing.Size(256, 21);
            this.sortexpression.TabIndex = 16;
            this.sortexpression.TextChanged += new System.EventHandler(this.sortexpression_TextChanged);
            this.sortexpression.Enter += new System.EventHandler(this.sortexpression_Enter);
            this.sortexpression.Leave += new System.EventHandler(this.sortexpression_Leave);
            // 
            // condition
            // 
            // 
            // 
            // 
            this.condition.Border.Class = "TextBoxBorder";
            this.condition.Location = new System.Drawing.Point(303, 50);
            this.condition.Name = "condition";
            this.condition.ReadOnly = true;
            this.condition.Size = new System.Drawing.Size(65, 21);
            this.condition.TabIndex = 13;
            this.condition.TextChanged += new System.EventHandler(this.condition_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "排序表达式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "过滤表达式：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "过滤条件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "排序条件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Xml文件：";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(8, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 67);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(458, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(359, 66);
            this.panel2.TabIndex = 20;
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(747, 433);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabControl1.TabIndex = 8;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Text = "tabControl1";
            this.tabControl1.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControl1_SelectedTabChanged);
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(747, 408);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 9;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "tabItem1";
            this.tabItem1.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 116);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.expandablePanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(832, 433);
            this.splitContainer2.SplitterDistance = 81;
            this.splitContainer2.TabIndex = 2;
            // 
            // openXmlFileDlg
            // 
            this.openXmlFileDlg.Filter = "(xml文件)|*.xml";
            // 
            // XmlDataSetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 549);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.groupPanel1);
            this.MinimumSize = new System.Drawing.Size(669, 563);
            this.Name = "XmlDataSetViewer";
            this.Text = "XmlDataSetViewer";
            this.SizeChanged += new System.EventHandler(this.XmlDataSetViewer_SizeChanged);
            this.expandablePanel1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.TreeView treeView1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx service_condition;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Optr;
        private DevComponents.DotNetBar.Controls.ComboBoxEx fields;
        private DevComponents.DotNetBar.Controls.TextBoxX sortexpression;
        private DevComponents.DotNetBar.Controls.TextBoxX condition;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.Controls.TextBoxX filterexpression;
        private DevComponents.DotNetBar.Controls.TextBoxX filename;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevComponents.Editors.ComboItem Itemlow;
        private DevComponents.Editors.ComboItem Itemhigh;
        private DevComponents.Editors.ComboItem Itemequal;
        private DevComponents.Editors.ComboItem Itemand;
        private DevComponents.Editors.ComboItem Itemor;
        private DevComponents.Editors.ComboItem Itemlowequal;
        private DevComponents.Editors.ComboItem Itemhighequal;
        private DevComponents.Editors.ComboItem Item1like;
        private DevComponents.Editors.ComboItem Item1notlike;
        private System.Windows.Forms.OpenFileDialog openXmlFileDlg;
        private System.Windows.Forms.Button btnOpenXml;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx sortField;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxSortType;
        private DevComponents.Editors.ComboItem comboItemAsc;
        private DevComponents.Editors.ComboItem comboItemDesc;
        private System.Windows.Forms.Button btnAddSortCondition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

    }
}
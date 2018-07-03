using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace HUST_Aux
{
    public partial class XmlDataSetViewer : DevComponents.DotNetBar.Office2007Form
    {
        DataView dv = new DataView();
        DataSet DS = new DataSet();
        string oldFilterString = "";   //保存前一个输入的过滤条件
        string oldSortString = "";     //保存前一个输入的排序条件

        //构造函数
        public XmlDataSetViewer()
        {
            InitializeComponent();
            //openXmlFileDlg.Filter = "(*.xml)|*.xml";
            //openXmlFileDlg.FilterIndex = 1;
        }

        //光标到达，松开鼠标
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (!path.EndsWith(".xml"))
                MessageBox.Show("无法打开文件:" + path);
            else
            {
                filename.Text = path;
                treeView1.Nodes.Clear();
                DS.Dispose();
                DS = new DataSet();
                DS.ReadXml(path);
                //从xml文件中读取所有的表
                foreach (DataTable dt in DS.Tables)
                    treeView1.Nodes.Add(dt.TableName);
            }

        }

        //光标到达控件处
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

    
        //列表控件，鼠标左键单击列表项事件 ，这个好像没什么用处
     
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show("hello world");
        }

        private void DisableAutoSort(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //窗体大小改变
        private void XmlDataSetViewer_SizeChanged(object sender, EventArgs e)
        {
            /*Label_Title.Location = new Point((this.Size.Width - Label_Title.Size.Width) / 2 ,
                Label_Title.Location.Y);*/
        }

        //列表控件，鼠标左键双击列表项事件
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MyTabItem foundTab=null;
            bool bNewTab = false;

            //已经有数据读到显示panel中,就设置为可写属性
            condition.ReadOnly=false;
            filterexpression.ReadOnly = false;
            sortexpression.ReadOnly = false;


            //根据列表项的内容，找到标签
            foreach (TabItem tabItem in tabControl1.Tabs)
                if (tabItem.Text == treeView1.SelectedNode.Text)
                    foundTab = tabItem as MyTabItem;  //as语句作为类型转换
            if (foundTab == null)
            {//如果没找到标签页
                MyTabItem newTab = new MyTabItem();
                tabControl1.Tabs.Add(newTab);
                newTab.Text = treeView1.SelectedNode.Text;
                tabControlPanel1 = new TabControlPanel();
                DataGridView dgv = new DataGridView();

                DataView dataView = new DataView();
                tabControlPanel1.Dock = DockStyle.Fill;
                dataView.Table = DS.Tables[treeView1.SelectedNode.Text];
                dataView.RowFilter = "";
                newTab.AttachedDataView = dataView;
                //设置表格属性

                dgv.AutoGenerateColumns = true;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.AllowUserToOrderColumns = false;
                dgv.DataSource = dataView;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                for (int i = 5; i < dgv.Columns.Count; i++)
                    dgv.Columns[i].MinimumWidth = 60;
                if (dataView.Table.Columns.Contains("Flg"))
                    foreach(DataGridViewColumn col in dgv.Columns)
                        if(col.Name=="Flg")
                            col.MinimumWidth= 70
                        ;
                dgv.Location = new Point(0, 0);
                dgv.Dock = DockStyle.Fill;
                newTab.AttachedDataGridview = dgv;
                
                
                
                DisableAutoSort(dgv);
                tabControlPanel1.Controls.Add(dgv);
                newTab.AttachedControl = tabControlPanel1;
                foundTab = newTab;
                bNewTab = true;
            }
            
            //表格显示
            tabControl1.SelectedTab = foundTab;
            tabControl1.SelectedPanel = foundTab.AttachedControl as TabControlPanel;
            if (bNewTab)//如果是新的标签页，则添加到控件中（个人感觉这句话放在上面大if里比较好）
                tabControl1.Controls.Add(tabControl1.SelectedPanel);
            tabControl1.SelectedPanel.Show();//显示表格

            //填充字段选择下拉列表
            fields.SelectedIndex = foundTab.fieldIndex;
        }

        //标签页变换
        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            TabItem selectedTab = tabControl1.SelectedTab;
            fields.Items.Clear();
            sortField.Items.Clear();
            if (selectedTab != tabItem1)
            {
                foreach (DataColumn column in DS.Tables[selectedTab.Text].Columns)
                {
                    fields.Items.Add(column.ColumnName);
                    sortField.Items.Add(column.ColumnName);
                }
                
                //显示过滤条件
                filterexpression.Text=(tabControl1.SelectedTab as MyTabItem).AttachedDataView.RowFilter;
                sortexpression.Text = (tabControl1.SelectedTab as MyTabItem).AttachedDataView.Sort;
                fields.SelectedIndex = -1;
                Optr.SelectedIndex = -1;
                sortField.SelectedIndex = -1;
                condition.Text = "";
            }
        }

        #region 过滤相关函数
        /* 
        ******************************过滤相关函数*****************************
        */

        //过滤字段改变
        private void fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fields.SelectedIndex == -1)
                return;
            filterexpression.Text =oldFilterString + fields.Text;
        }

        //比较符号改变
        private void Optr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(Optr.Text);这样的语句获取不到text
            if (fields.SelectedIndex == -1 || Optr.SelectedIndex == -1 || condition.Text == "")
                return;
            filterexpression.Text = oldFilterString + fields.Text + " " + Optr.SelectedItem.ToString() + " " + "'" + condition.Text + "'";  //筛选条件值要加单引号

        }
        //条件内容改变
        private void condition_TextChanged(object sender, EventArgs e)
        {
            if (fields.SelectedIndex==-1 || Optr.SelectedIndex == -1 || condition.Text == "")
                return;
            filterexpression.Text = oldFilterString + fields.Text + " " + Optr.SelectedItem.ToString() + " " + "'" + condition.Text + "'"; //筛选条件值要加单引号
            //dataView 的rowFilter的改变在filterexpression_TextChanged()函数里
        }

        //服务条件  and 或者 or
        private void service_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterexpression.Text == "")
                return;

            oldFilterString = filterexpression.Text + " " + service_condition.SelectedItem.ToString() + " ";
            filterexpression.Text =oldFilterString; 
            //显示过滤条件
            fields.SelectedIndex = -1;
            Optr.SelectedIndex = -1;
            condition.Text = "";
        }

        //过滤条件改变
        private void filterexpression_TextChanged(object sender, EventArgs e)
        {
            if (filterexpression.Text == "")
                return;
            try
            {
                (tabControl1.SelectedTab as MyTabItem).AttachedDataView.RowFilter = filterexpression.Text;
            }
            catch (Exception)
            {
                //有意忽略异常的处理
            }
        }
        //开始编辑 条件过滤
        private void filterexpression_Enter(object sender, EventArgs e)
        {
            try
            {
                oldFilterString = "";
                fields.SelectedIndex = -1;
                Optr.SelectedIndex = -1;
                condition.Text = "";
                service_condition.SelectedIndex = -1;
            }catch(Exception){}
        }
        private void filterexpression_Leave(object sender, EventArgs e)
        {
            //用户离开的时候程序期待过滤语句的完整性但不要求
            oldFilterString = filterexpression.Text;
        }
        #endregion


        #region 排序相关函数
        /* 
         *****************************排序相关函数****************************
         */
        //排序字段改变
        private void sortField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sortField.SelectedIndex == -1)
                return;
            sortexpression.Text = oldSortString + sortField.Text;
        }

        //排序类型改变
        private void comboBoxSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sortField.SelectedIndex == -1 || comboBoxSortType.SelectedIndex == -1)
                return;
            sortexpression.Text = oldSortString + sortField.Text + " " + (comboBoxSortType.SelectedIndex==0 ? "asc":"desc") + " ";
        }

        private void btnAddSortCondition_Click(object sender, EventArgs e)
        {
            if (sortexpression.Text == "")
                return;
            oldSortString = sortexpression.Text + " , ";
            sortexpression.Text = oldSortString;

            //显示过滤条件
            sortField.SelectedIndex = -1;
            comboBoxSortType.SelectedIndex = -1;
        }
        
        //排序条件内容改变
        private void sortexpression_TextChanged(object sender, EventArgs e)
        {
            if (sortexpression.Text == "")
                return;
            try
            {
                (tabControl1.SelectedTab as MyTabItem).AttachedDataView.Sort = sortexpression.Text;
            }
            catch (Exception)
            {
            }
        }
        //排序 编辑
        private void sortexpression_Enter(object sender, EventArgs e)
        {
            try
            {
                oldSortString = "";
                sortField.SelectedIndex = -1;
                comboBoxSortType.SelectedIndex = -1;
            }catch(Exception){}
        }
        private void sortexpression_Leave(object sender, EventArgs e)
        {
            //用户离开的时候程序期待排序语句的完整性但不要求
            oldSortString = sortexpression.Text;
        }
        #endregion

        //打开文件对话框
        private void btnOpenXml_Click(object sender, EventArgs e)
        {
            openXmlFileDlg.ShowDialog();

            //在左边的treeview中显示
            if (openXmlFileDlg.FileName != "")
            {
                filename.Text = openXmlFileDlg.FileName;
                treeView1.Nodes.Clear();
                DS.Dispose();
                DS = new DataSet();
                DS.ReadXml(filename.Text);
                //从xml文件中读取所有的表
                foreach (DataTable dt in DS.Tables)
                    treeView1.Nodes.Add(dt.TableName);
            }
        }

        public void forTest(DataTable[] d) 
        {
            DS = new DataSet();
            foreach(DataTable dt in d)
            {
                DS.Tables.Add(dt);
            }
            foreach (DataTable dt in DS.Tables)
                treeView1.Nodes.Add(dt.TableName);
        }

        //自定义标签页控件
        private class MyTabItem : TabItem
        {
            public DataView AttachedDataView;
            public DataGridView AttachedDataGridview;
            public int fieldIndex = -1;
            public int optrIndex = -1;
            public String conditionString = "";
            public int serviceIndex = -1;
            public String sortMode = "";
        }



    }//end of XmlDataSetViewer class
}//end of namespace

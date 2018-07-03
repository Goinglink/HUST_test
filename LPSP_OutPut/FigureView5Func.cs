﻿using DevComponents.DotNetBar;
using HUST_Com;
using ProS_Assm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace HUST_OutPut
{
    public partial class FigureView5Func : DevComponents.DotNetBar.Office2007Form
    {
        //public Form parentForm; //不再使用这个参数，不需要对父窗口进行操作了
        private progress myprogress;

        private List<MyBrush> fillBrushes = null;
        private List<string> brushDescriptions = null;

        // List<int> priorities = null;
        private bool defaultUnit = true;

        private const int COLUMNSCOUNT = 25;
        private const int WINDPOWERLOCATION = 12;             //用于标记LDC表中Flg=27的数据所在下标
        private const int PHOTOVOLTAICPOERLOCATION = 13;      //用于标记LDC表中Flg=28的数据所在下标
        private int maxRectangleY = 10000;                        //用于保存矩形最右边的最高点

        //添加by孙凯 2015.12.17
        private MyDrawHelper myDrawHelper = null;                    //保存绘图时需要使用的信息

        private MyFunPictureBox myFunPictureBox = null;              //保存图片框控件
        private Boolean isMyButtonsPanelDrag = false;                        //标识是否拖动按键
        private Point buttonsPanelPreStation;                        //标记buttonsPanel先前位置
        private bool isVertical = true;                              //图片是否为竖着显示

        public void progressB()
        {
            this.myprogress = new progress();
            myprogress.Start();
            myprogress.ShowDialog();
        }

        public FigureView5Func(bool unitFlg, bool isVertical)
        {
            defaultUnit = unitFlg;
            this.isVertical = isVertical;
            InitializeComponent();

            //获取画刷列表
            fillBrushes = GenerateFillBrushed();
            selectGenIDs = GenerateSelectGenIDs();
        }

        public void newTab(List<DataTable> d, Int32 maxValueOfData, Int32 curYear,
            Int32 curMonth, Int32 curDay, String pictureInfo, bool isDisGird)
        {
            //TODO 暂时修改为如下
            //for (int dtIndex = 0; dtIndex < d.Length / 2;dtIndex++ )
            for (int dtIndex = 0; dtIndex < 1; dtIndex++)
            {
                string[] arrStr = d[2 * dtIndex].TableName.Split(new string[] { "^" }, StringSplitOptions.None);

                //      string tpName = "图" + arrStr[0].Substring(1);  //默认的  表 换成 图

                TabItem tp = this.tabControl1.CreateTab("图1");//+ arrStr[2] + arrStr[3] + arrStr[4] + arrStr[5]);
                //tp.Tooltip = tableName;

                TabControlPanel tcp = new TabControlPanel();
                tcp.Visible = false;
                tcp.TabItem = tp;
                tcp.AutoScroll = true;
                tcp.Dock = DockStyle.Fill;

                tcp.Resize += new System.EventHandler(this.pictureBox_Resize);

                myFunPictureBox = new MyFunPictureBox();

                tcp.Controls.Add(myFunPictureBox);

                //TODO 暂时注释by孙凯
                //pictureBox.AutoScrollOffset = new Point(100, 100);
                myFunPictureBox.AutoScrollOffset = new Point(0, 0);

                tcp.ScrollControlIntoView(myFunPictureBox);

                //属性设置
                myFunPictureBox.LevelLines = d;
                //TODO 注释by孙凯 2015.12.17 pictureBox.genPos=d[2*dtIndex+1];
                myFunPictureBox.pageSettings = new System.Drawing.Printing.PageSettings();
                PageSettings pageSettings = myFunPictureBox.pageSettings;
                //TODO 暂时修改by孙凯 2016.1.6

                myFunPictureBox.Width = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
                myFunPictureBox.Height = (int)(pageSettings.PaperSize.Height / 100.0 * 96);

                int xDraw = (int)(pageSettings.Margins.Left / 254.0 * 96);
                int yDraw = (int)(pageSettings.Margins.Top / 254.0 * 96);
                int widthDraw = (int)(pageSettings.PaperSize.Width / 100.0 * 96) -
                    (int)((pageSettings.Margins.Left + pageSettings.Margins.Right) / 254.0 * 96);
                int heightDraw = (int)(pageSettings.PaperSize.Height / 100.0 * 96) -
                    (int)((pageSettings.Margins.Top + pageSettings.Margins.Bottom) / 254.0 * 96);
                myFunPictureBox.drawArea = new Rectangle(xDraw, yDraw, widthDraw, heightDraw);

                myFunPictureBox.logoPos = new Rectangle(myFunPictureBox.drawArea.Left + (int)(myFunPictureBox.drawArea.Width * 0.5)
                    , myFunPictureBox.drawArea.Bottom - (int)(myFunPictureBox.drawArea.Height * 0.4)
                    , 300
                    , (myFunPictureBox.LogoItems.Count + 1) / 2 * 50);

                if (!isVertical)
                {
                    myFunPictureBox.drawArea = new Rectangle(yDraw, xDraw, heightDraw, widthDraw);
                    myFunPictureBox.Height = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
                    myFunPictureBox.Width = (int)(pageSettings.PaperSize.Height / 100.0 * 96);
                    myFunPictureBox.logoPos = new Rectangle(myFunPictureBox.drawArea.Left + (int)(myFunPictureBox.drawArea.Width * 0.5) + 150
                        , myFunPictureBox.drawArea.Bottom - (int)(myFunPictureBox.drawArea.Height * 0.4) - 30
                        , (myFunPictureBox.LogoItems.Count + 1) / 2 * 50
                        , 300);
                }

                //foreach (DataColumn col in pictureBox.genPos.Columns)
                //{
                //    string[] colNames = col.ColumnName.Split(new char[] { '.' });
                //    col.ColumnName = colNames[colNames.Length-1];
                //}

                myFunPictureBox.ContextMenuStrip = contextMenuStrip1;

                //TODO 暂时修改by孙凯
                //pictureBox.Location = new Point(0, 0);
                //pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

                //事件处理
                myFunPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
                myFunPictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
                myFunPictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
                myFunPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
                myFunPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
                myFunPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
                myFunPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
                myFunPictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
                //pictureBox.Visible = false;

                this.tabControl1.Controls.Add(tcp);
                tp.AttachedControl = tcp;

                //添加by孙凯 2015.12.17 TODO简化
                myDrawHelper = new MyDrawHelper(curYear, curMonth, curDay,
                    maxValueOfData, defaultUnit, pictureInfo, isDisGird);
            }
        }

        private double GenerateMaxValue(DataTable dt)
        {
            double max = 0.0;

            for (int i = 1; i < dt.Columns.Count; i++)
                if (max < Convert.ToDouble(dt.Rows[0][i]))
                    max = Convert.ToDouble(dt.Rows[0][i]);

            for (int i = 1; i < dt.Columns.Count; i++)
                if (max < Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][i]))
                    max = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][i]);

            max += 200;
            int tmp = (int)Math.Floor(max / 100);
            return tmp * 100;
        }

        private Point[] GeneratePoints(MyPictureBox picture, int rowIndex)
        {
            DataRow row = picture.LevelLines.Rows[rowIndex];
            Point[] points = new Point[48];
            double max = GenerateMaxValue(picture.LevelLines);
            int h = (int)Math.Floor(max);
            double step = h / (picture.drawArea.Height * 0.9 / 10.2 * 10);
            for (int i = 1; i < COLUMNSCOUNT; i++)
            {
                double val = Convert.ToDouble(row[i]);
                if (step == 0)
                    points[i * 2 - 2].Y = 0;
                else
                    points[i * 2 - 2].Y = (int)(val / step);

                points[i * 2 - 1].Y = points[2 * i - 2].Y;
            }
            return points;
        }

        private string GenerateGenIDs(List<int> types)
        {
            DataView dv = UniVars.InDS.Tables["系统表"].DefaultView;
            dv.RowFilter = "节点类型 >=300 and 节点类型<400";
            string result = "(";
            for (int i = 0; i < dv.Count; i++)
            {
                if (!selectGenIDs.Contains(i) && types.Contains(Convert.ToInt32(dv[i]["节点类型"])))
                    result += i + ",";
            }
            if (result.Length > 1)
                result = result.Substring(0, result.Length - 1);
            else
                result += "-1";
            result += ")";
            return result;
        }

        private List<double> GenerateIntervals(DataView dv)
        {
            List<double> result = new List<double>();
            double up = -10000, low = -1;

            for (int i = 0; i < dv.Count; i++)
                if (Convert.ToDouble(dv[i]["Yn"]) - 10 <= up)
                    up = Convert.ToDouble(dv[i]["Yx"]);
                else
                {
                    result.Add(up);
                    low = Convert.ToDouble(dv[i]["Yn"]);
                    up = Convert.ToDouble(dv[i]["Yx"]);
                    result.Add(low);
                }
            result.Add(up);
            result.RemoveAt(0);
            return result;
        }

        private void DrawIntervals(List<double> intervals, MyPictureBox picture, Graphics g, Brush brush)
        {
            DataRow row = null;
            int variableItemCount = GetBrushCountByType(0);
            int commonItemCount = GetBrushCountByType(1);
            //获取电力不足曲线对应的行
            if (commonItemCount == picture.LevelLines.Rows.Count - 1)
                row = picture.LevelLines.Rows[1];
            else
                row = picture.LevelLines.Rows[variableItemCount + 1];
            string tmp = row[0].ToString();
            double max = GenerateMaxValue(picture.LevelLines);
            int h = (int)Math.Floor(max);

            double ystep = h / (picture.drawArea.Height * 0.9 / 10.2 * 10);
            int xstep = (int)(picture.drawArea.Width * 0.87 / 24);

            if (ystep == 0)
                return;
            Point zeroPoint = new Point(picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1), picture.drawArea.Top + (int)(picture.drawArea.Height * 0.9));

            for (int i = 0; i < intervals.Count / 2; i++)
            {
                List<Point> polygen = new List<Point>();
                for (int j = 1; j < COLUMNSCOUNT; j++)
                {
                    Point firstPoint = new Point(zeroPoint.X, zeroPoint.Y);
                    firstPoint.X += (int)((j - 1) * xstep);
                    firstPoint.Y -= (int)(intervals[2 * i] / ystep);
                    polygen.Add(firstPoint);

                    while (j < COLUMNSCOUNT)
                    {
                        double top = Convert.ToDouble(row[j]);
                        if (intervals[2 * i] > top)
                        {
                            break;
                        }
                        else
                        {
                            Point topLeft = new Point(zeroPoint.X, zeroPoint.Y);
                            topLeft.X += (int)((j - 1) * xstep);
                            if (intervals[2 * i + 1] < top)
                            {
                                topLeft.Y -= (int)(intervals[2 * i + 1] / ystep);
                            }
                            else
                            {
                                topLeft.Y -= (int)(top / ystep);
                            }
                            polygen.Add(topLeft);
                            polygen.Add(new Point(topLeft.X + xstep, topLeft.Y));
                            j++;
                        }
                    }
                    Point lastPoint = new Point(zeroPoint.X, zeroPoint.Y);
                    lastPoint.X += (int)((j - 1) * xstep);
                    lastPoint.Y -= (int)(intervals[2 * i] / ystep);
                    polygen.Add(lastPoint);

                    if (polygen.Count >= 4)
                        g.FillPolygon(brush, polygen.ToArray());
                    polygen.Clear();
                }
            }
        }

        private void DrawGenAreas(MyPictureBox picture, Graphics g)
        {
            DataView dv = new DataView();
            //dv.Table=picture.genPos;
            dv.Sort = "Yn ASC";

            int brushIndex = 0;

            //水电
            dv.RowFilter = "gID in " + GenerateGenIDs(new List<int>() { 307 });
            List<double> intervals = GenerateIntervals(dv);
            DrawIntervals(intervals, picture, g, fillBrushes[brushIndex].myBrush);
            if (intervals.Count > 0)
            {
                AddLogoItemWithCheck(picture, brushIndex);
            }
            brushIndex++;

            //核电
            dv.RowFilter = "gID in " + GenerateGenIDs(new List<int>() { 306 });
            intervals = GenerateIntervals(dv);
            DrawIntervals(intervals, picture, g, fillBrushes[brushIndex].myBrush);
            if (intervals.Count > 0)
            {
                AddLogoItemWithCheck(picture, brushIndex);
            }
            brushIndex++;

            //火电
            dv.RowFilter = "gID in " + GenerateGenIDs(new List<int>() { 300, 301, 302 });
            intervals = GenerateIntervals(dv);
            DrawIntervals(intervals, picture, g, fillBrushes[brushIndex].myBrush);
            if (intervals.Count > 0)
            {
                AddLogoItemWithCheck(picture, brushIndex);
            }
            brushIndex++;

            //新能源
            /*
            dv.RowFilter = "gID in " + GenerateGenIDs(new List<int>() { 309, 310 });
            intervals = GenerateIntervals(dv);
            DrawIntervals(intervals, picture, g, fillBrushes[brushIndex].myBrush);
            if (intervals.Count > 0)
            {
                AddLogoItemWithCheck(picture, brushIndex);

                LogoItem item=new LogoItem();
                item.brush = fillBrushes[brushIndex].myBrush;
                item.description = brushDescriptions[brushIndex];
                             item.priority = priorities[brushIndex];
   picture.LogoItems.Add(item);

                if (!bDelete)
                    bDelete = CheckToOverlapped(picture, intervals[intervals.Count - 1]);
            }*/
            brushIndex++;

            //抽蓄
            dv.RowFilter = "gID in " + GenerateGenIDs(new List<int>() { 308 });
            intervals = GenerateIntervals(dv);
            DrawIntervals(intervals, picture, g, fillBrushes[brushIndex].myBrush);
            if (intervals.Count > 0)
            {
                AddLogoItemWithCheck(picture, brushIndex);
            }
            brushIndex++;
        }

        private List<int> GenerateSelectGenIDs()
        {
            string[] genItems = GetBackColorAndHatchStyle();
            int recordLength = 4;
            //添加注释by孙凯2015.8.10  此处11为硬编码需根据config文件中selectItems下子节点
            //(在不选电站情况下)数目确定
            int count = genItems.Length / recordLength - 11;
            List<int> indexes = new List<int>();
            for (int i = 0; i < count; i++)
            {
                DataRow[] gens = ProS_Assm.UniVars.InDS.Tables["系统表"].Select("节点类型>=300 and 节点类型<400", "节点ID ASC");
                for (int j = 0; j < gens.Length; j++)
                    //添加注释by孙凯2015.8.10  此处11为硬编码需根据config文件中selectItems下子节点
                    //(在不选电站情况下)数目确定
                    if (gens[j]["节点名称"].ToString() == genItems[(11 + i) * recordLength + 2])
                    {
                        //注释by孙凯 2015.8.10 j为其机组ID（电站ID-最开始电站ID）
                        indexes.Add(j);
                        break;
                    }
            }
            return indexes;
        }

        private List<int> selectGenIDs = null;

        private void DrawCoordinates(MyPictureBox picture, Graphics g)
        {
            Pen coordinatePen = new Pen(Brushes.Black, 3.5F);

            Point zeroPoint = new Point(picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1), picture.drawArea.Top + (int)(picture.drawArea.Height * 0.9));
            Point xAxisEnd = new Point(picture.drawArea.Right, zeroPoint.Y);
            Point yAxisEnd = new Point(zeroPoint.X, picture.drawArea.Top);

            g.DrawLine(coordinatePen, zeroPoint, xAxisEnd);
            g.DrawLine(coordinatePen, zeroPoint, yAxisEnd);

            coordinatePen.Dispose();

            Font drawFont = new Font("宋体", picture.smallFontSize);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen drawAxisPen = new Pen(Brushes.Black, 1.5F);
            Pen drawLinePen = new Pen(Brushes.Black, 3F);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            //绘制X轴
            int step = (int)(picture.drawArea.Width * 0.87 / 24);
            for (int i = 0; i < 24; i++)
            {
                g.DrawLine(drawAxisPen, zeroPoint.X + (int)(i * step), zeroPoint.Y, zeroPoint.X + (int)(i * step), zeroPoint.Y - 10);
                g.DrawString(i.ToString(), drawFont, drawBrush, zeroPoint.X + (int)(i * step), zeroPoint.Y + 3, stringFormat);
            }
            g.DrawLine(drawLinePen, zeroPoint.X + (int)(24 * step), zeroPoint.Y, zeroPoint.X + (int)(24 * step), zeroPoint.Y - 10);

            Point[] arrow = new Point[]{
                new Point(xAxisEnd.X-10,xAxisEnd.Y-5),
                new Point(xAxisEnd.X-10,xAxisEnd.Y+5)
            };
            g.DrawLine(drawLinePen, xAxisEnd, arrow[0]);
            g.DrawLine(drawLinePen, xAxisEnd, arrow[1]);

            g.DrawString("时", drawFont, drawBrush, zeroPoint.X + (int)(24 * step), zeroPoint.Y + 3);

            //绘制Y轴
            double ystep = picture.drawArea.Height * 0.9 / 10.2;
            double max = 0;

            max = GenerateMaxValue(picture.LevelLines);
            if (!defaultUnit)
                max = max / 10;

            int unit = (int)max / 10;
            for (int i = 1; i < 11; i++)
            {
                g.DrawLine(drawAxisPen, zeroPoint.X, zeroPoint.Y - (int)(i * ystep), zeroPoint.X - 5, zeroPoint.Y - (int)(i * ystep));
                int chars = (unit * i).ToString().Trim().Length;
                double delta = 0;
                if (chars >= 4)
                    delta = 2;

                int strWidth = (int)((unit * i).ToString().Trim().Length * (drawFont.SizeInPoints - delta));
                Rectangle rect = new Rectangle(
                    zeroPoint.X - strWidth, zeroPoint.Y - (int)(i * ystep) - drawFont.Height / 2, strWidth, drawFont.Height);
                g.DrawString((unit * i).ToString(), drawFont, drawBrush, rect, stringFormat);
            }
            arrow = new Point[]{
                new Point(yAxisEnd.X-5,yAxisEnd.Y+10),
                new Point(yAxisEnd.X+5,yAxisEnd.Y+10)
            };
            g.DrawLine(drawLinePen, yAxisEnd, arrow[0]);
            g.DrawLine(drawLinePen, yAxisEnd, arrow[1]);

            if (defaultUnit)
                g.DrawString("MW", drawFont, drawBrush, zeroPoint.X - 2 * drawFont.SizeInPoints, yAxisEnd.Y - drawFont.Height / 2);
            else
                g.DrawString("万kW", drawFont, drawBrush, zeroPoint.X - 4 * drawFont.SizeInPoints, yAxisEnd.Y - drawFont.Height / 2);

            foreach (TabItem ti in tabControl1.Tabs)
                if (ti.IsSelected)
                {
                    Font nameFont = new Font("宋体", picture.largeFontSize, FontStyle.Bold);
                    string[] arrStr = ti.Tooltip.Split(new string[] { "\n" }, StringSplitOptions.None);
                    string name = "图" + arrStr[0].Substring(1) + " " + arrStr[1] + "\n";
                    if (arrStr[2].Trim() != "")
                        name += "(" + arrStr[2] + ")";
                    g.DrawString(name, nameFont, drawBrush,
                        picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1) + (int)(picture.drawArea.Width * 0.9 - arrStr[0].Length * nameFont.SizeInPoints) / 2
                        , picture.drawArea.Top + (int)(picture.drawArea.Height * 0.93), stringFormat);
                    nameFont.Dispose();
                }

            drawFont.Dispose();
            drawLinePen.Dispose();
            drawBrush.Dispose();
            drawAxisPen.Dispose();
        }

        /* 获取不同类型画刷的个数，其中
         * type=0:VariableItems
         * type=1:CommonItems
         */

        private int GetBrushCountByType(int type)
        {
            int result = 0;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\TableViewConfig_" + ProS_Assm.UnitMnt.mMode + ".xml");
            //得到顶层节点列表
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlNode element in topM)
            {
                if (element.Name.ToLower() == "output")
                {
                    XmlNodeList nodelist = element.ChildNodes;

                    foreach (XmlNode items in nodelist)
                    {
                        if (items.Name != "fixedItems")
                            continue;

                        string compareString = "";

                        if (type == 0)
                            compareString = "VariableItems";
                        else
                            compareString = "CommonItems";

                        foreach (XmlNode el in items.ChildNodes)//读元素值
                        {
                            if (el.Name != compareString)
                                continue;

                            foreach (XmlNode commonBrush in el.ChildNodes)
                                if (commonBrush.Name.ToLower() == "item")
                                    result++;
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        private string[] GetBackColorAndHatchStyle()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\TableViewConfig_" + ProS_Assm.UnitMnt.mMode + ".xml");
            //得到顶层节点列表
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlNode element in topM)
            {
                if (element.Name.ToLower() == "output")
                {
                    XmlNodeList nodelist = element.ChildNodes;
                    foreach (XmlNode items in nodelist)
                    {
                        if (items.Name != "selectItemsFun")
                            continue;

                        string[] returnStr = new string[items.ChildNodes.Count * 4];
                        int i = 0;

                        foreach (XmlNode el in items.ChildNodes)//读元素值
                        {
                            String c = el.Name.ToString();
                            if (el.Name.ToLower() == "item")
                            {
                                returnStr[i] = el.Attributes["ARGB"].Value.ToString();
                                returnStr[i + 1] = el.Attributes["hatchStyle"].Value.ToString();
                                returnStr[i + 2] = el.Attributes["name"].Value.ToString();
                                returnStr[i + 3] = el.Attributes["Priority"].Value.ToString();
                                i += el.Attributes.Count;
                            }
                        }
                        return returnStr;
                    }
                }
            }
            return null;
        }

        private List<MyBrush> GenerateFillBrushed()
        {
            List<MyBrush> brushes = new List<MyBrush>();
            brushDescriptions = new List<string>();
            //priorities = new List<int>();

            string[] colorAndHatchStyle = GetBackColorAndHatchStyle();
            int recordLength = 4;
            int count = colorAndHatchStyle.Length / recordLength;
            for (int i = 0; i < count; i++)
            {
                string[] argbStr = colorAndHatchStyle[recordLength * i].Split(new char[] { ' ' });
                int[] argb = new int[4];
                for (int index = 0; index < 4; index++)
                    argb[index] = Convert.ToInt32(argbStr[index]);

                Color backColor = Color.FromArgb(argb[0], argb[1], argb[2], argb[3]);

                //获得其Priority属性
                Int32 priorty = Convert.ToInt32(colorAndHatchStyle[recordLength * i + 3]);

                HatchStyle hatchStyle = HatchStyle.Min;
                if (colorAndHatchStyle[recordLength * i + 1] != "")
                    hatchStyle = (HatchStyle)Enum.Parse(typeof(HatchStyle), colorAndHatchStyle[recordLength * i + 1]);
                if (colorAndHatchStyle[recordLength * i + 1] == "")
                    brushes.Add(new MyBrush(new SolidBrush(backColor), priorty, colorAndHatchStyle[recordLength * i + 2]));
                else
                    brushes.Add(new MyBrush(new HatchBrush(hatchStyle, Color.Black, backColor), priorty, colorAndHatchStyle[recordLength * i + 2]));
                brushDescriptions.Add(colorAndHatchStyle[recordLength * i + 2]);
            }
            //使画刷列表按照priority排列，使其下标与priority对应
            brushes.Sort();
            return brushes;
        }

        private void DrawLevelLinesAndFill(MyPictureBox picture, Graphics g)
        {
            Pen thinPen = new Pen(Brushes.Black, 1.5F);
            Pen fatPen = new Pen(Brushes.Black, 2.0F);

            Point zeroPoint = new Point(picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1), picture.drawArea.Top + (int)(picture.drawArea.Height * 0.9));
            int step = (int)(picture.drawArea.Width * 0.87 / 24);

            List<Point[]> lineList = new List<Point[]>();
            Point[] bottomLine = new Point[48];
            for (int i = 0; i < 24; i++)
            {
                bottomLine[2 * i].X = zeroPoint.X + (int)(i * step);
                bottomLine[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                bottomLine[2 * i].Y = bottomLine[2 * i + 1].Y = zeroPoint.Y;
            }
            lineList.Add(bottomLine);
            int brushOffset = 0;
            if (GetBrushCountByType(1) == picture.LevelLines.Rows.Count - 1)
                brushOffset = GetBrushCountByType(0);

            //将Flg in (0, 100, 200, 300, 400, 500, 2100,2200, 2300, 2400, 2500, 2600）
            //的数据添加到lineList中，并通过 picture.LevelLines.Rows.Count - 2
            //将FLg in (2700, 2800)排除
            //修改、添加注释by孙凯 2015.7.7
            //注意：因为多了Flg=2700、2800没有对应图标，需要在图标brushIndex做对应修改 添加by2015.9.20
            for (int i = 1; i < picture.LevelLines.Rows.Count - 2; i++)
            {
                //硬编码3，因为Flg = 27、 28没有对应图标只是画线
                int brushIndex = picture.LevelLines.Rows.Count - 2 - i - 1 + brushOffset;
                if (brushIndex >= fillBrushes.Count)
                    brushIndex = brushIndex % fillBrushes.Count;

                DataRow upLine = picture.LevelLines.Rows[picture.LevelLines.Rows.Count - 2 - i];
                DataRow downLine = picture.LevelLines.Rows[picture.LevelLines.Rows.Count - 2 - i - 1];

                //Flg为0行，直接保存0值 添加注释by孙凯 2015.7.7
                if (picture.LevelLines.Rows.Count - 2 - i - 1 == 0)
                {
                    downLine = picture.LevelLines.NewRow();
                    for (int j = 0; j < COLUMNSCOUNT; j++)
                        downLine[j] = 0;
                }
                bool newLogoItem = false;
                for (int j = 0; j < 24; j++)
                {
                    if (brushIndex == 9)
                        newLogoItem = false;
                    if (Convert.ToDouble(upLine[j + 1]) - 10 > Convert.ToDouble(downLine[j + 1]))
                        newLogoItem = true;
                }

                //修改by孙凯 newLogoItem当数值小于10时就不在画图形
                //但原句是先添加图标，再根据newLogoItem去除图标，那么如果在AddLogoItemWithCheck
                //中本来就没添加图标时，则会误删原来添加的图标
                //AddLogoItemWithCheck(picture, brushIndex);
                //if (!newLogoItem)
                //{
                //    if (picture.LogoItems.Count > 0)
                //    {
                //        picture.LogoItems.RemoveAt(picture.LogoItems.Count - 1);
                //    }
                //}
                //修改为下
                if (newLogoItem)
                    AddLogoItemWithCheck(picture, brushIndex);

                Point[] points = GeneratePoints(picture, i);
                if (points != null)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        points[2 * j].X = zeroPoint.X + (int)(j * step);
                        points[2 * j + 1].X = zeroPoint.X + (int)((j + 1) * step);
                        points[2 * j].Y = (int)(zeroPoint.Y - points[2 * j].Y);
                        points[2 * j + 1].Y = (int)(zeroPoint.Y - points[2 * j + 1].Y);
                    }
                    lineList.Add(points);
                }
            }

            //brushOffset用于确定画刷的其实位置，
            //在绘制分区时，brushOffset应为VariableItems的个数
            //绘制系统时，brushOffset为0.
            for (int i = lineList.Count - 1; i > 0; i--)
            {
                Point[] upLine = lineList[i];
                Point[] downLine = lineList[i - 1];

                int brushIndex = i - 1 + brushOffset;
                if (brushIndex >= fillBrushes.Count)
                    brushIndex = brushIndex % fillBrushes.Count;

                for (int j = 0; j < 24; j++)
                {
                    //if(i == 6)
                    //    g.FillRectangle(fillBrushes[brushIndex].myBrush,
                    //    upLine[2 * j].X, upLine[2 * j].Y,
                    //    bottomLine[2 * j + 1].X - upLine[2 * j].X, bottomLine[2 * j + 1].Y - upLine[2 * j].Y);
                    //else
                    //因为Flg = 100、200、300、400 、500的数据应该小于Flg=2100的数据
                    //但是实际数据中有些大于Flg=2100，故添加检测修改
                    //注意：数据越大，在坐标轴中Y越小，因为屏幕坐标为左上到右下增长
                    if (i < 6)
                    {
                        if (upLine[2 * j].Y < lineList[6][2 * j].Y)
                        {
                            upLine[2 * j].Y = lineList[6][2 * j].Y;
                            upLine[2 * j + 1].Y = lineList[6][2 * j + 1].Y;
                        }
                        if (bottomLine[2 * j].Y < lineList[6][2 * j].Y)
                        {
                            bottomLine[2 * j].Y = lineList[6][2 * j].Y;
                            bottomLine[2 * j + 1].Y = lineList[6][2 * j + 1].Y;
                        }
                    }
                    g.FillRectangle(fillBrushes[brushIndex].myBrush,
                        upLine[2 * j].X, upLine[2 * j].Y,
                        bottomLine[2 * j + 1].X - upLine[2 * j].X, bottomLine[2 * j + 1].Y - upLine[2 * j].Y);
                }
                //用来判断矩形最右边的最高点
                if (upLine[47].Y < maxRectangleY)
                    maxRectangleY = upLine[47].Y;

                Pen drawPen = null;
                drawPen = thinPen;
                g.DrawLines(drawPen, upLine);
                g.DrawLine(drawPen, upLine[47], new Point(upLine[47].X, zeroPoint.Y));
            }

            thinPen.Dispose();
            fatPen.Dispose();
        }

        /*
         * 绘制要显示的图片
         * 编写by孙凯 2015.12.17
         */

        private void DrawShowPicture(MyFunPictureBox picture, Graphics g)
        {
            Pen thinPen = new Pen(Brushes.Black, 1.5F);
            Pen fatPen = new Pen(Brushes.Black, 2.0F);

            Point zeroPoint = new Point(picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1), picture.drawArea.Top + (int)(picture.drawArea.Height * 0.9));
            myDrawHelper.setXYInterval(this.myFunPictureBox.drawArea.Width, this.myFunPictureBox.drawArea.Height);
            myDrawHelper.setZeroPoint(zeroPoint.X, zeroPoint.Y);
            myDrawHelper.drawPicture(picture, g, thinPen, fatPen, fillBrushes);

            thinPen.Dispose();
            fatPen.Dispose();
        }

        private void AddLogoItemWithCheck(MyPictureBox picture, int brushIndex)
        {
            bool alreadyIn = false;
            foreach (LogoItem item in picture.LogoItems)
                if (item.name == brushDescriptions[brushIndex])
                    alreadyIn = true;
            if (!alreadyIn)
            {
                LogoItem item = new LogoItem();
                item.brush = fillBrushes[brushIndex].myBrush;
                item.name = brushDescriptions[brushIndex];
                item.priority = fillBrushes[brushIndex].priority;
                picture.LogoItems.Add(item);
            }
        }

        private void DrawSpecialLines(MyPictureBox picture, Graphics g)
        {
            Pen thinPen = new Pen(Brushes.Black, 2.0F);
            Pen fatPen = new Pen(Brushes.Black, 1.5F);

            Point zeroPoint = new Point(picture.drawArea.Left + (int)(picture.drawArea.Width * 0.1), picture.drawArea.Top + (int)(picture.drawArea.Height * 0.9));
            int step = (int)(picture.drawArea.Width * 0.87 / 24);

            List<Point[]> lineList = new List<Point[]>();
            Point[] bottomLine = new Point[48];
            for (int i = 0; i < 24; i++)
            {
                bottomLine[2 * i].X = zeroPoint.X + (int)(i * step);
                bottomLine[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                bottomLine[2 * i].Y = bottomLine[2 * i + 1].Y = zeroPoint.Y;
            }
            lineList.Add(bottomLine);
            //通过 picture.LevelLines.Rows.Count - 2
            //将FLg in (2700, 2800)排除
            //修改添加注释by孙凯
            for (int j = 1; j < picture.LevelLines.Rows.Count - 2; j++)
            {
                Point[] points = GeneratePoints(picture, j);
                if (points != null)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        points[2 * i].X = zeroPoint.X + (int)(i * step);
                        points[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                        points[2 * i].Y = (int)(zeroPoint.Y - points[2 * i].Y);
                        points[2 * i + 1].Y = (int)(zeroPoint.Y - points[2 * i + 1].Y);
                    }
                    lineList.Add(points);
                }
            }

            for (int i = lineList.Count - 1; i > 0; i--)
            {
                Point[] upLine = lineList[i];
                Pen drawPen = null;
                if (picture.LevelLines.Rows[i - 1][0].ToString() == "2300")
                {
                    drawPen = fatPen;
                    g.DrawLines(drawPen, upLine);
                    g.DrawLine(drawPen, upLine[47], new Point(upLine[47].X, zeroPoint.Y));
                }
            }

            Pen dashPen = new Pen(Color.Black, 1.0f);
            //绘制Flg 为 0 的线 添加注释by孙凯 2015.7.6
            dashPen.DashStyle = DashStyle.Dash;
            {
                Point[] points = GeneratePoints(picture, 0);
                if (points != null)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        points[2 * i].X = zeroPoint.X + (int)(i * step);
                        points[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                        points[2 * i].Y = (int)(zeroPoint.Y - points[2 * i].Y);
                        points[2 * i + 1].Y = (int)(zeroPoint.Y - points[2 * i + 1].Y);
                    }
                    g.DrawLines(dashPen, points);
                    // if (points[47].Y < maxRectangleY)
                    g.DrawLine(dashPen, points[47], new Point(points[47].X, maxRectangleY));
                }
            }
            //绘制Flg 为 2700 的线 添加by孙凯 2015.7.7
            dashPen.Color = Color.Orange;
            {
                Point[] points = GeneratePoints(picture, WINDPOWERLOCATION);
                if (points != null)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        points[2 * i].X = zeroPoint.X + (int)(i * step);
                        points[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                        points[2 * i].Y = (int)(zeroPoint.Y - points[2 * i].Y);
                        points[2 * i + 1].Y = (int)(zeroPoint.Y - points[2 * i + 1].Y);
                    }
                    g.DrawLines(dashPen, points);
                    // if (points[47].Y < maxRectangleY)
                    g.DrawLine(dashPen, points[47], new Point(points[47].X, maxRectangleY));
                }
            }
            //绘制Flg 为 2800 的线 添加by孙凯 2015.7.7
            dashPen.Color = Color.HotPink;
            {
                Point[] points = GeneratePoints(picture, PHOTOVOLTAICPOERLOCATION);
                if (points != null)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        points[2 * i].X = zeroPoint.X + (int)(i * step);
                        points[2 * i + 1].X = zeroPoint.X + (int)((i + 1) * step);
                        points[2 * i].Y = (int)(zeroPoint.Y - points[2 * i].Y);
                        points[2 * i + 1].Y = (int)(zeroPoint.Y - points[2 * i + 1].Y);
                    }
                    g.DrawLines(dashPen, points);
                    // if (points[47].Y < maxRectangleY)
                    g.DrawLine(dashPen, points[47], new Point(points[47].X, maxRectangleY));
                }
            }

            thinPen.Dispose();
            fatPen.Dispose();
            dashPen.Dispose();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            MyFunPictureBox picture = sender as MyFunPictureBox;

            try
            {
                Graphics g = e.Graphics; ;
                g.Clear(Color.White);

                picture.LogoItems.Clear();

                DrawShowPicture(picture, g);

                myDrawHelper.drawAxes(picture, g);

                DrawLogo(picture, g);

                picture.drawed = true;
                // picture.Image = memImage;
            }
            catch (Exception ex)
            {
                ex.WriteLog();
                MessageBox.Show(ex.ToString());
            }
        }

        private string WrapLogoString(string originalStr)
        {
            int j = 0;
            string result = "";
            for (int i = 0; i < originalStr.Length; i++)
            {
                result += originalStr[i];
                j++;
                if (j == 5)
                {
                    result += "\n";
                    j = 0;
                }
            }
            return result;
        }

        private void SortLogo(MyPictureBox picture)
        {
            for (int i = 0; i < picture.LogoItems.Count; i++)
            {
                int minPriority = 10000;
                int index = -1;
                for (int j = i; j < picture.LogoItems.Count; j++)
                    if (picture.LogoItems[j].priority < minPriority)
                    {
                        minPriority = picture.LogoItems[j].priority;
                        index = j;
                    }
                LogoItem item = new LogoItem();
                item.brush = picture.LogoItems[index].brush;
                item.name = picture.LogoItems[index].name;
                item.priority = picture.LogoItems[index].priority;
                picture.LogoItems.RemoveAt(index);
                picture.LogoItems.Insert(i, item); ;
            }
        }

        private void DrawLogo(MyPictureBox picture, Graphics g)
        {
            //修改如下 直接将所有的图标画出
            //SortLogo(picture);
            fillBrushes.Sort();
            for (Int32 brushIndex = 0; brushIndex < fillBrushes.Count; brushIndex++)
            {
                if (fillBrushes[brushIndex].describe.Contains("出力"))
                {
                    LogoItem item = new LogoItem();
                    item.brush = fillBrushes[brushIndex].myBrush;
                    item.name = fillBrushes[brushIndex].describe;
                    item.priority = fillBrushes[brushIndex].priority;
                    picture.LogoItems.Add(item);
                }
            }

            LogoItem newItem = new LogoItem();
            newItem.priority = 0;
            newItem.brush = new SolidBrush(Color.SkyBlue);
            newItem.name = "原始负荷";
            picture.LogoItems.Insert(0, newItem);

            //因为吴老师要求将部分图例合并，所以也在此处添加添加by孙凯 2016.1.19
            //抽蓄发电/抽蓄填谷合并、电力不足/调峰不足合并、新能源弃电/水电弃水合并
            newItem.brush = fillBrushes[myDrawHelper.getBrushArrayIndex(5)].myBrush;
            newItem.secondBrush = fillBrushes[myDrawHelper.getBrushArrayIndex(21)].myBrush;
            newItem.name = "抽蓄发电/抽蓄填谷";
            picture.LogoItems.Insert(1, newItem);

            newItem.brush = fillBrushes[myDrawHelper.getBrushArrayIndex(22)].myBrush;
            newItem.secondBrush = fillBrushes[myDrawHelper.getBrushArrayIndex(24)].myBrush;
            newItem.name = "电力不足/调峰不足";
            picture.LogoItems.Insert(2, newItem);

            newItem.brush = fillBrushes[myDrawHelper.getBrushArrayIndex(23)].myBrush;
            newItem.secondBrush = fillBrushes[myDrawHelper.getBrushArrayIndex(25)].myBrush;
            newItem.name = "新能源弃电/水电弃水";
            picture.LogoItems.Insert(3, newItem);

            //添加Flg = 27、28的Logo
            //添加by孙凯 2015.7.7
            //Logo 背景使用新能源 此处采用硬编码 为config文件中selectItems的Priority-1
            newItem.brush = fillBrushes[myDrawHelper.getBrushArrayIndex(23)].myBrush;
            newItem.name = "新能源/风/光发电";
            picture.LogoItems.Insert(4, newItem);

            //newItem = new LogoItem();
            //newItem.priority = 2;
            //newItem.brush = new SolidBrush(Color.Green);
            //newItem.description = "光伏发电位置曲线";
            //picture.LogoItems.Insert(2, newItem);
            //添加结束 by 孙凯

            SolidBrush backBrush = new SolidBrush(Color.White);

            Font drawFont = new Font("宋体", picture.smallFontSize);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen framePen = new Pen(Color.Black, 1.0f);
            Pen dashPen = new Pen(Color.Black, 1.0f);
            dashPen.DashStyle = DashStyle.Dash;
            //用于绘制Flg=27的图标
            Pen tmpPen1 = new Pen(Color.Orange, 1.0f);
            tmpPen1.DashStyle = DashStyle.Dash;
            //用于绘制Flg=28的图标
            //Pen tmpPen2 = new Pen(Color.HotPink, 1.0f);
            //tmpPen2.DashStyle = DashStyle.Dash;

            int vacant = 10;
            Font titleFont = new Font("宋体", picture.largeFontSize, FontStyle.Bold);

            picture.logoPos = new Rectangle(picture.logoPos.Left
                , picture.logoPos.Top
                , picture.logoWidth
                , (picture.LogoItems.Count + 1) / 2 * (drawFont.Height * 2 + vacant) +
                vacant + titleFont.Height + vacant + 3);

            g.FillRectangle(backBrush, picture.logoPos);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            int itemWidth = picture.logoPos.Width / 2;

            g.DrawString("图例", titleFont, drawBrush, picture.logoPos.Left + picture.logoPos.Width / 2, picture.logoPos.Top + vacant, stringFormat);
            Pen pen = new Pen(Color.Black);
            g.DrawLine(pen, picture.logoPos.Left, picture.logoPos.Top + vacant + titleFont.Height + 3,
                picture.logoPos.Right, picture.logoPos.Top + vacant + titleFont.Height + 3);
            Point startPoint = new Point(picture.logoPos.Left, picture.logoPos.Top + vacant + titleFont.Height + 3);

            for (int i = 0; i < picture.LogoItems.Count; i++)
            {
                Point point = new Point(startPoint.X + 5 + (i % 2) * itemWidth,
                    startPoint.Y + (i / 2) * (drawFont.Height * 2 + vacant) + vacant);
                //因为Flg = 0,27,28(对应下标分别为0, 1与其他情况不同故修改
                //修改by孙凯 2015.7.7
                switch (i)
                {
                    case 0:
                        g.FillRectangle(picture.LogoItems[i].brush, point.X, point.Y + drawFont.Height, 40, drawFont.Height);
                        g.DrawLine(framePen, point.X, point.Y + drawFont.Height, point.X + 40, point.Y + drawFont.Height);

                        PointF[] points = new PointF[]
                        {
                            new PointF(point.X, point.Y + (float)drawFont.Height*3.0f/2),
                            new PointF(point.X + 20, point.Y + (float)drawFont.Height*3.0f/2),
                            new PointF(point.X + 20, point.Y+(float)drawFont.Height/2.0f),
                            new PointF(point.X + 40, point.Y+(float)drawFont.Height/2.0f)
                        };
                        g.DrawLines(dashPen, points);
                        g.DrawString(WrapLogoString(picture.LogoItems[i].name), drawFont, drawBrush, point.X + 45, point.Y);
                        break;
                    //因为吴老师要求将部分图例合并，所以也在此处添加添加by孙凯 2016.1.19
                    //抽蓄发电/抽蓄填谷合并、电力不足/调峰不足合并、新能源弃电/水电弃水合并
                    case 1:
                    case 2:
                    case 3:
                        g.FillRectangle(picture.LogoItems[i].brush, point.X, point.Y, 40, drawFont.Height);
                        g.FillRectangle(picture.LogoItems[i].secondBrush, point.X, point.Y + drawFont.Height, 40, drawFont.Height);
                        g.DrawString(WrapLogoString(picture.LogoItems[i].name), drawFont, drawBrush, point.X + 45, point.Y);
                        break;

                    case 4:
                        g.FillRectangle(picture.LogoItems[i].brush, point.X, point.Y, 40, drawFont.Height * 2);
                        //g.DrawLine(framePen, point.X, point.Y + drawFont.Height, point.X + 40, point.Y + drawFont.Height);

                        PointF[] points3 = new PointF[]
                        {
                            new PointF(point.X, point.Y + (float)drawFont.Height*3.0f/4),
                            new PointF(point.X + 20, point.Y + (float)drawFont.Height*3.0f/4),
                            new PointF(point.X + 20, point.Y+(float)drawFont.Height/4.0f),
                            new PointF(point.X + 40, point.Y+(float)drawFont.Height/4.0f)
                        };
                        g.DrawLines(tmpPen1, points3);

                        PointF[] points4 = new PointF[]
                        {
                            new PointF(point.X, point.Y + (float)drawFont.Height*7.0f/4),
                            new PointF(point.X + 12, point.Y + (float)drawFont.Height*7.0f/4),
                            new PointF(point.X + 12, point.Y+(float)drawFont.Height*5/4.0f),
                            new PointF(point.X + 28, point.Y + (float)drawFont.Height*5.0f/4),
                            new PointF(point.X + 28, point.Y+(float)drawFont.Height*7/4.0f),
                            new PointF(point.X + 40, point.Y+(float)drawFont.Height*7/4.0f)
                        };
                        g.DrawLines(tmpPen1, points4);
                        g.DrawString(WrapLogoString(picture.LogoItems[i].name), drawFont, drawBrush, point.X + 45, point.Y);
                        break;
                    //case 2:
                    //    g.FillRectangle(picture.LogoItems[i].brush, point.X, point.Y+drawFont.Height, 40, drawFont.Height );
                    //    g.DrawLine(framePen, point.X, point.Y + drawFont.Height, point.X + 40, point.Y + drawFont.Height);

                    //    PointF[] points2 = new PointF[]
                    //    {
                    //        new PointF(point.X, point.Y + (float)drawFont.Height*3.0f/2),
                    //        new PointF(point.X + 20, point.Y + (float)drawFont.Height*3.0f/2),
                    //        new PointF(point.X + 20, point.Y+(float)drawFont.Height/2.0f),
                    //        new PointF(point.X + 40, point.Y+(float)drawFont.Height/2.0f)
                    //    };
                    //    g.DrawLines(tmpPen2,points2);
                    //    g.DrawString(WrapLogoString(picture.LogoItems[i].description), drawFont, drawBrush, point.X + 45, point.Y);
                    //    break;
                    default:
                        g.FillRectangle(picture.LogoItems[i].brush, point.X, point.Y, 40, drawFont.Height * 2);
                        g.DrawString(WrapLogoString(picture.LogoItems[i].name), drawFont, drawBrush, point.X + 45, point.Y);
                        break;
                }
                g.DrawRectangle(framePen, point.X, point.Y, 40, drawFont.Height * 2);
            }

            stringFormat.Dispose();
            pen.Dispose();
            framePen.Dispose();
            drawBrush.Dispose();
            titleFont.Dispose();
            drawFont.Dispose();
        }

        private void pictureBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (ctrl)
            {
                MyPictureBox picture = sender as MyPictureBox;
                Panel panel = picture.Parent as Panel;
                panel.AutoScroll = false;
                if (e.Delta < 0)
                {
                    shrinkPicture(picture, panel);
                    panel.AutoScroll = true;
                }
                else
                {
                    amplifyPicture(picture, panel);
                }
            }
            else
            {
                Point mousePoint = new Point(e.X, e.Y);
                Panel panel = (sender as PictureBox).Parent as Panel;
                mousePoint.Offset(this.Location.X, this.Location.Y);
                if (panel.RectangleToScreen(panel.DisplayRectangle).Contains(mousePoint))
                {
                    panel.AutoScrollPosition = new Point(0, panel.VerticalScroll.Value - e.Delta);
                }
            }
        }

        private static void amplifyPicture(MyPictureBox picture, Panel panel)
        {
            if (picture.Width * 1.1 <= 1400 && picture.Height * 1.1 <= 2400)
            {
                picture.Size = new Size((int)(picture.Width * 1.1), (int)(picture.Height * 1.1));
                panel.AutoScroll = true;
                picture.smallFontSize = (float)(picture.smallFontSize * 1.1);
                picture.largeFontSize = (float)(picture.largeFontSize * 1.1);
                picture.logoWidth = (int)(picture.logoWidth * 1.1);
                picture.logoPos = new Rectangle((int)(picture.logoPos.Left * 1.1)
                    , (int)(picture.logoPos.Top * 1.1)
                    , picture.logoPos.Width
                    , picture.logoPos.Height); ;
                picture.drawArea = new Rectangle((int)(picture.drawArea.Left * 1.1),
                      (int)(picture.drawArea.Top * 1.1),
                      (int)(picture.drawArea.Width * 1.1),
                      (int)(picture.drawArea.Height * 1.1
                      ));
                picture.Invalidate();
            }
        }

        private static void shrinkPicture(MyPictureBox picture, Panel panel)
        {
            if (picture.Width * 0.9 > 300 && picture.Height * 0.9 > 500)
            {
                picture.Size = new Size((int)(picture.Width * 0.9), (int)(picture.Height * 0.9));
                panel.AutoScroll = true;
                picture.smallFontSize = (float)(picture.smallFontSize * 0.9);
                picture.largeFontSize = (float)(picture.largeFontSize * 0.9);
                picture.logoWidth = (int)(picture.logoWidth * 0.9);
                picture.logoPos = new Rectangle((int)(picture.logoPos.Left * 0.9)
                    , (int)(picture.logoPos.Top * 0.9)
                    , picture.logoPos.Width
                    , picture.logoPos.Height);
                picture.drawArea = new Rectangle((int)(picture.drawArea.Left * 0.9),
                    (int)(picture.drawArea.Top * 0.9),
                    (int)(picture.drawArea.Width * 0.9),
                    (int)(picture.drawArea.Height * 0.9));
                picture.Invalidate();
            }
        }

        private void pictureBox_MouseEnter(object sender, System.EventArgs e)
        {
            //((sender as PictureBox).Parent as Panel).Focus();
            (sender as PictureBox).Focus();
        }

        private void pictureBox_MouseLeave(object sender, System.EventArgs e)
        {
            // this.Focus();
        }

        //指示是否正在进行图例拖动
        private bool isDragPic = false;

        private void pictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MyPictureBox picture = sender as MyPictureBox;
                Point mousePoint = new Point(e.X, e.Y);
                if (isDragPic == true) //2013-9-22 刘水兵：当鼠标拖离图例的时候，应该也是要进行移动的.所以 用一个变量指示 是否正在拖动
                {//if(picture.logoPos.Countains(mousePoint))
                    Point topleft = new Point(picture.logoPos.Left, picture.logoPos.Top);
                    picture.logoPos = new Rectangle(
                        picture.logoPos.Left + e.X - picture.previousPos.X,
                        picture.logoPos.Top + e.Y - picture.previousPos.Y,
                        picture.logoPos.Width,
                        picture.logoPos.Height);
                    picture.previousPos = mousePoint;
                    picture.Invalidate(new Rectangle(topleft.X, topleft.Y,
                        picture.Width + e.X - picture.previousPos.X, picture.logoPos.Height + e.Y - picture.previousPos.Y));
                }
            }
        }

        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MyPictureBox picture = sender as MyPictureBox;
            picture.previousPos = new Point(e.X, e.Y);

            //指示开始图例拖动
            if (e.Button == MouseButtons.Left && picture.logoPos.Contains(new Point(e.X, e.Y)))
                this.isDragPic = true;
        }

        private void pictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MyPictureBox picture = sender as MyPictureBox;

            //指示结束图例拖动
            if (e.Button == MouseButtons.Left)
                this.isDragPic = false;
        }

        private bool ctrl = false;

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            ctrl = e.Control;
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            ctrl = e.Control;
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            Control picture;
            Control parent;
            if (sender is PictureBox || sender is Panel)
            {
                if (sender is PictureBox)
                {
                    picture = sender as Control;
                    parent = picture.Parent;
                }
                else
                {
                    parent = sender as Control;
                    picture = parent.Controls[0];
                }
                int x = picture.Width < parent.Width ? (parent.Width - picture.Width) / 2 : 0;
                int y = picture.Height < parent.Height ? (parent.Height - picture.Height) / 2 : 0;
                picture.Location = new Point(x, y);
            }
        }

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
        IntPtr hdcDest, // handle to destination DC
        int nXDest, // x-coord of destination upper-left corner
        int nYDest, // y-coord of destination upper-left corner
        int nWidth, // width of destination rectangle
        int nHeight, // height of destination rectangle
        IntPtr hdcSrc, // handle to source DC
        int nXSrc, // x-coordinate of source upper-left corner
        int nYSrc, // y-coordinate of source upper-left corner
        System.Int32 dwRop // raster operation code
        );

        private const Int32 SRCCOPY = 0xCC0020;

        private void SavePicture(MyFunPictureBox picture, string filename)
        {
            /*
            Graphics graphic = picture.CreateGraphics();
            Size s = picture.Size;
            memImage = new Bitmap(s.Width, s.Height, graphic);
            Graphics memGraphic = Graphics.FromImage(memImage);
            IntPtr dc1 = graphic.GetHdc();
            IntPtr dc2 = memGraphic.GetHdc();
            BitBlt(dc2, 0, 0, picture.Width,
            picture.Height, dc1, 0, 0, SRCCOPY);
            graphic.ReleaseHdc(dc1);
            memGraphic.ReleaseHdc(dc2);
            */
            MyFunPictureBox picBox = tabControl1.SelectedPanel.Controls[0] as MyFunPictureBox;
            //MyFunPictureBox imagePic = CreatePictureFromSource(picBox);
            MyFunPictureBox imagePic = picBox;

            Bitmap memImage = new Bitmap(imagePic.Width, imagePic.Height);

            Graphics g = Graphics.FromImage(memImage);
            imagePic.LogoItems.Clear();

            g.Clear(Color.White);
            //DrawLevelLinesAndFill(imagePic, g);

            //DrawCoordinates(imagePic, g);

            //DrawSpecialLines(picBox, g);

            //DrawLogo(imagePic, g);

            DrawShowPicture(imagePic, g);

            myDrawHelper.drawAxes(imagePic, g);

            DrawLogo(imagePic, g);

            String picPath = "";
            if (filename == "")
                picPath = Application.StartupPath + "\\ListView.bmp";

            memImage.Save(filename);

            g.Dispose();
            memImage.Dispose();
        }

        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgSavePic.ShowDialog() == DialogResult.OK)
            {
                //开启进度条
                Thread thdSub = new Thread(new ThreadStart(this.progressB));
                thdSub.Start();
                Thread.Sleep(100);

                MyFunPictureBox picture = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as MyFunPictureBox;
                SavePicture(picture, dlgSavePic.FileName);

                //关闭进度条
                this.myprogress.isOver = true;

                MessageBox.Show("保存图片成功！");
            }
        }

        private void FigureView_Load(object sender, EventArgs e)
        {
        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPictureBox picture = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as MyPictureBox;

            PageSetupDialog setupDialog = new PageSetupDialog();
            setupDialog.PageSettings = picture.pageSettings;
            setupDialog.PrinterSettings =
                new System.Drawing.Printing.PrinterSettings();
            if (setupDialog.ShowDialog() == DialogResult.OK)
            {
                PageSettings pageSettings = setupDialog.PageSettings;
                picture.pageSettings = pageSettings;
                isVertical = !(picture.pageSettings.Landscape);
                int xDraw = (int)(pageSettings.Margins.Left / 254.0 * 96);
                int yDraw = (int)(pageSettings.Margins.Top / 254.0 * 96);
                int widthDraw = (int)(pageSettings.PaperSize.Width / 100.0 * 96) -
                    (int)((pageSettings.Margins.Left + pageSettings.Margins.Right) / 254.0 * 96);
                int heightDraw = (int)(pageSettings.PaperSize.Height / 100.0 * 96) -
                    (int)((pageSettings.Margins.Top + pageSettings.Margins.Bottom) / 254.0 * 96);

                picture.drawArea = new Rectangle(xDraw, yDraw, widthDraw, heightDraw);
                //添加by孙凯 2016.1.16 随着选择打印纸张大小改变图片大小
                myFunPictureBox.Width = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
                myFunPictureBox.Height = (int)(pageSettings.PaperSize.Height / 100.0 * 96);
                //添加by孙凯2016.1.16 使图片可以根据用户设定是横向显示还是竖向
                if (!isVertical)
                {
                    picture.drawArea = new Rectangle(yDraw, xDraw, heightDraw, widthDraw);
                    myFunPictureBox.Height = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
                    myFunPictureBox.Width = (int)(pageSettings.PaperSize.Height / 100.0 * 96);
                    myFunPictureBox.logoPos = new Rectangle(myFunPictureBox.drawArea.Left + (int)(myFunPictureBox.drawArea.Width * 0.5) + 150
                        , myFunPictureBox.drawArea.Bottom - (int)(myFunPictureBox.drawArea.Height * 0.4) - 30
                        , (myFunPictureBox.LogoItems.Count + 1) / 2 * 50
                        , 300);
                }
                else
                {
                    myFunPictureBox.logoPos = new Rectangle(myFunPictureBox.drawArea.Left + (int)(myFunPictureBox.drawArea.Width * 0.5)
                        , myFunPictureBox.drawArea.Bottom - (int)(myFunPictureBox.drawArea.Height * 0.4)
                        , 300
                        , (myFunPictureBox.LogoItems.Count + 1) / 2 * 50);
                }
                picture.Invalidate();
            }
            setupDialog.Dispose();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            MyPictureBox picture = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as MyPictureBox;

            PrintDocument pd = new PrintDocument();
            picture.pageSettings.Landscape = !isVertical;
            pd.DefaultPageSettings = picture.pageSettings;
            //设置页面
            //pd.DefaultPageSettings= picture.pageSettings;
            pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            // Allow the user to choose the page range he or she would
            // like to print.
            printDialog1.AllowSomePages = true;

            // Show the help button.
            printDialog1.ShowHelp = true;

            // Set the Document property to the PrintDocument for
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property
            // must be set
            printDialog1.Document = pd;

            DialogResult result = printDialog1.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK && printDialog1.Document != null)
            {
                pd.Print();
            }
            pd.Dispose();
            printDialog1.Document = null;
        }

        private MyFunPictureBox CreatePictureFromSource(MyFunPictureBox source)
        {
            MyFunPictureBox picture = new MyFunPictureBox();
            picture.LevelLines = source.LevelLines;
            //picture.genPos = source.genPos;
            picture.pageSettings = source.pageSettings;
            picture.LogoItems = source.LogoItems;

            //设置图片大小和图例位置
            PageSettings pageSettings = picture.pageSettings;
            picture.Width = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
            picture.Height = (int)(pageSettings.PaperSize.Height / 100.0 * 96);
            int xDraw = (int)(pageSettings.Margins.Left / 254.0 * 96);
            int yDraw = (int)(pageSettings.Margins.Top / 254.0 * 96);
            int widthDraw = (int)(pageSettings.PaperSize.Width / 100.0 * 96) -
                (int)((pageSettings.Margins.Left + pageSettings.Margins.Right) / 254.0 * 96);
            int heightDraw = (int)(pageSettings.PaperSize.Height / 100.0 * 96) -
                (int)((pageSettings.Margins.Top + pageSettings.Margins.Bottom) / 254.0 * 96);
            picture.drawArea = new Rectangle(xDraw, yDraw, widthDraw, heightDraw);
            picture.logoPos = new Rectangle((int)(picture.Width * source.logoPos.Left / 1.0 / source.Width)
                , (int)(picture.Height * source.logoPos.Top / 1.0 / source.Height)
                , 300
                , (picture.LogoItems.Count + 1) / 2 * 50);
            //添加by孙凯2016.1.16 使图片可以根据用户设定是横向显示还是竖向
            if (!isVertical)
            {
                myFunPictureBox.drawArea = new Rectangle(yDraw, xDraw, heightDraw, widthDraw);
                myFunPictureBox.Height = (int)(pageSettings.PaperSize.Width / 100.0 * 96);
                myFunPictureBox.Width = (int)(pageSettings.PaperSize.Height / 100.0 * 96);
            }
            return picture;
        }

        //打印事件处理
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //读取图片
            MyFunPictureBox picBox = tabControl1.SelectedPanel.Controls[0] as MyFunPictureBox;
            //MyFunPictureBox imagePic = CreatePictureFromSource(picBox);
            MyFunPictureBox imagePic = picBox;
            //Image temp = picBox.Image;
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            //int width = temp.Width;
            //int height = temp.Height;
            //Rectangle destRect = new Rectangle(x, y, width, height);
            Graphics g = e.Graphics;
            imagePic.LogoItems.Clear();

            //DrawLevelLinesAndFill(imagePic, g);

            ////if (imagePic.genPos.Rows.Count > 0)
            ////{
            ////    DrawGenAreas(imagePic, g);
            ////    DrawSelectGens(imagePic, g);
            ////}
            //DrawCoordinates(imagePic, g);

            //DrawSpecialLines(picBox, g);

            //DrawLogo(imagePic, g);

            DrawShowPicture(imagePic, g);

            myDrawHelper.drawAxes(imagePic, g);

            DrawLogo(imagePic, g);

            //e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
        }

        private void dayRightMove_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.dayRightMove())
                myFunPictureBox.Invalidate();
        }

        private void dayLeftMove_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.dayLeftMove())
                myFunPictureBox.Invalidate();
        }

        private void monthRightMove_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.monthRightMove())
                myFunPictureBox.Invalidate();
        }

        private void monthLeftMove_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.monthLeftMove())
                myFunPictureBox.Invalidate();
        }

        private void addDisDays_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.addDisDays())
                myFunPictureBox.Invalidate();
        }

        private void redDisDays_Click(object sender, EventArgs e)
        {
            if (this.myDrawHelper.redDisDays())
                myFunPictureBox.Invalidate();
        }

        private void dayLeftMove_MouseHover(object sender, EventArgs e)
        {
        }

        private void buttonsPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isMyButtonsPanelDrag = true;
                this.buttonsPanelPreStation = new Point(e.X, e.Y);
            }
        }

        private void buttonsPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isMyButtonsPanelDrag = false;
            }
        }

        private void buttonsPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.isMyButtonsPanelDrag)
            {
                this.buttonsPanel.Left = this.buttonsPanel.Left + e.X - this.buttonsPanelPreStation.X;
                this.buttonsPanel.Top = this.buttonsPanel.Top + e.Y - this.buttonsPanelPreStation.Y;
            }
        }

        private void biger_Click(object sender, EventArgs e)
        {
            amplifyPicture(myFunPictureBox, myFunPictureBox.Parent as Panel);
        }

        private void smaller_Click(object sender, EventArgs e)
        {
            shrinkPicture(myFunPictureBox, myFunPictureBox.Parent as Panel);
        }

        private void 放大Ctrl鼠标左键滚轮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            amplifyPicture(myFunPictureBox, myFunPictureBox.Parent as Panel);
        }

        private void 缩小Ctrl鼠标左键滚轮ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            shrinkPicture(myFunPictureBox, myFunPictureBox.Parent as Panel);
        }
    }

    public class MyFunPictureBox : MyPictureBox
    {
        new public List<DataTable> LevelLines { get; set; }
    }
}
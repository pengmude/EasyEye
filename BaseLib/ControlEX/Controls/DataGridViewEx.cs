using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// DataGridView扩展类
    /// </summary>
    public partial class DataGridViewEx : DataGridView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataGridViewEx()
        {
            InitializeComponent();
            this.CellMouseDown += DGV_R1_Base_CellMouseDown;
            this.CellMouseUp += DGV_R1_Base_CellMouseUp;
            this.CellEndEdit += DataGridViewEx_CellEndEdit;
            this.CellBeginEdit += DataGridViewEx_CellBeginEdit;
        }




        /// <summary>
        /// 推出编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewEx_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (startSecletRow == EndtSecletRow && startSecletCol == EndSecletCol)
                {
                    for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                    {
                        for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                        {
                            if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                            {

                                string valuess = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString();

                                if (!IsEnglishChar(valuess))
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value =
                                        CalcStr(valuess).ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        /// <summary>
        /// 可以编辑
        /// </summary>
        private bool _ctrCanEdit = false;

        /// <summary>
        /// 显示整个单元的数据
        /// </summary>
        [Category("RX.UI")]
        [Description("表格可以编辑")]
        public bool CtrlCanEdit
        {
            get { return _ctrCanEdit; }
            set
            {
                _ctrCanEdit = value;
            }
        }


        /// <summary>
        /// 选择一行模式
        /// </summary>
        private bool _ctrlSelectAllRow = false;

        /// <summary>
        /// 选择一行模式
        /// </summary>
        [Category("RX.UI")]
        [Description("选择一行模式")]
        public bool CtrlSelectAllRow
        {
            get { return _ctrlSelectAllRow; }
            set
            {
                _ctrlSelectAllRow = value;
            }
        }

        /// <summary>
        /// 通知Ctrl_V,复制了数据
        /// </summary>
        public delegate void TellHaveCopyEventHandler();
        /// <summary>
        /// 通知Ctrl_V,复制了数据
        /// </summary>
        public event TellHaveCopyEventHandler TellHaveCopyEvent;

        /// <summary>
        /// 通知Ctrl_V,复制了数据
        /// </summary>
        private void TellHaveCopy()
        {
            if (TellHaveCopyEvent != null)
            {
                TellHaveCopyEvent();
            }
        }

        /// <summary>
        /// 多个表格被编辑
        /// </summary>
        public delegate void MultipleCellEditEventHandler();

        /// <summary>
        /// 多个表格被编辑
        /// </summary>
        public event MultipleCellEditEventHandler MultipleCellEditEvent;


        /// <summary>
        /// 多个表格被编辑
        /// </summary>
        private void MultipleCellEdit()
        {
            if (MultipleCellEditEvent != null)
            {
                MultipleCellEditEvent();
            }
        }


        #region 复制表格
        DataGridView secletDatagrisview;
        int startSecletRow = 0;
        int startSecletCol = 0;

        int EndtSecletRow = 0;
        int EndSecletCol = 0;

        /// <summary>
        /// 多个表格被编辑
        /// </summary>
        private bool haveManyEdit = false;

        /// <summary>
        /// ProcessCmdKey 处理命令键
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="keyData">键值</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                //在检测到按Ctrl+V键后，系统无法复制多单元格数据，重写ProcessCmdKey方法，屏蔽系统粘贴事件，使用自定义粘贴事件，在事件中对剪贴板的HTML格式进行处理，获取表数据，更新DataGrid控件内容
                if (secletDatagrisview != null)
                {
                    if (keyData == (Keys.V | Keys.Control) && this.secletDatagrisview.SelectedCells.Count > 0 && !this.secletDatagrisview.SelectedCells[0].IsInEditMode)
                    {
                        IDataObject idataObject = Clipboard.GetDataObject();
                        string[] s = idataObject.GetFormats();
                        string data;
                        if (!s.Any(f => f == "OEMText"))
                        {
                            if (!s.Any(f => f == "HTML Format"))
                            {
                            }
                            else
                            {
                                //data = idataObject.GetData("HTML Format").ToString();//多个单元格
                                data = idataObject.GetData("System.String").ToString();//多个单元格
                                copyClipboardTexttoGrid(data);
                                //msg = Message.;
                                msg = new Message();
                                Task.Run(() =>
                                {
                                    //通知复制了数据
                                    TellHaveCopy();
                                });
                                return base.ProcessCmdKey(ref msg, Keys.Control);
                            }
                        }
                        else
                        {
                            data = idataObject.GetData("OEMText").ToString();//单个单元格,处于未编辑状态
                            for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                            {
                                for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                                {
                                    if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                    {
                                        this.secletDatagrisview.Rows[k].Cells[l].Value = data;
                                    }
                                    else
                                    {
                                        this.secletDatagrisview.Rows[k].Cells[l].Value = data;
                                    }
                                }
                            }
                            //int rowStart = this.secletDatagrisview.SelectedCells[0].RowIndex;
                            //int columnStart = this.secletDatagrisview.SelectedCells[0].ColumnIndex;
                            //this.secletDatagrisview.Rows[rowStart].Cells[columnStart].Hint = data;
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        }
                    }
                    else if (keyData <= Keys.NumPad9 && keyData >= Keys.D0)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);

                        //多个表格被编辑
                        haveManyEdit = true;
                        var input = KeyCodeToChar(keyData);
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value =
                                        this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Delete)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        if (startSecletCol >= 0)
                        {
                            for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                            {
                                for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = "";
                                }
                            }
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        }
                    }
                    else if (keyData == Keys.Back)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    string value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString();
                                    if (value.Length > 1)
                                    {
                                        this.secletDatagrisview.Rows[k].Cells[l].Value = value.Substring(0, value.Length - 1);
                                    }
                                    else
                                    {
                                        this.secletDatagrisview.Rows[k].Cells[l].Value = "";
                                    }
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Enter)
                    {
                        if (startSecletRow != EndtSecletRow || startSecletCol != EndSecletCol)
                        {
                            for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                            {
                                for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                                {
                                    if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                    {

                                        string valuess = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString();

                                        if (!IsEnglishChar(valuess))
                                        {
                                            this.secletDatagrisview.Rows[k].Cells[l].Value =
                                                CalcStr(valuess).ToString();
                                        }
                                    }
                                }
                            }
                        }

                        Task.Run(() =>
                        {
                            Thread.Sleep(3);
                            Invoke(new Action(() =>
                            {
                                startSecletRow = secletDatagrisview.SelectedCells[0].RowIndex;
                                startSecletCol = secletDatagrisview.SelectedCells[0].ColumnIndex;
                                EndtSecletRow = startSecletRow;
                                EndSecletCol = startSecletCol;
                            }));
                        });

                        if (haveManyEdit)
                        {
                            //多个表格被编辑
                            haveManyEdit = false;
                            MultipleCellEdit();
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Add || keyData == (Keys.Oemplus | Keys.Shift))
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        var input = "+";
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Multiply || keyData == Keys.OemMinus)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        var input = "-";
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Separator || keyData == (Keys.D8 | Keys.Shift))
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        var input = "*";
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.Subtract || keyData == Keys.Oem2)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        var input = "/";
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                    else if (keyData == Keys.OemPeriod || keyData == Keys.Decimal)
                    {
                        if (!_ctrCanEdit)
                            return base.ProcessCmdKey(ref msg, Keys.Control);
                        var input = ".";
                        for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                        {
                            for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                            {
                                if (this.secletDatagrisview.Rows[k].Cells[l].Value != null)
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = this.secletDatagrisview.Rows[k].Cells[l].Value.ToString() + input;
                                }
                                else
                                {
                                    this.secletDatagrisview.Rows[k].Cells[l].Value = input;
                                }
                            }
                        }
                        return base.ProcessCmdKey(ref msg, Keys.Control);
                    }
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception)
            {
                msg = new Message();
                return base.ProcessCmdKey(ref msg, Keys.Control);
            }
        }

        /// <summary>
        /// 是否包含英文字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEnglishChar(string text)
        {
            foreach (char tempchar in text.ToCharArray())
            {
                //是否是小写字母
                if (char.IsLetter(tempchar))
                    return true;
                if (char.IsUpper(tempchar))
                    return true;
            }
            return false;
        }

        private void copyClipboardTexttoGrid(string data)
        {
            try
            {
                string[] rows = data.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string[] cols;
                int count = startSecletRow + rows.Length - this.secletDatagrisview.RowCount;
                if (count < 0 || (count == 0 && !this.secletDatagrisview.AllowUserToAddRows))
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (startSecletCol < 0)
                        {
                            int SecletCol = 0;
                            cols = rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                            if ((SecletCol + cols.Length - this.secletDatagrisview.ColumnCount) <= 1)
                            {
                                for (int j = 0; j < cols.Length - 1; j++)
                                {
                                    this.secletDatagrisview.Rows[i + startSecletRow].Cells[j + SecletCol].Value = cols[j + 1];
                                }
                            }
                        }
                        else
                        {
                            cols = rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                            if (rows.Length == 1 && cols.Length == 1)
                            {
                                for (int k = startSecletRow; k < EndtSecletRow + 1; k++)
                                {
                                    for (int l = startSecletCol; l < EndSecletCol + 1; l++)
                                    {
                                        this.secletDatagrisview.Rows[k].Cells[l].Value = cols[0];
                                    }
                                }
                            }
                            else
                            {
                                if ((startSecletCol + cols.Length - this.secletDatagrisview.ColumnCount) <= 0)
                                {
                                    for (int j = 0; j < cols.Length; j++)
                                    {
                                        this.secletDatagrisview.Rows[i + startSecletRow].Cells[j + startSecletCol].Value = cols[j];
                                    }
                                }
                            }
                        }
                    }
                    this.secletDatagrisview.ClearSelection();
                }
                else
                {
                    if (this.secletDatagrisview.AllowUserToAddRows)
                    {
                        for (int i = 0; i < (count + 1); i++)
                        {
                            this.secletDatagrisview.Rows.Add();
                        }
                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (startSecletCol < 0)
                            {
                                int SecletCol = 0;
                                cols = rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                                if ((SecletCol + cols.Length - this.secletDatagrisview.ColumnCount) <= 1)
                                {
                                    for (int j = 0; j < cols.Length - 1; j++)
                                    {
                                        this.secletDatagrisview.Rows[i + startSecletRow].Cells[j + SecletCol].Value = cols[j + 1];
                                    }
                                }
                            }
                            else
                            {
                                cols = rows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                                if ((startSecletCol + cols.Length - this.secletDatagrisview.ColumnCount) <= 0)
                                {
                                    for (int j = 0; j < cols.Length; j++)
                                    {
                                        this.secletDatagrisview.Rows[i + startSecletRow].Cells[j + startSecletCol].Value = cols[j];
                                    }
                                }
                            }
                        }
                        this.secletDatagrisview.ClearSelection();
                    }

                }
            }
            catch (Exception)
            {
            }
        }


        private void DGV_R1_Base_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (secletDatagrisview == null)
                {
                    secletDatagrisview = new DataGridView();
                }
                secletDatagrisview = (DataGridView)sender;
                if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift) //判断Shift键
                {
                    startSecletRow = e.RowIndex;
                    startSecletCol = e.ColumnIndex;
                }
                if (CtrlSelectAllRow)
                {
                    if (this.secletDatagrisview.SelectedCells.Count == 1 && this.secletDatagrisview.SelectedCells[0].IsInEditMode)
                    {
                        return;
                    }
                    int nowRowIndex = 0;
                    // 遍历所有行  
                    foreach (DataGridViewRow row in this.Rows)
                    {
                        int nowColIndex = 0;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (nowRowIndex == startSecletRow && nowColIndex == startSecletCol)
                            {
                                cell.Style.BackColor = SystemColors.Highlight;
                                cell.Style.ForeColor = Color.White;
                            }
                            else if (nowRowIndex == startSecletRow)
                            {
                                cell.Style.BackColor = SystemColors.Highlight;
                                cell.Style.ForeColor = Color.LightSalmon;
                            }
                            else
                            {
                                cell.Style.BackColor = default;
                                cell.Style.ForeColor = default;
                            }
                            nowColIndex++;
                        }
                        nowRowIndex++;
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        private void DGV_R1_Base_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.secletDatagrisview.SelectedCells.Count == 1 && this.secletDatagrisview.SelectedCells[0].IsInEditMode)
                {
                    return;
                }

                EndtSecletRow = e.RowIndex;
                EndSecletCol = e.ColumnIndex;

                if (EndtSecletRow < startSecletRow)
                {
                    int ls = EndtSecletRow;
                    EndtSecletRow = startSecletRow;
                    startSecletRow = ls;
                }
                if (EndSecletCol < startSecletCol)
                {
                    int ls = EndSecletCol;
                    EndSecletCol = startSecletCol;
                    startSecletCol = ls;
                }

                if (EndtSecletRow == startSecletRow && EndSecletCol == startSecletCol)
                {
                    if (CtrlSelectAllRow)
                    {
                        int nowRowIndex = 0;
                        // 遍历所有行  
                        foreach (DataGridViewRow row in this.Rows)
                        {
                            int nowColIndex = 0;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (nowRowIndex == startSecletRow && nowColIndex == startSecletCol)
                                {
                                    cell.Style.BackColor = SystemColors.Highlight;
                                    cell.Style.ForeColor = Color.White;
                                }
                                else if (nowRowIndex == startSecletRow)
                                {
                                    cell.Style.BackColor = SystemColors.Highlight;
                                    cell.Style.ForeColor = Color.LightSalmon;
                                }
                                else
                                {
                                    cell.Style.BackColor = default;
                                    cell.Style.ForeColor = default;
                                }
                                nowColIndex++;
                            }
                            nowRowIndex++;
                        }
                    }
                    secletDatagrisview.ReadOnly = false;
                }
                else
                {
                    if (CtrlSelectAllRow)
                    {
                        int nowRowIndex = 0;
                        // 遍历所有行  
                        foreach (DataGridViewRow row in this.Rows)
                        {
                            int nowColIndex = 0;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (nowRowIndex >= startSecletRow && nowRowIndex <= EndtSecletRow
                                && nowColIndex >= startSecletCol && nowColIndex <= EndSecletCol)
                                {
                                    cell.Style.BackColor = SystemColors.Highlight;
                                    cell.Style.ForeColor = Color.White;
                                }
                                else
                                {
                                    cell.Style.BackColor = default;
                                    cell.Style.ForeColor = default;
                                }
                                nowColIndex++;
                            }
                            nowRowIndex++;
                        }
                    }
                    secletDatagrisview.ReadOnly = true;
                }
            }
            catch (Exception)
            {

            }
        }

        private void DataGridViewEx_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (CtrlSelectAllRow)
            {
                // 遍历所有行  
                foreach (DataGridViewRow row in this.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = default;
                        cell.Style.ForeColor = default;
                    }
                }
            }
        }

        #endregion

        #region Keys转char
        private static string KeyCodeToChar(Keys keyData)
        {
            char input = (char)keyData;
            string input2 = input.ToString();
            if (keyData <= Keys.Z && keyData >= Keys.A)
            {
                if (!Console.CapsLock)
                {
                    input2 = input2.ToLower();
                }
            }
            else if (keyData <= Keys.NumPad9 && keyData >= Keys.NumPad0)
            {
                input = (char)(keyData - 48);
                input2 = input.ToString();
            }
            return input2;
        }
        #endregion

        #region 字符串计算器
        /// <summary>
        /// 计算字符串解析表达式 1+2(2*(3+4))
        /// </summary>
        /// <param name="str">传入的字符串</param>
        /// <returns>计算得到的结果</returns>
        public double CalcStr(string str)
        {
            double num = 0;
            //数字集合
            List<double> numList = new List<double>();
            //操作符集合
            List<Operation> operList = new List<Operation>();
            string strNum = "";
            bool firstNun = true;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                //判断如果是数字和.
                if ((47 < c && c < 58) || c == '.')
                {
                    firstNun = false;
                    strNum += c;

                    if (i == str.Length - 1)
                    {
                        if (!string.IsNullOrEmpty(strNum))
                        {
                            double.TryParse(strNum, out num);
                            numList.Add(num);
                            strNum = "";
                        }
                    }
                    continue;
                }
                else if (c == '(')
                {
                    firstNun = true;
                    int temp = 1;
                    for (int j = i + 1; j < str.Length; j++)
                    {
                        var k = str[j];
                        if (k == '(')
                        {
                            temp++;
                        }
                        else if (k == ')')
                        {
                            temp--;
                        }

                        if (temp == 0)
                        {
                            temp = j - i - 1;
                        }
                    }

                    strNum = str.Substring(i + 1, temp);
                    numList.Add(CalcStr(strNum));
                    strNum = "";
                    i += temp + 1;
                }
                else
                {
                    if (!string.IsNullOrEmpty(strNum))
                    {
                        double.TryParse(strNum, out num);
                        numList.Add(num);
                        strNum = "";
                    }

                    if (c == '+')
                    {
                        if (firstNun)
                        {
                            strNum += c;
                            firstNun = false;
                        }
                        else
                        {
                            firstNun = true;
                            operList.Add(new AddOperation());
                        }
                    }
                    else if (c == '-')
                    {
                        if (firstNun)
                        {
                            strNum += c;
                            firstNun = false;
                        }
                        else
                        {
                            operList.Add(new SubOperation());
                            firstNun = true;
                        }
                    }
                    else if (c == '*')
                    {
                        firstNun = true;
                        operList.Add(new MultipOperation());
                    }
                    else if (c == '/')
                    {
                        firstNun = true;
                        operList.Add(new DivOperation());
                    }
                    else if (c == '%')
                    {
                        firstNun = true;
                        operList.Add(new ModOperation());
                    }
                    else
                    {
                        operList.Add(null);
                    }
                }
            }

            List<int> tempOrder = new List<int>();
            operList.ForEach(w =>
            {
                if (!tempOrder.Contains(w.PrioRity))
                {
                    tempOrder.Add(w.PrioRity);
                }

            });

            tempOrder.Sort();
            for (int t = 0; t < tempOrder.Count; t++)
            {
                for (int i = 0; i < operList.Count; i++)
                {
                    if (operList[i].PrioRity == tempOrder[t])
                    {
                        numList[i] = operList[i].OperationResult(numList[i], numList[i + 1]);
                        numList.RemoveAt(i + 1);
                        operList.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (numList.Count == 1) return numList[0];

            return 0;
        }

        /// <summary>
        /// 操作
        /// </summary>
        public class Operation
        {
            /// <summary>
            /// 操作优先级
            /// </summary>
            protected int priority = 99;
            /// <summary>
            /// 优先级
            /// </summary>
            public virtual int PrioRity
            {
                get
                {
                    return priority;
                }
                set
                {
                    priority = value;
                }
            }
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public virtual double OperationResult(double a, double b)
            {
                return 0;
            }
        }

        /// <summary>
        ///加操作 
        /// </summary>
        public class AddOperation : Operation
        {
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public override double OperationResult(double a, double b)
            {
                return a + b;
            }
        }

        /// <summary>
        /// 减操作
        /// </summary>
        public class SubOperation : Operation
        {
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public override double OperationResult(double a, double b)
            {
                return a - b;
            }
        }

        /// <summary>
        /// 乘操作
        /// </summary>
        public class MultipOperation : Operation
        {
            /// <summary>
            /// 优先级
            /// </summary>
            public override int PrioRity
            {
                get
                {
                    return 98;
                }
            }
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public override double OperationResult(double a, double b)
            {
                return a * b;
            }
        }

        /// <summary>
        /// 除操作
        /// </summary>
        public class DivOperation : Operation
        {
            /// <summary>
            /// 优先级
            /// </summary>
            public override int PrioRity
            {
                get
                {
                    return 98;
                }
            }
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public override double OperationResult(double a, double b)
            {
                return a / b;
            }
        }

        /// <summary>
        /// 取余操作
        /// </summary>
        public class ModOperation : Operation
        {
            /// <summary>
            /// 优先级
            /// </summary>
            public override int PrioRity
            {
                get
                {
                    return 97;
                }
            }
            /// <summary>
            /// 操作结果
            /// </summary>
            /// <param name="a">参数a</param>
            /// <param name="b">参数b</param>
            /// <returns></returns>
            public override double OperationResult(double a, double b)
            {
                return a % b;
            }
        }


        #endregion
    }
}

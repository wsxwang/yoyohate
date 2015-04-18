using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yoyohate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox_rowcount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBox_colcount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,

                e.RowBounds.Location.Y,

                dataGridView1.RowHeadersWidth - 4,

                e.RowBounds.Height);



            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),

                dataGridView1.RowHeadersDefaultCellStyle.Font,

                rectangle,

                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,

                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button_gen_Click(object sender, EventArgs e)
        {
            int rowCount = int.Parse(textBox_rowcount.Text);
            int colCount = int.Parse(textBox_colcount.Text);
            if ((rowCount <= 0) || (colCount <= 0))
            {
                MessageBox.Show("你脑子秀逗了？");
                return;
            }
            if ((rowCount > 15) || (colCount > 15))
            {
                MessageBox.Show("这绝对不是亲妈，想累死个人啊");
            }
            DataTable dt = new DataTable();
            for (int i = 0; i < colCount; i++)
            {
                dt.Columns.Add((i + 1).ToString());
            }
            for (int i = 0; i < rowCount; i++)
            {
                DataRow r = dt.NewRow();
                for (int j = 0; j < colCount; j++)
                {
                    r[j] = "";
                }
                dt.Rows.Add(r);
            }
            Random ran = new Random();
            for (int i = 0; i < colCount * rowCount / 2; i++)
            {
                do
                {
                    int r = ran.Next(0, rowCount);
                    int c = ran.Next(0, colCount);
                    if (dt.Rows[r][c] as string == "")
                    {
                        dt.Rows[r][c] = i + 1;
                        break;
                    }
                } while (true);
            }
            dataGridView1.DataSource = dt;
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt == null)
            {
                MessageBox.Show("你脑子秀逗了？");
                return;
            }
            string outputText = "";
            foreach (DataRow r in dt.Rows)
            {
                for (int i = 0; i < r.ItemArray.Length; i++)
                {
                    outputText += r[i] as string + ",";
                }
                outputText = outputText.Substring(0, outputText.Length - 1) + "\r\n";
            }
            System.IO.FileStream fs = new System.IO.FileStream("./yoyohate.csv", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            fs.SetLength(0);
            sw.Write(outputText);
            sw.Close();
            fs.Close();
            MessageBox.Show("yoyo会恨你的，去打开yoyohate.csv吧");
        }
    }
}

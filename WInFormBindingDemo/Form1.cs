using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WInFormBindingDemo
{
    public partial class Form1 : Form
    {
        JData.IJDataFile _file = new JData.JDataFile();

        public Form1()
        {
            InitializeComponent();

            string [][] data = new string[][]
            {
                new string[] {"cell_1_1", "cell_1_2", "cell_1_3"},
                new string[] {"cell_2_1", "cell_2_2", "cell_2_3"},
                new string[] {"cell_3_1", "cell_3_2", "cell_3_3"},
            };

            foreach(var line in data) {
                var row = _file.CreateDataRow();
                row.AddValues(line);
            }

           this.dataGridView1.DataSource = _file;
        }
    }
}

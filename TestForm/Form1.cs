﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

		}

        private void button1_Click(object sender, EventArgs e)
        {
			Form2 form = new Form2();
			Point location = SSTools.FormUtils.CalcLocation(button1,form.Size);
			Console.WriteLine("CalcLocation():{0}", location);
			form.Location = location;
			form.ShowDialog();
			form.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ServiceStack.OrmLite;
using Kimtoo.DbContext;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var err = Db.Init(@"Server=KIMTOOFLEX-PC\LOCALHOST;Database=Northwind;Trusted_Connection=True;", SqlServerDialect.Provider);

            if (err != null)
            {
                MessageBox.Show(err.Message);
            }

            var db = Db.Get();

        }

        private void button1_Click(object sender, EventArgs e)
        {
             Db.Close("database2");
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


namespace Reproductor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Media.URL == "" )
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Media.URL = openFileDialog1.FileName;
                }
                Media.Ctlcontrols.play();
            }
            else { 
                Media.Ctlcontrols.play();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Media.uiMode = "invisible";
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Media.URL = openFileDialog1.FileName;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide(); //cerrar formulario actual

            listarepro frm = new listarepro();

            frm.Show();
        }
    }
}

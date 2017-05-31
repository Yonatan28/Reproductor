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
using System.Xml;


namespace Reproductor
{
    public partial class Form1 : Form
    {
        List<reproducir> listareproduci = new List<reproducir>();
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
            this.Hide();
            listarepro frm = new listarepro();

            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Media.Ctlcontrols.stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Media.Ctlcontrols.pause();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
           

        }
      
    }
}

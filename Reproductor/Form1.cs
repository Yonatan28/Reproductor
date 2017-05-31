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
using System.Xml.Linq;



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
            int max = listareproduci.Count;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (label1.Text==listareproduci[i].Nombre)
                {
                    if (i == max)
                    {
                        Media.URL = listareproduci[0].Url;
                        label1.Text= listareproduci[0].Nombre;
                        break;
                    }
                    else {
                        Media.URL = listareproduci[i + 1].Url;
                        label1.Text = listareproduci[i+1].Nombre;
                        break;
                    }
                       
                }
            }
            Media.Ctlcontrols.play();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        public void sigycargar()
        {
            
        }
        private void button8_Click(object sender, EventArgs e)
        {
            leerxml();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listareproduci;
            dataGridView1.Columns["url"].Visible = false;
            dataGridView1.Refresh();

        }
      public void leerxml()
        {
            XDocument documento = XDocument.Load(@"C:\\Users\\Yonatan Coti\\Documents\\Visual Studio 2015\\Projects\\Reproductor\\Reproductor\\bin\\Debug\\miXML.xml");
            var listar = from lis in documento.Descendants("Lista_Favoritos") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                reproducir tmp = new reproducir();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Url = u.Element("Url").Value;
                listareproduci.Add(tmp);
             
            }
        }
    }
}

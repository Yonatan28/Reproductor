using System;
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
    public partial class listarepro : Form
    {
        List<reproducir> listareproduci = new List<reproducir>();
        public listarepro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            reproducir reprotemp = new reproducir();
            reprotemp.Url = openFileDialog1.FileName;
            reprotemp.Nombre =openFileDialog1.SafeFileName.ToString();
            listareproduci.Add(reprotemp);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listareproduci;
            dataGridView1.Refresh();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

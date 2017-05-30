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
        Form1 frm = new Form1();
        
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
            cargar();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void cargar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listareproduci;
            dataGridView1.Columns["url"].Visible = false;
            dataGridView1.Refresh();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            frm.Hide();
            frm.label1.Text = " ";
            frm.label1.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            frm.Media.URL= dataGridView1.CurrentRow.Cells["url"].Value.ToString();
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void listarepro_Load(object sender, EventArgs e)
        {
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           string nom = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (nom == listareproduci[i].Nombre)
                {
                    listareproduci.RemoveAt(i);
                }
            }
            cargar();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int contar = listareproduci.Count();
            int inicio = 0;
            listareproduci.RemoveRange(inicio,contar);
            cargar();
        }
    }
}

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
using System.Xml;

namespace Reproductor
{
    public partial class listarepro : Form
    {
        Form1 frm = new Form1();
      
        List<reproducir> listareproduci = new List<reproducir>();
        List<datosmp3> listadatosmp3 = new List<datosmp3>();
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
            xml();
        }
        public void xml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Lista_Favoritos");
            doc.AppendChild(raiz);

            XmlElement cancion = doc.CreateElement("Cancion");
            

            XmlElement titulo = doc.CreateElement("Titulo");
          

            XmlElement url = doc.CreateElement("Url");
          
            for (int i = 0; i < listareproduci.Count(); i++)
            {
                //nuevo documento
                cancion = doc.CreateElement("Cancion");
                raiz.AppendChild(cancion);

                titulo = doc.CreateElement("Titulo");
                titulo.AppendChild(doc.CreateTextNode(listareproduci[i].Nombre));
                cancion.AppendChild(titulo);

                url = doc.CreateElement("Url");
                url.AppendChild(doc.CreateTextNode(listareproduci[i].Url));
                cancion.AppendChild(url);

                doc.Save(@"miXML.xml");
            }
 

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
            string nom = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            label1.Text = nom;
          

        }
        public void limn(int c)
        {

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
            frm.Hide();
            frm.label1.Text = " ";

            frm.label1.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            frm.Media.URL = dataGridView1.CurrentRow.Cells["url"].Value.ToString();

            WMPLib.IWMPPlaylist playlist = frm.Media.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            media = frm.Media.newMedia(dataGridView1.CurrentRow.Cells["url"].Value.ToString());
            playlist.appendItem(media);

            frm.Media.currentPlaylist = playlist;
            listadatosmp3.RemoveRange(0,listadatosmp3.Count);
            string dat = dataGridView1.CurrentRow.Cells["url"].Value.ToString();
            cargarima(dat);
            TagLib.File file = TagLib.File.Create(dat);
            datosmp3 datmp = new datosmp3();
            datmp.Titulo1 = file.Tag.Title;
            datmp.Album1 = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Num = file.Properties.Duration.ToString();
            datmp.Artista1 = file.Tag.Album;
            listadatosmp3.Add(datmp);
            frm.dataGridView1.DataSource = null;
            frm.dataGridView1.Refresh();
            frm.dataGridView1.DataSource = listadatosmp3;
           frm.dataGridView1.Refresh();
            frm.Show();

        }
        public void cargarima(string dat)
        {
            TagLib.File file = TagLib.File.Create(dat);
            System.Drawing.Image currentImage = null;

            // In method onclick of the listbox showing all mp3's

            if (file.Tag.Pictures.Length > 0)
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                if (ms != null && ms.Length > 4096)
                {
                    currentImage = System.Drawing.Image.FromStream(ms);
                    // Load thumbnail into PictureBox
                    frm.caratula.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                }
                ms.Close();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int contar = listareproduci.Count();
            int inicio = 0;
            listareproduci.RemoveRange(inicio,contar);
            cargar();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (nomb == listareproduci[i].Nombre)
                {
                    listareproduci.RemoveAt(i);
                }
            }
            cargar();
            
        }
    }
}

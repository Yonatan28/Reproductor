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
        List<datosmp3> listadatosmp3 = new List<datosmp3>();
        public Form1()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (Media.URL == "")
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Media.URL = openFileDialog1.FileName;
                }
               
                Media.Ctlcontrols.play();
                tag(openFileDialog1.FileName);



            }

        
            else { 
                Media.Ctlcontrols.play();
            }
        }
        public void tag(string dato)
        {
            string dat = dato;
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
                    caratula.Image = currentImage.GetThumbnailImage(100, 100, null, System.IntPtr.Zero);
                }
                ms.Close();
            }

            datosmp3 datmp = new datosmp3();
            datmp.Titulo1 = file.Tag.Title;
            datmp.Album1 = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Num = file.Properties.Duration.ToString();
            datmp.Artista1 = file.Tag.Album;
            listadatosmp3.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listadatosmp3;
            dataGridView1.Refresh();
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
            listareproduci.RemoveRange(0, listareproduci.Count);
            actualizar();
            int max = listareproduci.Count;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (label1.Text==listareproduci[i].Nombre)
                {
                    if (i == max-1)
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
            listadatosmp3.RemoveRange(0,listadatosmp3.Count);
            tag(Media.URL);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0,listareproduci.Count);
            actualizar();
            int max = listareproduci.Count;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (label1.Text == listareproduci[i].Nombre)
                {
                    if (i == 0)
                    {
                        Media.URL = listareproduci[max-1].Url;
                        label1.Text = listareproduci[max-1].Nombre;
                        break;
                    }
                    else
                    {
                        Media.URL = listareproduci[i - 1].Url;
                        label1.Text = listareproduci[i - 1].Nombre;
                        break;
                    }

                }
            }
            listadatosmp3.RemoveRange(0, listadatosmp3.Count);
            tag(Media.URL);
        }
        public void sigycargar()
        {
            
        }
        public void actualizar()
        {
            if (listareproduci.Count==0)
            {
                leerxml();
            }
          
        }
        private void button8_Click(object sender, EventArgs e)
        {
         

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

        private void macTrackBar1_ValueChanged(object sender, decimal value)
        {
           
            Media.settings.volume = macTrackBar1.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0, listareproduci.Count);
            macTrackBar1.Value = Media.settings.volume;
            actualizar();
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (Media.URL==listareproduci[i].Url)
                {
                    label1.Text = listareproduci[i].Nombre;
                }
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listareproduci.RemoveRange(0, listareproduci.Count);
            actualizar();
            var myPlayList = Media.playlistCollection.newPlaylist("MyPlayList");
     
            for (int i = 0; i < listareproduci.Count; i++)
            {
                var mediaItem = Media.newMedia(listareproduci[i].Url);
                myPlayList.appendItem(mediaItem);
            }
            Media.currentPlaylist = myPlayList;
           
        }

        private void Media_MediaChange(object sender, AxWMPLib._WMPOCXEvents_MediaChangeEvent e)
        {
           
         
        }

        private void Media_PlaylistChange(object sender, AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {
           
        }

        private void Media_CdromMediaChange(object sender, AxWMPLib._WMPOCXEvents_CdromMediaChangeEvent e)
        {
          
        }

        private void caratula_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        List<Biblioteca> listabiblio = new List<Biblioteca>();
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
                label1.Text = openFileDialog1.Title;


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
                    caratula.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }

            datosmp3 datmp = new datosmp3();
            datmp.Titulo = file.Tag.Title;
            datmp.Num = Convert.ToString(file.Tag.Track);
            datmp.Album = file.Tag.Album;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Duracion = file.Properties.Duration.ToString();
            datmp.Artista = file.Tag.FirstArtist;
            datmp.Comentario = file.Tag.Comment;
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
           
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Media.URL = openFileDialog1.FileName;
                }
            listadatosmp3.RemoveRange(0,listadatosmp3.Count);
                Media.Ctlcontrols.play();
                tag(openFileDialog1.FileName);
                label1.Text = openFileDialog1.Title;

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide(); //cerrar formulario actual
            Media.Ctlcontrols.stop();
            this.Hide();
            listarepro frm = new listarepro();
            frm.Hide();
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
            if (listareproduci.Count==0)
            {
                listabiblio.RemoveRange(0,listabiblio.Count);
                actualizar2();
                int max2 = listabiblio.Count;
                for (int i = 0; i < listabiblio.Count; i++)
                {
                    if (label1.Text == listabiblio[i].Nombre)
                    {
                        if (i == max2 - 1)
                        {
                            Media.URL = listabiblio[0].Url;
                            label1.Text = listabiblio[0].Nombre;
                            break;
                        }
                        else
                        {
                            Media.URL = listabiblio[i + 1].Url;
                            label1.Text = listabiblio[i + 1].Nombre;
                            break;
                        }

                    }
                }
            }
            else
            {
                listareproduci.RemoveRange(0, listareproduci.Count);
                actualizar();
                int max = listareproduci.Count;
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    if (label1.Text == listareproduci[i].Nombre)
                    {
                        if (i == max - 1)
                        {
                            Media.URL = listareproduci[0].Url;
                            label1.Text = listareproduci[0].Nombre;
                            break;
                        }
                        else
                        {
                            Media.URL = listareproduci[i + 1].Url;
                            label1.Text = listareproduci[i + 1].Nombre;
                            break;
                        }

                    }
                }
               
            }
            Media.Ctlcontrols.play();
            listadatosmp3.RemoveRange(0, listadatosmp3.Count);
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
        public void actualizar2()
        {
            if (listabiblio.Count == 0)
            {
                leerbiblio();
            }

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
        public void leerbiblio()
        {
            XDocument documento = XDocument.Load(@"biblio.xml");
            var listar = from lis in documento.Descendants("Blibioteca") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                Biblioteca tmp = new Biblioteca();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Url = u.Element("Url").Value;
                tmp.Num = u.Element("No").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Duracion = u.Element("Duracion").Value;
                tmp.Calidad = u.Element("Calidad").Value;

                listabiblio.Add(tmp);

            }
        }
        public void leerxml()
        {
            XDocument documento = XDocument.Load(@"miXML.xml");
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
         
            macTrackBar1.Value = Media.settings.volume;
            actualizar();
           
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

        private void abrirListaFavoritosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
        public void eliminarlisre()
        {
             XmlDocument documento = new XmlDocument();
             string ruta = @"miXML.xml";
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Lista_Favoritos/Cancion");

            foreach (XmlNode item in listacancion)
            {
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    //Determinamos el nodo a modificar por medio del id de empleado.
                    if (item.FirstChild.InnerText == listareproduci[i].Nombre)
                    {
                        //Nodo sustituido.
                        XmlNode nodoOld = item;
                        bibliot.RemoveChild(nodoOld);
                    }
                }

                //Salvamos el documento.
                documento.Save(ruta);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            eliminarlisre();
            Application.ExitThread();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
            this.WindowState= FormWindowState.Minimized;
         
        }
    }
}

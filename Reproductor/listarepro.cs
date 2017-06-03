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
using System.Xml.Linq;


namespace Reproductor
{
    public partial class listarepro : Form
    {
        Form1 frm = new Form1();
        static XmlDocument documento = new XmlDocument();
        static string ruta = @"biblio.xml";
        List<reproducir> listareproduci = new List<reproducir>();
        List<datosmp3> listadatosmp3 = new List<datosmp3>();
        List<Biblioteca> listabiblioteca = new List<Biblioteca>();
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
    
        public void EscribirXml()
        {
            for (int i = 0; i < listabiblioteca.Count(); i++)
            {
                if (label1.Text == listabiblioteca[i].Nombre)
                {
                    //Creamos el escritor.
                    using (XmlTextWriter Writer = new XmlTextWriter(@"biblio.xml", Encoding.UTF8))
                    {
                        //Declaración inicial del Xml.
                        Writer.WriteStartDocument();

                        //Configuración.
                        Writer.Formatting = Formatting.Indented;
                        Writer.Indentation = 5;

                        //Escribimos el nodo principal.
                        Writer.WriteStartElement("Blibioteca");

                        //Escribimos un nodo empleado.
                        Writer.WriteStartElement("Cancion");

                        //Escribimos cada uno de los elementos del nodo empleado.
                        Writer.WriteElementString("nombre", listabiblioteca[i].Nombre);
                        Writer.WriteElementString("url", listabiblioteca[i].Url);
                        Writer.WriteElementString("num", listabiblioteca[i].Num);
                        Writer.WriteElementString("album", listabiblioteca[i].Album);
                        //Escribimos el subnodo teléfono.
                        Writer.WriteElementString("duracion", listabiblioteca[i].Duracion);
                        Writer.WriteElementString("calidad", listabiblioteca[i].Calidad);

                        //Cerramos el nodo y el documento.
                        Writer.WriteEndElement();
                        Writer.WriteEndDocument();
                        Writer.Flush();
                    }
                }
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
            button6.Visible = false;
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
            tagcan(dat);
            frm.dataGridView1.DataSource = null;
            frm.dataGridView1.Refresh();
            frm.dataGridView1.DataSource = listadatosmp3;
           frm.dataGridView1.Refresh();
            frm.Show();

        }
        public void tagcan(string car)
        {
            TagLib.File file = TagLib.File.Create(car);
            datosmp3 datmp = new datosmp3();
            datmp.Titulo = file.Tag.Title;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Duracion = file.Properties.Duration.ToString();
            datmp.Num = Convert.ToString(file.Tag.Track);
            datmp.Artista = file.Tag.TitleSort;
            datmp.Album = file.Tag.Album;
            datmp.Comentario = file.Tag.Comment;
            listadatosmp3.Add(datmp);
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
                    frm.caratula.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                Biblioteca blitmp = new Biblioteca();
                if (nomb == listareproduci[i].Nombre)
                {
                    blitmp.Url = listareproduci[i].Url;
                    blitmp.Nombre = listareproduci[i].Nombre;
                    TagLib.File file = TagLib.File.Create(listareproduci[i].Url);
                    blitmp.Titulo = file.Tag.Title;
                    blitmp.Año = Convert.ToString(file.Tag.Year);
                    blitmp.Duracion = file.Properties.Duration.ToString();
                    blitmp.Num = Convert.ToString(file.Tag.Track);
                    blitmp.Album = file.Tag.Album;
                    blitmp.Calidad = Convert.ToString(file.Properties.AudioBitrate);
                
                }
                listabiblioteca.Add(blitmp);
            }
            string archivo = @"biblio.xml";
            if (File.Exists(archivo)==true)
            {
                InsertarXml();
            }
            else { EscribirXml(); }
                
        }
        string nombre1, url, num, album, dura, cali;

        private void button6_Click(object sender, EventArgs e)
        {
            listabiblioteca.RemoveRange(0,listabiblioteca.Count);
            string nom=label1.Text;
            ModificarDatosXml(nom);
            leerbiblio();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listabiblioteca;
            dataGridView1.Refresh();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listabiblioteca.RemoveRange(0,listabiblioteca.Count);
            listareproduci.RemoveRange(0,listareproduci.Count);
            button2.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            button6.Visible = true;
            leerbiblio();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listabiblioteca;
            dataGridView1.Refresh();
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

                listabiblioteca.Add(tmp);

            }
        }
        private void InsertarXml()
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
           
            for (int i = 0; i < listabiblioteca.Count(); i++)
            {

                if (label1.Text == listabiblioteca[i].Nombre)
                {

                    nombre1 = listabiblioteca[i].Nombre;
                    url = listabiblioteca[i].Url;
                    num = listabiblioteca[i].Num;
                    album = listabiblioteca[i].Album;
                    dura = listabiblioteca[i].Duracion;
                    cali = listabiblioteca[i].Calidad;

                }
            }
            //Creamos el nodo que deseamos insertar.
            XmlNode empleado = this.CrearNodoXml(nombre1, url, num, album,dura, cali);
            //Obtenemos el nodo raiz del documento.
            XmlNode nodoRaiz = documento.DocumentElement;

            //Insertamos el nodo empleado al final del archivo
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);   //***

            documento.Save(ruta);
        }
        private XmlNode CrearNodoXml(string nom1, string url1, string num1, string album1, string dura1, string cali1)
        {
            //Creamos el nodo que deseamos insertar.
            XmlElement Cancion = documento.CreateElement("Cancion");

            //Creamos el elemento idEmpleado.
            XmlElement nombre = documento.CreateElement("Titulo");
            nombre.InnerText = nom1;
            Cancion.AppendChild(nombre);

            //Creamos el elemento nombre.
            XmlElement Url = documento.CreateElement("Url");
            Url.InnerText = url1;
            Cancion.AppendChild(Url);

            //Creamos el elemento apellidos.
            XmlElement num = documento.CreateElement("No");
            num.InnerText = num1;
            Cancion.AppendChild(num);

            //Creamos el elemento numeroSS.
            XmlElement album = documento.CreateElement("Album");
            album.InnerText = album1;
            Cancion.AppendChild(album);

            //Creamos el elemento fijo.
            XmlElement duracion = documento.CreateElement("Duracion");
            duracion.InnerText = dura1;
            Cancion.AppendChild(duracion);
            //Creamos el elemento movil.
            XmlElement calidad = documento.CreateElement("Calidad");
            calidad.InnerText = cali1;
            Cancion.AppendChild(calidad);

            return Cancion;
        }
        public void ModificarDatosXml(string url)
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Blibioteca/Cancion");

            foreach (XmlNode item in listacancion)
            {
                //Determinamos el nodo a modificar por medio del id de empleado.
                if (item.FirstChild.InnerText == url)
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
}

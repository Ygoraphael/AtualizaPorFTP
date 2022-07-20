using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtualizaPorFTP
{
    public partial class PaginaLoad : Form
    {
        public PaginaLoad()
        {
            InitializeComponent();
        }

        private void PaginaLoad_Shown(object sender, EventArgs e)
        {
            Pic.Update();
            lbl.Text = "ESTAMOS A ATUALIZAR O SEU PHC...";
            lbl.Update();
            VerificaFTP();
        }

        public void VerificaFTP()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            path = path + "ParametrosNovosCanais.txt";
            if (File.Exists(@path))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@path);
                String llocalFile = "";
                String lremoteFile = "/Exec/";
                int counter = 0;
                string tmpline;
                string CaminhoPHC = "";
                while ((tmpline = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        llocalFile = llocalFile + tmpline;
                    }
                    if (counter == 1)
                    {
                        lremoteFile = lremoteFile + tmpline;
                    }
                    if (counter == 2)
                    {
                        CaminhoPHC = tmpline;
                    }

                    counter++;
                }

                if ((llocalFile != "") && (lremoteFile != "/Exec/"))
                {
                    String _host = "ftp.novoscanais.com";


                    String lusername = "cliente@novoscanais.com";
                    String lpassword = "nc2018!+";
                    String lCompleteWay = "ftp://" + _host + lremoteFile;
                    FtpWebResponse response;
                    FtpWebRequest request;
                    bool Atualizar = false;
                    Atualizar = VerificaStatus(lusername, lpassword, lCompleteWay + "/Atualizar.txt");

                    if (Atualizar)
                    {
                        int CountFiles = ContadorFiles(lusername, lpassword, lCompleteWay);

                        request = (FtpWebRequest)WebRequest.Create(lCompleteWay);
                        request.Credentials = new NetworkCredential(lusername, lpassword);
                        request.Method = WebRequestMethods.Ftp.ListDirectory;
                        response = (FtpWebResponse)request.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        string line = streamReader.ReadLine();
                        int pos = 0;
                        while (!string.IsNullOrEmpty(line))
                        {
                            if ((line != ".") && (line != "..") && (line != ""))
                            {
                                pos++;
                                float Valor = pos * 100;
                                Valor = (Valor / CountFiles);
                                Loading.Value = Convert.ToInt32(Valor);
                                Loading.Update();
                                lbl.Text = "ESTAMOS A ATUALIZAR O SEU PHC... " + Math.Round(Valor).ToString();
                                lbl.Update();
                                CopiaArquivos(lusername, lpassword, lCompleteWay + "/" + line, llocalFile + "/" + line);
                            }
                            line = streamReader.ReadLine();                            
                        }
                        streamReader.Close();
                        CriarLogAtualizacoes();
                        System.Diagnostics.Process.Start(CaminhoPHC);
                        this.Close();
                        //MessageBox.Show("Atualização foi um sucesso!!");

                    }
                    else
                    {
                        System.Diagnostics.Process.Start(CaminhoPHC);
                        this.Close();
                        //MessageBox.Show("Sem atualizações!!!!!!!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Não existe arquivo ParametrosNovosCanais na pasta do executável.");
                this.Close();
            }
        }


        private static bool VerificaStatus(String puser, String ppass, String pFtp)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            path = path + "LogAtualizaNovosCanais.txt";
            bool retorno = false;
            bool IgnoraLogs = true;
            bool AcceptData = true;
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(puser, ppass);
                String Texto = client.DownloadString(pFtp);
                StringReader str = new StringReader(Texto);
                String line;
                int Count = 0;
                while ((line = str.ReadLine()) != null)
                {
                    if ((Count == 0) && (line.ToUpper().Trim() == "IGNORALOG"))
                    {
                        IgnoraLogs = false;
                    }
                    if ((Count == 1) && (File.Exists(@path)) && (IgnoraLogs))
                    {
                        System.IO.StreamReader Logfile = new System.IO.StreamReader(@path);
                        String Logline;
                        while ((Logline = Logfile.ReadLine()) != null)
                            if (Convert.ToDateTime(line).Date > Convert.ToDateTime(Logline).Date)
                            {
                                AcceptData = true;
                                retorno = true;
                            }
                            else
                            {
                                AcceptData = false;
                                retorno = false;
                            }
                    }
                    if ((Count == 2) && (line.ToUpper().Trim() == "TRUE") && (AcceptData))
                    {
                        retorno = true;
                    }
                    Count++;
                }
            }

            return retorno;
        }

        private static int ContadorFiles(String puser, String ppass, String pFtp)
        {
            FtpWebResponse tmpresponse;
            FtpWebRequest tmprequest;
            tmprequest = (FtpWebRequest)WebRequest.Create(pFtp);
            tmprequest.Credentials = new NetworkCredential(puser, ppass);
            tmprequest.Method = WebRequestMethods.Ftp.ListDirectory;
            tmpresponse = (FtpWebResponse)tmprequest.GetResponse();
            StreamReader CountStream = new StreamReader(tmpresponse.GetResponseStream());
            string Countline = CountStream.ReadLine();
            int CountFiles = 0;
            while (!string.IsNullOrEmpty(Countline))
            {
                if ((Countline != ".") && (Countline != "..") && (Countline != "")) { CountFiles++; }
                Countline = CountStream.ReadLine();
            }
            return CountFiles;
        }

        private static void CopiaArquivos(String puser, String ppass, String pFtp, String pLocalFiles)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(puser, ppass);
                byte[] fileData = client.DownloadData(pFtp);
                using (FileStream file = File.Create(pLocalFiles))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }

        }

        private static void CriarLogAtualizacoes()
        {
            StreamWriter s;
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            path = path + "LogAtualizaNovosCanais.txt";

            if (File.Exists(@path))
            {
                s = File.AppendText(path);
            }
            else
            {
                s = File.CreateText(path);
            }

            s.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            s.Close();
        }

        private void PaginaLoad_Activated(object sender, EventArgs e)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(3000);
        }
    }

}

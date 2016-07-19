using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using HtmlAgilityPack;
namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {

        string ShipName;
        string IMONO;

        HtmlDocument doc = null;
        int step = 0;
        public Form1() {
            InitializeComponent();
            webBrowser1.Visible = true;
            webBrowser1.Navigate("http://212.45.16.136/isss/public_apcis.php?Action=getSearchForm");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {

            step++;
            switch (step) {
                case 1:
                    doc = webBrowser1.Document as HtmlDocument;
                    doc.GetElementById("Val1").SetAttribute("value", "9160891");
                    //doc.GetElementById("Val1").SetAttribute("checked", "true");
                    var button = doc.GetElementsByTagName("input")[2];
                    button.InvokeMember("click");
                    //MessageBox.Show(webBrowser1.DocumentText);
                    break;
                case 2:
                    doc = webBrowser1.Document as HtmlDocument;
                    HtmlElementCollection tables = doc.GetElementsByTagName("table");

                    foreach (HtmlElement table in tables) {
                        int start = table.InnerText.IndexOf("Ship Name");
                        if (start == -1)
                            return;
                        else {
                            ShipName = table.InnerText.Substring(table.InnerText.IndexOf("Ship Name: ") + "Ship Name: ".Length, table.InnerText.IndexOf("IMO No.") - "IMO NO.".Length - 6);
                            break;
                        }

                    }

                    foreach (HtmlElement table in tables) {
                        int start = table.InnerText.IndexOf("IMO No.: ");
                        if (start == -1)
                            return;
                        else {
                            IMONO = table.InnerText.Substring(table.InnerText.IndexOf("IMO No.: ") + "IMO No.: ".Length, table.InnerText.IndexOf("Call Sign") - "Call Sign".Length);
                            IMONO = IMONO.Substring(0, IMONO.IndexOf(" "));
                            break;
                        }

                    }
                    MessageBox.Show("IMO NO : " + IMONO + "\n" + "Ship Name : " + ShipName + "\n");


                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }
    }
}

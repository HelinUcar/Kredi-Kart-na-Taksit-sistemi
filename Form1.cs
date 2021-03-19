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

namespace final1soru
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Uyari()
        {
            MessageBox.Show("Bu alanı boş geçemezsiniz.");
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar) == false) && (Char.IsControl(e.KeyChar) == false))
                e.KeyChar = '\0';
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Cyan;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            if (textBox1.Text == "")
            {
                Uyari();
                textBox1.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = "0123456789";
            if (str.IndexOf(e.KeyChar) != -1)
                e.KeyChar = '\0';
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.HotPink;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            textBox2.Text = textBox2.Text.ToUpper();
            if (textBox2.Text == "")
            {
                Uyari();
                textBox2.Focus();
            }
        }
        ToolTip bilgi = new ToolTip();
        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            bilgi.ToolTipIcon = ToolTipIcon.Info;
            bilgi.ToolTipTitle = "Bilgilendirme";
            bilgi.Show("1- Halkbank (3), Vakıfbank (4) ve Ziraatbank (5) taksit seçildiğinde peşin fiyatına taksitlendirme yapılır.\n2 " +
                "2- Diğer taksit seçeneklerinde aylık % 2 faiz uygulanır.\n3 " +
                "3- Diğer kartlar ile aylık % 2 faiz uygulanır.\n4 " +
                "4- En fazla 6 taksit seçilebilir.\n5 " +
                "5- Nakit ve tek çekimde ürün fiyatı uygulanır", comboBox1);

        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            bilgi.Hide(comboBox1);
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBox1.BackColor = Color.LightGoldenrodYellow;
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
			comboBox1.BackColor = Color.White;
            if (comboBox1.Text == "")
            {
                Uyari();
                comboBox1.Focus();
            }
		}

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            comboBox2.BackColor = Color.SkyBlue;
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            comboBox2.BackColor = Color.White;
            if (comboBox2.Text == "")
            {
                Uyari();
                comboBox2.Focus();
            }
        }

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            maskedTextBox1.BackColor = Color.LightSalmon;
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            maskedTextBox1.BackColor = Color.White;
            if (maskedTextBox1.Text == "")
            {
                Uyari();
                maskedTextBox1.Focus();
            }
        }
		private void Form1_Load_1(object sender, EventArgs e)
		{
			comboBox1.Items.Add("Nakit");
			comboBox1.Items.Add("Halkbank");
			comboBox1.Items.Add("Vakıfbank");
			comboBox1.Items.Add("Ziraat Bankası");
			comboBox1.Items.Add("Diğer kart seçenekleri");

			comboBox2.Items.Add("Tek çekim");
			comboBox2.Items.Add("2");
			comboBox2.Items.Add("3");
			comboBox2.Items.Add("4");
			comboBox2.Items.Add("5");
			comboBox2.Items.Add("6");
			timer1.Start();

		}
		private string mesaj()
		{
			string mesaj;
			if (string.Compare(textBox1.Text, "") > 0 &&
			   string.Compare(textBox2.Text, "") > 0 &&
			   comboBox1.SelectedIndex > -1 &&
			   comboBox2.SelectedIndex > -1 &&
			   string.Compare(maskedTextBox1.Text, "") > 0)
			{
				
				string banka = comboBox1.Text;
				string taksitSecim = comboBox2.Text;
				float faiz = 0;

				
				int taksit = 0;
				switch (taksitSecim)
				{
					case "Tek Çekim":
						taksit = 1;
						break;
					default:
                        taksit = int.Parse(taksitSecim);
						break;
				}

				switch (banka)
				{
					case "Halkbank":
						if (taksit > 3) faiz = 0.02f;
						break;
					case "Vakıfbank":
						if (taksit > 4) faiz = 0.02f;
						break;
					case "Ziraatbank":
						if (taksit > 5) faiz = 0.02f;
						break;
					case "Nakit":
						taksit = 1;
						faiz = 0f;
						break;
                    case "Diğer kart seçenekleri":
                        faiz = 0.02f;
						break;
				}
				
				float tutar = float.Parse(textBox1.Text);

				float toplamTutar = tutar + tutar * faiz * taksit;

				float aylikTaksit = toplamTutar / taksit;

				mesaj = "Seçiminiz\t\t: " + banka +
							 "\nTaksit Sayısı\t\t:" + taksit +
							 "\n" + toplamTutar + " borcunuzu aylık " + aylikTaksit + " olarak ödeyebilirsiniz." +
							 "\n Tarih:\t\t" + dateTimePicker1.Text +
							 "\n Saat:\t\t" + label6.Text;
			}
			else
			{
			
				mesaj = "Tüm bilgileri giriniz!";
			}
			return mesaj;
		}
		private void button1_Click(object sender, EventArgs e)
	    {
            DialogResult sonuc = MessageBox.Show(mesaj(), "", MessageBoxButtons.OK);
        }
        Font fontum;
        SolidBrush renk;

        private void button2_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            fontum = new Font(fontDialog1.Font.Name, fontDialog1.Font.Size, fontDialog1.Font.Style);
            colorDialog1.ShowDialog();
            renk = new SolidBrush(colorDialog1.Color);
            printDialog1.ShowDialog();
            printDocument1.Print();
        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
               
                Font printFont = fontum;

                float yPos = topMargin + (1 *
                    printFont.GetHeight(e.Graphics));
                
                e.Graphics.DrawString(mesaj(), fontum, renk,
                   leftMargin, yPos, new StringFormat());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream dosya;
            SaveFileDialog dosyakaydet = new SaveFileDialog();
            dosyakaydet.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; dosyakaydet.FilterIndex = 2;
            dosyakaydet.RestoreDirectory = true;
            if (dosyakaydet.ShowDialog() == DialogResult.OK)
            {
                if ((dosya = dosyakaydet.OpenFile()) != null)
                {
                    StreamWriter a = new StreamWriter(dosya);
                    a.Write(comboBox1.Text + comboBox2.Text);
                    a.Close();
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            label6.Text = datetime.ToString("HH:mm:ss");
        }
    }
}

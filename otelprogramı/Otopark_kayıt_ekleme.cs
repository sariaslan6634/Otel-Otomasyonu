using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace otelprogramı
{
    public partial class Otopark_kayıt_ekleme : Form
    {
        public Otopark_kayıt_ekleme()
        {
            InitializeComponent();
        }
        //access veri tabanına baglanti kuruldu
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel.accdb");        
        private void button1_Click(object sender, EventArgs e)
        {
            //Kayıt Ekleme İşlemleri
            DialogResult tus = MessageBox.Show("Kayıt eklemek istiyor musunuz?", "Kayıt ekleme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || textBox3.Text == "" )
                {
                    MessageBox.Show("Bütün Alanları doldurunuz.");
                }
                else if (maskedTextBox1.TextLength < 10)
                {
                    MessageBox.Show("Lütfen Telefon alanını kontrol ediniz ve Basında 0 olmadan giriniz.");
                }
                else
                {
                    if (tus == DialogResult.Yes)
                    {
                        baglanti.Open();
                        OleDbCommand komut = new OleDbCommand("INSERT INTO otopark(plaka,ad,kaldıgıoda,tel)VALUES(@a,@b,@c,@d)", baglanti);
                        komut.Parameters.AddWithValue("@a", textBox1.Text);
                        komut.Parameters.AddWithValue("@b", textBox2.Text);
                        komut.Parameters.AddWithValue("@c", textBox3.Text);
                        komut.Parameters.AddWithValue("@d", maskedTextBox1.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt  eklendi");
                        this.Close();
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt ekleme sırasında bir hata oldu.","kayıt ekleme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Kayıt Düzeltme İşlemleri
            DialogResult tus = MessageBox.Show("Kayıt düzeltmek istiyor musunuz?", "Kayıt düzeltme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Bütün Alanları doldurunuz.");
                }
                else if (maskedTextBox1.TextLength < 10)
                {
                    MessageBox.Show("Lütfen Telefon alanını kontrol ediniz ve Basında 0 olmadan giriniz.");
                }
                else
                {
                    if (tus == DialogResult.Yes)
                    {
                        baglanti.Open();
                        OleDbCommand komut = new OleDbCommand("UPDATE otopark SET plaka =@a,ad=@b,kaldıgıoda=@c,tel=@d WHERE id=@id", baglanti);
                        komut.Parameters.AddWithValue("@a", textBox1.Text);
                        komut.Parameters.AddWithValue("@b", textBox2.Text);
                        komut.Parameters.AddWithValue("@c", textBox3.Text);
                        komut.Parameters.AddWithValue("@d", maskedTextBox1.Text);
                        komut.Parameters.AddWithValue("@id", textBox5.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt düzeltildi");
                        this.Close();
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt düzeltme sırasında bir hata oldu.", "kayıt düzeltme işlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //metin kutusuna büyük harfle yazı yazdırma işlemi
            textBox1.Text = textBox1.Text.ToUpper();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void Otopark_kayıt_ekleme_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}

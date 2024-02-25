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
    public partial class Personel_kayıt_ekleme : Form
    {
        public Personel_kayıt_ekleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel.accdb");
        private void button1_Click(object sender, EventArgs e)
        {
            //kayıt ekleme işlemi
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("Insert INTO personel(p_ad,p_soyad,p_tc,p_tel,p_maas,p_isebaslamatarihi) VALUES (@a,@b,@c,@d,@e,@f)", baglanti);
                komut.Parameters.AddWithValue("@a",textBox1.Text);
                komut.Parameters.AddWithValue("@b",textBox2.Text);
                komut.Parameters.AddWithValue("@c",maskedTextBox2.Text);
                komut.Parameters.AddWithValue("@d",maskedTextBox3.Text);
                komut.Parameters.AddWithValue("@e",textBox5.Text);
                komut.Parameters.AddWithValue("@f",maskedTextBox1.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt başarılı bir şekilde eklendi", "PERSONEL KAYIT EKLEME", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt ekleme işleminde bir hata oldu!", "PERSONEL KAYIT EKLEME", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Kayıt düzetme işlemi//Kayıt Düzeltme İşlemleri

            DialogResult tus = MessageBox.Show("Kayıt düzeltmek istiyor musunuz?", "Kayıt düzeltme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (tus == DialogResult.Yes)
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("UPDATE personel SET p_ad =@a,p_soyad= @b,p_tc= @c,p_tel= @d,p_maas= @e,p_isebaslamatarihi =@f WHERE id=@id", baglanti);
                    komut.Parameters.AddWithValue("@a", textBox1.Text);
                    komut.Parameters.AddWithValue("@b", textBox2.Text);
                    komut.Parameters.AddWithValue("@c", maskedTextBox2.Text);
                    komut.Parameters.AddWithValue("@d", maskedTextBox3.Text);
                    komut.Parameters.AddWithValue("@e", textBox5.Text);
                    komut.Parameters.AddWithValue("@f", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@id",textBox7.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıt düzeltildi");
                    this.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt düzeltme işleminde bir hata oldu!", "PERSONEL KAYIT DÜZELTME", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //metin kutusuna büyük harfle yazı yazdırma işlemi
            textBox2.Text = textBox2.Text.ToUpper();
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        private void Personel_kayıt_ekleme_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}

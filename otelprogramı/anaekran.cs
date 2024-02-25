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
    public partial class anaekran : Form
    {
        public anaekran()
        {
            InitializeComponent();
        }
        //access veri tabanına baglantı kuruldu
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=otel.accdb");
        //temizle metodu olusturuldu
        void temizle() 
        {
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox4.Text = "";
            maskedTextBox1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
        }
        //listele metodu olusturuldu
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from musteri",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void anaekran_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            listele();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //otopark ekranı(form) gecıs yapıldı
            Otopark_ekranı frm = new Otopark_ekranı();
            frm.ShowDialog();
            this.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //personel ekranı(form) gecıs yapıldı
            Personel_ekranı frm = new Personel_ekranı();
            frm.ShowDialog();
            this.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //kayıt ekleme işlemi
            if ( textBox2.Text == "" || textBox3.Text == "" || maskedTextBox4.Text == "" ||
                textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" ||
                textBox11.Text == "" || textBox12.Text == "")
            {
                MessageBox.Show("Bütün Alanları doldurunuz.");
            }
            else if (maskedTextBox4.TextLength <11)
            {
                MessageBox.Show("Lütfen Tc kimlik alanını kontrol ediniz.");
            }
            else if (maskedTextBox1.TextLength <10)
            {
                MessageBox.Show("Lütfen Telefon alanını kontrol ediniz ve Basında 0 olmadan giriniz.");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("INSERT INTO musteri(mad,msoyad,mtc,mtel,msehir,mkisisayisi,modano,mgirist,mcıkıst,mfiyat,maraba) VALUES (@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)",baglanti);
                    komut.Parameters.AddWithValue("@a", textBox2.Text);
                    komut.Parameters.AddWithValue("@b", textBox3.Text);
                    komut.Parameters.AddWithValue("@c", maskedTextBox4.Text);
                    komut.Parameters.AddWithValue("@d", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@e", textBox6.Text);
                    komut.Parameters.AddWithValue("@f", textBox7.Text);
                    komut.Parameters.AddWithValue("@g", textBox8.Text);
                    komut.Parameters.AddWithValue("@h", maskedTextBox2.Text);
                    komut.Parameters.AddWithValue("@i", maskedTextBox3.Text);
                    komut.Parameters.AddWithValue("@j", textBox11.Text);
                    komut.Parameters.AddWithValue("@k", textBox12.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    if (textBox12.Text == "VAR" || textBox12.Text == "var")
                    {
                        Otopark_kayıt_ekleme frm = new Otopark_kayıt_ekleme();
                        frm.label5.Visible = false;
                        frm.textBox5.Visible = false;
                        frm.button2.Visible = false;
                        frm.textBox2.Text = textBox2.Text;
                        frm.textBox3.Text = textBox8.Text;
                        frm.maskedTextBox1.Text = maskedTextBox1.Text;
                        frm.ShowDialog();
                        this.Show();
                    }
                    MessageBox.Show("Kayıt başarılı bir şekilde eklendi");
                    listele();

                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri kayıt ekleme sırasında bir hata olustu!!","Müşteri Kayıt Ekleme",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //kayıtları getirme işlemi
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            //id metnini görünür yapar
            textBox1.Visible = true;
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // kayıtları silme işlemi
            try
            {
                baglanti.Open();
                DialogResult tus = MessageBox.Show("Kayıdı Silmek İstiyor Musunuz?", "KAYIT SİLME İŞLEMİ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tus == DialogResult.Yes)
                {
                    OleDbCommand komut1 = new OleDbCommand("DELETE FROM musteri WHERE id= @a", baglanti);
                    komut1.Parameters.AddWithValue("@a", textBox1.Text);
                    komut1.ExecuteNonQuery();                    
                    if (textBox12.Text == "VAR")
                    {
                        OleDbCommand komut2 = new OleDbCommand("DELETE * FROM otopark WHERE tel= @a", baglanti);
                        komut2.Parameters.AddWithValue("@a", maskedTextBox1.Text);
                        komut2.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    baglanti.Close();
                    MessageBox.Show("Kayıt silindi","Müşteri Kayıt Silme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    listele();
                }
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Müşteri kayıt silme sırasında bir hata olustu!!", "Müşteri Kayıt Silme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //kayıtları düzeltme işlemi
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("UPDATE musteri SET mad=@a,msoyad=@b,mtc=@c,mtel=@d,msehir=@e,mkisisayisi=@f,modano=@g,mgirist=@h,mcıkıst=@i,mfiyat=@j,maraba =@k WHERE id=@id", baglanti);
                komut.Parameters.AddWithValue("@a", textBox2.Text);
                komut.Parameters.AddWithValue("@b", textBox3.Text);
                komut.Parameters.AddWithValue("@c", maskedTextBox4.Text);
                komut.Parameters.AddWithValue("@d", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@e", textBox6.Text);
                komut.Parameters.AddWithValue("@f", textBox7.Text);
                komut.Parameters.AddWithValue("@g", textBox8.Text);
                komut.Parameters.AddWithValue("@h", maskedTextBox2.Text);
                komut.Parameters.AddWithValue("@i", maskedTextBox3.Text);
                komut.Parameters.AddWithValue("@j", textBox11.Text);
                komut.Parameters.AddWithValue("@k", textBox12.Text);
                komut.Parameters.AddWithValue("@id",textBox1.Text);
                komut.ExecuteNonQuery();
                if (textBox12.Text == "YOK")
                {
                    OleDbCommand komut2 = new OleDbCommand("DELETE * FROM otopark WHERE tel= @a", baglanti);
                    komut2.Parameters.AddWithValue("@a", maskedTextBox1.Text);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                }
                if (textBox12.Text == "VAR" && textBox12.Text != dataGridView1.CurrentRow.Cells[11].Value.ToString() )
                {
                    Otopark_kayıt_ekleme frm = new Otopark_kayıt_ekleme();
                    frm.label5.Visible = false;
                    frm.textBox5.Visible = false;
                    frm.button2.Visible = false;
                    frm.textBox2.Text = textBox2.Text;
                    frm.textBox3.Text = textBox8.Text;
                    frm.maskedTextBox1.Text = maskedTextBox1.Text;
                    frm.ShowDialog();
                    this.Show();
                }
                baglanti.Close();
                MessageBox.Show("Kayıt düzeltildi", "Müşteri Kayıt Düzeltme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            catch (Exception)
            {
                MessageBox.Show("Müşteri kayıt düzeltme sırasında bir hata olustu!!", "Müşteri Kayıt düzeltme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // kayıtlari arama işlemi
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM musteri WHERE mad  LIKE '" + toolStripTextBox1.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //form ekranını temizledi
            temizle();
            //form ekranından id metin kutusunu gizledi
            textBox1.Visible = false;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            // sadece sayı girişi yapmak için
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Metin kutusana büyük harfle yazı yazdırma işlemi
            textBox3.Text = textBox3.Text.ToUpper();
            textBox3.SelectionStart = textBox3.Text.Length;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = textBox6.Text.ToUpper();
            textBox6.SelectionStart = textBox6.Text.Length;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //Metin kutusana büyük harfle yazı yazdırma işlemi
            textBox12.Text = textBox12.Text.ToUpper();
            textBox12.SelectionStart = textBox12.Text.Length;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

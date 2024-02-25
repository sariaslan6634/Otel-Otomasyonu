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
    public partial class Otopark_ekranı : Form
    {
        public Otopark_ekranı()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel.accdb");
        Otopark_kayıt_ekleme frm = new Otopark_kayıt_ekleme();
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT*FROM otopark", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        void temizle() 
        {
            frm.textBox1.Text = "";
            frm.textBox2.Text = "";
            frm.textBox3.Text = "";
            frm.maskedTextBox1.Text = "";
            frm.textBox5.Text = "";
        }
        private void Otopark_ekranı_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //kayıt ekleme
            temizle();
            frm.textBox5.Visible = false;
            frm.label5.Visible = false;
            frm.button2.Visible = false;
            frm.button1.Visible = true;
            this.Hide();
            frm.ShowDialog();
            this.Show();
            listele();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frm.textBox5.Visible = true;
                frm.label5.Visible = true;
                frm.textBox5.Enabled = false;
                frm.button1.Visible = false;
                frm.button2.Visible = true;

                frm.textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm.textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm.maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                this.Hide();
                frm.ShowDialog();
                this.Show();
                listele();
            }
            catch (Exception)
            {

                MessageBox.Show("Kayıt Düzeltme İşlemi Sırasında Bir Hata oldu","OTOPARK KAYIT DÜZELTME",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult tus = MessageBox.Show("Kayıdı Silmek İstiyor Musunuz?", "OTOPARK KAYIT SİLME", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tus == DialogResult.Yes)
                {
                    OleDbCommand komut3 = new OleDbCommand("DELETE FROM otopark WHERE id= @a", baglanti);
                    komut3.Parameters.AddWithValue("@a", id);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                }
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("kayıt silerken bir hata olustu","OTOPARK KAYIT SİLME",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // kayıtlari arama işlemi
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM otopark WHERE plaka  LIKE '" + textBox1.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Metin kutusana büyük harfle yazı yazdırma işlemi
            textBox1.Text = textBox1.Text.ToUpper();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

    }
}

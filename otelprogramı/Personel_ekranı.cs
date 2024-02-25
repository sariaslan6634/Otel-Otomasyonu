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
    public partial class Personel_ekranı : Form
    {
        public Personel_ekranı()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=otel.accdb");
        Personel_kayıt_ekleme frm = new Personel_kayıt_ekleme();
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT*FROM personel", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
        private void Personel_ekranı_Load(object sender, EventArgs e)
        {
            //form yükleme ekranı
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //personel kayıt ekleme ekranına gitme
            Personel_kayıt_ekleme frm = new Personel_kayıt_ekleme();
            frm.button2.Visible = false;
            frm.label7.Visible = false;
            frm.textBox7.Visible = false;
            this.Hide();
            frm.ShowDialog();
            this.Show();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Kayıt silme İşlemi
            try
            {
                baglanti.Open();
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult tus = MessageBox.Show("Kayıdı Silmek İstiyor Musunuz?", "PERSONEL KAYIT SİLME", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tus == DialogResult.Yes)
                {
                    OleDbCommand komut = new OleDbCommand("DELETE * FROM personel WHERE id= @a", baglanti);
                    komut.Parameters.AddWithValue("@a", id);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show("kayıt başarılı bir şekilde silindi.", "PERSONEL KAYIT SİLME", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                baglanti.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt silme işleminde bir hata oldu!", "PERSONEL KAYIT SİLME", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //personel kayıt düzeltme ekranına gitme
            Personel_kayıt_ekleme frm = new Personel_kayıt_ekleme();
            frm.button1.Visible = false;
            frm.textBox7.Enabled = false;
            frm.textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm.maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frm.maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frm.textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frm.maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            this.Hide();
            frm.ShowDialog();
            this.Show();
            listele();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            {
                // kayıtlari arama işlemi
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM personel WHERE p_ad  LIKE '" + textBox1.Text + "%'", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Seyehat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=monster;Initial Catalog=Yolcu_Bilet;Integrated Security=True");
        private void btn_Kaydet_Click(object sender, EventArgs e)
        { //Yolcu Kaydetme
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Ad.Text) || string.IsNullOrWhiteSpace(txt_Soyad.Text) || string.IsNullOrWhiteSpace(msk_Telefon.Text) || string.IsNullOrWhiteSpace(txt_Tc.Text) || string.IsNullOrWhiteSpace(cmb_Cinsiyet.Text) || string.IsNullOrWhiteSpace(txt_Mail.Text))
                {//Boş alan kalmayacak bu sayede
                    MessageBox.Show("Lütfen Tüm Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Yolcular (AD,SOYAD,TELEFON,TC,CINSIYET,MAIL) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                    komut.Parameters.AddWithValue("@p1", txt_Ad.Text);
                    komut.Parameters.AddWithValue("@p2", txt_Soyad.Text);
                    komut.Parameters.AddWithValue("@p3", msk_Telefon.Text);
                    komut.Parameters.AddWithValue("@p4", txt_Tc.Text);
                    komut.Parameters.AddWithValue("@p5", cmb_Cinsiyet.Text);
                    komut.Parameters.AddWithValue("@p6", txt_Mail.Text);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Yolcu Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yolcu Kaydedilemedi"+ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                baglanti.Close();
            }
        }

        private void btn_Kaptan_Kaydet_Click(object sender, EventArgs e)
        {//Kaptan Kaydetme
            try
            {
                if (string.IsNullOrWhiteSpace(msk_Kaptan_No.Text) || string.IsNullOrWhiteSpace(txt_Kaptan_Ad_Soyad.Text) || string.IsNullOrWhiteSpace(msk_Telefon_Kaptan.Text))
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Insert INTO Tbl_Kaptan (KAPTANNO,ADSOYAD,TELEFON)values(@p1,@p2,@p3)", baglanti);
                    komut.Parameters.AddWithValue("@p1",msk_Kaptan_No.Text);
                    komut.Parameters.AddWithValue("@p2", txt_Kaptan_Ad_Soyad.Text);
                    komut.Parameters.AddWithValue("@p3", msk_Telefon_Kaptan.Text);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Kaptan Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaptan Kaydedilemedi" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                baglanti.Close();
            }
        }

        private void btn_Sefer_Olustur_Click(object sender, EventArgs e)
        {
            //Sefer Oluşturma
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Kalkıs.Text) || string.IsNullOrWhiteSpace(txt_Varıs.Text) || string.IsNullOrWhiteSpace(msk_TariH.Text)|| string.IsNullOrWhiteSpace(msk_Saat.Text) || string.IsNullOrWhiteSpace(msk_Kaptan.Text) || string.IsNullOrWhiteSpace(txt_Fiyat.Text) )
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Sefer_Bilgi (KALKIS,VARIS,TARIH,SAAT,KAPTAN,FIYAT)values(@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                    komut.Parameters.AddWithValue("@p1", txt_Kalkıs.Text);
                    komut.Parameters.AddWithValue("@p2", txt_Varıs.Text);
                    komut.Parameters.AddWithValue("@p3", msk_TariH.Text);
                    komut.Parameters.AddWithValue("@p4", msk_Saat.Text);
                    komut.Parameters.AddWithValue("@p5", msk_Kaptan.Text);
                    komut.Parameters.AddWithValue("@p6", txt_Fiyat.Text);
                    komut.ExecuteNonQuery();

                    MessageBox.Show("Sefer Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sefer Kaydedilemedi" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                baglanti.Close();
            }
        }
        void sefer_getir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Sefer_Bilgi", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        void yolcu_bilet()
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select S.SEFERNO,Y.AD+''+Y.SOYAD AS'Ad-Soyad',s.KOLTUK from Tbl_Sefer_Detay s\r\nINNER JOIN Tbl_Yolcular y ON S.YOLCULUKID=Y.ID", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglanti.Close();
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sefer_getir();
            yolcu_bilet();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_Sefer_Yap.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
           
        }
        //Koltuklara isim verdik
        private void btn_1_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "1";
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "2";
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "3";
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "4";
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "5";
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "6";
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "7";
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "8";
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            txt_Koltuk.Text = "9";
        }

        private void btn_Rezervasyon_Yap_Click(object sender, EventArgs e)
        {
            //REZERVASYON yapılsın (3 Seferimiz var ve koltuklarımız var koltuk boş ise bilet alınsın değilse alınmasın)
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Sefer_Yap.Text) || string.IsNullOrWhiteSpace(msk_Yolcu_Tc.Text) || string.IsNullOrWhiteSpace(txt_Koltuk.Text))
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                baglanti.Open();

                // Seçilen koltuğun rezerve edilip edilmediğini kontrol et
                SqlCommand Komut = new SqlCommand("SELECT COUNT(*) FROM Tbl_Sefer_Detay WHERE SEFERNO = @p1 AND KOLTUK = @p2", baglanti);//seferleri saycak
                Komut.Parameters.AddWithValue("@p1", txt_Sefer_Yap.Text);
                Komut.Parameters.AddWithValue("@p2", txt_Koltuk.Text);

                int rezerveSayisi = Convert.ToInt32(Komut.ExecuteScalar());//rezerve sayısı komuttan gelecek olan deger
                // ExecuteScalar-->ilk sütunun ilk satırındaki değeri alır(yani sefer numaramızı alıyo(sefer yap))
                if (rezerveSayisi > 0)
                {
                    MessageBox.Show("Seçilen koltuk zaten rezerve edilmiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Rezerve Edilmemişse rezervazyon işlemi yapılsın
                    SqlCommand rezervasyonKomut = new SqlCommand("INSERT INTO Tbl_Sefer_Detay (SEFERNO, YOLCUTC, KOLTUK) VALUES (@p1, @p2, @p3)", baglanti);
                    rezervasyonKomut.Parameters.AddWithValue("@p1", txt_Sefer_Yap.Text);
                    rezervasyonKomut.Parameters.AddWithValue("@p2", msk_Yolcu_Tc.Text);
                    rezervasyonKomut.Parameters.AddWithValue("@p3", txt_Koltuk.Text);
                    rezervasyonKomut.ExecuteNonQuery();

                    MessageBox.Show("Rezervasyon Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon Yapılamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                baglanti.Close();
            }
        }

        
    }
}

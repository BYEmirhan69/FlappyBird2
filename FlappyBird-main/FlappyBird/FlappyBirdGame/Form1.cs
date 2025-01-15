using System;
using System.Windows.Forms;

namespace FlappyBirdGame
{
    public partial class Form1 : Form
    {
        Random rastgele = new Random();
        int boruHızı = 9;  
        int yercekimi = 11;
        int puan = 0;
        bool oyunBasladi = false;
        int rekor = 0;
        public Form1()
        {
            InitializeComponent();  // Form üzerindeki bileşenleri başlatır
            OyunuBaslat();  // Oyunu başlatmak için gerekli ayarları yapar
            menu.Visible = true;  // Başlangıç menüsü ekranda görünür olur
        }
        private void OyunuBaslat()
        {
            // Oyun başlatılmadan önce borular ve kuşun başlangıç pozisyonu ile puan ayarlanır
            kus.Top = this.Height / 2 - 80; 
            boruAlt.Left = 370;
            boruUst.Left = 370;
            boruAlt.Top = 300;
            boruUst.Top = -100;
            puan = 0;  
            skor.Text = "Skor: " + puan;  
            oyunBasladi = false;  // Oyun başlangıçta durdurulmuş
            timer.Stop();  // Oyun başlamadan önce zamanlayıcı durdurulmalı
            menu.Visible = true;  // Başlangıç yazısını görünür yap
        }

        private void gameTimer(object sender, EventArgs e)  // Zamanlayıcı ile sürekli olarak çağrılan oyun döngüsü
        {
            if (!oyunBasladi) return;  // Eğer oyun başlamadıysa, timer'ın geri kalanı çalışmaz

            kus.Top += yercekimi;  // Kuşa yerçekimi etkisi uygula

            boruAlt.Left -= boruHızı;  // Boruları sola hareket ettir
            boruUst.Left -= boruHızı;

            skor.Text = "Skor: " + puan;  // Skoru güncel tut

            if (0 < boruAlt.Left && boruAlt.Left < 10)  // Borular kuşu geçince puanı artır
            {
                puan++;
            }
            if(boruAlt.Left < -100)  // Borular ekranın solundan çıktıysa konumlarını tekrar ayarla ve yeni rastgele yükseklik ayarla
            {
                int konum = rastgele.Next(-90,90);  // rastgele yükseklik üret
                boruAlt.Left = 400;
                boruUst.Left = 400;
                boruAlt.Top = 300 + konum;
                boruUst.Top = -100 + konum;
            }            

            if(kus.Bounds.IntersectsWith(boruAlt.Bounds) || kus.Bounds.IntersectsWith(boruUst.Bounds) ||
            kus.Bounds.IntersectsWith(zemin.Bounds) || kus.Bounds.Top < -25)  // Kuş bir engelle veya zeminle çarpışırsa oyunu bitir
            {
                OyunuBitir();
            }
        }
        public void OyunuBitir()
        {
            timer.Stop();  // zamanlayıcı durduruldu

            if (puan > rekor)  // rekor geçildiyse güncelle
            {
                rekor = puan;
            }
            MessageBox.Show("OYUN BİTTİ  SKOR: " + puan + " REKOR: " + rekor + "\nTekrar oynamak için Tamam'a basın");
            // Oyun bitiş mesajını göster
            OyunuBaslat(); 
        }

        private void asagı(object sender, KeyEventArgs e)  // Space tuşuna basıldığında çalışır
        {
            if(e.KeyCode == Keys.Space)
            {
                if (!oyunBasladi)
                {
                    oyunBasladi = true;  // Eğer oyun başlamamışsa başlat
                    yercekimi = 11;  // Oyun başladıktan sonra yer çekimi aktif olsun
                    timer.Start();  // Zamanlayıcı başlatılsın
                    menu.Visible = false;  // Menü gizlensin
                }
                yercekimi = -11;  // Space tuşuna basıldığında kuşu yukarı doğru hareket ettir
            }
        }

        private void yukari(object sender, KeyEventArgs e)  // Space tuşuna bırakıldığında çalışır
        {
            if (e.KeyCode == Keys.Space)
            {
                yercekimi = 11;  // Space tuşu bırakıldığında kuş tekrar aşağı yönlü hareket etsin
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

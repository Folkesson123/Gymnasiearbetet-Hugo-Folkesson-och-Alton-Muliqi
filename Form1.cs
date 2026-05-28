using System;
using System.Globalization;
using System.Windows.Forms;

namespace Ga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void beräkna_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Hämta värden från TextBoxes (säkrare parse)
                if (!double.TryParse(lutning1.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out double lutningGrader))
                {
                    MessageBox.Show("Fel: Lutning måste vara ett tal.");
                    return;
                }

                if (!double.TryParse(avstånd2.Text, NumberStyles.Float, CultureInfo.CurrentCulture, out double avstand))
                {
                    MessageBox.Show("Fel: Avstånd måste vara ett tal.");
                    return;
                }

                if (!int.TryParse(antal3.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out int antalFlytt))
                {
                    MessageBox.Show("Fel: Antal måste vara ett heltal.");
                    return;
                }

                // 2. Hantera Grepp-poäng via ComboBox (null-säkert, case-insensitive)
                if (grepp1.SelectedItem == null)
                {
                    MessageBox.Show("Välj ett grepp i listan.");
                    return;
                }

                double greppPoäng = 0;
                string greppVal = grepp1.SelectedItem.ToString().ToLowerInvariant();
                if (greppVal == "jug") greppPoäng = 1;
                else if (greppVal == "sloper") greppPoäng = 5;
                else if (greppVal == "crimp") greppPoäng = 8;
                else
                {
                    MessageBox.Show("Okänt grepp valt.");
                    return;
                }

                // 3. Hantera Komplexitet-poäng via ComboBox (null-säkert, case-insensitive)
                if (komplexitet2.SelectedItem == null)
                {
                    MessageBox.Show("Välj en komplexitet i listan.");
                    return;
                }

                double komplexitetFaktor = 0;
                string komplexVal = komplexitet2.SelectedItem.ToString().ToLowerInvariant();
                if (komplexVal == "enkel") komplexitetFaktor = 1.0;
                else if (komplexVal == "teknisk") komplexitetFaktor = 1.3;
                else if (komplexVal == "svår") komplexitetFaktor = 1.6;
                else
                {
                    MessageBox.Show("Okänd komplexitet valt.");
                    return;
                }

                // 4. Fysikalisk beräkning
                double radianer = lutningGrader * (Math.PI / 180.0);
                double flyttSvårighet = (greppPoäng * (1 + Math.Sin(radianer) * 2)) + (avstand * 0.1);
                double totalPoäng = (flyttSvårighet * komplexitetFaktor) * antalFlytt * 10;

                // 5. Visa resultatet
                resultat.Text = $"Resultat: {totalPoäng:F0} poäng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel uppstod: {ex.Message}");
            }
        }

        private void grepp_Click(object sender, EventArgs e)
        {

        }
    }
}
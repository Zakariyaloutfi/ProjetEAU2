using System;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnRecupererReserves_Click(object sender, RoutedEventArgs e)
        {
            var reserves = await GetReservesEauAsync();
            txtResultats.Text = $"Nombre de réserves récupérées : {reserves.Count}";
        }

        private async void BtnRecupererParCodePostal_Click(object sender, RoutedEventArgs e)
        {
            var codePostal = txtCodePostal.Text;
            var reserve = await GetReserveEauAsync(codePostal);
            txtResultats.Text = $"Résultat : {reserve.Nom}, {reserve.VolumeEau}L";
        }

        private async void BtnCreerReserve_Click(object sender, RoutedEventArgs e)
        {
            var reserve = new ReserveEau
            {
                Nom = txtNom.Text,
                VolumeEau = double.Parse(txtVolumeEau.Text),
                CodePostal = "12345" 
            };
            await CreateReserveEauAsync(reserve);
            txtResultats.Text = "Réservé créée avec succès";
        }

      
        public async Task<List<ReserveEau>> GetReservesEauAsync()
        {
            return new List<ReserveEau>(); 
        }

        public async Task<ReserveEau> GetReserveEauAsync(string codePostal)
        {
            return new ReserveEau(); 
        }

        public async Task CreateReserveEauAsync(ReserveEau reserveEau)
        {
            var json = JsonConvert.SerializeObject(reserveEau);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44356/api/ReservesEau\r\n", content);

            if (!response.IsSuccessStatusCode)
            {
$                throw new Exception("Erreur lors de la création de la réserve d'eau.");
            }

            
        }

    }

    public class ReserveEau
    {
        public string Nom { get; set; }
        public double VolumeEau { get; set; }
        public string CodePostal { get; set; }
    }
}

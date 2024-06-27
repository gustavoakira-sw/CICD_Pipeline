using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FiapApi.Tests.Controllers
{
    public class ColetaDeLixoControllerTest
    {
        private readonly HttpClient _client;

        public ColetaDeLixoControllerTest()
        {
            // Inicializa HttpClient com API key
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5049/");
            _client.DefaultRequestHeaders.Add("Authorization", "admin123");
        }

        // GET all
        [Fact]
        public async Task GetColetaDeLixo_RetornaListaDeColetas()
        {
            // Arrange
            var request = "/api/ColetaDeLixo";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Falha se response code não for entre 200 e 299
        }
        
        // GET by ID
        [Fact]
        public async Task GetColetaDeLixoById_RetornaColetaEspecifica()
        {
            // Arrange
            int coletaId = 1; // Usar qualquer ID de coleta válida
            var request = $"/api/ColetaDeLixo/{coletaId}";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Falha se response code não for entre 200 e 299
        }
    }
}
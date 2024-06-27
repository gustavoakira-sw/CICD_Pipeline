using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FiapApi.Tests.Controllers
{
    public class UsersControllerTest
    {
        private readonly HttpClient _client;

        public UsersControllerTest()
        {
            // Inicializa HttpClient com API key
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5049/");
        }

        // GET all
        [Fact]
        public async Task GetUsersList_RetornaListaDeUsuarios()
        {
            // Arrange
            var request = "/Users/List";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Falha se response code não for entre 200 e 299
        }
        
        // GET by ID
        [Fact]
        public async Task GetUserById_RetornaUserEspecifica()
        {
            // Arrange
            int userId = 1; // Usar qualquer ID de coleta válida
            var request = $"/Users/Edit/{userId}";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Falha se response code não for entre 200 e 299
        }
    }
}
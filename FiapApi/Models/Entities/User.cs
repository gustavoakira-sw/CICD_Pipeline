namespace FiapApi.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
        public string Role { get; set; }
        public string Password { get; set; } // Nullable field
    }
}

// Warnings de nullable fields podem ser corrigidos com o código abaixo.
// Incerto quanto as alterações na funcionalidade.

// namespace FiapApi.Models.Entities
// {
//     public class User
//     {
//         public int Id { get; set; }
//         public string Name { get; set; } = string.Empty;
//         public string Email { get; set; } = string.Empty;
//         public string Key { get; set; } = string.Empty;
//         public string Role { get; set; } = string.Empty;
//         public string? Password { get; set; } // Nullable, as per the table data
//
//         public User()
//         {
//             // Default constructor
//         }
//
//         public User(int id, string name, string email, string key, string role, string? password = null)
//         {
//             Id = id;
//             Name = name;
//             Email = email;
//             Key = key;
//             Role = role;
//             Password = password;
//         }
//     }
// }
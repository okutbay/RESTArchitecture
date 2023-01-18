using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatalogLibrary.Entity
{
    public class User
    {
        public User() 
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.NormalizedEmail = string.Empty;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Salt = string.Empty;
            this.Roles = new List<string>() { "standart" };
            this.Token = string.Empty;
            this.ApiKey = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private string email = string.Empty;

        public string Email
        {
            get { return email.ToLower(); }
            set
            {
                email = value.ToLower();
                normalizedEmail = value.ToUpperInvariant();
            }
        }

        private string normalizedEmail = string.Empty;

        public string NormalizedEmail
        {
            get { return normalizedEmail.ToUpperInvariant(); }
            set { normalizedEmail = value.ToUpperInvariant(); }
        }

        private string userName = string.Empty;

        public string Username
        {
            get { return userName.ToLower(); }
            set { userName = value.ToLower(); }
        }

        public string Password { get; set; }

        public string Salt { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }

        public string ApiKey { get; set; }

        public DateTime ApiKeyDate { get; set; }
    }
}

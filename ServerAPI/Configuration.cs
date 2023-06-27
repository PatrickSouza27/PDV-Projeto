namespace ServerAPI
{
    public static class Configuration
    {
        public readonly static string Conexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Patrick\source\repos\ProjetoSenac\ProjetoSenac\Data\Database1.mdf;Integrated Security=True";

        public static string JWTKey { get; set; } = "NjgxNTY2NTctMjljNy00OWM2LTg3ZjItYTk5NDViODAzMjEw"; 

        public static string ApiKeyName = "api_key";
        public static string ApiKey = "123456";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 587;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}

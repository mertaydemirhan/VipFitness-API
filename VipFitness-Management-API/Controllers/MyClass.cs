using Microsoft.Extensions.Configuration;
namespace VipFitness_Management_API.Controllers
{
    public class MyClass
    {

        private readonly IConfiguration _configuration;

        public MyClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DoSomething()
        {
            // User Secrets'tan bilgilere erişim
            string awsAccessKey = _configuration["AWS:AccessKey"];
            string awsSecretKey = _configuration["AWS:SecretKey"];

            // Bilgileri kullanma
            Console.WriteLine($"AWS Access Key: {awsAccessKey}");
            Console.WriteLine($"AWS Secret Key: {awsSecretKey}");

            // Diğer işlemleri gerçekleştir
        }
    }

}


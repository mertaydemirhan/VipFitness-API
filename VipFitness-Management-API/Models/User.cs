using System.Text.Json.Serialization;

namespace VipFitness_Management_API.Models
{
    public class User
    {
        public int id {  get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int UType { get; set; }
        public int isdeleted { get; set; }


    }
}

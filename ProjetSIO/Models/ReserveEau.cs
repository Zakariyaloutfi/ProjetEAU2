using System.ComponentModel.DataAnnotations;

namespace ProjetSIO.Models
{
    public class ReserveEau
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public double VolumeEau { get; set; }
        public string CodePostal { get; set; }
    }
}

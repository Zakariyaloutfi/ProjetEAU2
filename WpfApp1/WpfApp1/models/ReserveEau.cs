using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.models
{ 
  public class ReserveEau
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public double VolumeEau { get; set; }
    public string CodePostal { get; set; }
}
}
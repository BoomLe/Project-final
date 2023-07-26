using System.ComponentModel.DataAnnotations;

namespace MVCT.Models.Attandance
{
    public class AttandanceUpdate
    {
       
 
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

      

        public string? UserId { set; get; }

        public AppUser? User { set; get; }
    }
}

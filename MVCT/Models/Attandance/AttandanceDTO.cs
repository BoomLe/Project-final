using System.ComponentModel.DataAnnotations;

namespace MVCT.Models.Attandance
{
    public class AttandanceDTO
    {
        public int Id { set; get; }
        public string? Username { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        public List<Attandances>? Attandances { set; get; }

        public string? UserId { set; get; }

        public AppUser? User { set; get; }
    }
}

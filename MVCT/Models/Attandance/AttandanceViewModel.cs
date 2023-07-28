namespace MVCT.Models.Attandance
{
    public class AttandanceViewModel
    {

        public List<Attandances> Attandances { get; set; }
        public Attandances Attandance { set; get; }

        public List<AttandanceDTO> AttandanceDTOs { set; get; }

        public AttandanceDTO AttandanceDTO { set; get; }
    }
}

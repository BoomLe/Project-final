namespace MVCT.Models.UserReport
{
    public class UserReportsViewModel
    {
        public string? Username { set; get; }
        public DateTime SendnDate { set; get; }
        public string Description { set; get; }


        public string Messages { set; get; }
        public List<UserReports>? UserReports { set; get; }

        public UserReports? UserReport { set; get; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MVCT.Models.Attandance;
using MVCT.Models.UserReport;

namespace MVCT.Models 
{
    public class AppUser: IdentityUser 
    {
          [Column(TypeName = "nvarchar")]
          [StringLength(400)]  
          public string HomeAdress { get; set; }

        // [Required]       
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

          public List<Attandances> Attandances { set; get; }
          
          public List<UserReports> UserReports { set; get; }



    }
}

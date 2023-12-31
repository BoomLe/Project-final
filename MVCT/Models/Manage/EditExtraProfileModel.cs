using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCT.Models.Manage
{
    public class EditExtraProfileModel
    {
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string UserEmail { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(400)]
        public string HomeAdress { get; set; }


        [Display(Name = "Ngày sinh")]
        //[DataType(DataType.DateTime)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string SelectCountries { set; get; }
        public List<SelectListItem> Countrieslist { set; get; }


    }
}
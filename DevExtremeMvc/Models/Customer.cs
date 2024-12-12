using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevExtremeMvc.Models
{
    public class CustomerModel
    {
        [Key]
        public int? customerId { get; set; }

        [DisplayName("Họ và tên")]
        [Required]
        public string fullName { get; set; }

        [DisplayName("Email")]
        [Required]
        public string email { get; set; }

        [DisplayName("Mật khẩu")]
        [Required]
        public string password { get; set; }

        [DisplayName("Ngày sinh")]
        [Required]
        public DateTime dateOfBirth { get; set; }

    }
}

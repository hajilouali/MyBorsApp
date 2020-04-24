using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Roles()
        {
            this.Users = new HashSet<Users>();
        }
        [Key]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان نقش")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]
        public string RoleTitle { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        [Display(Name = "نام نقش")]
        [MaxLength(150, ErrorMessage = "حداکثر تعداد کاراکتر مورد قبول {0} می باشد. ")]

        public string RoleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
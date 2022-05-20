using System.ComponentModel.DataAnnotations;

namespace sajjadmirzapour.Models
{
    public class Banner
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "تصویر یک")]
        [Required(ErrorMessage ="وارد کردن الزامی است")]
        public string Picture1 { get; set; }

        [Display(Name = "تصویر دو")]
        [Required(ErrorMessage = "وارد کردن الزامی است")]
        public string Picture2 { get; set; }


        [Display(Name = "تصویر سه")]
        [Required(ErrorMessage = "وارد کردن الزامی است")]
        public string Picture3 { get; set; }
    }
}

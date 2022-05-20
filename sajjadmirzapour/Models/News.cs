using System.ComponentModel.DataAnnotations;

namespace sajjadmirzapour.Models
{
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        [Display(Name ="عنوان خبر")]
        [Required(ErrorMessage ="عنوان خبر اجباریست")]
        public string NewsTitle { get; set; }
        [Display(Name ="شرح خبر")]
        [Required(ErrorMessage ="نوشتن متن خبر اجباریست")]
        public string  NewsDescription { get; set; }
        [Display(Name ="عکس")]
        public string  NewsImage { get; set; }
    }
}

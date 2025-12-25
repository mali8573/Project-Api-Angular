using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }

}

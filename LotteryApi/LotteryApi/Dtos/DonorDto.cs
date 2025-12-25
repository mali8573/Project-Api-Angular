using System.ComponentModel.DataAnnotations;

namespace LotteryApi.Dtos
{
    public class DonorDto
    {
        public string? Tz { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, Phone]
        public string Phone { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
    public class DonorUpdateDto
    {
       
        public string? Tz { get; set; }

        public string? Name { get; set; }
        [ Phone]
        public string? Phone { get; set; }
        [ EmailAddress]
        public string? Email { get; set; }
    }

}

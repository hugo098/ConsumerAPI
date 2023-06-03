using System.ComponentModel.DataAnnotations;

namespace ConsumerAPI.Models.Posts.Dtos
{
    public class UpdatePostDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100), MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500), MinLength(1)]
        public string Body { get; set; }
    }
}

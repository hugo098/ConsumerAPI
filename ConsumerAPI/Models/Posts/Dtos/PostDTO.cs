using System.ComponentModel.DataAnnotations;

namespace JSONPlaceholderConsumer.Models.Posts.Dtos
{

    public class PostDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }


}

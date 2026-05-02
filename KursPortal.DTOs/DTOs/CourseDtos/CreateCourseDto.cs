using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.CourseDtos
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewVideoUrl { get; set; }
        public int CourseHour { get; set; }
        public double Rating { get; set; } = 5.0;
        public bool IsPopular { get; set; }

        public Guid CategoryId { get; set; }

        public Guid TeacherId { get; set; }
    }
}

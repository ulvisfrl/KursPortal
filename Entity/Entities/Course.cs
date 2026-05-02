using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewVideoUrl { get; set; }
        public double Rating { get; set; } = 5.0;
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public bool IsPopular { get; set; }
        public int CourseHour { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid TeacherId { get; set; }
        public AppUser Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}

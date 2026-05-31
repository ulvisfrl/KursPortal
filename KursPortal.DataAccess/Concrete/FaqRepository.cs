using KursPortal.DataAccess.Abstract;
using KursPortal.DataAccess.Context;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DataAccess.Concrete
{
    public class FaqRepository : Repository<Faq>, IFaqRepository
    {
        public FaqRepository(AppDbContext context) : base(context)
        {
        }
    }
}

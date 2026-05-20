using KursPortal.Business.Abstract;
using KursPortal.DataAccess.Abstract;
using KursPortal.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class SubscriberService : GenericService<Subscriber>, ISubscriberService
    {
        public SubscriberService(IRepository<Subscriber> repository) : base(repository)
        {
        }
    }
}

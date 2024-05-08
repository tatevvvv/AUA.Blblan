using Blblan.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blblan.Data.Repositories
{
    public class MessagesRepository : Repository<Message>
    {
        public MessagesRepository(BlblanDbContext context) : base(context)
        {
        }
    }
}

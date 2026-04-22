using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Entities
{
    public class SendingEmail : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Framework.Data.Entities
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public DateTime PublishTime { get; set; }
    }
}

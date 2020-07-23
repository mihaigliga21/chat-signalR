using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Model
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

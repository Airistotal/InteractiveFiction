using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction.Business.Infrastructure.MessageBus.Message
{
    public class GameNameSelected : IMessage
    {
        public string? Name { get; set; }
    }
}

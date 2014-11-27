using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Persistence.Domain
{
    public class Notification : Entidad
    {
        public int SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
        public DateTime? NotificationDate { get; set; }
        public State State { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Message { get; set; }
    }

    public enum State
    {
       Pending = 0,
       Sent = 1
    }
}

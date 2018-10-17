using System;
using MessageConsumer.Entities.Enums;

namespace MessageConsumer.Entities.Events
{
    public class ManagerEventArgs<K,V>:EventArgs
    {
        public ManagerAction Action { get; set; }
        public K Key { get; set; }
        public V Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotWorld2Core.Game.General
{
    public class ObservableVar<T>
    {
        public event Action<ObservableVar<T>> Changed;
        public T Value 
        { 
            get => _value; 
            set
            {
                _value = value;
                Changed?.Invoke(this);
            }
        }
        private T _value;

        public ObservableVar(T value)
        {
            Value = value;
        }
    }
}

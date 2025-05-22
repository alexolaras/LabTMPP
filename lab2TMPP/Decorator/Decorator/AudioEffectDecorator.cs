using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    abstract class AudioEffectDecorator : IAudioStream
    {
        protected IAudioStream _stream;

        public AudioEffectDecorator(IAudioStream stream)
        {
            _stream = stream;
        }

        public abstract string Process();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class EchoDecorator : AudioEffectDecorator
    {
        public EchoDecorator(IAudioStream stream) : base(stream)
        {
        }

        public override string Process()
        {
            return _stream.Process() + " + Echo";
        }
    }
}

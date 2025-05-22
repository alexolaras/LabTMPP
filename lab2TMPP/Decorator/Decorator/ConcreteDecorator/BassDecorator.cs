using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class BassDecorator : AudioEffectDecorator
    {
        public BassDecorator(IAudioStream stream) : base(stream)
        {
        }

        public override string Process()
        {
            return _stream.Process() + " + BassBoost";
        }
    }
}
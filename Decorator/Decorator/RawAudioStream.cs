using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class RawAudioStream : IAudioStream
    {
        private string _data;

        public RawAudioStream(string data)
        {
            _data = data;
        }

        public string Process()
        {
            return _data;
        }
    }
}
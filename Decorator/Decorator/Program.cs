using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IAudioStream audio = new RawAudioStream("RawAudio");

            audio = new EchoDecorator(audio);
            audio = new BassDecorator(audio);
            audio = new ReverbDecorator(audio);

            Console.WriteLine("Processed Audio: " + audio.Process());
        }
    }
}
using System;

namespace LabOWav
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter input path (with file name and .wav) : ");
            var inputPath = Console.ReadLine();
            
            Console.WriteLine("Enter output path (with file name and .wav) : ");
            var outputPath = Console.ReadLine();
            
            WaveIO waveIo = new WaveIO(inputPath, outputPath);
            var b = waveIo.ReadByteStream();
            waveIo.WriteByteStream(b);

          
        }
    }
}
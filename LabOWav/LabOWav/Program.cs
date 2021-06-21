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

            Console.WriteLine("Enter in how many times you want to increase length of the track :");
            var times = Convert.ToDouble(Console.ReadLine());
            
            WaveIO waveIo = new WaveIO(inputPath, outputPath);
            Interpolation interpolation = new Interpolation();
            var b = waveIo.ReadByteStream();
            var updatedData = interpolation.Execute(b.Data, times, b.NumChannels);
            b.UpdateStream(updatedData);
            waveIo.WriteByteStream(b);

          
        }
    }
}
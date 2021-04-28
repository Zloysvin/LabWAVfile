using System;

namespace LabOWav
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Interpolation interpolation = new Interpolation();
            byte[] numbers = {104, 213 };
            interpolation.Execute(numbers, 2);
        }
    }
}

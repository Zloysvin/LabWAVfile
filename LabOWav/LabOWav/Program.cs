﻿using System;

namespace LabOWav
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Interpolation interpolation = new Interpolation();
            byte[] numbers = {104, 213, 250, 104,};
            interpolation.Execute(numbers, 2.5, 1);
        }
    }
}

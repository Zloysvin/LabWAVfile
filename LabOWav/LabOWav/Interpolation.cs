using System;
using System.Collections.Generic;
using System.Text;

namespace LabOWav
{
    class Interpolation
    {
        public void Execute(byte[] data, int times)
        {
            var dataConv = readData(data);
            float delta = 1 / times;
            List<int> extendedData = new List<int>();
            for (int i = 0; i < dataConv.Length -1; i++)
            {
                extendedData.Add(dataConv[i]);
                for (int j = 1; j <= times; j++)
                {
                    extendedData.Add((dataConv[i]- dataConv[i+1]) * (j/times + 1) + dataConv[i]);
                }
            }
            extendedData.Add(dataConv[^1]);
        }

        private int[] readData(byte[] data)
        {
            List<int> dataList = new List<int>();
            string hexValue = "";
            int counter = 0;
            for (int i = 0; i < data.Length; i++)
            {
                hexValue += data[i].ToString("X");
                counter++;

                if (counter == 2)
                {
                    dataList.Add(Convert.ToInt32(hexValue, 16));
                    counter = 0;
                    hexValue = "";
                }
            }
            return dataList.ToArray();
        }
    }
}

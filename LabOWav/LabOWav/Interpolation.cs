using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LabOWav
{
    public class Interpolation
    {
        public Byte[] Execute(byte[] data, double times, int channels)
        {
            var dataConv = ConvertByteToInt(data);
            double step = 1 / times; 
            int[] dataUpdate;
            if (channels == 1)
            {
                dataUpdate = CalculateInterpolation(dataConv, step);
            }
            else
            {
                dataUpdate = InterpolateStereo(dataConv, step);
            }

            return ConvertIntToByte(dataUpdate);
        }
        private int[] InterpolateStereo(int[] data, double step)
        {
            List<int> firstChannel = new List<int>();
            List<int> secondChannel = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                if(i%2 == 0)
                    firstChannel.Add(data[i]);
                else
                    secondChannel.Add(data[i]);
            }

            int[] firstArray = CalculateInterpolation(firstChannel.ToArray(), step);
            int[] secondArray = CalculateInterpolation(secondChannel.ToArray(), step);
            return MergeChannels(firstArray, secondArray);
        }
        private int[] MergeChannels(int[] firstChannel, int[] secondChannel)
        {
            List<int> mergedChannels = new List<int>();
            for (int i = 0; i < firstChannel.Length; i++)
            {
                mergedChannels.Add(firstChannel[i]);
                mergedChannels.Add(secondChannel[i]);
            }

            return mergedChannels.ToArray();
        }
        private int[] CalculateInterpolation(int[] data, double step)
        {
            double currentDistance = 0;
            int currentZone = 1;
            List<int> extendedData = new List<int>();
            extendedData.Add(data[0]);
            while (currentDistance < data.Length - 1)
            {
                if (currentDistance >= currentZone)
                {
                    currentZone++;
                }
                extendedData.Add(Interpolate(currentDistance, currentZone, data));

                currentDistance += step;
            }

            return extendedData.ToArray();
        }
        private int Interpolate(double currentDistance, int currentZone, int[] dataConv)
        {
            return (int) ((currentDistance - currentZone + 1) * (dataConv[currentZone] - dataConv[currentZone - 1]) +
                          dataConv[currentZone - 1]);
        }
        private int[] ConvertByteToInt(byte[] data)
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
                    int converted = Convert.ToInt32(hexValue, 16);
                    if (converted > 32767)
                        dataList.Add(converted - 65536);
                    else
                        dataList.Add(converted);
                    
                    counter = 0;
                    hexValue = "";
                }
            }
            return dataList.ToArray();
        }
        private byte[] ConvertIntToByte(int[] data)
        {
            List<byte> dataList = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                string hexValue = data[i].ToString("X");
                string hex1 = "00"; 
                string hex2 = "00";
                if (hexValue.Length > 2)
                {
                    hex1 = hexValue[0].ToString() + hexValue[1];
                    if (hexValue.Length == 3)
                    {
                        hex2 = hexValue[2].ToString();
                    }
                    else if (hexValue.Length == 4)
                    {
                        hex2 = hexValue[2].ToString() + hexValue[3];
                    }
                }
                else
                {
                    hex1 = hexValue;
                    hex2 = "00";
                }
                dataList.Add(Convert.ToByte(Convert.ToInt32(hex1, 16)));
                dataList.Add(Convert.ToByte(Convert.ToInt32(hex2, 16)));
            }

            return dataList.ToArray();
        }
    }
}

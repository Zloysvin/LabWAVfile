using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LabOWav
{
    public class Interpolation
    {
        public byte[] Interpolate(byte[] data, double times)
        {
            var step = 1 / times;
            var newCount = (int)Math.Round(data.Length / step);
            var newData = new byte[newCount];

            for (int i = 0; i < newCount-1; i++)
            {
                var xI = step * i;
                int upLimit = 0;
                upLimit = (int)Math.Round(xI, MidpointRounding.AwayFromZero);

                if (upLimit == 0)
                {
                    newData[i] = data[upLimit];
                    continue;
                }

                var downLimit = upLimit - 1;

                newData[i] = InterpolateBetweenTwoPoints((float)xI, data[downLimit], data[upLimit]);
            }

            return newData;
        }

        byte InterpolateBetweenTwoPoints(float x, byte val1, byte val2)
        {
            var x1 = Math.Round(x, MidpointRounding.ToZero);
            var x2 = Math.Round(x, MidpointRounding.AwayFromZero);
            if(x!=x1)
            {
                var delta = val2 - val1;
                var res = (byte) ((float) delta / (x2 - x1) * (x - x1) + val1);
                return res;
            }

            return val2;
        }

        public Byte[] Execute(byte[] data, double times, int channels)
        {
            byte[] dataUpdate;
            if (channels == 1)
            {
                dataUpdate = Interpolate(data, times);
            }
            else
            {
                List<byte> firstChannel = new List<byte>();
                List<byte> secondChannel = new List<byte>();
                for (int i = 0; i < data.Length; i++)
                {
                    if (i % 2 == 0)
                        firstChannel.Add(data[i]);
                    else
                        secondChannel.Add(data[i]);
                }

                var firstChannelArr = firstChannel.ToArray();
                var secondChannelArr = secondChannel.ToArray();
                dataUpdate = MergeChannels(Interpolate(firstChannelArr, times), Interpolate(secondChannelArr, times));
            }

            return dataUpdate;
        }


        private byte[] MergeChannels(byte[] firstChannel, byte[] secondChannel)
        {
            List<byte> mergedChannels = new List<byte>();
            for (int i = 0; i < firstChannel.Length; i++)
            {
                mergedChannels.Add(firstChannel[i]);
                mergedChannels.Add(secondChannel[i]);
            }

            return mergedChannels.ToArray();
        }
    }
}
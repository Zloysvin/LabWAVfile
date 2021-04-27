namespace LabOWav
{
    using System;

    public static class Operation
    {
        public static int ByteArrayToInt(byte[] bytes)
        {
            var result = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                result += bytes[i] * (int) Math.Pow(256, bytes.Length - i - 1);
            }

            return result;
        }

        public static byte[] IntToByteArray(int integer, int bytesCount)
        {
            var result = new byte[bytesCount];

            for (var i = bytesCount - 1; i >= 0; i--)
            {
                result[i] = (byte) ((integer % (int) Math.Pow(256, bytesCount - i)) /
                            (int) Math.Pow(256, bytesCount - i -1));
                integer -= result[i];
            }

            return result;
        }

        public static byte[] ReverseEndian(byte[] originBytes)
        {
            var reversedBytes = new byte[originBytes.Length];

            for (var i = 0; i < originBytes.Length; i++)
            {
                reversedBytes[i] = originBytes[originBytes.Length - i - 1];
            }

            return reversedBytes;
        }
    }
}
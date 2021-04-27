namespace LabOWav
{
    using System;
    using System.IO;
    using System.Security;

    public class WaveIO
    {
        private readonly BinaryReader reader;
        private readonly BinaryWriter writer;

        public WaveIO(string inputPath, string outputPath)
        {
            Stream readStream = new FileStream(inputPath, FileMode.Open);
            reader = new BinaryReader(readStream);
            Stream writeStream = new FileStream(outputPath, FileMode.OpenOrCreate);
            writer = new BinaryWriter(writeStream);
        }

        public ByteStream ReadByteStream()
        {
            var chunkId = reader.ReadBytes(4);
            var chunkSize = reader.ReadBytes(4);
            var format = reader.ReadBytes(4);

            var subchunk1Id = reader.ReadBytes(4);
            var subchunk1Size = reader.ReadBytes(4);
            var audioFormat = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(2)));
            var numChannels = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(2)));
            var sampleRate = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(4)));
            var byteRate = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(4)));
            var blockAlign = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(2)));
            var bitsPerSample = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(4)));

            var subchunk2Id = reader.ReadBytes(4);
            var subchunk2Size = Operation.ByteArrayToInt(Operation.ReverseEndian(reader.ReadBytes(4)));

            var data = reader.ReadBytes(subchunk2Size - 8);

            var resultByteStream =
                new ByteStream(chunkId, chunkSize, format, subchunk1Id, subchunk1Size, audioFormat, numChannels,
                    sampleRate, byteRate, blockAlign, bitsPerSample, subchunk2Id, subchunk2Size, data);

            reader.Close();

            return resultByteStream;
        }

        public void WriteByteStream(ByteStream byteStream)
        {
            writer.Write(byteStream.ChunkId);
            writer.Write(byteStream.ChunkSize);
            writer.Write(byteStream.Format);
            writer.Write(byteStream.Subchunk1ID);
            writer.Write(byteStream.Subchunk1Size);
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.AudioFormat, 2)));
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.NumChannels, 2)));
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.SampleRate, 4)));
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.ByteRate, 4)));
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.BlockAlign, 2)));
            writer.Write(Operation.ReverseEndian(Operation.IntToByteArray(byteStream.BitsPerSample, 2)));
            writer.Write(byteStream.Subchunk2ID);
            writer.Write(byteStream.Subchunk2Size);
            writer.Write(byteStream.Data);
        }
    }
}
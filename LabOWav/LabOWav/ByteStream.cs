namespace LabOWav
{
    public class ByteStream
    {
        public readonly byte[] ChunkId = new[] {(byte) 82, (byte) 73, (byte) 70, (byte) 70};

        public byte[] ChunkID { get; private set; }
        public byte[] ChunkSize { get; private set; }
        public byte[] Format { get; private set; }
        public byte[] Subchunk1ID { get; private set; }
        public byte[] Subchunk1Size { get; private set; }
        public int AudioFormat { get; private set; }
        public int NumChannels { get; private set; }
        public int SampleRate { get; private set; }
        public int ByteRate { get; private set; }
        public int BlockAlign { get; private set; }
        public int BitsPerSample { get; private set; }
        public byte[] Subchunk2ID { get; private set; }
        public int Subchunk2Size { get; private set; }
        public byte[] Data { get; private set; }

        public ByteStream(byte[] chunkId, byte[] chunkSize, byte[] format, byte[] subchunk1Id, byte[] subchunk1Size,
            int audioFormat, int numChannels, int sampleRate, int byteRate, int blockAlign, int bitsPerSample,
            byte[] subchunk2Id, int subchunk2Size, byte[] data)
        {
            ChunkID = chunkId;
            ChunkSize = chunkSize;
            Format = format;
            Subchunk1ID = subchunk1Id;
            Subchunk1Size = subchunk1Size;
            AudioFormat = audioFormat;
            NumChannels = numChannels;
            SampleRate = sampleRate;
            ByteRate = byteRate;
            BlockAlign = blockAlign;
            BitsPerSample = bitsPerSample;
            Subchunk2ID = subchunk2Id;
            Subchunk2Size = subchunk2Size;
            Data = data;
        }

        public void UpdateStream(byte[] newData)
        {
            Data = newData;
            Subchunk2Size = newData.Length + 4 + 4;
            ChunkSize = Operation.ReverseEndian(Operation.IntToByteArray(newData.Length + 36, 4));
        } 
    }
}

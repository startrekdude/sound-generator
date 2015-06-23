using System;
using System.IO;

/* Template Copied from: http://blogs.msdn.com/b/dawate/archive/2009/06/24/intro-to-audio-programming-part-3-synthesizing-simple-wave-audio-using-c.aspx */
namespace SoundGenerator.WaveCreator
{
    public class WaveGenerator
    {
        // Header, Format, Data chunks
        internal WaveHeader header;
        internal WaveFormatChunk format;
        internal WaveDataChunk data;

        public WaveGenerator()
        {
            this.header = new WaveHeader();
            this.format = new WaveFormatChunk();
            this.data = new WaveDataChunk();
        }

        public WaveGenerator(ushort channels, WaveSampleType samples)
            : this()
        {
            Console.WriteLine("Creating in-memory representations of neccessary Wave RIFF structures.");
            this.format.wChannels = channels;
            this.format.dwSamplesPerSec = (uint) samples;
        }

        public void Generate(short[] data)
        {
            Console.WriteLine("Populating Format and Data RIFF structures with Int16s and meta");
            this.data.shortArray = data;
            Console.WriteLine("Calculating RIFF Data Chunk size.");
            this.data.dwChunkSize = (uint)(this.data.shortArray.Length * (format.wBitsPerSample / 8));
            this.format.dwAvgBytesPerSec = this.format.dwSamplesPerSec * this.format.wBlockAlign;
        }

        public void Save(Stream stream)
        {
            Console.WriteLine("Wrapping provided Stream in BinaryWriter...");
            BinaryWriter writer = new BinaryWriter(stream);
            Console.WriteLine("Writing RIFF Structure: Header");
            writer.Write(this.header.sGroupID.ToCharArray());
            writer.Write(this.header.dwFileLength);
            writer.Write(this.header.sRiffType.ToCharArray());
            Console.WriteLine("Writing RIFF Structure: Format");
            writer.Write(this.format.sChunkID.ToCharArray());
            writer.Write(this.format.dwChunkSize);
            writer.Write(this.format.wFormatTag);
            writer.Write(this.format.wChannels);
            writer.Write(this.format.dwSamplesPerSec);
            writer.Write(this.format.dwAvgBytesPerSec);
            writer.Write(this.format.wBlockAlign);
            writer.Write(this.format.wBitsPerSample);
            Console.WriteLine("Writing RIFF Structure Data");
            writer.Write(this.data.sChunkID.ToCharArray());
            writer.Write(this.data.dwChunkSize);
            Console.WriteLine("Writing Data");
            foreach (short dataPoint in this.data.shortArray)
            {
                writer.Write(dataPoint);
            }
            Console.WriteLine("Seeking to Header dwFileLength Position");
            writer.Seek(4, SeekOrigin.Begin);
            uint filesize = (uint)writer.BaseStream.Length;
            Console.WriteLine("Writing WAVE Structure Length");
            writer.Write(filesize - 8);
            Console.WriteLine("Closing BinaryWriter. Wave written to Stream.");
            writer.Close();
        }
    }
}

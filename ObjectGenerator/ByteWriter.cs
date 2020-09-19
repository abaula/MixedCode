using System.Buffers;

namespace ObjectGenerator
{
    public class ByteWriter
    {
        protected byte[] buffer;
        protected int i;

        public ByteWriter(int length)
        {
            buffer = ArrayPool<byte>.Shared.Rent(length);
            i = 0;
        }

        public void Write(byte v)
        {
            buffer[i++] = v;
        }

        public void Write(bool v)
        {
            if (v) buffer[i++] = 0x01;
            else buffer[i++] = 0x00;
        }

        public void Write(int v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
        }

        public void Write(long v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
            buffer[i++] = (byte)(v >> 32);
            buffer[i++] = (byte)(v >> 40);
            buffer[i++] = (byte)(v >> 48);
            buffer[i++] = (byte)(v >> 56);
        }

        public void Write(ulong v)
        {
            buffer[i++] = (byte)v;
            buffer[i++] = (byte)(v >> 8);
            buffer[i++] = (byte)(v >> 16);
            buffer[i++] = (byte)(v >> 24);
            buffer[i++] = (byte)(v >> 32);
            buffer[i++] = (byte)(v >> 40);
            buffer[i++] = (byte)(v >> 48);
            buffer[i++] = (byte)(v >> 56);
        }

        public void Write(string v)
        {
            byte[] strBytes = System.Text.Encoding.Default.GetBytes(v);
            int len = strBytes.Length;
            Write(len);
            for(int j=0;j<len;j++)
            {
                buffer[i++] = strBytes[j];
            }
        }

        public byte[] EndWrite()
        {
            ArrayPool<byte>.Shared.Return(buffer);
            return buffer;
        }
    }
}
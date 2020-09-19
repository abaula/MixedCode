using System.Buffers;

namespace VariantObject
{
    public class ByteWriter
    {
        protected byte[] Buffer;
        protected int I;

        public ByteWriter(int length)
        {
            Buffer = ArrayPool<byte>.Shared.Rent(length);
            I = 0;
        }

        public void Write(byte v)
        {
            Buffer[I++] = v;
        }

        public void Write(bool v)
        {
            if (v) Buffer[I++] = 0x01;
            else Buffer[I++] = 0x00;
        }

        public void Write(int v)
        {
            Buffer[I++] = (byte)v;
            Buffer[I++] = (byte)(v >> 8);
            Buffer[I++] = (byte)(v >> 16);
            Buffer[I++] = (byte)(v >> 24);
        }

        public void Write(long v)
        {
            Buffer[I++] = (byte)v;
            Buffer[I++] = (byte)(v >> 8);
            Buffer[I++] = (byte)(v >> 16);
            Buffer[I++] = (byte)(v >> 24);
            Buffer[I++] = (byte)(v >> 32);
            Buffer[I++] = (byte)(v >> 40);
            Buffer[I++] = (byte)(v >> 48);
            Buffer[I++] = (byte)(v >> 56);
        }

        public void Write(ulong v)
        {
            Buffer[I++] = (byte)v;
            Buffer[I++] = (byte)(v >> 8);
            Buffer[I++] = (byte)(v >> 16);
            Buffer[I++] = (byte)(v >> 24);
            Buffer[I++] = (byte)(v >> 32);
            Buffer[I++] = (byte)(v >> 40);
            Buffer[I++] = (byte)(v >> 48);
            Buffer[I++] = (byte)(v >> 56);
        }

        public void Write(string v)
        {
            byte[] strBytes = System.Text.Encoding.Default.GetBytes(v);
            int len = strBytes.Length;
            Write(len);
            for(int j=0;j<len;j++)
            {
                Buffer[I++] = strBytes[j];
            }
        }

        public byte[] EndWrite()
        {
            ArrayPool<byte>.Shared.Return(Buffer);
            return Buffer;
        }
    }
}
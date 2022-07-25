using System;
using System.Text;

public class BytesBuffer
{
    /*
     *  private static Dictionary<Type, int> genericSizes = new Dictionary<Type, int>()
        {
            { typeof(bool),     sizeof(bool) },
            { typeof(float),    sizeof(float) },
            { typeof(double),   sizeof(double) },
            { typeof(sbyte),    sizeof(sbyte) },
            { typeof(byte),     sizeof(byte) },
            { typeof(short),    sizeof(short) },
            { typeof(ushort),   sizeof(ushort) },
            { typeof(int),      sizeof(int) },
            { typeof(uint),     sizeof(uint) },
            { typeof(ulong),    sizeof(ulong) },
            { typeof(long),     sizeof(long) },
        };
     * */

    /*
     * 可以通过将 16 位、32 位或 64 位整数传递给IPAddress.HostToNetworkOrder方法，从网络字节顺序转换为主计算机的字节顺序，
     * 而无需检索字段的值BitConverter.IsLittleEndian。
     * */
    public static readonly int BoolSize = sizeof(bool);
    public static readonly int FloatSize = sizeof(float);
    public static readonly int ByteSize = sizeof(byte);
    public static readonly int ShortSize = sizeof(short);
    public static readonly int IntSize = sizeof(int);
    public static readonly int LongSize = sizeof(long);

    public static void WriteBool(bool value, byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                *(bool*)(ptr + offset) = value;
                offset += BoolSize;
            }
        }
    }
    public static void WriteFloat(float value, byte[] buffer, ref int offset)
    {
        var data = BitConverter.GetBytes(value);
        unsafe
        {
            //IntPtr ptr = (IntPtr)Unsafe.AsPointer(ref MemoryMarshal.GetArrayDataReference(data));
            //Marshal.Copy(ptr, buffer, offset, data.Length);
            Array.Copy(data, 0, buffer, offset, data.Length);
            offset += FloatSize;
        }
    }
    public static void WriteShort(short value, byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += ShortSize;
            }
        }

    }
    public static void WriteInt(int value, byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += IntSize;
            }
        }
    }
    public static void WriteLong(long value, byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += LongSize;
            }
        }

    }
    public static void WriteByte(byte value, byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                *(ptr + offset) = value;
                offset += ByteSize;
            }
        }

    }
    public static void WriteBytes(byte[] value, byte[] buffer, ref int offset)
    {
        WriteInt(value.Length, buffer, ref offset);
        unsafe
        {
            //IntPtr ptr = (IntPtr)Unsafe.AsPointer(ref MemoryMarshal.GetArrayDataReference(value));
            //Marshal.Copy(ptr, buffer, offset, value.Length);
            Array.Copy(value, 0, buffer, offset, value.Length);
            offset += value.Length;
        }
    }
    public static void WriteString(string value, byte[] buffer, ref int offset)
    {
        var data = Encoding.UTF8.GetBytes(value);
        WriteInt(data.Length, buffer, ref offset);
        unsafe
        {
            Array.Copy(data, 0, buffer, offset, value.Length);
            offset += value.Length;
        }
    }
    public static bool ReadBool(byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                var value = *(bool*)(ptr + offset);
                offset += BoolSize;
                return value;
            }
        }
    }
    public static float ReadFloat(byte[] buffer, ref int offset)
    {
        float value = BitConverter.ToSingle(buffer, offset);
        offset += FloatSize;
        return value;
    }
    public static short ReadShort(byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + offset);
                offset += ShortSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }
    }
    public static int ReadInt(byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                var value = *(int*)(ptr + offset);
                offset += IntSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }
    }
    public static long ReadLong(byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + offset);
                offset += LongSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }
    }
    public static byte ReadByte(byte[] buffer, ref int offset)
    {
        unsafe
        {
            fixed (byte* ptr = buffer)
            {
                var value = *(ptr + offset);
                offset += ByteSize;
                return value;
            }
        }
    }
    public static byte[] ReadBytes(byte[] buffer, ref int offset)
    {
        int dataLength = ReadInt(buffer, ref offset);
        byte[] result = new byte[dataLength];
        Array.Copy(buffer, offset, result, 0, dataLength);
        offset += dataLength;
        return result;
    }
    public static string ReadString(byte[] buffer, ref int offset)
    {
        int dataLength = ReadInt(buffer, ref offset);
        string str = Encoding.UTF8.GetString(buffer, offset, dataLength);
        offset += dataLength;
        return str;
    }
}
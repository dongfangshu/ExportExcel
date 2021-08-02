using System;
using System.Linq;
using System.Text;

public class BytesBuffer
{
    private static int IntSize = sizeof(int);
    private static int LongSize = sizeof(long);
    private static int BoolSize = sizeof(bool);
    private static int ShortSize = sizeof(short);

    public static unsafe void WriteBoolBytes(bool value, byte[] buffer, ref int size)
    {
        fixed (byte* ptr = buffer)
        {
            *(bool*)(ptr + size) = value;
            size += BoolSize;
        }
    }
    public static unsafe void WriteIntBytes(int value, byte[] buffer, ref int size)
    {
        fixed (byte* ptr = buffer)
        {
            *(int*)(ptr + size) = value;
            size += IntSize;
        }
    }
    public static unsafe void WriteLongBytes(long value, byte[] buffer, ref int size)
    {
        fixed (byte* ptr = buffer)
        {
            *(long*)(ptr + size) = value;
            size += LongSize;
        }
    }
    public static unsafe void WriteShortBytes(short value, byte[] buffer, ref int size)
    {
        fixed (byte* ptr = buffer)
        {
            *(short*)(ptr + size) = value;
            size += ShortSize;
        }
    }
    public static void WriteStringBytes(string value, byte[] buffer, ref int size)
    {
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(value);
        int length = temp.Length;//先拿出长度
        WriteIntBytes(length, buffer, ref size);//先存长度 size+4
        Array.Copy(temp, 0, buffer, size, length);//长度存完再存string 的byte[] size+length
        size += length;
    }
    public static string ReadStringBytes(byte[] data, ref int index)
    {
        //先取出长度
        int length = ReadIntBytes(data, ref index);
        byte[] temp = data.Skip(index).Take(length).ToArray();//取出目标数组
        index += length;
        return System.Text.Encoding.UTF8.GetString(temp);
    }
    public static unsafe int ReadIntBytes(byte[] data, ref int index)
    {
        fixed (byte* ptr = data)
        {
            int value = *(int*)(ptr + index);
            index += IntSize;
            return value;
        }
    }
    public static unsafe long ReadLongBytes(byte[] data, ref int index)
    {
        fixed (byte* ptr = data)
        {
            long value = *(long*)(ptr + index);
            index += LongSize;
            return value;
        }
    }
    public static unsafe bool ReadBoolBytes(byte[] data, ref int index)
    {
        fixed (byte* ptr = data)
        {
            bool value = *(bool*)(ptr + index);
            index += BoolSize;
            return value;
        }
    }
    public static unsafe short ReadShortBytes(byte[] data, ref int index)
    {
        fixed (byte* ptr = data)
        {
            short value = *(short*)(ptr + index);
            index += BoolSize;
            return value;
        }
    }
}


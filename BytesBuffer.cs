using System;
using System.Linq;
using System.Text;

public class BytesBuffer
{
    private static int IntSize = sizeof(int);
    private static int LongSize = sizeof(long);
    private static int BoolSize = sizeof(bool);
    private static int ShortSize = sizeof(short);

    public static unsafe void WriteBoolBytes(bool value, byte[] buffer, ref int offset)
    {
        fixed (byte* ptr = buffer)
        {
            *(bool*)(ptr + offset) = value;
            offset += BoolSize;
        }
    }
    public static unsafe void WriteIntBytes(int value, byte[] buffer, ref int offset)
    {
        fixed (byte* ptr = buffer)
        {
            *(int*)(ptr + offset) = value;
            offset += IntSize;
        }
    }
    public static unsafe void WriteLongBytes(long value, byte[] buffer, ref int offset)
    {
        fixed (byte* ptr = buffer)
        {
            *(long*)(ptr + offset) = value;
            offset += LongSize;
        }
    }
    public static unsafe void WriteShortBytes(short value, byte[] buffer, ref int offset)
    {
        fixed (byte* ptr = buffer)
        {
            *(short*)(ptr + offset) = value;
            offset += ShortSize;
        }
    }
    public static void WriteStringBytes(string value, byte[] buffer, ref int offset)
    {
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(value);
        int length = temp.Length;//先拿出长度
        WriteIntBytes(length, buffer, ref offset);//先存长度 size+4
        Array.Copy(temp, 0, buffer, offset, length);//长度存完再存string 的byte[] size+length
        offset += length;
    }
    public static string ReadStringBytes(byte[] data, ref int offset)
    {
        //先取出长度
        int length = ReadIntBytes(data, ref offset);
        //byte[] temp = data.Skip(offset).Take(length).ToArray();//取出目标数组
        var str = System.Text.Encoding.UTF8.GetString(data,offset,length);
        offset += length;
        return str;
    }
    public static unsafe int ReadIntBytes(byte[] data, ref int offset)
    {
        fixed (byte* ptr = data)
        {
            int value = *(int*)(ptr + offset);
            offset += IntSize;
            return value;
        }
    }
    public static unsafe long ReadLongBytes(byte[] data, ref int offset)
    {
        fixed (byte* ptr = data)
        {
            long value = *(long*)(ptr + offset);
            offset += LongSize;
            return value;
        }
    }
    public static unsafe bool ReadBoolBytes(byte[] data, ref int offset)
    {
        fixed (byte* ptr = data)
        {
            bool value = *(bool*)(ptr + offset);
            offset += BoolSize;
            return value;
        }
    }
    public static unsafe short ReadShortBytes(byte[] data, ref int offset)
    {
        fixed (byte* ptr = data)
        {
            short value = *(short*)(ptr + offset);
            offset += BoolSize;
            return value;
        }
    }
}


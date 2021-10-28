using System.Runtime.InteropServices;

namespace DK64Converter
{
    [StructLayout(LayoutKind.Explicit, Size = 0x09)]
    public class Exit
    {
        [FieldOffset(0x00)] public short x_pos;
        [FieldOffset(0x02)] public short y_pos;
        [FieldOffset(0x04)] public short z_pos;
        [FieldOffset(0x06)] public short angle;
        [FieldOffset(0x08)] public byte has_autowalk;
        [FieldOffset(0x09)] public byte size;
    }
}

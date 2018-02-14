namespace ZplLabels.ZPL
{
    public interface IFieldGenerator
    {
        string ToString();
        IFieldGenerator At(int x, int y);
        IFieldGenerator At(ZplLabels.Utilities.DPIHelper dpiHelper, double x, double y);
        IFieldGenerator Move(int x, int y);
        IFieldGenerator Move(ZplLabels.Utilities.DPIHelper dpiHelper, double x, double y);
    }
}
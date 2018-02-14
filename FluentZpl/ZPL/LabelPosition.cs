using System;

namespace ZplLabels.ZPL
{
    public class LabelPosition : ICloneable
    {
        public LabelPosition(int xAxis, int yAxis)
        {
            X = xAxis;
            Y = yAxis;
        }
        public int X { get; private set; }
        public int Y { get; private set; }
        public override string  ToString()
        {
            return string.Format("^FO{0},{1}", X, Y);
        }

        public object Clone()
        {
            return new LabelPosition(X, Y);
        }
    }
}
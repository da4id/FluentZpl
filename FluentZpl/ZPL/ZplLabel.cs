namespace ZplLabels.ZPL
{
    public class ZplLabel
    {

        private string _script;
        private int _homeX;
        private int _homeY;
        private int _customCutOffset;
        private string _customZPL = "";
        private PrintMode _mode = PrintMode.tearOff;

        public ZplLabel()
        {
        }

        /// <summary>
        /// Load ZPl Label Data
        /// </summary>
        /// <param name="generators"></param>
        /// <returns></returns>
        public ZplLabel Load(params IFieldGenerator[] generators)
        {
            foreach (IFieldGenerator gen in generators)
            {
                _script += gen.ToString();

            }
            return this;
        }

        /// <summary>
        /// returns ZPL Code
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return getHeader() + getHome() + _customZPL + _script + getFooter();
        }

        private string getHome()
        {
            return string.Format("^LH{0},{1}\r\n", _homeX, _homeY);
        }

        /// <summary>
        /// Sets Homeposition of Label in Pixel
        /// </summary>
        /// <param name="fromLeft"></param>
        /// <param name="fromTop"></param>
        /// <returns></returns>
        public ZplLabel At(int fromLeft, int fromTop)
        {
            _homeX = fromLeft;
            _homeY = fromTop;
            return this;
        }
        /// <summary>
        /// Sets Homeposition of Label in mm
        /// </summary>
        /// <param name="dpiHelper"></param>
        /// <param name="fromLeft"></param>
        /// <param name="fromTop"></param>
        /// <returns></returns>
        public ZplLabel At(ZplLabels.Utilities.DPIHelper dpiHelper, double fromLeft, double fromTop)
        {
            ;
            _homeX = dpiHelper.mmToPx(fromLeft);
            _homeY = dpiHelper.mmToPx(fromTop);
            return this;
        }

        /// <summary>
        /// Cut Offset in Pixel
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ZplLabel CutOffset(int offset)
        {
            _customCutOffset = offset;
            return this;
        }

        /// <summary>
        /// Cut Offset in milimeter
        /// </summary>
        /// <param name="dpiHelper"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ZplLabel CutOffset(ZplLabels.Utilities.DPIHelper dpiHelper, double offset)
        {
            _customCutOffset = dpiHelper.mmToPx(offset);
            return this;
        }

        /// <summary>
        /// Sets Mode of the Printer
        /// Tear Off
        /// Peel Off
        /// Cut
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ZplLabel Mode(PrintMode mode)
        {
            _mode = mode;
            return this;
        }
        /// <summary>
        /// Insert Custom ZPL Code inside Label
        /// </summary>
        /// <param name="customZPL"></param>
        /// <returns></returns>
        public ZplLabel customZPLCommand(string customZPL)
        {
            _customZPL = customZPL;
            return this;
        }

        private string getFooter()
        {
            return @"^XZ\r\n";
        }

        private string getHeader()
        {
            var mode = "";
            switch (_mode)
            {
                case PrintMode.peelOff:
                    mode = "^MMP,N";
                    break;
                case PrintMode.cut:
                    mode = "^MMC,N";
                    break;
                case PrintMode.tearOff:
                    mode = "^MMT,N";
                    break;
            }

            return "^XA" + mode + "~TA" + _customCutOffset.ToString("000") + "\r\n";
        }
    }
}

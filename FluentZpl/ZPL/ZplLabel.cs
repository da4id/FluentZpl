namespace ZplLabels.ZPL
{
    public class ZplLabel
    {

        private string _script;
        private int _homeX;
        private int _homeY;
        private int _customCutOffset;
        private string _customZPL = "";
        private PrintMode _mode = PrintMode.continous;

        public ZplLabel ()
        { 
        }

        public ZplLabel Load(params IFieldGenerator [] generators)
        {
            foreach(IFieldGenerator gen in generators)
            {
                _script += gen.ToString();

            } 
            return this;
        }

        public override string ToString()
        {
            return getHeader() + getHome() + _customZPL + _script + getFooter();
        }

        private string getHome()
        {
            return string.Format("^LH{0},{1}\r\n", _homeX, _homeY);
        }

        public ZplLabel At(int fromLeft, int fromTop)
        {
            _homeX = fromLeft;
            _homeY = fromTop;
            return this;
        }
        public ZplLabel CutOffset(int offset)
        {
            _customCutOffset = offset;
            return this;
        }
        public ZplLabel Mode(PrintMode mode)
        {
            _mode = mode;
            return this;
        }
        public ZplLabel customZPLCommand (string customZPL)
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
                case PrintMode.continous:
                    mode = "^MMT,N";
                    break;
            }

            return "^XA" + mode + "~TA" + _customCutOffset.ToString("000") + "\r\n";
        }
    }
}

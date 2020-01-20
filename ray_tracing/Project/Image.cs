namespace COSIG_RAY_TRACING
{
    public class Image
    {
        private int horizontal;
        private int vertical;
        private Color background;

        public Image()
        {
            horizontal = 0;
            vertical = 0;
            background = new Color();
        }

        public Image(int horizontal, int vertical, Color background)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.background = background;
        }

        public Color Background
        {
            get { return background; }
            set { background = value; }
        }

        public int Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }

        public int Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }
    }
}
using BoomermanServer.Data;

namespace BoomermanServer.Patterns.Iterator
{
    public class ColorPalette
    {
        private PlayerColor[] colors;
        private int current = 0;

        public ColorPalette()
        {
            colors = new PlayerColor[] { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Green };
        }

        public IIterator GetIterator()
        {
            return new ColorIterator(this);
        }

        private class ColorIterator : IIterator
        {
            private ColorPalette colorPalette;
            private int current = 0;

            public ColorIterator(ColorPalette colorPalette)
            {
                this.colorPalette = colorPalette;
            }

            public object First()
            {
                return colorPalette.colors[0];
            }

            public object Next()
            {
                PlayerColor color = colorPalette.colors[current++];
                if (current >= colorPalette.colors.Length)
                {
                    current = 0;
                }
                return color;
            }

            public bool IsDone()
            {
                return false;
            }

            public object CurrentItem()
            {
                return colorPalette.colors[current];
            }
        }
    }
}

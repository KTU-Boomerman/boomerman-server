using System;
using BoomermanServer.Data;

namespace BoomermanServer.Patterns.Iterator
{
    public class ColorPalette
    {
        private PlayerColor[] colors;

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
            private int current;
            private readonly object colorLock = new object();

            public ColorIterator(ColorPalette colorPalette)
            {
                this.colorPalette = colorPalette;
                this.current = 0;
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

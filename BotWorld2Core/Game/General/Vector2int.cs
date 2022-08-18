namespace BotWorld2Core.Game.General
{
    public struct Vector2int
    {
        public static Vector2int operator +(Vector2int a, Vector2int b)
        {
            return new Vector2int(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2int operator -(Vector2int a, Vector2int b)
        {
            return new Vector2int(a.X - b.X, a.Y - b.Y);
        }

        public int X, Y;
        public Vector2int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void RotateLeft()
        {
            if (X == 0)
            {
                X = -Y;
            }
            else if (X == 1)
            {
                if (Y != 1)
                    Y += 1;
                else { X = 0; Y = 1; }
            }
            else if (X == -1)
            {
                if (Y != -1)
                    Y -= 1;
                else { X = 0; Y = -1; }
            }
        }
        public void RotateRight()
        {
            if (X == 0)
            {
                X = Y;
            }
            else if (X == 1)
            {
                if (Y != -1)
                    Y -= 1;
                else { X = 0; Y = -1; };
            }
            else if (X == -1)
            {
                if (Y != 1)
                    Y += 1;
                else { X = 0; Y = 1; };
            }
        }
    }
}
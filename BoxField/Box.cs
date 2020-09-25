using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{
    class Box
    {
        public Color color;
        public int x, y, size;

        public Box(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public Box(int _x, int _y, int _size, Color _color)
        {
            x = _x;
            y = _y;
            size = _size;
            color = _color;
        }

        public void Move(int speed)
        {
            y += speed;
        }

        public void Move(int speed, Boolean direction)
        {
            if (direction)
            {
                x += speed;
            }
            else
            {
                x -= speed;
            }
            
        }
    }
}

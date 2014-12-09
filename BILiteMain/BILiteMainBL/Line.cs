using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BILiteConnectionForm
{
    public class Line
    {
        public Pen pen { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }

        public Line(Pen p, Point p1, Point p2)
        {
            pen = p;
            Start = p1;
            End = p2;
        }

        public bool IsStartPoint(Point p, int cushion)
        {
            if (p.X <= Start.X + cushion && p.X >= Start.X - cushion && p.Y <= Start.Y + cushion && p.Y >= Start.Y - cushion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEndPoint(Point p, int cushion)
        {
            if (p.X <= End.X + cushion && p.X >= End.X - cushion && p.Y <= End.Y + cushion && p.Y >= End.Y - cushion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

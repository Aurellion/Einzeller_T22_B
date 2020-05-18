using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Einzeller
{
    abstract class Zellen
    {
        //int x, y;
        //public int X { get { return x; } }
        //public int Y { get { return y; } }
        public int x { get; private set; }
        public int y { get; private set; }

        protected static Random rnd = new Random();

        public Zellen()
        {
            x = rnd.Next(50,751);
            y = rnd.Next(50,401);
        }
        public Zellen(Zellen z)
        {
            x = z.x;
            y = z.y;
        }

        public abstract void Draw(Canvas petrischale);

        public void Move()
        {
            x += rnd.Next(-1, 2);
            y += rnd.Next(0, 3) - 1;
        }

        public abstract List<Zellen> Teilen();
        public abstract List<Zellen> Sterben();
        /// <summary>
        /// p legt die Todeswahrscheinlichkeit fest
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public abstract List<Zellen> Sterben(double p);
        public abstract List<Zellen> Fressen(List<Zellen> z, List<Zellen> t);
      
    }

    class Amöbe : Zellen
    {
        bool hatGefressen = false;
      
        public Amöbe() { }
        public Amöbe(Amöbe a) : base(a)
        {
            //hatGefressen = a.hatGefressen;
        }
        public override List<Zellen> Teilen()
        {
            double p = hatGefressen ? 0.04 : 0.02;
            List<Zellen> Töchter = new List<Zellen>();
            if (rnd.NextDouble() < p)
            {
                Töchter.Add( new Amöbe(this));
            }
            return Töchter;            
        }

        public override List<Zellen> Sterben()
        {
            List<Zellen> gestorben = new List<Zellen>();
            if (rnd.NextDouble() < 0.01)
            {
                gestorben.Add(this);
            }
            return gestorben;
        }

        public override List<Zellen> Sterben(double p)
        {
            List<Zellen> gestorben = new List<Zellen>();
            if (rnd.NextDouble() < p)
            {
                gestorben.Add(this);
            }
            return gestorben;
        }
        public override void Draw(Canvas petrischale)
        {
            Ellipse e = new Ellipse();
            e.Width = 8;
            e.Height = 8;
            //if (hatGefressen) e.Fill = Brushes.Blue;
            //else e.Fill = Brushes.Red;
            e.Fill = hatGefressen ? Brushes.Blue : Brushes.Red;
            petrischale.Children.Add(e);
            Canvas.SetLeft(e, x-4);
            Canvas.SetTop(e, y-4);            
        }

        public override List<Zellen> Fressen(List<Zellen> z, List<Zellen> t)
        {
            List<Zellen> gefressen = new List<Zellen>();
            foreach (Zellen item in z)
            {
                if (item is Bakterie && !t.Contains(item))
                {
                    double dx, dy;
                    dx = item.x - x;
                    dy = item.y - y;
                    if (dx*dx+dy*dy < 36)
                    {
                        gefressen.Add(item);
                        hatGefressen = true;
                    }
                }
            }
            return gefressen;
        }


    }

    class Bakterie : Zellen
    {
        public Bakterie() { }
        public Bakterie(Bakterie b) : base(b)
        {

        }
        public override List<Zellen> Teilen()
        {
            List<Zellen> Töchter = new List<Zellen>();
            if (rnd.NextDouble() < 0.02)
            {
                Töchter.Add(new Bakterie(this));
            }
            return Töchter;

        }
        public override List<Zellen> Sterben()
        {
            List<Zellen> gestorben = new List<Zellen>();
            if (rnd.NextDouble() < 0.01)
            {
                gestorben.Add(this);
            }
            return gestorben;
        }
        public override List<Zellen> Sterben(double p)
        {
            List<Zellen> gestorben = new List<Zellen>();
            if (rnd.NextDouble() < p)
            {
                gestorben.Add(this);
            }
            return gestorben;
        }

        public override void Draw(Canvas petrischale)
        {
            Ellipse e = new Ellipse();
            e.Width = 4;
            e.Height = 4;
            e.Fill = Brushes.Green;
            petrischale.Children.Add(e);
            Canvas.SetLeft(e, x - 2);
            Canvas.SetTop(e, y - 2);
        }

        public override List<Zellen> Fressen(List<Zellen> z, List<Zellen> t)
        {
            return new List<Zellen>();
        }

    }
}

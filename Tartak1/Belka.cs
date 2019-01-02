using System.Collections.Generic;

namespace Tartak1
{
    public class Belka
    {  
        public double? size;
        public double empty;
        public List<double> items;

        public Belka(double contSize)
        {
            this.size = contSize;
            this.empty = contSize;
            this.items = new List<double>();
        }

        public Belka()
        {
            this.items = new List<double>();
        }

        public bool put(double itemSize)
        {
            const double chainsaw = 0.05;
            if (itemSize + chainsaw > this.empty)
            { 
                return false;
            }
            else
            {
                this.empty -= itemSize;
                this.empty -= chainsaw; //Szerokość cięcia

                this.items.Add(itemSize);
                return true;
            }
        }
    }
}

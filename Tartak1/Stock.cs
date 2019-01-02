using System;

namespace Tartak1
{
    public class Stock : ICloneable
    {
        public double size;
        public double total;

        public Stock()
        {
        }

        public Stock(double itemSize, int count)
        {
            this.size = itemSize;
            this.total = itemSize * count;
        }

        public bool isEmpty() => this.total <= 0;

        public double remove()
        {
            this.total -= this.size;
            return this.size;
        }

        public object Clone()
        {
            Stock s = new Stock()
            {
                size = this.size,
                total = this.total
            };

            return s;
        }
    }
}

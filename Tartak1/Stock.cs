namespace Tartak1
{
    public class Stock
    {
        public readonly double size;
	    public double total;

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
    }
}

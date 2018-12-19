using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tartak1
{
    public partial class Form1 : Form
    {
        private List<Stock> stocks;

        public double ContSize => double.Parse(this.maskedTextBox1.Text);

        public Form1()
        {
            InitializeComponent();
            stocks = new List<Stock>();
            this.maskedTextBox1.Text = "6,00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.stocks = new List<Stock>();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(openFileDialog1.FileName);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;

                try
                { 
                    for (int i = 2; i <= rowCount; i++)
                    {
                        double.TryParse(Convert.ToString(xlRange.Cells[i, 1].Value2), out double d);
                        int.TryParse(Convert.ToString(xlRange.Cells[i, 2].Value2), out int c);
                        if (c > 0 && d > 0)
                        { 
                            stocks.Add(new Stock(d, c));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message);
                }
                finally
                {
                    xlWorkbook.Close();
                    xlApp.Quit();
                }

                MessageBox.Show($"Liczba elementów: {stocks.Count}");


            }
        }

        private void BestFit_Click(object sender, EventArgs e)
        {
            const double chainsaw = 0.05;
            List<Belka> bins = new List<Belka>
            {
                new Belka(this.ContSize)
            };

            for (var i = 0; i < stocks.Count; ++i)
            {
                Stock stock = stocks[i];
                if (stock.size > this.ContSize)
                { 
                    continue;
                }
                while (!stock.isEmpty())
                {
                    Belka bestbin = new Belka();
                    for (var j = 0; j < bins.Count; ++j)
                    {
                        if (bins[j].empty >= stock.size + chainsaw)
                        {
                            if (bestbin.size == null || bestbin.empty > bins[j].empty)
                            { 
                                bestbin = bins[j];
                            }
                        }
                    }
                    if (bestbin.size != null && bestbin.put(stock.size))
                        stock.remove();
                    else
                    {
                        bins.Add(new Belka(this.ContSize));
                        bins[bins.Count - 1].put(stock.remove());
                    }
                }
            }

            double totalEmpty = bins.Sum(x => x.empty);
            double totalPercent = 1 - totalEmpty / (bins.Count * this.ContSize);
            MessageBox.Show($"Liczba belek: {bins.Count}\nCałkowity procent wykorzystania: {totalPercent:0.00%}\nSuma odrzutu [m]: {totalEmpty:0.00}");

            int b = 0;
            foreach (Belka bin in bins)
            {
                dataGridView1.Rows.Add(++b, String.Join("; ", bin.items), String.Format("{0:0.00}", bin.empty), String.Format("{0:0.00%}", (1 - bin.empty/ this.ContSize)));
            }
            dataGridView1.Refresh();
        }

        private void WorstFit_Click(object sender, EventArgs e)
        {
            List<Belka> bins = new List<Belka>
            {
                new Belka(this.ContSize)
            };

            for (var i = 0; i < stocks.Count; ++i)
            {
                Stock stock = stocks[i];
                if (stock.size > this.ContSize)
                { 
                    continue;
                }
                while (!stock.isEmpty())
                {
                    var worstbin = bins[0];
                    for (var j = 1; j < bins.Count; ++j)
                    {
                        if (bins[j].empty > worstbin.empty)
                        { 
                            worstbin = bins[j];
                        }
                    }
                    if (worstbin.put(stock.size))
                    { 
                        stock.remove();
                    }
                    else
                    {
                        bins.Add(new Belka(this.ContSize));
                        bins[bins.Count - 1].put(stock.remove());
                    }
                }
            }

            double totalEmpty = bins.Sum(x => x.empty);
            double totalPercent = 1 - totalEmpty / (bins.Count * this.ContSize);
            MessageBox.Show($"Liczba belek: {bins.Count}\nCałkowity procent wykorzystania: {totalPercent:0.00%}\nSuma odrzutu [m]: {totalEmpty:0.00}");

            int b = 0;
            foreach (Belka bin in bins)
            {
                dataGridView1.Rows.Add(++b, String.Join("; ", bin.items), String.Format("{0:0.00}", bin.empty), String.Format("{0:0.00%}", (1 - bin.empty / this.ContSize)));
            }
            dataGridView1.Refresh();
        }

        private void clrBtn_Click(object sender, EventArgs e) => dataGridView1.Rows.Clear();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods
{
    public class FunctionValueTable
    {
        #region Public Fields

        #region Properties

        public List<(double x, double y)> Points { get; set; }

        #endregion

        #endregion

        #region Constructors

        public FunctionValueTable()
        {
            Points = new List<(double x, double y)>();
        }

        public FunctionValueTable(List<(double x, double y)> points)
        {
            Points = points;
        }

        public FunctionValueTable(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("The lengths of the vectors are not equal");
            }

            Points = new List<(double x, double y)>();

            for (int i = 0; i < x.Length; i++)
            {
                Points.Add((x[i], y[i]));
            }
        }

        #endregion

        public string ToStringInline()
        {
            // Variables //
            var str = new StringBuilder(string.Empty);
            const int offset = 8; // Offset for values
            // Generate string //
            str.Append("I :");
            for (int i = 0; i < Points.Count; i++)
            {
                str.Append($"{i,offset}");
            }

            str.Append("\nX :");
            for (int i = 0; i < Points.Count; i++)
            {
                str.Append($"{Points[i].x,offset:###.####}");
            }

            str.Append("\nY :");
            for (int i = 0; i < Points.Count; i++)
            {
                str.Append($"{Points[i].y,offset:###.####}");
            }

            return str.ToString();
        }

        public override string ToString()
        {
            var str = new StringBuilder(string.Empty);

            str.AppendLine($"{"I",2}{"Xi",6}{"y(XI)",10}");
            for (int i = 0; i < Points.Count; i++)
            {
                str.AppendLine($"{i,2}{Math.Round(Points[i].x, 1),6}{Math.Round(Points[i].y, 4),10}");
            }

            return str.ToString();
        }
    }
}

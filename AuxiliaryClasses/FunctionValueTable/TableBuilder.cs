using System;

namespace ComputationalMethods
{
    class TableBuilder
    {
        #region Private Fields

        private FunctionValueTable _table;

        #endregion

        #region Public Properties

        public double StartOfRange { get; set; }

        public double EndOfRange { get; set; }

        public double Increment { get; set; }

        public Func<double, double> Function { get; set; }

        #endregion

        #region Constructor

        public FunctionValueTable BuildTable()
        {
            if (Function is null)
            {
                throw new ArgumentNullException(nameof(Function), "Function  is null");
            }

            if (EndOfRange < StartOfRange)
            {
                throw new ArgumentException("End position less then start position");
            }

            if (Increment <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Increment), "Shift value must be positive.");
            }

            _table = new FunctionValueTable();

            for (double currentX = StartOfRange; currentX <= EndOfRange; currentX += Increment)
            {
                _table.Points.Add((currentX, Function(currentX)));
            }

            return _table;
        }

        #endregion
    }
}

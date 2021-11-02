using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Integrate.NumericalIntegration
{
    class Gauss : IIntegration
    {
        public const int MAX_PARTITIONS = 6;

        private int _numberOfPartitions = 6;

        private double[] _x = new[] {-0.93246951, -0.6612093, -0.23861918, 0.23861918, 0.6612093, 0.93246951};
        private double[] _c = new[] {0.171324492, 0.36076157, 0.46793, 0.46793, 0.36076157, 0.171324492};

        public int NumberOfPartitions
        {
            get => _numberOfPartitions;
            set
            {
                int n = value;
                _x = new double[n];
                _c = new double[n];

                switch (n)
                {
                    case 1:
                        _x[0] = 0; _c[0] = 2;
                        break;
                    case 2:
                        _x[0] = _x[1] = 0.57735026;
                        _c[0] = _c[1] = 1;
                        break;
                    case 3:
                        _x[1] = 0; _x[2] = 0.7745966; _x[0] = -(_x[2]);
                        _c[1] = 0.8888888889; _c[0] = _c[2] = 0.5555555556;
                        break;
                    case 4:
                        _x[2] = 0.3399811; _x[1] = -(_x[2]);
                        _x[0] = 0.86113631; _x[3] = -(_x[0]);

                        _c[1] = _c[2] = 0.6521451549;
                        _c[0] = _c[3] = 0.034785484;
                        break;
                    case 5:
                        _x[2] = 0;
                        _x[3] = 0.538469; _x[1] = -(_x[3]);
                        _x[4] = 0.9061798; _x[0] = -(_x[4]);

                        _c[2] = 0.568888889;
                        _c[1] = _c[3] = 0.47862867;
                        _c[0] = _c[4] = 0.2369268851;
                        break;
                    case 6:
                        _x[5] = 0.93246951; _x[0] = -(_x[5]);
                        _x[4] = 0.6612093; _x[1] = -(_x[4]);
                        _x[3] = 0.23861918; _x[2] = -(_x[3]);

                        _c[0] = _c[5] = 0.171324492;
                        _c[1] = _c[4] = 0.36076157;
                        _c[2] = _c[3] = 0.46793;
                        break;
                    default:
                        throw new ArgumentException("Invalid Count of Partitions");
                }
            }
        }

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd)
        {
            //double h = Math.Round((intervalEnd - intervalBegin) / NumberOfPartitions, 1);

            var a = (intervalEnd - intervalBegin) / 2;
            var b = (intervalEnd + intervalBegin) / 2;

            double integralValue = 0;

            for (int i = 0; i < NumberOfPartitions; i++)
            {
                integralValue += _c[i] * f(a * _x[i] + b);
            }

            integralValue *= a;

            return integralValue;
        }
    }
}

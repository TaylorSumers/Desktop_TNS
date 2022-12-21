using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCalulationLibrary
{
    public class Calculation
    {
        public static double Main(double S, double Sb, double k, double S2, double S3)
        {
            double R0 = Radius(S); //радиус района обслуживания
            Console.WriteLine(String.Format("R0={0}", R0));
            double R = Radius(Sb); //Радиус покрытия БС
            Console.WriteLine(String.Format("R={0}", R));
            double L = k * Math.Pow(R0 / R, 2); //количество сот
            Console.WriteLine(String.Format("L={0}", L));

            double D1 = R * 2; //диаметр основной бс
            Console.WriteLine($"D1={D1}");
            double D2 = Radius(S2) * 2; //диаметры еще двух бс
            Console.WriteLine("D2=" + D2);
            double D3 = Radius(S3) * 2;
            Console.WriteLine("D3=" + D3);
            double[] StationsDiameter = { D1, D2, D3 };
            Array.Sort(StationsDiameter);
            Array.Reverse(StationsDiameter);
            foreach (var s in StationsDiameter)
            {
                Console.WriteLine(s);
            }

            double C = Math.Sqrt(Math.Pow(StationsDiameter[0], 5)) + Math.Sqrt(Math.Pow(StationsDiameter[1], 3)) + Math.Sqrt(Math.Sqrt(StationsDiameter[2])); //количество БС в одном кластере
            Console.WriteLine("C=" + C);
            double n = L/C; //итоговое количество БС

            return n;


        }

        static double Radius(double s) //метод вычисления радиуса по площади
        {
            double R = Math.Sqrt(s/Math.PI);
            return R;
        }
    }
}

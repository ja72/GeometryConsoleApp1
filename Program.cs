using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace JA
{
    using System;
    using JA.Sng;

    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Define Three Random Points.");
            Point3 PA = new Point3(Geometry.RandomVector4(-1, 1));
            Point3 PB = new Point3(Geometry.RandomVector4(-1, 1));
            Point3 PC = new Point3(Geometry.RandomVector4(-1, 1));

            Console.WriteLine($"PA = {PA}, {PA.Center}");
            Console.WriteLine($"PB = {PB}, {PB.Center}");
            Console.WriteLine($"PC = {PC}, {PC.Center}");

            Console.WriteLine("A Plane Through the 3 Points.");

            Plane3 WABC = Plane3.FromThreePoints(PA, PB, PC);

            Console.WriteLine($"WABC = {WABC}, {WABC.Center.Center}");

            Console.WriteLine("Check for Plane*Point incidence");

            Console.WriteLine($"WABC * PA = {WABC * PA}");
            Console.WriteLine($"WABC * PB = {WABC * PB}");
            Console.WriteLine($"WABC * PC = {WABC * PC}");

            Console.WriteLine();

            Plane3 WA = new Plane3(Geometry.RandomVector4(-1, 1));
            Plane3 WB = new Plane3(Geometry.RandomVector4(-1, 1));
            Plane3 WC = new Plane3(Geometry.RandomVector4(-1, 1));

            Console.WriteLine("Define Three Random Planes.");
            Console.WriteLine($"WA = {WA}, {WA.Center.Center}");
            Console.WriteLine($"WB = {WB}, {WB.Center.Center}");
            Console.WriteLine($"WC = {WC}, {WC.Center.Center}");

            Console.WriteLine("A Point From the 3 Planes.");
            
            Point3 PABC = Point3.FromThreePlanes(WA, WB, WC);

            Console.WriteLine($"PABC = {PABC}, {PABC.Center}");

            Console.WriteLine("Check for Plane*Point incidence");
            
            Console.WriteLine($"PABC * WA = {PABC * WA}");
            Console.WriteLine($"PABC * WB = {PABC * WB}");
            Console.WriteLine($"PABC * WC = {PABC * WC}");
        }
    }
}

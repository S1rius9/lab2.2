using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;

namespace lab2_2 {
    public interface DLL_Interface {
        double CalcFunction(double x);
        string CalcFunctionName();
    }

    public class Lib_DLL1: DLL_Interface {
        [DllImport("Lib.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);
        [DllImport("Lib.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();
        public double CalcFunction(double x) {
            try {
                return TheFunc(x);
            }
            catch {
                throw new Exception ("Ошибка при построении графика.");
            }
        }

        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    public class Lib_DLL2: DLL_Interface {

        [DllImport ("Lib2-2-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);

        [DllImport ("Lib2-2-1.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();

        public double CalcFunction(double x) {
            try {
                return TheFunc(x);

            }
            catch {
                throw new Exception("Ошибка при построении графика.");
            }
        }

        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    public class Lib_DLL3: DLL_Interface {

        [DllImport("Lib2-2-2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);

        [DllImport("Lib2-2-2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();

        public double CalcFunction(double x) {
            try {
                return TheFunc(x);
            }
            catch {
                throw new Exception ("Ошибка при построении графика.");
            }
        }

        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    public class Lib_DLL4: DLL_Interface {
        [DllImport("Lib2-2-3-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);

        [DllImport("Lib2-2-3-1.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();

        public double CalcFunction(double x) {
            try {
                return TheFunc(x);
            }
            catch {
                throw new Exception ("Ошибка при построении графика.");
            }
        }
        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    public class Lib_DLL5: DLL_Interface {
        [DllImport("Lib2-2-3-2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);

        [DllImport("Lib2-2-3-2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();

        public double CalcFunction(double x) {
            try {
                return TheFunc(x);
            }
            catch {
                throw new Exception ("Ошибка при построении графика.");
            }
        }
        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    public class Lib_DLL6: DLL_Interface {
        [DllImport("Lib2-2-3.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double TheFunc(double x);

        [DllImport("Lib2-2-3.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr FuncName();

        public double CalcFunction(double x) {
            try {
                return TheFunc(x);
            }
            catch {
                throw new Exception ("Ошибка при построении графика.");
            }
        }
        public string CalcFunctionName() {
            try {
                return Marshal.PtrToStringAnsi(FuncName());
            }
            catch {
                return "..";
            }
        }
    }

    class Program {

        public static void GetGraph(DLL_Interface dll) {
            const double Step = 0.5;
            const double MinX = 0;
            const double MaxX = 10;
            const string Img = "Print.png";
            var xList = new List<double> ();
            var yList = new List<double> ();
            for (var xx = MinX; xx <= MaxX; xx += Step) {
                var res = dll.CalcFunction(xx);
                xList.Add(xx);
                yList.Add(res);
            }

            using (Bitmap bmp = new Bitmap (1200, 1200)) {
                using (Graphics g = Graphics.FromImage(bmp)) {                    
                    g.Clear(Color.White);

                    Font font = new Font ("Ubuntu", 20);
                    SolidBrush Brush = new SolidBrush (Color.Black);
                    Pen blackPen = new Pen (Color.Black, 3);
                    Pen redPen = new Pen (Color.Red, 3);
                    
                    int centre = bmp.Width / 2;
                    
                    g.TranslateTransform(centre, centre);
                    g.DrawString(dll.CalcFunctionName(), font, Brush, new PointF (50.0f, 50.0f));
                    g.ScaleTransform(1, -1);
                    g.DrawLine(blackPen, 0, centre, 0, -centre);
                    g.DrawLine(blackPen, -centre, 0, centre, 0);
                    
                    var PointList = new PointF [yList.Count];
                    
                    for (int i = 0; i < yList.Count; i++) {
                        var p = new PointF((float) xList [i] * 5, (float) yList [i] * 5);
                        PointList[i] = p;
                    }
                    g.DrawLines(redPen, PointList);
                }
                bmp.Save("E:\\" + Img, System.Drawing.Imaging.ImageFormat.Png);                
            }
            Console.WriteLine ("\nГрафик построен и сохранён в диск E.\n\n");
        }

        static void Main() {
            List <DLL_Interface> dllList = new List <DLL_Interface> {
                new Lib_DLL1(),
                new Lib_DLL2(),
                new Lib_DLL3(),
                new Lib_DLL4(),
                new Lib_DLL5(),
                new Lib_DLL6()
            };
            List <int> IsFunc = new List <int> ();
            for (int i =0; i< dllList.Count; i++) {
                if (dllList[i].CalcFunctionName() != "..") {
                    IsFunc.Add(i);
                }
            }
            Console.WriteLine ("Функции, доступные в динамических библиотеках:\n");
            if (IsFunc.Count == 0) {
                Console.WriteLine ("В библиотеках нет функций.");
            }
            for (int j = 0; j < IsFunc.Count; j++) {
                Console.WriteLine ($"{IsFunc [j] + 1}. {dllList[IsFunc[j]].CalcFunctionName()}\n");
            }
            try {
                Console.WriteLine ("\nВаш выбор: ");
                int z = Convert.ToInt32(Console.ReadLine());
                GetGraph(dllList[z - 1]);
            }
            catch {
                Console.WriteLine ("\nОшибка ввода данных. Программа завершает свою работу.\n");
            }
        }
    }
}
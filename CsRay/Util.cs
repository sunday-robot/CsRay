﻿namespace CsRay
{
    public static class Util
    {
        /// <returns>原点を中心とする半径1のXY平面上の円の中のランダムな位置</returns>
        public static Vec3 RandomInUnitDisk()
        {
            while (true)
            {
                var p = new Vec3(2 * Random.Shared.NextDouble() - 1, 2 * Random.Shared.NextDouble() - 1, 0);
                if (p.SquaredLength < 1)
                    return p;
            }
        }

        /// <returns>原点を中心とする半径1の球内のランダムな座標</returns>
        public static Vec3 RandomInUnitSphere()
        {
            while (true)
            {
                var p = 2 * new Vec3(Random.Shared.NextDouble(), Random.Shared.NextDouble(), Random.Shared.NextDouble()) - new Vec3(1, 1, 1);
                if (p.SquaredLength < 1)
                    return p;
            }
        }

        public static double NextDouble(double min, double max) => min + Random.Shared.NextDouble() * (max - min);


        /// <summary>
        /// BMP形式のファイルに保存する
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="pixels">画素</param>
        /// <param name="filePath">ファイルパス</param>

        public static void SaveAsBmp(int width, int height, Rgb[] pixels, string filePath)
        {
            var data = new byte[width * height * 3];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var p = pixels[x + y * width];
                    var r = (byte)(Math.Min(Math.Sqrt(p.R), 1) * 255 + 0.5);
                    var g = (byte)(Math.Min(Math.Sqrt(p.G), 1) * 255 + 0.5);
                    var b = (byte)(Math.Min(Math.Sqrt(p.B), 1) * 255 + 0.5);
                    data[(y * width + x) * 3] = b;
                    data[(y * width + x) * 3 + 1] = g;
                    data[(y * width + x) * 3 + 2] = r;
                }
            }
            Bmp.Save(filePath, data, width, height);
        }

        public static Rgb RandomRgb(double min, double max)
        {
            var r = NextDouble(min, max);
            var g = NextDouble(min, max);
            var b = NextDouble(min, max);
            return new Rgb(r, g, b);
        }

        public static Rgb RandomRgb() => RandomRgb(0, 1);

        public static Rgb RandomSaturatedRgb(double s, double v)
        {
            var min = (1 - s) * v;
            var range = v - min;
            var h6 = Random.Shared.NextDouble() * 6;
            if (h6 < 1)
            {
                var g = h6 * range + min;
                return new Rgb(v, g, min);
            }
            if (h6 < 2)
            {
                var r = (2 - h6) * range + min;
                return new Rgb(r, v, min);
            }
            if (h6 < 3)
            {
                var b = (h6 - 2) * range + min;
                return new Rgb(min, v, b);
            }
            if (h6 < 4)
            {
                var g = (4 - h6) * range + min;
                return new Rgb(min, g, v);
            }
            if (h6 < 5)
            {
                var r = (h6 - 4) * range + min;
                return new Rgb(r, min, v);
            }
            {
                var b = (6 - h6) * range + min;
                return new Rgb(v, min, b);
            }
        }
    }
}

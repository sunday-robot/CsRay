﻿namespace CsRay
{
    public sealed class Perlin
    {
        //==============================================================================================
        // Originally written in 2016 by Peter Shirley <ptrshrl@gmail.com>
        //
        // To the extent possible under law, the author(s) have dedicated all copyright and related and
        // neighboring rights to this software to the public domain worldwide. This software is
        // distributed without any warranty.
        //
        // You should have received a copy (see file COPYING.txt) of the CC0 Public Domain Dedication
        // along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
        //==============================================================================================

        const int _pointCount = 256;

        readonly Vec3[] _ranvec;
        readonly int[] _permX;
        readonly int[] _permY;
        readonly int[] _permZ;

        public Perlin()
        {
            _ranvec = new Vec3[_pointCount];
            for (int i = 0; i < _pointCount; ++i)
            {
                _ranvec[i] = (new Vec3(
                    Random.Shared.NextDouble() * 2 - 1,
                    Random.Shared.NextDouble() * 2 - 1,
                    Random.Shared.NextDouble() * 2 - 1)).Unit;
            }

            _permX = PerlinGeneratePerm();
            _permY = PerlinGeneratePerm();
            _permZ = PerlinGeneratePerm();
        }

        double Noise(Vec3 p)
        {
            var u = p.X - Math.Floor(p.X);
            var v = p.Y - Math.Floor(p.Y);
            var w = p.Z - Math.Floor(p.Z);
            var i = (int)(Math.Floor(p.X));
            var j = (int)(Math.Floor(p.Y));
            var k = (int)(Math.Floor(p.Z));
            var c = new Vec3[2, 2, 2];

            for (int di = 0; di < 2; di++)
                for (int dj = 0; dj < 2; dj++)
                    for (int dk = 0; dk < 2; dk++)
                        c[di, dj, dk] = _ranvec[
                            _permX[(i + di) & 255] ^
                            _permY[(j + dj) & 255] ^
                            _permZ[(k + dk) & 255]
                        ];

            return PerlinInterp(c, u, v, w);
        }

        public double Turb(Vec3 p, int depth = 7)
        {
            var accum = 0.0;
            var temp_p = p;
            var weight = 1.0;

            for (int i = 0; i < depth; i++)
            {
                accum += weight * Noise(temp_p);
                weight *= 0.5;
                temp_p *= 2;
            }

            return Math.Abs(accum);
        }

        static int[] PerlinGeneratePerm()
        {
            var p = new int[_pointCount];

            for (int i = 0; i < _pointCount; i++)
                p[i] = i;

            Permute(p, _pointCount);

            return p;
        }

        static void Permute(int[] p, int n)
        {
            for (int i = n - 1; i > 0; i--)
            {
                int target = Random.Shared.Next() % i;
                (p[target], p[i]) = (p[i], p[target]);
            }
        }

        static double PerlinInterp(Vec3[,,] c, double u, double v, double w)
        {
            var uu = u * u * (3 - 2 * u);
            var vv = v * v * (3 - 2 * v);
            var ww = w * w * (3 - 2 * w);
            var accum = 0.0;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                    {
                        var weightY = new Vec3(u - i, v - j, w - k);
                        accum += (i * uu + (1 - i) * (1 - uu)) *
                            (j * vv + (1 - j) * (1 - vv)) *
                            (k * ww + (1 - k) * (1 - ww)) * c[i, j, k].Dot(weightY);
                    }

            return accum;
        }
    }
}

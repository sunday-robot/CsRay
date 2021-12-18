﻿using CsRay.Hittables;
using CsRay.Materials;
using System.Collections.Generic;

namespace CsRay.Main
{
    public partial class Program
    {
        /// <summary>
        /// 世界の中心に、半径1のグレイの球のみがあるシーンを生成する。
        /// </summary>
        static (List<Hittable>, Camera, Rgb) CreateSimpleScene2()
        {
            var hittables = new List<Hittable>
            {
                new Sphere(new Vec3(-2, 0, 0.0), 1.0, new Lambertian(new Rgb(1, 0, 0))),
                new Sphere(new Vec3(0, 0, 0.0), 1.0, new Lambertian(new Rgb(0, 1, 0))),
                new Sphere(new Vec3(2, 0, 0.0), 1.0, new Lambertian(new Rgb(0, 0, 1)))
            };

            Camera camera;
            {
                var lookFrom = new Vec3(3, 2, 10);
                var lookAt = new Vec3(0, 0, 0);
                var vFov = 40.0;
                var aperture = 0.1;
                var distanceToFocus = (lookAt - lookFrom).Length;
                var time0 = 0.0;
                var time1 = 1.0;
                camera = Camera.CreateCamera(lookFrom, lookAt, new Vec3(0.0, 1.0, 0.0), vFov, 16.0 / 9, aperture, distanceToFocus, time0, time1);
            }

            return (hittables, camera, null);
        }
    }
}
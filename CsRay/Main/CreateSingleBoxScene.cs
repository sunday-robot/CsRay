﻿using CsRay.Hittables;
using CsRay.Materials;
using System.Collections.Generic;

namespace CsRay.Main
{
    public partial class Program
    {
        static (List<Hittable>, Camera, Rgb) CreateSingleBoxScene()
        {
            var objects = new List<Hittable>();
            {
                var red = new Lambertian(0.8, 0.2, 0.2);
                var blue = new Lambertian(0.2, 0.2, 0.8);
                var box = new Box(new Vec3(-1, -1, -1), new Vec3(1, 1, 1), red);
                objects.Add(box);
#if true
                var sphere1 = new Sphere(new Vec3(-2, 0, 0), 1, blue);
                objects.Add(sphere1);

                var sphere2 = new Sphere(new Vec3(2, 0, 0), 1, blue);
                objects.Add(sphere2);
#endif
            }

            Camera camera;
            {
                var lookFrom = new Vec3(5.0, 2.0, 13.0);
                var lookAt = new Vec3(0.0, 0.0, 0.0);
                var vFov = 20.0;
                var aperture = 0.1;
                var distanceToFocus = (lookAt - lookFrom).Length;
                var time0 = 0.0;
                var time1 = 1.0;
                camera = Camera.CreateCamera(lookFrom, lookAt, new Vec3(0.0, 1.0, 0.0), vFov, 16.0 / 9, aperture, distanceToFocus, time0, time1);
            }

            return (objects, camera, null);
        }
    }
}
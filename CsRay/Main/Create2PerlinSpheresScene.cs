﻿using CsRay.Hittables;
using CsRay.Materials;
using CsRay.Textures;

namespace CsRay.Main
{
    public partial class Program
    {
        static (List<Hittable>, Camera, Rgb?) Create2PerlinSpheresScene()
        {
            var hittables = new List<Hittable>();
            {
                var pertext = new NoiseTexture(4);
                hittables.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(pertext)));
                hittables.Add(new Sphere(new Vec3(0, 2, 0), 2, new Lambertian(pertext)));
            }

            Camera camera;
            {
                var lookFrom = new Vec3(13, 2, 3);
                var lookAt = new Vec3(0, 0, 0);
                var vFov = 20;
                var aperture = 0.1;
                var exposureTime = 1;
                camera = Camera.CreateCamera(lookFrom, lookAt, new Vec3(0, 1, 0), vFov, 16.0 / 9, aperture, exposureTime);
            }

            return (hittables, camera, null);
        }
    }
}

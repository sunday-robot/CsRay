﻿using CsRay.Hittables;
using CsRay.Materials;

namespace CsRay.Main
{
    public partial class Program
    {
        /// <summary>
        /// 地面となる非常に大きな球と、三つの大きな球と、多数の小さな球を生成する
        /// </summary>
        /// <returns></returns>
        static (List<Hittable>, Camera, Rgb?) CreateScene1()
        {
            var hittables = new List<Hittable>();
            {
                // 多数の小さな級
                for (var a = -11; a < 10; a++)
                {
                    for (var b = -11; b < 10; b++)
                    {
                        var center = new Vec3(a + 0.9 * Rand(), 0.2, b + 0.9 * Rand());
                        if ((center - (new Vec3(4, 0.2, 0))).Length <= 0.9)
                            continue;
                        Material material;
                        var chooseMat = Rand();
                        if (chooseMat < 0.8)
                        {
                            // ざらついたプラスチックのような素材
                            material = new Lambertian(new Rgb(Rand2(), Rand2(), Rand2()));
                            var velocity = new Vec3(0, Rand() * 0.5, 0);
                            hittables.Add(new MovingSphere(center, 0.2, material, velocity));
                        }
                        else if (chooseMat < 0.95)
                        {
                            // 金属
                            material = new Metal(new Rgb(0.5 * (1 + Rand()), 0.5 * (1 + Rand()), 0.5 * (1 + Rand())),
                                    0.5 * Rand());
                            hittables.Add(new Sphere(center, 0.2, material));
                        }
                        else
                        {
                            // ガラス
                            material = new Dielectric(1.5);
                            hittables.Add(new Sphere(center, 0.2, material));
                        }
                    }
                }

                // 三つの大きな球
                hittables.Add(new Sphere(new Vec3(0, 1, 0), 1, new Dielectric(1.5)));
                hittables.Add(new Sphere(new Vec3(-4, 1, 0), 1, new Lambertian(new Rgb(0.4, 0.2, 0.1))));
                hittables.Add(new Sphere(new Vec3(4, 1, 0), 1, new Metal(new Rgb(0.7, 0.6, 0.5), 0)));

                // 地面となる非常に大きな球
                hittables.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(new Rgb(0.5, 0.5, 0.5))));
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

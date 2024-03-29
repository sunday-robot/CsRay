﻿namespace CsRay
{
    public abstract class Hittable
    {
        public abstract HitRecord? Hit(Ray ray, double tMin, double tMax);

        /// <summary>
        /// レンダリング前のBVHツリー構築時に一度呼ばれるだけなので、速度についてあまり考慮する必要はない。
        /// </summary>
        /// <param name="exposureTime"></param>
        /// <returns></returns>
        public abstract Aabb BoundingBox(double exposureTime);
    }
}

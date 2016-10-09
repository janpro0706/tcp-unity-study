using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class EasingFunction
    {
        public static IEnumerator<float> Sine(float begin, float end, long millis)
        {
            float t = 0, T = (millis / 1000.0f);
            float y = begin;
            float v = 0;
            
            while (t < T)
            {
                y = (float)(-Math.Sin(2 * Math.PI * t / T) + (end - begin) * t / T  + begin);
                t += Time.deltaTime;

                yield return y;
            }
        }
    }
}

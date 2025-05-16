using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity
{
    public sealed class Discolorator
    {
        private Color color = Color.red;

        public Color Color
        {
            get => color;
        }

        private float deltaH = 0f;

        private float deltaS = 0f;

        private float deltaV = 0f;

        public Discolorator(ColorPalette color) : this(color.ToColor())
        {

        }

        public Discolorator(Color color)
        {
            this.color = color;

            Reset();
        }

        public void Reset()
        {
            moveHSVRoutile = MoveHSVRoutile(color);
        }

        public Color MoveHSV(float deltaH, float deltaS, float deltaV)
        {
            this.deltaH = deltaH;

            this.deltaS = deltaS;

            this.deltaV = deltaV;

            moveHSVRoutile.MoveNext();

            return moveHSVRoutile.Current;
        }

        private IEnumerator<Color> moveHSVRoutile;

        private IEnumerator<Color> MoveHSVRoutile(Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);

            var linearH = new Linear(H, 1f, LinearMode.Repeat);

            var linearS = new Linear(S, 1f, LinearMode.PingPong);

            var linearV = new Linear(V, 1f, LinearMode.PingPong);

            while (true)
            {
                yield return Color.HSVToRGB(H, S, V);

                H = linearH.Interval(deltaH, LinearMode.Repeat);

                S = linearS.Interval(deltaS, LinearMode.PingPong);

                V = linearV.Interval(deltaV, LinearMode.Repeat);
            }
        }
    }
}
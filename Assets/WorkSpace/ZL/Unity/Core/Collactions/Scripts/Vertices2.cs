using System;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ZL.CS;

namespace ZL.Unity.Collections
{
    public static partial class Vertices2
    {
        private static readonly Comparer comparer = new Comparer();

        public static Vector2[] Dots(float radius, int column, int row)
        {
            var vertices = new Vector2[column * row];

            float interval = radius * Mathf.Sqrt(2f);

            float y = interval * (column - 1) * 0.5f;

            float xStart = interval * (row - 1) * -0.5f;

            int index = 0;

            for (int j = 0; j < column; ++j)
            {
                float x = xStart;

                for (int i = 0; i < row; ++i)
                {
                    vertices[index++] = new Vector2(x, y);

                    x += interval;
                }

                y -= interval;
            }

            return vertices;
        }

        public static Vector2[] Map(float radius, char[][] map)
        {
            int column = map.Length;

            int row = map[0].Length;

            var vertices = new List<Vector2>(column * row);

            float interval = radius * Mathf.Sqrt(2f);

            float y = interval * (column - 1) * 0.5f;

            float xStart = interval * (row - 1) * -0.5f;

            for (int j = 0; j < column; ++j)
            {
                float x = xStart;

                for (int i = 0; i < row; ++i)
                {
                    if (map[j][i] != ' ')
                    {
                        vertices.Add(new Vector2(x, y));
                    }

                    x += interval;
                }

                y -= interval;
            }

            return vertices.ToArray();
        }

        public static Vector2[] Regular(float radius, int vertexCount, float rotation = 0f)
        {
            var vertices = new Vector2[vertexCount];

            float angleStep = MathFEx.PI2 / vertexCount;

            float radian = (90f + rotation) * Mathf.Deg2Rad;

            for (int i = 0; i < vertexCount; ++i)
            {
                float theta = i * angleStep + radian;

                float x = radius * Mathf.Cos(theta);

                float y = radius * Mathf.Sin(theta);

                vertices[i] = new Vector2(x, y).Round(2);
            }

            Array.Sort(vertices, comparer);

            return vertices;
        }

        public static Vector2[] Union(params Vector2[][] set)
        {
            var union = new SortedSet<Vector2>();

            foreach (var vertices in set)
            {
                foreach (var vertex in vertices)
                {
                    union.Add(vertex);
                }
            }

            return union.ToArray();
        }

        private sealed class Comparer : IComparer<Vector2>
        {
            public int Compare(Vector2 x, Vector2 y)
            {
                int comparison = y.y.CompareTo(x.y);

                if (comparison != 0)
                {
                    return comparison;
                }

                return x.x.CompareTo(y.x);
            }
        }
    }
}
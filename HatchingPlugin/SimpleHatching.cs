using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Base.Shapes;

namespace HatchingPlugin
{
    /// <summary>
    /// hatching demonstration source code provided "as is" without warranty
    /// </summary>
    class SimpleHatching
    {
        private static List<List<Vector3>> GetPolygons(ActionCommandList commands)
        {
            List<List<Vector3>> polygons = new List<List<Vector3>>();
            if (commands == null) return polygons;

            for (int i = 0; i < commands.Count; i++)
            {
                ActionCommand cmd = commands[i];
                if (!cmd.IsClosed) continue;

                List<Vector3> points = cmd.GetPolyline(0.001);
                polygons.Add(points);
            }
            return polygons;
        }

        private static bool Intersects(double y, Vector3 p1, Vector3 p2) 
        { 
            if (p1.y < p2.y) return y > p1.y && y < p2.y;
            return y > p2.y && y < p1.y;
        }

        private static double GetIntersectionPoint(double y, Vector3 p1, Vector3 p2)
        {
            if (Math.Abs(p1.x - p2.x) < 0.00001) return p1.x;
            double k = (p1.y - p2.y) / (p1.x - p2.x);
            return (y - (p2.y - k * p2.x)) / k;
        }

        private static List<double> GetIntersections(List<List<Vector3>> polygons, double y)
        {
            List<double> intersections = new List<double>();
            for (int i = 0; i < polygons.Count; i++) {
                List<Vector3> polygon = polygons[i];
                for (int j = 1; j < polygon.Count; j++)
                { 
                    if (Intersects(y, polygon[j-1], polygon[j]))
                        intersections.Add(GetIntersectionPoint(y, polygon[j-1], polygon[j])); 
                    
                }
                if (polygon.Count > 2 && Intersects(y, polygon[0], polygon[polygon.Count - 1]))
                    intersections.Add(GetIntersectionPoint(y, polygon[0], polygon[polygon.Count - 1])); 
            }
            return intersections;
        }

        public static ActionCommandList Hatch(ActionCommandList commands, Cube size, double step)
        {
            ActionCommandList result = new HatchingActionCommands();
            List<List<Vector3>> polygons = GetPolygons(commands);
            double y0 = size.min.y;
            double y1 = size.max.y;

            for (double y = y0; y <= y1; y += step)
            {
                if (State.is_cancel) break;

                List<double> intersections = GetIntersections(polygons, y);
                intersections.Sort();

                int index = 0;
                while (true) {
                    int i1 = index; int i2 = index+1;
                    if (i2 >= intersections.Count) break;

                    result.Add(new LineCommand(new Vector3(intersections[i1], y, 0), new Vector3(intersections[i2], y, 0)));
                    index += 2;
                }
                intersections.Clear();
            }
            polygons.Clear();
            return result;
        }
    }
}

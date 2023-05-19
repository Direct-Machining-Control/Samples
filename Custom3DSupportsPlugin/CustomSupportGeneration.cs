using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using Base.Shapes;
using Core;
using Core.Commands;

namespace Custom3DSupportsPlugin
{
    public class CustomSupportGeneration : ISupportGenerator
    {
        public override string GetName()
        {
            return "My support";
        }

        SupportGenerationGUI gui;

        ParamSD spacing = new ParamSD("spacing", "Spacing (mm)", 1);

        public CustomSupportGeneration()
        {
            Add(spacing);
        }

        protected override MultiParameter CloneObj() { return CloneISupportGenerator(); }

        public override ISupportGenerator CloneISupportGenerator()
        {
            CustomSupportGeneration ch = new CustomSupportGeneration();
            ch.AssignValuesFrom(parameters);
            return ch;
        }

        public override System.Windows.Forms.UserControl GetGUI(Object3D icommand)
        {
            if (gui == null) gui = new SupportGenerationGUI();
            gui.Set(this);
            return gui;
        }

        public override bool Compile()
        {
            if (!ICommand.ParseAll(parameters)) return false;

            if (spacing.number < Settings.Epsilon) return Functions.Error("'" + spacing.title + "' needs to be larger than zero");
            return true;
        }

        public override Base.Shapes.ActionCommand GenerateSupport(Object3D icommand, CADImport._3D.STLActionCommand command)
        {
            // support generation
            ActionCommandList beams = new ActionCommandList();
            _3DSupport.GenerateVerticalBeams(command, (float)spacing.number, beams);
            return beams;
        }
    }






    public class _3DSupport
    {
        public static bool GenerateVerticalBeams(CADImport._3D.STLActionCommand cmd, float beam_radius, ActionCommandList beams)
        {
            Base.Triangle[] tri = cmd.Triangles;
            if (tri == null) return Base.Functions.Error("No structure found");

            Cube size = cmd.GetCube(true);
            float w = (float)size.SizeX;
            float h = (float)size.SizeY;

            float dx = beam_radius;
            float dy = beam_radius;

            int xn = (int)Geometry.RoundInt(w / dx);
            int yn = (int)Geometry.RoundInt(h / dy);

            for (int iy = 0; iy < yn; iy++)
                for (int ix = 0; ix < xn; ix++)
                {

                    float x = (float)size.min.x + dx * ix;
                    float y = (float)size.min.y + dy * iy;
                    List<int> beam_intersections = GetBeamIntersections(x, y, tri);
                    float min_z = 99999.0f;

                    for (int i = 0; i < beam_intersections.Count; i++)
                    {
                        float cz = tri[beam_intersections[i]].p1.z;
                        if (min_z > cz) min_z = cz;
                    }

                    if (min_z < 99999.0f)
                    {
                        beams.Add(new LineCommand(new Vector3(x, y, size.min.z), new Vector3(x, y, min_z)));
                    }

                }

            int n = beams.Count;
            for (float z = cmd.GetSliceStart(); z <= cmd.GetSliceEnd(); z += cmd.GetSliceDZ()) {

                for (int i = 0; i < n; i++) {
                    Vector3 p1 = beams[i].GetStartPosition();
                    Vector3 p2 = beams[i].GetEndPosition();
                    double min = Math.Min(p1.z, p2.z);
                    double max = Math.Max(p1.z, p2.z);

                    if (z < min || z > max) continue; // check if beam is intersected at Z
                    beams.Add(new Base.Shapes.CircleCommand(new Vector3(p1.x, p1.y, z), beam_radius / 6));
                }
            
            }


            return true;
        }


        private static List<int> GetBeamIntersections(float x, float y, Base.Triangle[] tri)
        {
            List<int> beam_intersections = new List<int>(2);
            Vector3f point = new Vector3f(x, y, 0);

            for (int i = 0; i < tri.Length; i++)
            {
                if (tri[i].normal.z > -0.5) continue;
                if (tri[i].PointInTriangle(point))
                    beam_intersections.Add(i);
            }

            return beam_intersections;
        }
    }
}

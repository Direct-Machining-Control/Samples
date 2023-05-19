using Base.Shapes;
using Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base;
using CADImport._3D;

namespace PointImport
{
    class Import3DFile : Core.Commands.IFile3DImporter
    {
        public static void AddImporter()
        {
            Core.Commands.FileImport.FileImporters.Add(new Import3DFile());
        }

        CADImport._3D.STLActionCommand cmd = null;
        Cube size = Cube.None;

        public string FilterFull => string.Format("XML files ({0})|{0}", this.Filter);
        public string Filter => "*.xml";

        public ActionCommandList Commands { get; private set; }

        public List<CADLayer> Layers { get; private set; }

        public STLActionCommand GetCommand() { return cmd; }

        public Cube GetSize() { return size; }

        public bool ImportFile(string fileName)
        {
            var triangles = CADImport._3D._3DSupport.MakeCone(Vector3f.UnitZ, Vector3f.Zero, 0, 1, false);
            size = STLReader.UpdateSize(triangles);
            cmd = new STLActionCommand(triangles, null, size);
            return true;
        }

        public void SetICommand(Object3D cmd)
        {
            cmd.CustomTitle = "Cone";
        }
    }
}

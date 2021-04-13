using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using Converter.Models;

namespace Converter.Readers
{
    public class ObjReader
    {
        public List<Vector3> Vertexes { get; private set; }
        public List<Vector3> Normals { get; private set; }
        public List<List<Vertex>> Faces { get; private set; }
        public List<Triangle> Triangles { get; private set; }

        public List<Triangle> Read(string path)
        {
            var data = File.ReadAllLines(path);
            Vertexes = new List<Vector3>();
            Normals = new List<Vector3>();
            Faces = new List<List<Vertex>>();
            Triangles = new List<Triangle>();

            foreach (var str in data)
            {
                var line = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (line.Length == 0)
                {
                    continue;
                }

                switch (line[0])
                {
                    case "v":
                        ProcessVertex(line);
                        break;
                    case "vm":
                        ProcessNormal(line);
                        break;
                    case "f":
                        ProcessFace(line);
                        break;
                }

                foreach (var face in Faces)
                {
                    var triangle = new Triangle(
                        Vertexes[face[0].V],
                        Vertexes[face[1].V],
                        Vertexes[face[2].V]);
                    Triangles.Add(triangle);
                }
            }

            return Triangles;

            void ProcessVertex(string[] lines)
            {
                Vertexes.Add(new Vector3(
                    float.Parse(lines[1], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(lines[2], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(lines[3], CultureInfo.InvariantCulture.NumberFormat)));
            }

            void ProcessNormal(string[] lines)
            {
                Normals.Add(new Vector3(
                    float.Parse(lines[1], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(lines[2], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(lines[3], CultureInfo.InvariantCulture.NumberFormat)));
            }

            void ProcessFace(string[] lines)
            {
                var temp = new List<Vertex>();
                foreach (var line in lines)
                {
                    var vertices = line.Split('/');
                    var vertex = new Vertex()
                    {
                        V = int.Parse(vertices[0], CultureInfo.InvariantCulture.NumberFormat) - 1,
                        Vm = int.Parse(vertices[2], CultureInfo.InvariantCulture.NumberFormat) - 1
                    };
                    temp.Add(vertex);
                }
                Faces.Add(temp);
            }
        }
    }
}

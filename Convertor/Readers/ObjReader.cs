using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Converter.ImageBase;
using Converter.ImageConcrete;
using Converter.Interfaces;
using Converter.Models;

namespace Converter.Readers
{
    public class ObjReader : IImageReader
    {
        private readonly IRenderer _renderer;
        private readonly IVectorConverter _converter;

        public List<Vector3> Vertexes { get; private set; }
        public List<Vector3> Normals { get; private set; }
        public List<List<Vertex>> Faces { get; private set; }
        public List<Triangle> Triangles { get; private set; }

        public ObjReader(IRenderer renderer, IVectorConverter converter)
        {
            _renderer = renderer;
            _converter = converter;
        }

        public Image Read(string path)
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
                    case "vn":
                        ProcessNormal(line);
                        break;
                    case "f":
                        ProcessFace(line);
                        break;
                }
            }

            foreach (var face in Faces)
            {
                var triangle = new Triangle(
                    Vertexes[face[0].V],
                    Vertexes[face[1].V],
                    Vertexes[face[2].V],
                    Normals[face[0].Vn],
                    Normals[face[1].Vn],
                    Normals[face[2].Vn]);
                Triangles.Add(triangle);
            }

            var vectorColors = _renderer.Render(Triangles);
            var colors = _converter.ConvertFromVectorToColors(vectorColors);
            var cool = new List<Color>();
            for (int i = 0; i < colors.GetLength(0); i++)
            {
                for (int j = 0; j < colors.GetLength(1); j++)
                {
                    if (colors[i, j].R != 255 && colors[i, j].G != 255 && colors[i, j].B != 255)
                    {
                        cool.Add(colors[i,j]);
                    }
                }
            }
            ImageObj result = new ImageObj()
            {
                Bitmap = colors,
                Header = new HeaderObj()
                {
                    BitsPerComponent = 32,
                    Height = colors.GetLength(0),
                    Width = colors.GetLength(1)
                },
                Path = path
            };

            return result;

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
                foreach (var line in lines.Skip(1).ToArray())
                {
                    var vertices = line.Split('/');
                    var vertex = new Vertex()
                    {
                        V = int.Parse(vertices[0], CultureInfo.InvariantCulture.NumberFormat) - 1,
                        Vn = int.Parse(vertices[2], CultureInfo.InvariantCulture.NumberFormat) - 1
                    };
                    temp.Add(vertex);
                }
                Faces.Add(temp);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stltry
{
    public class Model
    {
        public List<Triangle> Triangles { get; set; } = new List<Triangle>();
    }
    public class Triangle
    {
        public Triangle(NormalVector vector, List<Vertex> vertex)
        {
            Vector = vector;
            Vertex = vertex;
        }

        public NormalVector Vector { get; set; }
        public List<Vertex> Vertex { get; set; } = new List<Vertex>();
    }
    public class NormalVector
    {
        public NormalVector()
        {
        }

        public NormalVector(double dirX, double dirY, double dirZ)
        {
            DirX = dirX;
            DirY = dirY;
            DirZ = dirZ;
        }

        public double DirX { get; set; }
        public double DirY { get; set; }
        public double DirZ { get; set; }

    }
    public class Vertex
    {
        public Vertex(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

    }
    class Program
    {
        public static string make8CharBinary(string x)
        {
            string ret = x;
            while (ret.Length != 8)
                ret = '0' + ret;
            return ret;
        }
       
        public static double calculate3x3Determinant(Triangle tri)
        {
            return tri.Vertex[0].x * tri.Vertex[1].y * tri.Vertex[2].z + tri.Vertex[0].y * tri.Vertex[1].z * tri.Vertex[2].x
                + tri.Vertex[0].z * tri.Vertex[1].x * tri.Vertex[2].y - (tri.Vertex[0].z * tri.Vertex[1].y * tri.Vertex[2].x
                + tri.Vertex[0].x * tri.Vertex[1].z * tri.Vertex[2].y + tri.Vertex[0].y * tri.Vertex[1].x * tri.Vertex[2].z);
        }
      
        public static double Real32ToDouble(int x1, int x2, int x3, int x4)
        {
            if (x1 == 0 && x2 == 0 && x3 == 0 && x4 == 0)
                return 0;
            string y1 = make8CharBinary(Convert.ToString(x4, 2));
            string y2 = make8CharBinary(Convert.ToString(x3, 2));
            string y3 = make8CharBinary(Convert.ToString(x2, 2));
            string y4 = make8CharBinary(Convert.ToString(x1, 2));
            string IEE = y1 + y2 + y3 + y4;
            int exponent = Convert.ToInt32(IEE.Substring(1, 8), 2);
            double fraction = 1;
            for (int i = 1; i < 24; i++)
            {
                
                if (IEE[i + 8] == '0')
                    continue;
                else 
                    fraction += 1 * Math.Pow(2, -i);
              
            }
            if(IEE[0]=='0')
            {
                return Math.Pow(2, exponent - 127) * fraction;
            }
            else if(IEE[0]=='1')
            {

                return -1 * Math.Pow(2, exponent - 127) * fraction;
            }
            return 0;
        }
        static void Main(string[] args)
        {
            Model nowy = new Model();
            // Declare input File HERE
            string inputPath = @"F:\models\standing.stl";
            // Declare output File HERE
            string outputPath = @"F:\ASCII6.stl";
                string header = "";
                UInt32 trianglesCount = 0;
             
             using (FileStream fs = File.OpenRead(inputPath))
             {
                 var bajt=0;
               
                double dx, dy, dz;
                int x1, x2, x3, x4;
                for (int i = 0; i < 80; i++)
                {
                     bajt = fs.ReadByte();
                     header += Convert.ToChar(bajt);
                }
                
                    x1 = fs.ReadByte();
                    x2 = fs.ReadByte();
                    x3 = fs.ReadByte();
                    x4 = fs.ReadByte();
                    string bits = make8CharBinary(Convert.ToString(x4, 2)) + make8CharBinary(Convert.ToString(x3, 2))
                        + make8CharBinary(Convert.ToString(x2, 2)) + make8CharBinary(Convert.ToString(x1, 2));
                    trianglesCount = Convert.ToUInt32(bits, 2);

                for (UInt32 i = 0; i < trianglesCount; i++)
                {
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dx = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dy = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dz = Real32ToDouble(x1, x2, x3, x4);
                            NormalVector vector = new NormalVector(dx, dy, dz);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dx = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dy = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dz = Real32ToDouble(x1, x2, x3, x4);
                            Vertex v1 = new Vertex(dx, dy, dz);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dx = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dy = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dz = Real32ToDouble(x1, x2, x3, x4);
                            Vertex v2 = new Vertex(dx, dy, dz);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dx = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dy = Real32ToDouble(x1, x2, x3, x4);
                            x1 = fs.ReadByte();
                            x2 = fs.ReadByte();
                            x3 = fs.ReadByte();
                            x4 = fs.ReadByte();
                            dz = Real32ToDouble(x1, x2, x3, x4);
                            Vertex v3 = new Vertex(dx, dy, dz);
                            List<Vertex> nowa = new List<Vertex>();
                            nowa.Add(v1);
                            nowa.Add(v2);
                            nowa.Add(v3);
                            Triangle tri = new Triangle(vector, nowa);
                            nowy.Triangles.Add(tri);
                            fs.ReadByte();
                            fs.ReadByte();
                }
  
             }
            using (FileStream fs = File.Create(outputPath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("solid " + header);
                fs.Write(info, 0, info.Length);
                foreach (var tri in nowy.Triangles)
                {
                    byte[] facet = new UTF8Encoding(true).GetBytes(Environment.NewLine + "facet normal "
                        + tri.Vector.DirX.ToString() + " " + tri.Vector.DirY.ToString() + " " + tri.Vector.DirZ.ToString());
                    byte[] outerloop = new UTF8Encoding(true).GetBytes(Environment.NewLine + "outer loop");
                    byte[] vertex1 = new UTF8Encoding(true).GetBytes(Environment.NewLine + "vertex "
                                         + tri.Vertex[0].x.ToString() + " " + tri.Vertex[0].y.ToString() + " " + tri.Vertex[0].z.ToString());
                    byte[] vertex2 = new UTF8Encoding(true).GetBytes(Environment.NewLine + "vertex "
                                            + tri.Vertex[1].x.ToString() + " " + tri.Vertex[1].y.ToString() + " " + tri.Vertex[1].z.ToString());
                    byte[] vertex3 = new UTF8Encoding(true).GetBytes(Environment.NewLine + "vertex "
                                            + tri.Vertex[2].x.ToString() + " " + tri.Vertex[2].y.ToString() + " " + tri.Vertex[2].z.ToString());
                    byte[] endloop = new UTF8Encoding(true).GetBytes(Environment.NewLine + "endloop");
                    byte[] endfacet = new UTF8Encoding(true).GetBytes(Environment.NewLine + "endfacet");
                    fs.Write(facet);
                    fs.Write(outerloop);
                    fs.Write(vertex1);
                    fs.Write(vertex2);
                    fs.Write(vertex3);
                    fs.Write(endloop);
                    fs.Write(endfacet);

                }
                byte[] end = new UTF8Encoding(true).GetBytes(Environment.NewLine + "endsolid " + header);
                fs.Write(end);

            }
            double volume = 0;
            foreach(var tri in nowy.Triangles)
            {
                volume += calculate3x3Determinant(tri)/6;
            }

            Console.WriteLine("Volume = " + volume);
        }
    }
}

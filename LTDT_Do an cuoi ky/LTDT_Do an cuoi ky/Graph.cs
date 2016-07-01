using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LTDT_Do_an_cuoi_ky
{
    public class Graph
    {
        private const int maxX = 900;
        private const int maxY = 600;

        private int[,] _matrix;
        private List<Vertex> _nVertex;
        private List<Edge> _nEdge;

        public List<Vertex> NVertex
        {
            get { return _nVertex; }
            set { _nVertex = value; }
        }

        public List<Edge> NEdge
        {
            get { return _nEdge; }
            set { _nEdge = value; }
        }

        public int[,] Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        public Graph()
        {
            _nVertex = new List<Vertex>();
            _nEdge = new List<Edge>();
        }

        public void initLocationVertex()
        {
            NVertex.Clear();
            Random rd = new Random();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                NVertex.Add(new Vertex(new Point(rd.Next(50, maxX), rd.Next(50, maxY)), i));
                NVertex[i].VertexDegree = 0;
            }

        }

        public void initGraph()
        {
            NEdge.Clear();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = i; j < Matrix.GetLength(0); j++)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        NVertex[i].VertexDegree++;
                        NEdge.Add(new Edge(NVertex[i],NVertex[j],Matrix[i,j]));
                    }
                }
            }
        }

    }
}

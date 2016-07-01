using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LTDT_Do_an_cuoi_ky
{
    public class Vertex
    {
        private const int rectangleSize = 24;
        private Rectangle _vertexRectangle;
        private Point _vertexCenterPoint;
        private string _vertexName;
        private int _vertexLabel;
        private Brush _vertexBrush;
        private int _vertexIndex;
        private int _vertexDegree;

        private int _dijkstraLengthPath;
        public Point DrawDijkstraLengthPathPoint;

        public Rectangle VertexRectangle
        {
            get{ return _vertexRectangle; }
            set{ _vertexRectangle = value; }
        }

        public Point VertexCenterPoint
        {
            get { return _vertexCenterPoint; }
            set { _vertexCenterPoint = value; }
        }

        public string VertexName
        {
            get { return _vertexName; }
            set { _vertexName = value; }
        }

        public Vertex(Point center, int index)
        {
            VertexIndex = index;
            VertexCenterPoint = center;
            VertexRectangle = new Rectangle(new Point(center.X-rectangleSize/2,center.Y-rectangleSize/2), new Size(rectangleSize,rectangleSize));
            index += 65;
            char c = (char)index;
            VertexName = c.ToString();
            
        }

        public int VertexLabel
        {
            get { return _vertexLabel; }
            set { _vertexLabel = value; }
        }

        public Brush VertexBrush
        {
            get { return _vertexBrush; }
            set { _vertexBrush = value; }
        }

        public int VertexIndex
        {
            get { return _vertexIndex; }
            set { _vertexIndex = value; }
        }

        public int DijkstraLengthPath
        {
            get { return _dijkstraLengthPath; }
            set { _dijkstraLengthPath = value; }
        }

        public int VertexDegree
        {
            get { return _vertexDegree; }
            set { _vertexDegree = value; }
        }
        

        public Vertex()
        {
        }

        public Vertex(Vertex vertex)
        {
            this._dijkstraLengthPath = vertex._dijkstraLengthPath;
            this._vertexBrush = vertex._vertexBrush;
            this._vertexCenterPoint = vertex._vertexCenterPoint;
            this._vertexIndex = vertex._vertexIndex;
            this._vertexLabel = vertex._vertexLabel;
            this._vertexName = vertex._vertexName;
            this._vertexRectangle = vertex._vertexRectangle;
           
        }
        public void paintVertex(Graphics g)
        {
            Pen vertexPen = new Pen(Color.Black, 2);
            Font f = new Font("Arial", 12);

            g.DrawEllipse(vertexPen, VertexRectangle);
            g.FillEllipse(VertexBrush, VertexRectangle);
            g.DrawString(VertexName, f, Brushes.Black, VertexCenterPoint.X - 8,VertexCenterPoint.Y - 8);
        }

        public bool isOverlapVertex(Vertex vertex)
        {
            if (this.VertexCenterPoint.X == vertex.VertexCenterPoint.X && this.VertexCenterPoint.Y == vertex.VertexCenterPoint.Y)
                return true;
            return false;
        }
            
    }
}

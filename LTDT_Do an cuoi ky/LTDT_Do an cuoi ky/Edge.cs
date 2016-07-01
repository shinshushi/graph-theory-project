using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LTDT_Do_an_cuoi_ky
{
    public class Edge
    {
        private const int rectangleWidth = 26;
        private const int rectangleHeigth = 22;

        private Vertex _beginVertex;
        private Vertex _endVertex;
        private int _lengthEdge;
        private bool _rightEdgeInAlgorithm;
        private Pen _edgePen;
        private int _egdeLabel;

        private Rectangle _lengthDisplayRectangle;
        private Point _lengthDisplayCenterPoint;

        public Vertex BeginVertex
        {
            get { return _beginVertex; }
            set { _beginVertex = value; }
        }

        public Vertex EndVertex
        {
            get { return _endVertex; }
            set { _endVertex = value; }
        }

        public int LengthEdge
        {
            get { return _lengthEdge; }
            set { _lengthEdge = value; }
        }

        public Rectangle LengthDisplayRectangle
        {
            get { return _lengthDisplayRectangle; }
            set { _lengthDisplayRectangle = value; }
        }

        public Point LengthDisplayCenterPoint
        {
            get { return _lengthDisplayCenterPoint; }
            set{ _lengthDisplayCenterPoint = value;}
        }

        public bool RightEdge
        {
            get { return _rightEdgeInAlgorithm; }
            set { _rightEdgeInAlgorithm = value; }
        }

        public Pen EdgePen
        {
            get { return _edgePen; }
            set { _edgePen = value; }
        }

        public int EdgeLabel
        {
            get { return _egdeLabel; }
            set { _egdeLabel = value; }
        }
    
        public void initLengthDisplayCenterPoint()
        {
            _lengthDisplayCenterPoint.X = (BeginVertex.VertexCenterPoint.X + EndVertex.VertexCenterPoint.X) / 2;
            _lengthDisplayCenterPoint.Y = (BeginVertex.VertexCenterPoint.Y + EndVertex.VertexCenterPoint.Y) / 2;
        }
        public void initLengthDisplayRectangle()
        {
            _lengthDisplayRectangle = new Rectangle(new Point(_lengthDisplayCenterPoint.X - rectangleWidth/2, _lengthDisplayCenterPoint.Y - rectangleHeigth/2), new Size(rectangleWidth, rectangleHeigth));
        }

        public Edge(Vertex begin, Vertex end, int length)
        {
            BeginVertex = new Vertex(begin);
            EndVertex = new Vertex(end);
            //BeginVertex = begin;
            //EndVertex = end;
            LengthEdge = length;
            initLengthDisplayCenterPoint();
            initLengthDisplayRectangle();
        }

        public Edge()
        {
        }

        public void paintEdge(Graphics g)
        {
            Font f = new Font("Arial", 12);
            g.DrawLine(EdgePen,BeginVertex.VertexCenterPoint,EndVertex.VertexCenterPoint);
            g.FillEllipse(Brushes.Yellow,LengthDisplayRectangle);
            g.DrawString(LengthEdge.ToString(),f, Brushes.Black,LengthDisplayCenterPoint.X - 7,LengthDisplayCenterPoint.Y - 8);
        }

        public bool isOverlapEdge(Edge edge)
        {
            if ((this.BeginVertex.isOverlapVertex(edge.BeginVertex) && this.EndVertex.isOverlapVertex(edge.EndVertex)) || (this.BeginVertex.isOverlapVertex(edge.EndVertex) && this.EndVertex.isOverlapVertex(edge.BeginVertex)))
                return true;
            return false;
        }
    }
}

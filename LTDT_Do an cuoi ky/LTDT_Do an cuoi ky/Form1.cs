using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace LTDT_Do_an_cuoi_ky
{
    public partial class Form1 : Form
    {

        private int control;
        private int pause;
        private Graph graph = new Graph();
        int[,] matrix;

        bool mouseMoveEnable;
        bool isClicked;
        int selectedVertexIndex = -1;

        private int startPostion = 0;
        bool isFinish;

        Pen originalEdgePen = new Pen(Color.Red, dash);
        Brush originalVertexBrush = Brushes.LightBlue;
        Pen algorithmEdgePen = new Pen(Color.Blue, dash);
        Brush checkedVertexBrush = Brushes.Violet;
        private const int dash = 8;

        int currentEdgeIndex;
        Edge currentEdge;
        Point pointToPaintInEdge;

        List<Edge> paintCurrentPassedEdgeList = new List<Edge>();
        List<Vertex> paintCurrentCheckedVertexList = new List<Vertex>();

        //findConnectedComponent
        int numberOfConnectedComponent;
        List<Edge> connectedComponentEdgeList = new List<Edge>();

        List<Pen> penOfConnectedComponentEdgeList = new List<Pen>() { new Pen(Color.Blue, dash), new Pen(Color.LightGreen, dash), new Pen(Color.Violet, dash), new Pen(Color.Orange, dash), new Pen(Color.LightPink, dash) };
        List<Brush> brushOfConnectedComponentVertexList = new List<Brush>() { Brushes.Blue, Brushes.LightGreen, Brushes.Violet, Brushes.Orange, Brushes.LightPink };

        //Prim Algorithm
        List<Edge> PrimEdgeList = new List<Edge>();
        int round;
        int redraw;

        //Kruskal Algorithm
        List<Edge> KruskalEdgeList = new List<Edge>();

        //Dijsktra Algorithm
        List<Edge> DijsktraEdgeList = new List<Edge>();
        List<Edge> theShortedPathList = new List<Edge>();
        private const int INF = -1;
        private const string infcharacter = "∞";
        int choosePaint;
        int beginVertex;
        int endVertex;

        //Euler Circuit
        int[] EulerPath;
        int nVer;
        List<Edge> EulerCircuitList = new List<Edge>();
        int first;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = 1;
            numericUpDown1.Value = timer1.Interval;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 1000;
            findConnectedbButton.Enabled = false;
            primButton.Enabled = false;
            kruskalButton.Enabled = false;
            dijstraButton.Enabled = false;
            resetButton.Enabled = false;
            eulerButton.Enabled = false;
            pauseButton.Enabled = false;

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            switch (control)
            {
                case 0:
                    paintGraph(g);
                    break;
                case 1:
                    paintConnectedComponent(g);
                    break;
                case 2:
                    paintPrimAlgorithm(g);
                    break;
                case 3:
                    paintKruskalAlgorithm(g);
                    break;
                case 4:
                    {
                        if (choosePaint == 1)
                            paintDijkstraAlgorithm(g);
                        else
                            paintTheShortestPathInDijsktraAlgorithm(g);
                    }
                    break;
                case 5:
                    paintEulerianCircuit(g);
                    break;

            }
        }



        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            ofd.Title = "Please open Graph Structure file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String fileName = ofd.FileName;
                FileStream fs = new FileStream(fileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string line;
                string[] integerStrings;
                char[] seperate = new char[] { ' ', '\t', '\r', '\n' };

                line = sr.ReadLine();
                int n = int.Parse(line);
                matrix = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    integerStrings = line.Split(seperate, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < integerStrings.Length; j++)
                        matrix[i, j] = int.Parse(integerStrings[j]);
                }

                graph.Matrix = matrix;
                graph.initLocationVertex();
                graph.initGraph();

                addVertexToCombobox();

                sr.Close();
                fs.Close();
                control = 0;
                mouseMoveEnable = true;
                findConnectedbButton.Enabled = true;
                primButton.Enabled = true;
                kruskalButton.Enabled = true;
                dijstraButton.Enabled = true;
                resetButton.Enabled = true;
                eulerButton.Enabled = true;
                button1.Enabled = true;
                pauseButton.Enabled = false;
                thongbao.Text = "";
                result.Text = "";
                algoName.Text = "";
                TPLT.Text = "";
                TPLT2.Text = "";
                L.Text = "";
                listView1.Items.Clear();
                this.panel1.Invalidate();
            }
        }


        public void addVertexToCombobox()
        {
            beginVertexBox.Items.Clear();
            endVertexBox.Items.Clear();
            for (int i = 0; i < graph.NVertex.Count; i++)
            {
                beginVertexBox.Items.Add(graph.NVertex[i].VertexName);
                endVertexBox.Items.Add(graph.NVertex[i].VertexName);
            }
            beginVertexBox.Text = beginVertexBox.Items[0].ToString();
            endVertexBox.Text = endVertexBox.Items[0].ToString();
        }

        public void paintGraph(Graphics g)
        {

            foreach (Edge edge in graph.NEdge)
            {
                edge.EdgePen = new Pen(Color.Red, 7);
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.VertexBrush = originalVertexBrush;
                vertex.paintVertex(g);
            }
        }


        //Mouse Event
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (mouseMoveEnable == true)
            {
                for (int i = 0; i < graph.NVertex.Count; i++)
                {
                    if (graph.NVertex[i].VertexRectangle.Contains(e.X, e.Y))
                    {
                        isClicked = true;
                        selectedVertexIndex = i;
                        break;
                    }
                }
            }

        }
        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isClicked == true && selectedVertexIndex > -1 && e.X >= 10 && e.X < panel1.Size.Width - 15 && e.Y < panel1.Size.Height - 15 && e.Y >= 10)
            {
                Vertex curVertex = new Vertex(new Point(e.X, e.Y), selectedVertexIndex);
                graph.NVertex[selectedVertexIndex] = curVertex;
                graph.initGraph();
                this.panel1.Invalidate();
            }
        }
        private void panel1_MouseUp_1(object sender, MouseEventArgs e)
        {
            isClicked = false;
        }


        //Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            startPostion += 1;
            this.panel1.Invalidate();
        }



        //Tim thanh phan lien thong
        private void findConnectedbButton_Click(object sender, EventArgs e)
        {
            algoName.Text = "Find all connected components of graph by DFS algorithm.";
            control = 0;
            this.panel1.Invalidate();
            findConnectedbButton.Enabled = false;
            primButton.Enabled = false;
            kruskalButton.Enabled = false;
            dijstraButton.Enabled = false;
            eulerButton.Enabled = false;
            button1.Enabled = false;
            pauseButton.Enabled = true;
            control = 1;
            isFinish = false;
            mouseMoveEnable = false;
            connectedComponentEdgeList.Clear();
            paintCurrentPassedEdgeList.Clear();
            paintCurrentCheckedVertexList.Clear();
            currentEdgeIndex = 0;
            findConnectedComponent();
            listView1.Items.Clear();
            timer1.Stop();
            timer1.Start();
        }

        public void fillListConnectedComponent()
        {
            int i = 1;
            while (i <= numberOfConnectedComponent)
            {
                ListViewItem tplt = new ListViewItem(i.ToString());
                string temp = "";
                for (int j = 0; j < graph.NVertex.Count; j++)
                {
                    if (graph.NVertex[j].VertexLabel == i)
                    {
                        temp = temp + graph.NVertex[j].VertexName + " ";
                    }
                }
                ListViewItem.ListViewSubItem vertex = new ListViewItem.ListViewSubItem(tplt, temp);
                tplt.SubItems.Add(vertex);
                listView1.Items.Add(tplt);
                i++;
            }
        }

        public void paintConnectedComponent(Graphics g)
        {

            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }


            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
            }

            if (currentEdgeIndex == connectedComponentEdgeList.Count)
            {
                timer1.Stop();
                isFinish = true;
                thongbao.Text = "ALGORITHM IS FINISHED!!!";
                pauseButton.Enabled = false;
                this.panel1.Invalidate();
            }

            if (isFinish == false)
            {
                currentEdge = connectedComponentEdgeList[currentEdgeIndex];

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);

                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);

                    currentEdge.BeginVertex.VertexBrush = brushOfConnectedComponentVertexList[currentEdge.BeginVertex.VertexLabel - 1];
                    currentEdge.BeginVertex.paintVertex(g);

                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);

                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {
                    if (currentEdge.RightEdge == true)
                    {
                        paintCurrentPassedEdgeList.Add(connectedComponentEdgeList[currentEdgeIndex]);

                        paintCurrentCheckedVertexList.Add(currentEdge.BeginVertex);

                        currentEdge.EndVertex.VertexBrush = brushOfConnectedComponentVertexList[currentEdge.BeginVertex.VertexLabel - 1];
                        paintCurrentCheckedVertexList.Add(currentEdge.EndVertex);
                    }
                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdgeIndex++;
                    startPostion = 0;

                }

            }

            foreach (Edge edge in paintCurrentPassedEdgeList)
            {
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in paintCurrentCheckedVertexList)
            {
                vertex.paintVertex(g);
            }

            if (isFinish == true)
            {
                foreach (Vertex vertex in graph.NVertex)
                {
                    vertex.VertexBrush = brushOfConnectedComponentVertexList[vertex.VertexLabel - 1];
                    vertex.paintVertex(g);
                }
                if (numberOfConnectedComponent > 1)
                {
                    TPLT.Text = "There are " + numberOfConnectedComponent + " connected component.";
                    listView1.Items.Clear();
                    fillListConnectedComponent();
                }
                else
                {
                    listView1.Items.Clear();
                    TPLT2.Text = "Graph is connected.";
                }
            }

        }

        public void visit(int i, int vertexLable)
        {
            graph.NVertex[i].VertexLabel = vertexLable;
            for (int j = 0; j < graph.Matrix.GetLength(0); j++)
            {
                if (graph.Matrix[i, j] != 0 || graph.Matrix[j, i] != 0)
                {
                    Edge tempEdge = new Edge(graph.NVertex[i], graph.NVertex[j], graph.Matrix[i, j]);
                    if (graph.NVertex[j].VertexLabel == 0)
                    {
                        tempEdge.RightEdge = true;
                        tempEdge.EdgePen = penOfConnectedComponentEdgeList[vertexLable - 1];
                        connectedComponentEdgeList.Add(tempEdge);
                        visit(j, vertexLable);
                    }
                    else
                    {
                        tempEdge.RightEdge = false;
                        tempEdge.EdgePen = penOfConnectedComponentEdgeList[vertexLable - 1];
                        if (findIndexOfEdgeInEdgeList(tempEdge, connectedComponentEdgeList) == -1)
                        {
                            connectedComponentEdgeList.Add(tempEdge);
                            Edge tempEdge2 = new Edge(tempEdge.EndVertex, tempEdge.BeginVertex, tempEdge.LengthEdge);
                            tempEdge2.RightEdge = false;
                            tempEdge2.EdgePen = originalEdgePen;
                            connectedComponentEdgeList.Add(tempEdge2);
                        }
                    }

                }
            }
        }


        public void findConnectedComponent()
        {
            connectedComponentEdgeList.Clear();
            foreach (Vertex vertex in graph.NVertex)
                vertex.VertexLabel = 0;
            numberOfConnectedComponent = 0;
            for (int i = 0; i < graph.NVertex.Count; i++)
            {
                if (graph.NVertex[i].VertexLabel == 0)
                {
                    numberOfConnectedComponent++;
                    visit(i, numberOfConnectedComponent);
                }
            }
        }



        public Point getNextPoint(Point beginPoint, Point endPoint, int value)
        {

            Point resultPoint = new Point();
            if (beginPoint.X <= endPoint.X && beginPoint.Y <= endPoint.Y)
            {
                resultPoint.X = beginPoint.X + Math.Abs(beginPoint.X - endPoint.X) * value / 100;
                resultPoint.Y = beginPoint.Y + Math.Abs(beginPoint.Y - endPoint.Y) * value / 100;
            }
            if (beginPoint.X <= endPoint.X && beginPoint.Y >= endPoint.Y)
            {
                resultPoint.X = beginPoint.X + Math.Abs(beginPoint.X - endPoint.X) * value / 100;
                resultPoint.Y = beginPoint.Y - Math.Abs(beginPoint.Y - endPoint.Y) * value / 100;
            }
            if (beginPoint.X >= endPoint.X && beginPoint.Y <= endPoint.Y)
            {
                resultPoint.X = beginPoint.X - Math.Abs(beginPoint.X - endPoint.X) * value / 100;
                resultPoint.Y = beginPoint.Y + Math.Abs(beginPoint.Y - endPoint.Y) * value / 100;
            }
            if (beginPoint.X >= endPoint.X && beginPoint.Y >= endPoint.Y)
            {
                resultPoint.X = beginPoint.X - Math.Abs(beginPoint.X - endPoint.X) * value / 100;
                resultPoint.Y = beginPoint.Y - Math.Abs(beginPoint.Y - endPoint.Y) * value / 100;
            }
            return resultPoint;
        }

        public bool checkValidNextPoint(Point beginPoint, Point endPoint)
        {
            if (endPoint.X == beginPoint.X && endPoint.Y == beginPoint.Y)
            {
                return false;
            }
            return true;
        }


        public int findIndexOfEdgeInEdgeList(Edge edge, List<Edge> A)
        {
            for (int i = 0; i < A.Count; i++)
            {
                if (A[i].isOverlapEdge(edge))
                    return i;
            }
            return -1;
        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            findConnectedbButton.Enabled = true;
            primButton.Enabled = true;
            kruskalButton.Enabled = true;
            dijstraButton.Enabled = true;
            eulerButton.Enabled = true;
            button1.Enabled = true;
            pauseButton.Enabled = false;
            control = 0;
            startPostion = 0;
            currentEdgeIndex = 0;
            mouseMoveEnable = true;
            timer1.Stop();
            thongbao.Text = "";
            result.Text = "";
            algoName.Text = "";
            L.Text = "";

            this.panel1.Invalidate();
        }




        //Prim Algorithm
        private void primButton_Click(object sender, EventArgs e)
        {
            algoName.Text = "The Kruskal algorithm that can find the shortest spanning tree of connected graph.";
            findConnectedComponent();
            if (numberOfConnectedComponent == 1)
            {
                findConnectedbButton.Enabled = false;
                primButton.Enabled = false;
                kruskalButton.Enabled = false;
                dijstraButton.Enabled = false;
                eulerButton.Enabled = false;
                button1.Enabled = false;
                control = 2;
                isFinish = false;
                mouseMoveEnable = false;
                pauseButton.Enabled = true;
                currentEdgeIndex = 0;
                round = 1;
                redraw = 0;
                PrimEdgeList.Clear();
                paintCurrentPassedEdgeList.Clear();
                paintCurrentCheckedVertexList.Clear();
                PrimAlgorithm();
                timer1.Stop();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Graph doesn't have spanning tree!!!");
            }
        }

        public void PrimAlgorithm()
        {
            List<Edge> spanningTree = new List<Edge>(); ;
            List<Edge> spanningTree_temp = new List<Edge>();
            List<int> primCheckedVertexList = new List<int>(); ;
            foreach (Vertex vertex in graph.NVertex)
                vertex.VertexLabel = 0;
            graph.NVertex[0].VertexLabel = 1;
            primCheckedVertexList.Add(0);

            Edge minEdge = new Edge();

            int indexNextVertex;

            while (spanningTree.Count != graph.NVertex.Count - 1)
            {

                for (int i = 0; i < primCheckedVertexList.Count; i++)
                {
                    for (int j = 0; j < graph.NVertex.Count; j++)
                    {
                        if (graph.NVertex[j].VertexLabel == 0 && (graph.Matrix[primCheckedVertexList[i], j] != 0 || graph.Matrix[j, primCheckedVertexList[i]] != 0))
                        {
                            Edge tempEdge = new Edge(graph.NVertex[primCheckedVertexList[i]], graph.NVertex[j], graph.Matrix[primCheckedVertexList[i], j]);

                            spanningTree_temp.Add(tempEdge);
                        }
                    }
                }

                minEdge = findTheMinEdgeInAnEdgeList(spanningTree_temp);

                for (int i = 0; i < spanningTree_temp.Count; i++)
                {
                    Edge tempEdge = new Edge(spanningTree_temp[i].BeginVertex, spanningTree_temp[i].EndVertex, spanningTree_temp[i].LengthEdge);
                    tempEdge.RightEdge = false;
                    tempEdge.EdgePen = algorithmEdgePen;
                    if (i == 0)
                        tempEdge.EdgeLabel = 1;
                    PrimEdgeList.Add(tempEdge);

                }

                for (int i = 0; i < spanningTree_temp.Count; i++)
                {
                    if (spanningTree_temp[i].isOverlapEdge(minEdge) == false)
                    {
                        Edge tempEdge = new Edge(spanningTree_temp[i].EndVertex, spanningTree_temp[i].BeginVertex, spanningTree_temp[i].LengthEdge);
                        tempEdge.RightEdge = false;
                        tempEdge.EdgePen = originalEdgePen;
                        if (i == spanningTree_temp.Count - 1)
                            tempEdge.EdgeLabel = 2;
                        PrimEdgeList.Add(tempEdge);
                    }
                    else
                    {
                        Edge tempEdge = new Edge(spanningTree_temp[i].BeginVertex, spanningTree_temp[i].EndVertex, spanningTree_temp[i].LengthEdge);
                        tempEdge.RightEdge = true;
                        tempEdge.EdgePen = new Pen(Color.Green, dash);
                        if (i == spanningTree_temp.Count - 1)
                            tempEdge.EdgeLabel = 2;
                        PrimEdgeList.Add(tempEdge);
                    }

                }

                spanningTree.Add(minEdge);
                indexNextVertex = minEdge.EndVertex.VertexIndex;
                graph.NVertex[indexNextVertex].VertexLabel = 1;
                primCheckedVertexList.Add(indexNextVertex);
                spanningTree_temp.Clear();
            }
        }

        public Edge findTheMinEdgeInAnEdgeList(List<Edge> A)
        {
            int lenghtMinEdge = -1;
            Edge minEdge = new Edge();
            for (int i = 0; i < A.Count; i++)
            {
                if (lenghtMinEdge == -1 || A[i].LengthEdge < lenghtMinEdge)
                {
                    lenghtMinEdge = A[i].LengthEdge;
                    minEdge = A[i];
                }
            }
            return minEdge;
        }


        public void paintPrimAlgorithm(Graphics g)
        {
            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }



            if (currentEdgeIndex == PrimEdgeList.Count)
            {
                timer1.Stop();
                isFinish = true;
                thongbao.Text = "ALGORITHM IS FINISHED!!!";
                result.Text = "";
                pauseButton.Enabled = false;
                this.panel1.Invalidate();
            }

            if (isFinish == false)
            {
                currentEdge = PrimEdgeList[currentEdgeIndex];
                if (currentEdge.EdgeLabel == 1 && redraw == 0)
                {
                    result.Text = "";
                    thongbao.Text = "Algorithm is running in round " + round.ToString() + "...";
                    round++;
                    redraw = 1;
                }
                if (currentEdge.EdgeLabel != 1)
                    redraw = 0;
                if (currentEdge.RightEdge == true)
                    result.Text = currentEdge.BeginVertex.VertexName + currentEdge.EndVertex.VertexName + " is the shortest edge which is selected.";

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);
                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);
                    if (currentEdgeIndex == 0)
                    {
                        currentEdge.BeginVertex.VertexBrush = checkedVertexBrush;
                        graph.NVertex[currentEdge.BeginVertex.VertexIndex].VertexBrush = checkedVertexBrush;
                    }
                    currentEdge.BeginVertex.paintVertex(g);
                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);
                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {
                    if (currentEdge.RightEdge == true)
                    {
                        paintCurrentPassedEdgeList.Add(PrimEdgeList[currentEdgeIndex]);
                        paintCurrentCheckedVertexList.Add(currentEdge.BeginVertex);
                        currentEdge.BeginVertex.VertexBrush = checkedVertexBrush;
                        currentEdge.EndVertex.VertexBrush = checkedVertexBrush;
                        paintCurrentCheckedVertexList.Add(currentEdge.EndVertex);
                        graph.NVertex[currentEdge.EndVertex.VertexIndex].VertexBrush = checkedVertexBrush;
                    }
                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdgeIndex++;
                    startPostion = 0;

                }

            }

            foreach (Edge edge in paintCurrentPassedEdgeList)
            {
                edge.paintEdge(g);
            }


            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
            }

            foreach (Vertex vertex in paintCurrentCheckedVertexList)
            {
                vertex.paintVertex(g);
            }


        }




        //Kruskal Algorithm

        private void kruskalButton_Click(object sender, EventArgs e)
        {
            algoName.Text = "The Kruskal algorithm that can find the shortest spanning tree of connected graph.";
            findConnectedComponent();
            if (numberOfConnectedComponent == 1)
            {
                findConnectedbButton.Enabled = false;
                primButton.Enabled = false;
                kruskalButton.Enabled = false;
                dijstraButton.Enabled = false;
                eulerButton.Enabled = false;
                button1.Enabled = false;
                pauseButton.Enabled = true;
                control = 3;
                isFinish = false;
                mouseMoveEnable = false;
                currentEdgeIndex = 0;
                KruskalEdgeList.Clear();
                paintCurrentPassedEdgeList.Clear();
                paintCurrentCheckedVertexList.Clear();
                KruskalAlgorithm();
                timer1.Stop();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Graph doesn't have spanning tree!!!");
            }
        }

        public void sortEdgeAscending(List<Edge> A)
        {
            Edge tempEdge = new Edge();
            for (int i = 0; i < A.Count - 1; i++)
            {
                for (int j = i; j < A.Count; j++)
                {
                    if (A[i].LengthEdge > A[j].LengthEdge)
                    {
                        tempEdge = A[i];
                        A[i] = A[j];
                        A[j] = tempEdge;
                    }
                }
            }
        }

        public bool isCircle(int[] V, int index)
        {
            if (V[graph.NEdge[index].BeginVertex.VertexIndex] == V[graph.NEdge[index].EndVertex.VertexIndex])
                return true;
            else
            {
                int label1, label2;
                if (V[graph.NEdge[index].BeginVertex.VertexIndex] > V[graph.NEdge[index].EndVertex.VertexIndex])
                {
                    label1 = V[graph.NEdge[index].EndVertex.VertexIndex];
                    label2 = V[graph.NEdge[index].BeginVertex.VertexIndex];
                }
                else
                {
                    label2 = V[graph.NEdge[index].EndVertex.VertexIndex];
                    label1 = V[graph.NEdge[index].BeginVertex.VertexIndex];
                }
                for (int i = 0; i < V.Length; i++)
                    if (V[i] == label2)
                        V[i] = label1;
                return false;
            }
        }

        public void KruskalAlgorithm()
        {
            int[] V = new int[graph.NVertex.Count];
            List<Edge> spanningTree = new List<Edge>();
            sortEdgeAscending(graph.NEdge);
            for (int i = 0; i < V.Length; i++)
                V[i] = i;
            int iMinIndex = 0;
            while (spanningTree.Count != graph.NVertex.Count - 1)
            {
                Edge tempEdge = new Edge(graph.NEdge[iMinIndex].BeginVertex, graph.NEdge[iMinIndex].EndVertex, graph.NEdge[iMinIndex].LengthEdge);
                if (isCircle(V, iMinIndex) == false)
                {
                    tempEdge.RightEdge = true;
                    tempEdge.EdgePen = new Pen(Color.Green, dash);
                    spanningTree.Add(tempEdge);
                    KruskalEdgeList.Add(tempEdge);
                }
                else
                {
                    tempEdge.RightEdge = false;
                    tempEdge.EdgePen = new Pen(Color.Green, dash);
                    KruskalEdgeList.Add(tempEdge);
                    Edge tempEdge2 = new Edge(tempEdge.EndVertex, tempEdge.BeginVertex, tempEdge.LengthEdge);
                    tempEdge2.RightEdge = false;
                    tempEdge2.EdgePen = originalEdgePen;
                    KruskalEdgeList.Add(tempEdge2);
                }
                iMinIndex++;
            }

        }

        public void paintKruskalAlgorithm(Graphics g)
        {
            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }


            if (currentEdgeIndex == KruskalEdgeList.Count)
            {
                timer1.Stop();
                isFinish = true;
                thongbao.Text = "ALGORITHM IS FINISHED!!!";
                pauseButton.Enabled = false;
                this.panel1.Invalidate();
            }

            if (isFinish == false)
            {
                currentEdge = KruskalEdgeList[currentEdgeIndex];

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);
                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);

                    currentEdge.BeginVertex.VertexBrush = checkedVertexBrush;
                    graph.NVertex[currentEdge.BeginVertex.VertexIndex].VertexBrush = checkedVertexBrush;

                    currentEdge.BeginVertex.paintVertex(g);
                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);
                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {
                    if (currentEdge.RightEdge == true)
                    {
                        paintCurrentPassedEdgeList.Add(KruskalEdgeList[currentEdgeIndex]);

                        paintCurrentCheckedVertexList.Add(currentEdge.BeginVertex);

                        currentEdge.EndVertex.VertexBrush = checkedVertexBrush;
                        paintCurrentCheckedVertexList.Add(currentEdge.EndVertex);
                        graph.NVertex[currentEdge.EndVertex.VertexIndex].VertexBrush = checkedVertexBrush;
                    }
                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdgeIndex++;
                    startPostion = 0;

                }

            }

            foreach (Edge edge in paintCurrentPassedEdgeList)
            {
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
            }

            foreach (Vertex vertex in paintCurrentCheckedVertexList)
            {
                vertex.paintVertex(g);
            }
        }

        //Dijkstra Algorithm

        public void initForDijkstra(int[] lengthPath, int[] prevVertex)
        {
            for (int i = 0; i < graph.NVertex.Count; i++)
            {
                graph.NVertex[i].VertexLabel = 1;
                graph.NVertex[i].DijkstraLengthPath = INF;
                lengthPath[i] = INF;
                prevVertex[i] = -1;
            }
            lengthPath[beginVertex] = 0;
            graph.NVertex[beginVertex].DijkstraLengthPath = 0;
        }


        public bool DijkstraAlgorithm()
        {
            int[] lengthPath = new int[graph.NVertex.Count];
            int[] prevVertex = new int[graph.NVertex.Count];
            initForDijkstra(lengthPath, prevVertex);

            while (true)
            {
                int iMin = INF;
                int v = -1;
                for (int i = 0; i < graph.NVertex.Count; i++)
                {
                    if (graph.NVertex[i].VertexLabel == 1 && lengthPath[i] != INF && (lengthPath[i] < iMin || iMin == INF))
                    {
                        iMin = lengthPath[i];
                        v = i;
                    }
                }
                if (iMin == INF)
                    return false;
                lengthPath[v] = iMin;
                graph.NVertex[v].VertexLabel = 0;
                if (graph.NVertex[endVertex].VertexLabel == 0)
                    break;
                int first = 0;
                while (first < graph.NVertex.Count && !(graph.NVertex[first].VertexLabel == 1 && graph.Matrix[v, first] != 0))
                    first++;

                for (int j = 0; j < graph.NVertex.Count; j++)
                {
                    if (graph.NVertex[j].VertexLabel == 1 && graph.Matrix[v, j] != 0)
                    {
                        Edge tempEdge = new Edge(graph.NVertex[v], graph.NVertex[j], graph.Matrix[v, j]);
                        tempEdge.EdgePen = algorithmEdgePen;
                        tempEdge.RightEdge = false;
                        if (j == first)
                            tempEdge.EdgeLabel = 0;
                        else tempEdge.EdgeLabel = 1;

                        Edge tempEdge2 = new Edge(graph.NVertex[j], graph.NVertex[v], graph.Matrix[v, j]);
                        if (lengthPath[j] == INF || lengthPath[j] > lengthPath[v] + graph.Matrix[v, j])
                        {
                            lengthPath[j] = lengthPath[v] + graph.Matrix[v, j];
                            prevVertex[j] = v;
                            tempEdge2.RightEdge = true;
                            tempEdge2.EdgePen = new Pen(Color.Green, dash);
                            tempEdge2.EdgePen.EndCap = LineCap.ArrowAnchor;
                            tempEdge2.EdgeLabel = 2;
                        }
                        else
                        {
                            tempEdge2.RightEdge = false;
                            tempEdge2.EdgePen = originalEdgePen;
                            tempEdge2.EdgeLabel = 2;
                        }
                        tempEdge.EndVertex.DijkstraLengthPath = lengthPath[j];
                        DijsktraEdgeList.Add(tempEdge);
                        DijsktraEdgeList.Add(tempEdge2);
                    }
                }

            }
            getPath(prevVertex);
            return true;

        }

        public void getPath(int[] prevVertex)
        {
            int k = endVertex;
            List<int> vertex = new List<int>();
            vertex.Add(k);
            while (k != beginVertex)
            {
                Edge tempEdge = new Edge(graph.NVertex[k], graph.NVertex[prevVertex[k]], graph.Matrix[prevVertex[k], k]);
                tempEdge.EdgePen = new Pen(Color.Violet, dash);
                tempEdge.EdgePen.EndCap = LineCap.ArrowAnchor;
                tempEdge.RightEdge = true;
                if (k == endVertex)
                    tempEdge.EdgeLabel = 1;
                if (prevVertex[k] == beginVertex)
                    tempEdge.EdgeLabel = 2;
                k = prevVertex[k];
                vertex.Add(k);
                theShortedPathList.Add(tempEdge);
            }

            for (int i = vertex.Count - 1; i > 0; i--)
            {
                Edge tempEdge = new Edge(graph.NVertex[vertex[i]], graph.NVertex[vertex[i - 1]], graph.Matrix[vertex[i], vertex[i - 1]]);
                tempEdge.EdgePen = new Pen(Color.Black, dash);
                tempEdge.EdgePen.EndCap = LineCap.ArrowAnchor;
                tempEdge.RightEdge = true;
                theShortedPathList.Add(tempEdge);
            }
        }

        public void paintDijkstraAlgorithm(Graphics g)
        {
            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }


            if (currentEdgeIndex == DijsktraEdgeList.Count)
            {
                isFinish = true;
            }

            if (isFinish == false)
            {
                currentEdge = DijsktraEdgeList[currentEdgeIndex];
                if (currentEdge.EdgeLabel == 0 && redraw == 0)
                {
                    thongbao.Text = "Algorithm is running in round " + round.ToString() + "...";
                    result.Text = "Vertex " + currentEdge.BeginVertex.VertexName + " has L[" + currentEdge.BeginVertex.VertexName + "] which is minimum and it was removed from T";
                    round++;
                    redraw = 1;
                }
                if (currentEdge.EdgeLabel != 0)
                    redraw = 0;

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);
                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);
                    if (currentEdge.EdgeLabel == 2)
                    {
                        currentEdge.BeginVertex.VertexBrush = originalVertexBrush;
                        currentEdge.BeginVertex.paintVertex(g);
                    }
                    if (currentEdge.EdgeLabel == 0 || currentEdge.EdgeLabel == 1)
                    {
                        currentEdge.BeginVertex.VertexBrush = checkedVertexBrush;
                        currentEdge.BeginVertex.paintVertex(g);
                        graph.NVertex[currentEdge.BeginVertex.VertexIndex].VertexBrush = checkedVertexBrush;
                    }

                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);
                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {
                    if (currentEdge.EdgeLabel == 0 || currentEdge.EdgeLabel == 1)
                    {
                        graph.NVertex[currentEdge.EndVertex.VertexIndex].DijkstraLengthPath = currentEdge.EndVertex.DijkstraLengthPath;
                        paintCurrentCheckedVertexList.Add(currentEdge.BeginVertex);
                    }

                    if (currentEdge.RightEdge == true)
                        paintCurrentPassedEdgeList.Add(DijsktraEdgeList[currentEdgeIndex]);

                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdge.EdgePen.EndCap = LineCap.NoAnchor;
                    currentEdgeIndex++;
                    startPostion = 0;

                }
            }
            foreach (Edge edge in paintCurrentPassedEdgeList)
            {
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
                vertex.DrawDijkstraLengthPathPoint = vertex.VertexCenterPoint;
                if (vertex.VertexCenterPoint.Y < 300)
                    vertex.DrawDijkstraLengthPathPoint.Y = vertex.DrawDijkstraLengthPathPoint.Y - 30;
                else
                    vertex.DrawDijkstraLengthPathPoint.Y = vertex.DrawDijkstraLengthPathPoint.Y + 20;
                if (vertex.VertexCenterPoint.X < 500)
                    vertex.DrawDijkstraLengthPathPoint.X = vertex.DrawDijkstraLengthPathPoint.X - 50;
                else
                    vertex.DrawDijkstraLengthPathPoint.X = vertex.DrawDijkstraLengthPathPoint.X + 20;

                if (vertex.DijkstraLengthPath != INF)
                    g.DrawString("L[" + vertex.VertexName + "] = " + vertex.DijkstraLengthPath, new Font("Arial", 15), Brushes.Black, vertex.DrawDijkstraLengthPathPoint);
                else
                    g.DrawString("L[" + vertex.VertexName + "] = " + infcharacter, new Font("Arial", 15), Brushes.Black, vertex.DrawDijkstraLengthPathPoint);
            }

            foreach (Vertex vertex in paintCurrentCheckedVertexList)
            {
                vertex.paintVertex(g);
            }

            if (isFinish == true)
            {
                currentEdgeIndex = 0;
                startPostion = 0;
                changeColorListToGraph();
                paintCurrentPassedEdgeList.Clear();
                paintCurrentCheckedVertexList.Clear();
                graph.NVertex[endVertex].VertexBrush = Brushes.Blue;
                graph.NVertex[endVertex].paintVertex(g);
                graph.NVertex[beginVertex].VertexBrush = Brushes.Blue;
                graph.NVertex[beginVertex].paintVertex(g);
                result.Text = "";
                isFinish = false;
                choosePaint = 2;

            }

        }

        public void paintTheShortestPathInDijsktraAlgorithm(Graphics g)
        {

            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }

            if (currentEdgeIndex == theShortedPathList.Count)
            {
                timer1.Stop();
                isFinish = true;
                thongbao.Text = "ALGORITHM IS FINISHED!!!";
                this.panel1.Invalidate();
            }

            if (isFinish == false)
            {
                currentEdge = theShortedPathList[currentEdgeIndex];
                if (currentEdge.EdgeLabel == 1)
                    thongbao.Text = "Finding previous vertex from end vertex...";

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);
                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);
                    if (currentEdge.BeginVertex.VertexIndex == endVertex)
                    {
                        currentEdge.BeginVertex.VertexBrush = Brushes.Blue;
                        currentEdge.BeginVertex.paintVertex(g);
                    }
                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);
                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {
                    paintCurrentPassedEdgeList.Add(theShortedPathList[currentEdgeIndex]);
                    foreach (Edge edge in paintCurrentPassedEdgeList)
                    {
                        edge.paintEdge(g);
                    }
                    if (currentEdge.EdgeLabel == 2)
                    {
                        foreach (Edge edge in graph.NEdge)
                        {
                            edge.EdgePen = originalEdgePen;
                        }
                        changeColorListToGraph();
                        paintCurrentPassedEdgeList.Clear();
                        thongbao.Text = "Drawing the shortest path from " + graph.NVertex[beginVertex].VertexName + " to " + graph.NVertex[endVertex].VertexName + "...";
                    }
                    currentEdge.EdgePen.EndCap = LineCap.NoAnchor;
                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdgeIndex++;
                    startPostion = 0;
                }

            }


            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
            }
        }

        public void changeColorListToGraph()
        {
            for (int i = 0; i < paintCurrentPassedEdgeList.Count; i++)
                graph.NEdge[findIndexOfEdgeInEdgeList(paintCurrentPassedEdgeList[i], graph.NEdge)].EdgePen = paintCurrentPassedEdgeList[i].EdgePen;
            for (int i = 0; i < paintCurrentCheckedVertexList.Count; i++)
                graph.NVertex[paintCurrentCheckedVertexList[i].VertexIndex].VertexBrush = paintCurrentCheckedVertexList[i].VertexBrush;
        }



        private void dijstraButton_Click(object sender, EventArgs e)
        {
            algoName.Text = "The Dijkstra algorithm that can find the shortest path between 2 vertexs in graph.";

            control = 4;
            mouseMoveEnable = false;
            isFinish = false;
            currentEdgeIndex = 0;
            round = 1;
            redraw = 0;
            beginVertex = beginVertexBox.SelectedIndex;
            endVertex = endVertexBox.SelectedIndex;
            DijsktraEdgeList.Clear();
            paintCurrentPassedEdgeList.Clear();
            paintCurrentCheckedVertexList.Clear();
            theShortedPathList.Clear();
            choosePaint = 1;
            if (DijkstraAlgorithm() == true && beginVertex != endVertex)
            {
                L.Text = "L[ ]: min length of path from begin vertex to this vetex.\nT[ ]: the set of vertex use in Dijkstra algorithm.";
                findConnectedbButton.Enabled = false;
                primButton.Enabled = false;
                kruskalButton.Enabled = false;
                dijstraButton.Enabled = false;
                eulerButton.Enabled = false;
                button1.Enabled = false;
                pauseButton.Enabled = true;
                timer1.Stop();
                timer1.Start();
            }
            else
            {

                MessageBox.Show("Can't find path from " + beginVertexBox.Items[beginVertex] + " to " + endVertexBox.Items[endVertex]);
            }

        }

        //Eulerian Circuit

        public int findEulerianCircuitAlgorithm()
        {
            EulerPath = new int[100];
            nVer = 0;
            EulerCircuit E = new EulerCircuit(graph.NVertex.Count, graph.Matrix);
            int ret = E.runFleury(ref EulerPath, ref nVer);
            if (ret == 1)
            {
                for (int i = 0; i < nVer - 1; i++)
                {
                    Edge tempEdge = new Edge(graph.NVertex[EulerPath[i]], graph.NVertex[EulerPath[i + 1]], graph.Matrix[EulerPath[i], EulerPath[i + 1]]);
                    tempEdge.EdgePen = new Pen(Color.Black, dash);
                    tempEdge.EdgePen.EndCap = LineCap.ArrowAnchor;
                    EulerCircuitList.Add(tempEdge);
                }
            }
            return ret;
        }

        public void paintEulerianCircuit(Graphics g)
        {
            foreach (Edge edge in graph.NEdge)
            {
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in graph.NVertex)
            {
                vertex.paintVertex(g);
            }

            if (currentEdgeIndex == EulerCircuitList.Count)
            {
                timer1.Stop();
                isFinish = true;
                thongbao.Text = "ALGORITHM IS FINISHED!!!";
                pauseButton.Enabled = false;
                this.panel1.Invalidate();
            }

            if (isFinish == false)
            {
                currentEdge = EulerCircuitList[currentEdgeIndex];

                pointToPaintInEdge = getNextPoint(currentEdge.BeginVertex.VertexCenterPoint, currentEdge.EndVertex.VertexCenterPoint, startPostion);

                if (checkValidNextPoint(pointToPaintInEdge, currentEdge.EndVertex.VertexCenterPoint) == true)
                {
                    g.DrawLine(currentEdge.EdgePen, currentEdge.BeginVertex.VertexCenterPoint, pointToPaintInEdge);
                    if (currentEdge.BeginVertex.VertexIndex == first)
                        currentEdge.BeginVertex.VertexBrush = Brushes.Blue;
                    else currentEdge.BeginVertex.VertexBrush = checkedVertexBrush;
                    currentEdge.BeginVertex.paintVertex(g);

                    g.FillEllipse(Brushes.Yellow, currentEdge.LengthDisplayRectangle);

                    g.DrawString(currentEdge.LengthEdge.ToString(), new Font("Arial", 12), Brushes.Black, currentEdge.LengthDisplayCenterPoint.X - 7, currentEdge.LengthDisplayCenterPoint.Y - 8);
                }
                else
                {

                    paintCurrentPassedEdgeList.Add(EulerCircuitList[currentEdgeIndex]);

                    paintCurrentCheckedVertexList.Add(currentEdge.BeginVertex);
                    if (currentEdge.EndVertex.VertexIndex == first)
                        currentEdge.EndVertex.VertexBrush = Brushes.Blue;
                    else currentEdge.EndVertex.VertexBrush = checkedVertexBrush;
                    paintCurrentCheckedVertexList.Add(currentEdge.EndVertex);

                    graph.NEdge[findIndexOfEdgeInEdgeList(currentEdge, graph.NEdge)].EdgePen = currentEdge.EdgePen;
                    currentEdgeIndex++;
                    startPostion = 0;

                }

            }

            foreach (Edge edge in paintCurrentPassedEdgeList)
            {
                edge.paintEdge(g);
            }

            foreach (Vertex vertex in paintCurrentCheckedVertexList)
            {
                vertex.paintVertex(g);
            }
        }

        private void eulerButton_Click(object sender, EventArgs e)
        {
            algoName.Text = "The Fleury algorithm that can find the Euler circuit in connected graph.";
            control = 5;
            mouseMoveEnable = false;
            isFinish = false;
            currentEdgeIndex = 0;
            startPostion = 0;
            EulerCircuitList.Clear();
            paintCurrentPassedEdgeList.Clear();
            paintCurrentCheckedVertexList.Clear();
            int ret = findEulerianCircuitAlgorithm();
            if (ret == 1)
            {
                first = EulerCircuitList[0].BeginVertex.VertexIndex;
                eulerButton.Enabled = false;
                findConnectedbButton.Enabled = false;
                primButton.Enabled = false;
                kruskalButton.Enabled = false;
                dijstraButton.Enabled = false;
                button1.Enabled = false;
                pauseButton.Enabled = true;
                timer1.Stop();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Graph doesn't have Euler circuit");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Group: TÊN GÌ CŨNG ĐƯỢC\n\n1. Nguyễn Mạnh Sinh 1212325 (Group leader)\n2. Võ Nhật Sinh 1212326\n3. Trần Phú Quý 1212316\n4. Trương Hồng Phúc 1212297\n\n\tTHANKS FOR YOUR INTEREST ^_^");
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            int temp = 0;
            if (pause == 1)
            {
                timer1.Stop();
                pause = 0;
                pauseButton.Text = "Continue";
                temp = 1;
                
            }
            if (pause == 0 && temp==0)
            {
                timer1.Start();
                pause = 1;
                pauseButton.Text = "Pause";
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
            timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
        }

    }
}



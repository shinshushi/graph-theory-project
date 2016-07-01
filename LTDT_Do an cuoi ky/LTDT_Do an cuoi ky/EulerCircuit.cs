using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTDT_Do_an_cuoi_ky
{

    class EulerCircuit
    {

        public const int MAX = 100;
        public const int NON_BRIDGE = -1;
        public const int BRIDGE = 0;
        public const int DONE = 1;
        public const int NOT_DONE = 0;
        public const int HAS_EULER_PATH = 0;
        public const int HAS_EULER_CYCLE = 1;
        public const int NOT_HAS_EULER_PATH = -1;

        public int nV; //số đỉnh của đồ thị
        public int[,] adjencyMatrix; //ma trận trọng số

        //Hàm khởi tạo ma trận rỗng
        public EulerCircuit()
        {
            nV = 0;
        }


        //Hàm khởi tạo đồ thị từ ma trận trọng số
        public EulerCircuit(int n, int[,] A)
        {
            nV = n;
            int temp;
            adjencyMatrix = new int[n, n];
            for (int i = 0; i < nV; i++)
            {
                for (int j = 0; j < nV; j++)
                {
                    temp = A[i, j];
                    if (A[i, j] != 0)
                    {
                        temp = 1;
                    }
                    adjencyMatrix[i, j] = temp;
                }
            }
        }


        //Hàm thêm cạnh (i,j)
        public int addEdge(int i, int j)
        {
            adjencyMatrix[i, j] = 1;
            return 1;
        }

        //Hàm xóa cạnh (i,j);
        public int removeEdge(int i, int j)
        {
            adjencyMatrix[i, j]--;
            adjencyMatrix[j, i]--;
            return 1;
        }

        //Hàm lấy cạnh (i,j)
        public int getEdge(int i, int j)
        {
            return adjencyMatrix[i, j];
        }

        //Ham tinh so luong canh cua do thi
        public int getNumEdge()
        {
            int nE = 0;
            for (int i = 0; i < nV; i++)
                for (int j = i; j < nV; j++)
                {
                    nE = nE + adjencyMatrix[i, j];
                }
            return nE;
        }

        //Hàm xác định bậc của một đỉnh
        public int getVertexDeg(int v)
        {
            int count = 0;
            for (int i = 0; i < nV; i++)
            {
                count = count + adjencyMatrix[v, i];
            }
            return count;
        }

        //Hàm kiểm tra cạnh (i,j) có phải là cầu hay không
        public int bridgeDetection(int i, int j)
        {
            int[] dfsNum = new int[MAX];
            int curNum = 0;
            //begin 1
            for (int k = 0; k < nV; k++)
            {
                dfsNum[k] = -1;
            }

            //end 1
            int ret = NON_BRIDGE;
            dfsNum[j] = curNum;
            curNum++;

            //Tạo G1 = G
            EulerCircuit g1 = new EulerCircuit(this.nV, this.adjencyMatrix);
            g1.removeEdge(i, j);
            ret = g1.dfsSearch(i, j, ref dfsNum, ref curNum, dfsNum[j], i);
            if (ret <= dfsNum[j])
            {
                return NON_BRIDGE;
            }
            else
                return BRIDGE;
        }


        //Ham duyet dfs, co xac dinh cau
        public int dfsSearch(int u, int p, ref int[] dfsNum, ref int curNum, int num_p, int startV)
        {
            int ret = 0;
            dfsNum[u] = curNum;
            int oldestAncestor = dfsNum[u] = curNum;
            curNum++;
            for (int j = 0; j < nV; j++)
            {
                if (adjencyMatrix[u, j] != 0)
                {
                    removeEdge(u, j);
                    //begin 2
                    if (dfsNum[j] == -1)
                    {
                        if (u == startV)
                        {
                            int t = 1;
                        }
                        ret = dfsSearch(j, u, ref dfsNum, ref curNum, num_p, startV);
                        if (u == startV)
                            if (ret <= num_p)
                            {
                                return NON_BRIDGE;
                            }
                        oldestAncestor = min(oldestAncestor, ret);
                    }

                    //end 2
                    else
                    //begin 3
                    {
                        oldestAncestor = min(oldestAncestor, dfsNum[j]);
                    }
                    //end 3 
                }
            }
            return oldestAncestor;
        }

        public int min(int a, int b)
        {
            if (a > b)
                return b;
            return a;
        }

        //Fleury algorithm
        public int runFleury(ref int[] eulerPath, ref int nVer)
        {
            int count = 0;
            int[] u = new int[100];
            //begin 4
            for (int i = 0; i < nV; i++)
            {
                if (getVertexDeg(i) % 2 != 0)
                {
                    u[count] = i;
                    count++;
                }
                if (count >0)
                {
                    return NOT_HAS_EULER_PATH;
                }
            }
            //end 4
            //Tạo G1 = G
            EulerCircuit g1 = new EulerCircuit(this.nV, this.adjencyMatrix);
            int ret;
            //begin 5
            if (count == 2)
            {
                ret = HAS_EULER_PATH;
                g1.addEdge(u[0], u[1]);
            }
            //end 5
            else
            {
                ret = HAS_EULER_CYCLE;
            }

            //Chọn đỉnh đầu tiên: P = {0}
            int startVertex = 0;
            nVer = 0;
            eulerPath[nVer] = startVertex;
            nVer++;
            int nE = getNumEdge();
            while (nVer - 1 < nE)
            {
                //Chọn 1 cạnh e=(startVertex,i) từ g1
                int bridgeEndV = -1;
                int flag = NOT_DONE;
                for (int i = 0; i < nV; i++)
                {
                    if (g1.getEdge(eulerPath[nVer - 1], i) != 0)
                    {
                        //begin 6
                        if (g1.bridgeDetection(eulerPath[nVer - 1], i) == NON_BRIDGE)
                        {
                            eulerPath[nVer] = i;
                            //Xóa cạnh khỏi G: G = G - e
                            g1.removeEdge(eulerPath[nVer - 1], i);
                            flag = DONE;
                            nVer++;
                            break;
                        }
                        else
                        {
                            bridgeEndV = i;
                        }
                        //end 6
                    }
                }
                //begin 7
                if (bridgeEndV != -1 && flag == NOT_DONE)
                {
                    eulerPath[nVer] = bridgeEndV;
                    //Xóa cạnh khỏi G: G = G - e
                    g1.removeEdge(eulerPath[nVer - 1], bridgeEndV);
                    nVer++;
                }
                //end 7
            }
            return ret;
        }
    }
}






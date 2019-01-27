using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenCLTemplate;

namespace CLKmeansTracking
{
    /// <summary>Computes Kmeans clusterization of a set of vectors</summary>
    public class CLKmeans
    {
        /// <summary>Were any values changed? int[0], {0} = 0, no, {0}=1, yes</summary>
        private CLCalc.Program.Variable CLChanged;

        /// <summary>Length of each vector</summary>
        private int vecLen = 0;
        /// <summary>Number of centers in OpenCL memory</summary>
        private CLCalc.Program.Variable CLvecLen;


        /// <summary>Number of vectors</summary>
        private int nVecs = 0;
        /// <summary>Number of centers in OpenCL memory</summary>
        private CLCalc.Program.Variable CLnVecs;

        /// <summary>Matrix containing all vectors, one per column: v[i].x = M[i + 0*nVecs], v[i].y = M[i + 1*nVecs], M[vecLen * nVecs]</summary>
        private float[] vecMatrix;
        /// <summary>Matrix containing all vectors in Device memory</summary>
        private CLCalc.Program.Variable CLvecMatrix;

        /// <summary>Attempted centers</summary>
        private float[] centers;

        /// <summary>Number of centers being tried</summary>
        private int nCenters;
        /// <summary>Number of centers in OpenCL memory</summary>
        private CLCalc.Program.Variable CLnCenters;


        /// <summary>Constructor.</summary>
        /// <param name="Vectors">Vectors to be clusterized</param>
        public CLKmeans(List<float[]> Vectors)
        {
            if (Vectors == null || Vectors.Count == 0)
                throw new Exception("No points to clusterize");

            vecLen = Vectors[0].Length;
            nVecs = Vectors.Count;

            vecMatrix = new float[vecLen*nVecs];

            for (int i = 0; i < nVecs; i++)
            {
                if (Vectors[i].Length != vecLen)
                    throw new Exception("float[] list contains vectors of different lengths");

                for (int j = 0; j < vecLen; j++)
                {
                    vecMatrix[i + j * nVecs] = Vectors[i][j];
                }
            }

            Init();
        }



        /// <summary>Initializes OpenCL and other variables</summary>
        private void Init()
        {
            if (CLCalc.CLAcceleration == CLCalc.CLAccelerationType.Unknown) CLCalc.InitCL();


            if (CLCalc.CLAcceleration == CLCalc.CLAccelerationType.UsingCL)
            {
                //Copies matrix to Device memory
                CLvecMatrix = new CLCalc.Program.Variable(vecMatrix);

                CLvecLen = new CLCalc.Program.Variable(new int[] { vecLen });
                CLnCenters = new CLCalc.Program.Variable(new int[] { 0 });
                CLnVecs = new CLCalc.Program.Variable(new int[] { nVecs });
                CLChanged = new CLCalc.Program.Variable(new int[] { 0 });

                if (kernelAssignToCenters == null)
                {
                    CLSrc src = new CLSrc();
                    CLCalc.Program.Compile(new string[] { src.srcClusterize });

                    kernelAssignToCenters = new CLCalc.Program.Kernel("AssignToCenters");
                }
                //CLClusterize();

                //CLAssignment.WriteToDevice(new int[nVecs]);
                //CLClusterize();
            }
            else
            {
                //Clusterize();
            }
        }

        #region OpenCL codes

        /// <summary>Assignment of each vector to its center</summary>
        CLCalc.Program.Variable CLAssignment;
        /// <summary>Center coordinates in Device memory</summary>
        CLCalc.Program.Variable CLCenters;
        /// <summary>Kernel to assign centers</summary>
        static CLCalc.Program.Kernel kernelAssignToCenters;

        private class CLSrc
        {
            public string srcClusterize = @"



//Manhattan distance
float dist(int indVec, int indCenter, int nVecs, int nCenters, int vecLen,
           __global const float * vecMatrix,
           __global const float * centers)

{
    float dd = 0;
    int iNVecs = 0;
    int iNCenters = 0;
    for (int i = 0; i < vecLen; i++)
    {
        float dif = fabs(vecMatrix[indVec + iNVecs] - centers[indCenter + iNCenters]);

        dd += dif;

        iNVecs += nVecs;
        iNCenters += nCenters;

    }
    return dd;
}

//Euclidean distance
float distEuc(int indVec, int indCenter, int nVecs, int nCenters, int vecLen,
              __global const float * vecMatrix,
              __global const float * centers)

{
    float dd = 0;
    int iNVecs = 0;
    int iNCenters = 0;
    for (int i = 0; i < vecLen; i++)
    {
        float dif = vecMatrix[indVec + iNVecs] - centers[indCenter + iNCenters];

        dd += dif*dif;

        iNVecs += nVecs;
        iNCenters += nCenters;

    }
    return dd;
}

//global_size = number of vectors nVecs
__kernel void AssignToCenters(__global const float * vecMatrix,
                              __global const float * centers,
                              __global       int *   assignment,
                              __constant     int *   numCenters,
                              __constant     int *   vectorLength,
                              __global       int *   changed)
{
    int i = get_global_id(0);
    int nVecs = get_global_size(0);
   
    int nCenters = numCenters[0];
    int vecLen = vectorLength[0];
   
   
    float dd = 1.0E30f;
    int indMin = -1;
    for (int k = 0; k < nCenters; k++)
    {
        float localD = distEuc(i, k, nVecs, nCenters, vecLen, vecMatrix, centers);
        indMin = (localD<dd) ? k : indMin;
        dd =     (localD<dd) ? localD : dd;

    }

    if (assignment[i] != indMin)
    {
        changed[0] = 1;
    }
    assignment[i] = indMin;
}

";
        }

        /// <summary>Clusterizes points using OpenCL. Disabled for now</summary>
        /// <param name="numCenters">Number of centers to use</param>
        /// <param name="assignment">Assigned centers</param>
        /// <param name="centerErrors">Total error per center</param>
        public List<float[]> CLClusterize(int numCenters, out int[] assignment, out float[] centerErrors)
        {
            if (CLCalc.CLAcceleration != CLCalc.CLAccelerationType.UsingCL) return Clusterize(numCenters, out assignment, out centerErrors);

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swAssignCL = new System.Diagnostics.Stopwatch();

            sw.Start();

            //Idea: compute clusters of 2, 3, 4, ... N to a point where the total volume increase is small
            //metric: sum of ln(dimensions)

            assignment = new int[nVecs];
            //Initializes assignment in Device memory
            if (CLAssignment == null || CLAssignment.OriginalVarLength != nVecs) CLAssignment = new CLCalc.Program.Variable(assignment);


            nCenters = numCenters;
            

            //Writes number of centers to device
            CLnCenters.WriteToDevice(new int[] { nCenters });

            centers = new float[vecLen * nCenters]; //c[i].x = centers[i + 0 * nCenters]
            if (CLCenters == null || CLCenters.OriginalVarLength != centers.Length) CLCenters = new CLCalc.Program.Variable(centers);


            //Step 0: randomly chooses centers
            Random rnd = new Random();
            for (int i = 0; i < nCenters; i++)
            {
                int ind = rnd.Next(nVecs);
                for (int j = 0; j < vecLen; j++) centers[i + j * nCenters] = vecMatrix[ind + j * nVecs] * (0.95f + 0.1f * (float)rnd.NextDouble());
            }

            CLCenters.WriteToDevice(centers);

            bool changed = true;
            for (int iter = 0; iter < 10000; iter++)
            {
                changed = false;

                //Step 1: assign
                swAssignCL.Start();
                CLCenters.WriteToDevice(centers);

                CLChanged.WriteToDevice(new int[1]);
                CLCalc.Program.Variable[] args = new CLCalc.Program.Variable[] { CLvecMatrix, CLCenters, CLAssignment, CLnCenters, CLvecLen, CLChanged };
                kernelAssignToCenters.Execute(args, nVecs);

                CLAssignment.ReadFromDeviceTo(assignment);

                int[] intChanged = new int[1]; CLChanged.ReadFromDeviceTo(intChanged);
                swAssignCL.Stop();

                changed = intChanged[0] == 1;

                if (changed)
                {
                    //Step 2: recompute centers
                    int[] nPtsInCenter = new int[nCenters];
                    for (int i = 0; i < centers.Length; i++) centers[i] = 0;

                    for (int i = 0; i < nVecs; i++)
                    {
                        nPtsInCenter[assignment[i]]++;

                        for (int j = 0; j < vecLen; j++)
                        {
                            centers[assignment[i] + j * nCenters] += vecMatrix[i + j * nVecs];
                        }
                    }

                    for (int i = 0; i < nCenters; i++)
                    {
                        if (nPtsInCenter[i] >= 0)
                        {
                            float temp = 1.0f / (float)nPtsInCenter[i];
                            for (int j = 0; j < vecLen; j++)
                            {
                                centers[i + j * nCenters] *= temp;
                            }
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    sw.Stop();
                    iter = 100005;
                }
            }

            //Builds centers
            List<float[]> ClusterCenters = new List<float[]>();

            for (int i = 0; i < nCenters; i++)
            {
                float[] cntr = new float[vecLen];
                for (int j = 0; j < vecLen; j++)
                {
                    cntr[j] = centers[i + j * nCenters];
                }
                ClusterCenters.Add(cntr);
            }


            centerErrors = new float[nCenters];
            for (int i = 0; i < nVecs; i++) centerErrors[assignment[i]] += dist(i, assignment[i]);

            return ClusterCenters;
        }

        #endregion

        /// <summary>Clusterizes points using a given number of centers</summary>
        /// <param name="numCenters">Desired number of centers</param>
        /// <param name="assignment">Assigned centers</param>
        /// <param name="centerErrors">Total error per center</param>
        public List<float[]> Clusterize(int numCenters, out int[] assignment, out float[] centerErrors)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swAssign = new System.Diagnostics.Stopwatch();

            sw.Start();


            //Idea: compute clusters of 2, 3, 4, ... N to a point where the total volume increase is small
            //metric: sum of ln(dimensions)

            assignment = new int[nVecs];

            nCenters = numCenters;
            centers = new float[vecLen * nCenters]; //c[i].x = centers[i + 0 * nCenters]
            centerErrors = new float[nCenters];

            //Step 0: randomly chooses centers
            Random rnd = new Random();
            for (int i = 0; i < nCenters; i++)
            {
                int ind = rnd.Next(nVecs);
                for (int j = 0; j < vecLen; j++) centers[i + j * nCenters] = vecMatrix[ind + j * nVecs] * (0.95f + 0.1f * (float)rnd.NextDouble());
            }

            bool changed = true;
            for (int iter = 0; iter < 1000; iter++)
            {
                changed = false;
                //Step 1: assign
                swAssign.Start();
                for (int i = 0; i < nVecs; i++)
                {
                    float d = 1.0E30f;
                    int indMin = -1;
                    for (int k = 0; k < nCenters; k++)
                    {
                        float localD = dist(i, k);
                        if (localD < d)
                        {
                            d = localD;
                            indMin = k;
                        }
                    }
                    centerErrors[indMin] += d;
                    if (assignment[i] != indMin)
                    {
                        assignment[i] = indMin;
                        changed = true;
                    }
                }
                swAssign.Stop();

                if (changed)
                {
                    //Step 2: recompute centers
                    int[] nPtsInCenter = new int[nCenters];
                    for (int i = 0; i < centers.Length; i++) centers[i] = 0;
                    for (int i = 0; i < nCenters; i++) centerErrors[i] = 0;

                    for (int i = 0; i < nVecs; i++)
                    {
                        nPtsInCenter[assignment[i]]++;

                        for (int j = 0; j < vecLen; j++)
                        {
                            centers[assignment[i] + j * nCenters] += vecMatrix[i + j * nVecs];
                        }
                    }

                    for (int i = 0; i < nCenters; i++)
                    {
                        float temp = 1.0f / (float)nPtsInCenter[i];
                        for (int j = 0; j < vecLen; j++)
                        {
                            centers[i + j * nCenters] *= temp;
                        }
                    }
                }
                else
                {
                    sw.Stop();
                    iter = 1005;
                }
            }

            //Builds centers
            List<float[]> ClusterCenters = new List<float[]>();

            for (int i = 0; i < nCenters; i++)
            {
                float[] cntr = new float[vecLen];
                for (int j = 0; j < vecLen; j++)
                {
                    cntr[j] = centers[i + j * nCenters];
                }
                ClusterCenters.Add(cntr);
            }

            return ClusterCenters;
        }

        /// <summary>Computes distance between two vectors stored in the vector matrix</summary>
        private float dist(int indVec, int indCenter)
        {
            float d = 0;
            //if (this.metric == Metric.Infinity)
            //{
            //    int iNVecs = 0;
            //    int iNCenters = 0;
            //    for (int i = 0; i < vecLen; i++)
            //    {
            //        float dif = Math.Abs(vecMatrix[indVec + iNVecs] - centers[indCenter + iNCenters]);

            //        d = Math.Max(dif, d);

            //        iNVecs += nVecs;
            //        iNCenters += nCenters;

            //    }
            //}
            //else if (this.metric == Metric.Euclidean)
            {
                int iNVecs = 0;
                int iNCenters = 0;

                for (int i = 0; i < vecLen; i++)
                {

                    float dif = vecMatrix[indVec + iNVecs] - centers[indCenter + iNCenters];

                    d += dif * dif;

                    iNVecs += nVecs;
                    iNCenters += nCenters;
                }

                d = (float)Math.Sqrt(d);
            }

            return d;
        }
    }
}

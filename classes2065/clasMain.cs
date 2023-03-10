using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Sparse;
using MathNet.Numerics.Transformations;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Management;

namespace Skydat.classes2065
{
    public class clasMain
    {
        public double[] AR = new double[16];
        public double[] BR = new double[16];
        public double[] CR = new double[16];
        public int tq = 0;
        public double CVE = 0;
        Matrix V = new Matrix(10, 10, 0);
        Matrix D = new Matrix(10, 10, 0);


        //public void Write_registry_Val(string key, string val)
        //{
        //    RegistryKey regkey; /* new Microsoft.Win32 Registry Key */
        //    regkey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Control Panel\Settings\stkady");
        //    regkey.SetValue(key, val); //"KeyProg"
        //}

        //public string Read_registry_Val(string key)
        //{
        //    RegistryKey regkey; /* new Microsoft.Win32 Registry Key */
        //    regkey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Control Panel\Settings\stkady");
        //    return (string)regkey.GetValue(key);
        //}

        public Matrix Diag(Matrix m)
        {
            Matrix r = new Matrix(m.RowCount, 1, 0);
            for (int i = 0; i < m.RowCount; i++)
                //for (int j = 0; j < m.ColumnCount; j++)
                //if (i == j)
                r[i, 0] = m[i, 0];   // dx = x((d + 1):m) - x(1:(m - d));
            return r;
        }

        public Matrix Cholesky(Matrix A) //Cholesy Decomposition
        {
            int n = A.RowCount;
            int i = 0, j = 0;
            for (i = 0; i < A.ColumnCount - 1; i++) // A = triu(A)
                for (j = i + 1; j < A.RowCount; j++)
                    A[j, i] = 0;
            int p = 0;
            double t = 0;
            double akj = 0;
            double ajj = 0;
            for (j = 0; j < n; j++)
            {
                //%s = real(A(j,j)) - A(1:j-1,j)' * A(1:j-1,j);
                if (j > 0)
                    for (int k = 0; k < j - 1; k++)
                    {
                        akj = A[k, j];
                        t = t + (akj * akj);  //t = t + real(akj)*real(akj) + imag(akj)*imag(akj)  -> for complex numbers
                    }
                ajj = A[j, j] - t;

                if (ajj <= 0)
                {
                    A[j, j] = ajj;
                    p = j;
                }
            }

            ajj = Math.Sqrt(ajj);
            A[j - 1, j - 1] = ajj;

            if (j < n)
            {               //%A(j,j+1:n) = A(j,j+1:n) - A(1:j-1,j)'*A(1:j-1,j+1:n);
                for (int k = j + 1; k < n; k++)
                {
                    double temp = 0;
                    if (j > 0)
                    {
                        for (int w = 0; w < j - 1; w++)
                            temp = temp + A[w, j] * A[w, k];
                        A[j, k] = (A[j, k] - temp) / ajj;
                    }
                    else
                    {
                        double tre = 0;  //, tim = 0, tim2 = 0;
                        if (j > 0)
                            for (int w = 0; w < j - 1; w++)
                            {
                                double awj = A[w, j];
                                double awk = A[w, k];
                                tre = tre + (awj * awk);
                                // + imag(awj) * imag(awk) //for complexnumber
                                //tim = tim + real(awj) * imag(awk)
                                //tim2 = tim2 + imag(awj) * real(awk)
                            }

                        A[j, k] = (A[j, k] /*- complex(tre,tim - tim2)*/) / ajj;
                    }
                }
            }
            return A;
        }

        public Matrix ddMat(Matrix X, int d) //SpDiags function
        {
            int mm = X.RowCount;
            Matrix dx = new Matrix(X.RowCount - d, X.ColumnCount, 0);
            if (d == 0)
                D = DefhermMatrix(mm);
            else
            {
                for (int i = d; i < mm; i++)
                    dx[i - d, 0] = X[i, 0] - X[i - d, 0];   // dx = x((d + 1):m) - x(1:(m - d));
                dx = NumDivMatrices(1, dx);
                V = Spdiags(dx, mm - d, mm - d);
                D = Multiply2Matrices(V, DifRows_Matrix(ddMat(X, d - 1)));   // V = spdiags(1 ./ dx, 0, m - d, m - d); // D = V * diff(ddmat(x, d - 1));
            }
            return D;
        }

        public Matrix Sparse(Matrix m1, int mm, int n) //Sparse function
        {
            int R_Max = 0, C_Max = 0;
            for (int k = 0; k < mm; k++)
            {
                if ((int)m1[k, 0] > R_Max) R_Max = (int)m1[k, 0];
                if ((int)m1[k, 1] > C_Max) C_Max = (int)m1[k, 1];
            }
            Matrix M = new Matrix(R_Max + 1, C_Max + 1, 0);
            int r = 0, c = 0;
            for (int k = 0; k < mm; k++)
            {
                r = (int)m1[k, 0];
                c = (int)m1[k, 1];
                M[r, c] = /*M[r, c] + */m1[k, 2];
            }
            return M;
        }

        public Matrix Spdiags(Matrix Y, int mm, int n) //Spdiags function
        {
            Matrix arg1 = Y;
            Matrix arg2 = new Matrix(1, 3, 0);
            //arg2[0, 0] = -1; arg2[0, 1] = 0; arg2[0, 2] = 1;
            int arg3 = 3;
            int arg4 = 3;
            int i = 0, j = 0;

            //   % Create a sparse matrix from its diagonals
            Matrix B = arg1;
            Matrix d = new Matrix(arg4, 1);// d= arg2(:)
            for (i = 0; i < arg2.RowCount; i++)
                for (j = 0; j < arg2.ColumnCount; j++)
                    d[(i * arg2.ColumnCount) + j, 0] = arg2[i, j];
            int p = d.RowCount;

            Matrix A = new Matrix(arg3, arg4);
            //% Start from scratch
            for (i = 0; i < arg3; i++)
                for (j = 0; j < arg4; j++)
                    A[i, j] = 0;  // = sparse(arg3,arg4)


           
            Matrix a = new Matrix(1, 3);
            int k = 0;
            for (i = 0; i < A.RowCount; i++)
                for (j = 0; j < A.ColumnCount; j++)
                {
                    if (A[i, j] != 0)
                    {
                        a[k, 1] = (i * A.RowCount + j);
                        a[k, 2] = j; //?????
                        a[k, 3] = A[i, j];
                    }
                }
            //m = A.RowCount;   //[m,n] = size(A) //
            //n = A.ColumnCount;
            double max = 0, min = 0;
            Matrix len = new Matrix(p + 1, 1, 0);   //len=zeros(p+1,1) // make matrix p+1 by 1 with zero
            for (k = 0; k < p; k++)                  // for k = 1:p   
            {                                    // len(k+1) = len(k)+length(max(1,1-d(k)):min(m,n-d(k)));
                if (1 - d[k, 0] > 1) max = 1 - d[k, 0];   //max=max(1,1-d(k))
                else max = 1;

                if (mm < n - d[k, 0]) min = mm;      //max=min(m,n-d(k))
                else min = n - d[k, 0];
                if (max <= min) max = min - max + 1;
                else max = 0;
                len[k + 1, 0] = len[k, 0] + max;   //length(max(1,1-d(k)):min(m,n-d(k)))
            }
            // if not moda
            a = new Matrix((int)(len[1, 0]), 3, 0);  //a = zeros(len(p+1),3);
            k = 0;
            if (1 - d[k, 0] > 1) max = 1 - d[k, 0];   //max=max(1,1-d(k))
            else max = 1;

            if (mm < n - d[k, 0]) min = mm;      //max=min(m,n-d(k))
            else min = n - d[k, 0];
            Matrix ii = new Matrix(1, (int)Math.Abs(min - max + 1));
            for (i = 0; i < min - max + 1; i++)  //       % Append new d(k)-th diagonal to compact form
                ii[0, i] = max + i - 1;     //     ii = (max(1,1-d(k)):min(m,n-d(k)))';
            int ll = 0;
            for (int jj = (int)len[k, 0]; jj < (int)len[k + 1, 0]; jj++)
            {
                a[jj, 0] = ii[0, ll];
                a[jj, 1] = ii[0, ll] + d[k, 0];
                int nn = 0;
                if (mm >= n) nn = 1;
                a[jj, 2] = Math.Round(B[(int)(ii[0, ll] + (nn * d[k, 0])), 0]);
                ll++;
            }

            //Matrix res1 = sparse((a(:,1),a(:,2),full(a(:,3)),m,n);
            a = Sparse(a, a.RowCount, 3);
            arg1 = a;

            return arg1;
        }

        public void Whittaker(double[] X, double[] Y, Matrix w, double lambda, int d)
        {
           
            Matrix x = new Matrix(X, X.Length);
            Matrix y = new Matrix(Y, Y.Length);
            int mm = Y.Length;
            Matrix E = DefhermMatrix(mm);
            D = ddMat(x, d);
            Matrix Zero = new Matrix(mm, 1, 0);
            Matrix W = Spdiags(w, mm, mm);      //Matrix C = W+ lambda*Matrix.Transpose(D)*D; 
            //Matrix DD = ;
            Matrix C = Add2Matrices(W, NumMultiplyMatrices(lambda, Multiply2Matrices(Matrix.Transpose(D), D))); //W + lambda * D' * D
            CholeskyDecomposition CC = new CholeskyDecomposition(C);
            C = Matrix.Transpose(CC.GetL());  //C = Cholesky(C);
            double mmm = C[0, 0];
            for (int i = 0; i < C.RowCount; i++)
                for (int j = 0; j < C.ColumnCount; j++)
                    if (C[i, j] > mmm) mmm = C[i, j];
            //mmm = max(max(C));
            C = DivNumMatrices(C, mmm);    //C = C./mmm;
            Matrix wy = DotMultiplyMatrices(w, y);
            Matrix cprimwy = BSlashDiv2Matrices(Matrix.Transpose(C), wy);
            Matrix z = BSlashDiv2Matrices(C, /*Matrix.Transpose(*/cprimwy);  // C \ (C' \ (w .*y)
            z = DivNumMatrices(z, mmm * mmm);   //z = z./(mmm^2);
            C = NumMultiplyMatrices(mmm, C);     //C = C*mmm;
            double[] ret = new double[z.RowCount * z.ColumnCount];
            for (int i = 0; i < z.RowCount; i++)
                for (int j = 0; j < z.ColumnCount; j++)
                    ret[i * z.ColumnCount + j] = z[i, j];

            // Computation of hat diagonal and cross-validation    if nargout > 1
            if (mm <= 100)    //    % Exact hat diagonal
            {
                W = Diag(w);
                Matrix H = Add2Matrices(W, NumMultiplyMatrices(lambda, Multiply2Matrices(Matrix.Transpose(D), D)));        //(W + lambda * D' * D) \ W;
                H = BSlashDiv2Matrices(H, W);
                Matrix h = Diag(H);
            }
            else           // Map to diag(H) for n = 100
            {
                int n = 100;
                Matrix E1 = DefhermMatrix(n);
                Matrix g = new Matrix(n - 1, 1, 0);
                double[] xx = new double[n];
                double[] ww = new double[n];
                for (int i = 0; i < n - 1; i++)
                {
                    g[i, 0] = (int)Math.Round((double)(i * (mm - 1) / (n - 1) + 1));       //round(((1:n) - 1) * (m - 1) / (n - 1) + 1);
                    xx[i] = x[(int)g[i, 0], 0];
                    ww[i] = w[(int)g[i, 0], 0];
                }
                Matrix xxx = new Matrix(xx, n);
                Matrix www = new Matrix(ww, n);
                Matrix D1 = ddMat(xxx, d);
                Matrix W1 = Diag(www);
                double lambda1 = lambda * Math.Pow((double)n / (double)mm, 2 * d);
                Matrix H1 = Add2Matrices(W1, NumMultiplyMatrices(lambda, Multiply2Matrices(Matrix.Transpose(D), D)));   //;inv(W1 + lambda1 * D1' * D1);
                H1 = H1.Inverse();
                Matrix h1 = Diag(H1);
                Matrix u = new Matrix(mm, 1, 0);   // zeros(m,1)
                int k = (int)Math.Floor((double)((double)mm / 2));   // k = floor(m/2)
                int k1 = (int)Math.Floor((double)((double)n / 2));  // k1 = floor(n/2) 
                u[k - 1, 0] = 1;    //  A\B = inv(A)*B if A is square      
                Matrix v = BSlashDiv2Matrices(C, BSlashDiv2Matrices(Matrix.Transpose(C), u));            //C \ (C' \ u);
                double hk = v[k, 0];

                Matrix f = new Matrix(1, mm, 0);
                Matrix h1h1 = new Matrix(1, mm, 0);
                for (int i = 0; i < mm - 1; i++)
                {
                    f[0, i] = (int)Math.Round((double)(i * (n - 1) / (mm - 1) + 1));       //round(((1:m)' - 1) * (m - 1) / (n - 1) + 1);
                    h1h1[0, i] = h1[(int)f[0, i], 0];
                }
                Matrix h = DotMultiplyMatrices(w, h1h1);
                h = NumMultiplyMatrices(v[k, 0] / h1[k1, 0], h);    //w .* h1(f) * v(k) / h1(k1);
                W = h;
            }
            Matrix rr = DotSlashDiv2Matrices(Minus2Matrices(y, z), NumMinusMatrice(1, W));     //rr = (y - z) ./ (1 - h);
            Matrix cve = Multiply2Matrices(Matrix.Transpose(rr), DotMultiplyMatrices(w, rr));
            cve = SlashDiv2Matrices(cve, SumMatrice(w));
            cve = SqrtMatrice(cve);    // cve = sqrt(rr' * (w .* rr) / sum(w));
            CVE = cve[0, 0];
            AR = X;
            BR = ret;
        }

        public void Whittaker_Smooth(double[] x, double[] y, double zLambda)
        {
            double maxx = x[0];
            for (int i = 1; i < x.Length; i++)
                if (maxx < x[i]) maxx = x[i];
            for (int i = 0; i < x.Length; i++)
                if (maxx != 0)
                    x[i] = x[i] / maxx;
            for (int i = 0; i < x.Length - 1; i++)
                if (x[i] == x[i + 1])
                {
                    x[i + 1] = x[i + 1] + (i * 0.0000001);
                    y[i + 1] = y[i + 1] + (i * 0.0000001);
                }
            Random r = new Random(10);
            Matrix w = new Matrix(y.Length, 1, 0);
            for (int k = 0; k < y.Length; k++)
            {
                w[k, 0] = Math.Round(r.NextDouble());
            }
            double FLamdda = -6;
            double ELamdda = 0;
            double QLamdda = zLambda;

            int num = (int)((ELamdda - FLamdda) / QLamdda);
            Matrix cvs = new Matrix(num, 1, 0);
            int iw = 0;
            for (double l = FLamdda; l < ELamdda; l += QLamdda)    //10 .^ (-6:0.2:0);
            {
                double lambda = Math.Pow(10, l);
                if (iw < num)
                {
                    try
                    {
                        Whittaker(x, y, w, lambda, 2);
                        cvs[iw, 0] = CVE;
                    }
                    catch { }

                }
                iw++;
            }

            // Choose optimal lambda
            double cm = cvs[0, 0];
            int ci = 0;
            for (iw = 1; iw < cvs.RowCount; iw++)
                if (cm > cvs[iw, 0]) { cm = cvs[iw, 0]; ci = iw; }      //[cm ci] = min(cvs);
            double lamda = Math.Pow(10, (-6 + (0.2 * ci))); //lambda = lambdas(ci);
            Whittaker(x, y, w, lamda, 2);  //0.00000630957344480193;
            for (int i = 0; i < x.Length; i++)
                if (maxx != 0)
                    x[i] = x[i] * maxx;
            AR = x;
        }
        public Matrix DifRows_Matrix(Matrix dif)
        {
            Matrix df = new Matrix(dif.RowCount - 1, dif.ColumnCount, 0);

            for (int i = 0; i < dif.RowCount - 1; i++)
                for (int j = 0; j < dif.ColumnCount; j++)
                    df[i, j] = dif[i + 1, j] - dif[i, j];
            return df;
        }

        public Matrix DefhermMatrix(int d)
        {
            Matrix hm = new Matrix(d, d, 0);
            for (int i = 0; i < d; i++)
                for (int j = 0; j < d; j++)
                    if (i == j)
                        hm[i, j] = 1;
            return hm;
        }

        public Matrix SumMatrice(Matrix mm)
        {
            Matrix r = new Matrix(1, mm.ColumnCount, 0);
            for (int i = 0; i < mm.ColumnCount; i++)
                for (int j = 0; j < mm.RowCount; j++)
                    r[0, i] = r[0, i] + mm[j, i];
            return r;
        }

        public Matrix Add2Matrices(Matrix m1, Matrix m2)
        {
            int R = 0, C = 0;
            if (m1.RowCount > m2.RowCount) R = m1.RowCount;
            else R = m2.RowCount;
            if (m1.ColumnCount > m2.ColumnCount) C = m1.ColumnCount;
            else C = m2.ColumnCount;
            Matrix r1 = new Matrix(R, C, 0);
            Matrix r2 = new Matrix(R, C, 0);
            Matrix r3 = new Matrix(R, C, 0);

            for (int i = 0; i < m1.RowCount; i++)
                for (int j = 0; j < m1.ColumnCount; j++)
                    r1[i, j] = m1[i, j];
            for (int i = 0; i < m2.RowCount; i++)
                for (int j = 0; j < m2.ColumnCount; j++)
                    r2[i, j] = m2[i, j];
            Matrix r = new Matrix(R, C, 0);

            for (int i = 0; i < r1.RowCount; i++)
                for (int j = 0; j < r1.ColumnCount; j++)
                    r[i, j] = r1[i, j] + r2[i, j];
            return r;
        }

        public Matrix Minus2Matrices(Matrix m1, Matrix m2)
        {
            Matrix r = new Matrix(m1.RowCount, m1.ColumnCount, 0);
            if (m1.RowCount == m2.ColumnCount && m1.ColumnCount == m2.RowCount)
                m2 = Matrix.Transpose(m2);
            if (m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount)
                for (int i = 0; i < m1.RowCount; i++)
                    for (int j = 0; j < m1.ColumnCount; j++)
                        r[i, j] = m1[i, j] - m2[i, j];
            return r;
        }

        public Matrix NumMinusMatrice(double Num, Matrix x)
        {
            for (int i = 0; i < x.RowCount; i++)
                for (int j = 0; j < x.ColumnCount; j++)
                    x[i, j] = x[i, j] - Num;
            return x;
        }

        public Matrix SqrtMatrice(Matrix mm)
        {
            for (int i = 0; i < mm.RowCount; i++)
                for (int j = 0; j < mm.ColumnCount; j++)
                    if (mm[i, j] > 0)
                        mm[i, j] = Math.Sqrt(mm[i, j]);
                    else
                        mm[i, j] = 0;
            return mm;
        }

        public Matrix NumMultiplyMatrices(double Num, Matrix x)
        {
            for (int i = 0; i < x.RowCount; i++)
                for (int j = 0; j < x.ColumnCount; j++)
                    x[i, j] = Num * x[i, j];
            return x;
        }

        public Matrix Multiply2Matrices(Matrix m1, Matrix m2)
        {
            int R = 0;
            R = m2.RowCount;
            if (m1.ColumnCount != m2.RowCount)
                if (m1.RowCount == m2.ColumnCount)
                    m2 = Matrix.Transpose(m2);
            Matrix r3 = new Matrix(m1.RowCount, m2.ColumnCount, 0);
            if (m1.ColumnCount == m2.RowCount)
                for (int i = 0; i < m1.RowCount; i++)
                    for (int j = 0; j < m2.ColumnCount; j++)
                        for (int k = 0; k < m2.RowCount; k++)
                            r3[i, j] += (m1[i, k] * m2[k, j]);
            return r3;
        }

        public Matrix DotMultiplyMatrices(Matrix m1, Matrix m2)
        {
            Matrix r = new Matrix(m1.RowCount, m1.ColumnCount, 0);
            if (m1.RowCount == m2.ColumnCount && m1.ColumnCount == m2.RowCount)
                m2 = Matrix.Transpose(m2);
            if (m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount)
                for (int i = 0; i < m1.RowCount; i++)
                    for (int j = 0; j < m1.ColumnCount; j++)
                        r[i, j] = m1[i, j] * m2[i, j];
            return r;
        }

        public Matrix NumDivMatrices(double num, Matrix mm)
        {
            for (int i = 0; i < mm.RowCount; i++)
                for (int j = 0; j < mm.ColumnCount; j++)
                    if (mm[i, j] != 0)
                        mm[i, j] = num / mm[i, j];  //for \ operator
                    else
                        mm[i, j] = 0;
            return mm;
        }

        public Matrix DivNumMatrices(Matrix mm, double num)
        {
            for (int i = 0; i < mm.RowCount; i++)
                for (int j = 0; j < mm.ColumnCount; j++)
                    if (num != 0)
                        mm[i, j] = mm[i, j] / num;  //for / operator
                    else
                        mm[i, j] = 0;
            return mm;
        }

        public Matrix CoFactor(Matrix T)
        {
            Matrix T1 = new Matrix(T.RowCount, T.ColumnCount);
            Matrix c = new Matrix(T.ColumnCount, T.ColumnCount, 0);
            int i = 0, j = 0, ii = 0, jj = 0, i1 = 0, j1 = 0;

            for (j = 0; j < T.RowCount; j++)
            {
                for (i = 0; i < T.ColumnCount; i++)
                {
                    for (ii = 0; ii < T.ColumnCount; ii++)
                    {
                        if (ii == i)
                            continue;
                        j1 = 0;
                        for (jj = 0; jj < T.ColumnCount; jj++)
                        {
                            if (jj == j)
                                continue;
                            c[i1, j1] = T[ii, jj];
                            j1++;
                        }
                        i1++;
                    }

                    /* Calculate the determinate */
                    double det = c.Determinant();

                    /* Fill in the elements of the cofactor */
                    T1[i, j] = (int)Math.Pow(-1.0, i + j + 2) * det;
                }
            }
            return T1;
        }

        public Matrix SlashDiv2Matrices(Matrix m1, Matrix m2)    // slash (m1/m2)
        {
            Matrix r = new Matrix(1, 1, 0);
            try
            {
                Matrix rev = m2.Inverse();
                r = Multiply2Matrices(m1, m2.Inverse());
            }
            catch
            {
                r = (Matrix.Transpose(BSlashDiv2Matrices(Matrix.Transpose(m1), Matrix.Transpose(m2))));
            }
            return r;
        }

        public Matrix BSlashDiv2Matrices(Matrix m1, Matrix m2)   // back slash (m1\m2)
        {
            if (m1.ColumnCount == m2.ColumnCount && m1.ColumnCount != m2.RowCount)
                m2 = Matrix.Transpose(m2);
            Matrix r = Multiply2Matrices(m1.Inverse(), m2);
            return r;
        }

        public Matrix DotSlashDiv2Matrices(Matrix m1, Matrix m2)    // ./ (m1./m2)
        {
            Matrix r = new Matrix(m1.RowCount, m1.ColumnCount, 0);
            if (m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount)
                for (int i = 0; i < m1.RowCount; i++)
                    for (int j = 0; j < m1.ColumnCount; j++)
                        if (m2[i, j] != 0)
                            r[i, j] = m1[i, j] / m2[i, j];
            return r;
        }

        public Matrix DotBSlashDivMatrices(Matrix m1, Matrix m2)    // .\ (m1.\m2)
        {
            Matrix r = new Matrix(m1.RowCount, m1.ColumnCount, 0);
            if (m1.RowCount == m2.RowCount && m1.ColumnCount == m2.ColumnCount)
                for (int i = 0; i < m1.RowCount; i++)
                    for (int j = 0; j < m1.ColumnCount; j++)
                        if (m1[i, j] != 0)
                            r[i, j] = m2[i, j] / m1[i, j];
            return r;
        }

        public void ncube_SPLine(double[] x, double[] y)
        {
            int n = x.Length;
            double[] alpha = new double[n];
            double[] l = new double[n];
            double[] u = new double[n];
            double[] z = new double[n];
            u[0] = 0; z[0] = 0; l[0] = 0;
            for (int i = 1; i < n - 1; i++)
                alpha[i - 1] = 3 * ((y[i + 1] * (x[i] - x[i - 1])) - (y[i] * (x[i + 1] - x[i - 1])) + (y[i - 1] * (x[i + 1] - x[i]))) / ((x[i + 1] - x[i]) * (x[i] - x[i - 1]));
            for (int i = 1; i < n - 1; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - (u[i - 1] * (x[i] - x[i - 1]));
                u[i] = (1 / l[i]) * (x[i + 1] - x[i]);
                z[i] = (1 / l[i]) * (alpha[i] - (x[i] - x[i - 1]) * z[i - 1]);
            }
            double[] c = new double[n];
            double[] b = new double[n];
            double[] d = new double[n];
            c[n - 1] = z[n - 1];
            z[n - 1] = 0; l[n - 1] = 1;
            for (int j = n - 2; j >= 0; j--)
            {
                c[j] = z[j] - (u[j] * c[j + 1]);
                b[j] = ((y[j + 1] - y[j]) / (x[j + 1] - x[j])) - (((x[j + 1] - x[j]) * (c[j + 1] + (2 * c[j]))) / 3);
                d[j] = (c[j + 1] - c[j]) / (3 * (x[j + 1] - x[j]));
            }
            double[] S = new double[n];
            double sum = 0;
            for (int j = 0; j < x.Length - 1; j++)
                S[j] = sum + y[j] + (b[j] * (x[j + 1] - x[j])) + (c[j] * Math.Pow(x[j + 1] - x[j], 2)) + (d[j] * Math.Pow(x[j + 1] - x[j], 3));
            AR = x;
            BR = S;
        }

        public int idxMinArrayVal(double[] X)
        {
            int min = 0;
            for (int i = 0; i < X.Length; i++)
                if (X[i] < min) min = i;
            return min;
        }

        public int idxMaxArrayVal(double[] X)
        {
            int max = 0;
            for (int i = 0; i < X.Length; i++)
                if (X[i] > max) max = i;
            return max;
        }

        public void Peak_Points(short step, double[] A, double[] B)
        {
            double[] R = new double[A.Length];
            for (int i = 0; i < A.Length - 1; i++)
                if (A[i + 1] - A[i] != 0)
                    R[i] = ((B[i + 1] - B[i]) / (A[i + 1] - A[i])) * 100000;
            AR = A;
            BR = R;
        }
        public void smoothAverage(short step, double[] A, double[] B, bool is_XValues,int start_index)
        {
            // step 0-> 3    1-> 5 point    2-> 9 point    3-> 15 point    4-> 23 point
            /* for (int i = 0; i < A.Length-1; i++)
             {
                 if (B[i + 1] == 0) B[i + 1] = B[i];

                 if (A[i] == A[i + 1]) A[i + 1] += i * 0.000000001;
                 if (B[i] == B[i + 1]) B[i + 1] += i * 0.000000001;
             }*/
            if (A.Length > 2)
            {
                double[] BB = B;

                int num_of_avg = (step * step) + step + 3;
                if (step == 4) num_of_avg = 15;
                double[] Resultx = new double[A.Length]; // -num_of_avg
                double[] Resulty = new double[A.Length]; // -num_of_avg

                if (step >= 0 && A.Length > num_of_avg)
                {
                    for (int i = 0; i < start_index; i++)
                    {
                        Resultx[i] = A[i];
                        Resulty[i] = B[i];
                    }
                    for (int i = start_index; i < A.Length - num_of_avg; i++)
                    {
                        double sum = 0;
                        if (is_XValues)
                        {
                            for (int j = i; j < i + num_of_avg; j++)
                                sum = sum + A[j];
                            Resultx[i] = sum / num_of_avg;
                        }
                        sum = 0;
                        for (int j = i; j < i + num_of_avg; j++)
                            sum = sum + B[j];
                        Resulty[i] = sum / num_of_avg;
                    }
                    //if (num_of_avg > 3)
                        for (int i = A.Length - num_of_avg; i < A.Length; i++)
                        {

                            double sum = 0;
                            if (is_XValues)
                            {
                                for (int j = i; j < A.Length; j++)
                                    sum = sum + A[j];
                                if (A.Length - i != 0)
                                    Resultx[i] = sum / (A.Length - i);
                            }
                            sum = 0;
                            for (int j = i; j < B.Length; j++)
                                sum = sum + B[j];
                            if (B.Length - i != 0)
                                Resulty[i] = sum / (B.Length - i);
                        }
                    Resultx[A.Length - 1] = A[A.Length - 1];
                    Resulty[A.Length - 1] = B[A.Length - 1];
                }
                if (is_XValues)
                    AR = Resultx;
                else
                    AR = A;
                BR = Resulty;
            }
        }

        public void fourier(double[] A, double[] B)
        {
            double[] t = B;
            double[] ax = A;
            double[] ay = new double[A.Length];
            int i = 0, j = 0;

            double sum1 = 0, sum2 = 0;
            double[] w = new double[A.Length];
            double[] b = new double[A.Length];
            double[] a = new double[A.Length];
            double[] c = new double[A.Length];
            w[0] = 0;
            double W = 0;
            for (i = 1; i < A.Length; i++)
            {
                W += 0.001;
                w[i] = w[i - 1] + 0.001;
                sum1 = sum2 = 0;
                for (j = 0; j < A.Length - 1; j++)
                {
                    sum1 += (ax[j] * Math.Cos(W * t[j]) * (t[j + 1] - t[j]));
                    sum2 += (ax[j] * Math.Sin(W * t[j]) * (t[j + 1] - t[j]));
                }
                a[i] = sum1 / Math.PI;
                b[i] = sum2 / Math.PI;
                c[i] = Math.Sqrt((a[i] * a[i]) + (b[i] * b[i]));
            }
            W = 0;
            for (i = 0; i < A.Length; i++)
            {
                W += 0.001;
                sum1 = 0;
                for (j = 0; j < A.Length; j++)
                {
                    //sum1 += (((a[j] * Math.Cos(w[j] * t[i]) * 0.1) + (b[j] * Math.Sin(w[j] * t[i]) * 0.1)) * c[i]);
                    sum1 += (((a[i] * Math.Cos(W * t[j])) + (b[i] * Math.Sin(W * t[j]))) * c[i]);
                }
                ay[i] = sum1;
            }


            /*double[] c = new double[A.Length];
             int j = 0, k = 0;


             double[] sumCos = new double[12000];
             double[] sumSin = new double[12000];
             double[] sum = new double[12000];
             double total = 0;
             double[] omegaVal = new double[12000];
             double[] f = new double[A.Length];

             for (double omega = 0.01; omega < 52; omega += 0.005)
             {
                 omegaVal[k] = omega;
                 for (j = 0; j < A.Length - 2; j++)
                 {
                     sumCos[k] = sumCos[k] + ((B[j] * Math.Cos(omega * A[j]) * (A[j + 1] - A[j])) / Math.PI);
                     sumSin[k] = sumSin[k] + ((B[j] * Math.Sin(omega * A[j]) * (A[j + 1] - A[j])) / Math.PI);
                 }
                 sum[k] = Math.Sqrt(Math.Pow(sumCos[k], 2) + Math.Pow(sumSin[k], 2));
                 k++;
             }

             for (int i = 0; i < A.Length; i++)
             {
                 total = 0;
                 for (int l = 0; omegaVal[l] < 50; l++)
                     total = total + (B[l]*Math.Cos(omegaVal[l]*A[i])*0.1) + (A[l] * Math.Sin(omegaVal[l] * A[j]) + sumSin[l] * Math.Sin(omegaVal[l] * A[j]));
 //                total = total + (omegaVal[l + 1] - omegaVal[l]) * (sumCos[l] * Math.Cos(omegaVal[l] * A[j]) + sumSin[l] * Math.Sin(omegaVal[l] * A[j]));
                 f[j] = total;
             }*/
            AR = A;
            BR = ay;
        }
        /*for(i=1;i<=length;i++)
       {
       sum1=0;
          for(j=1;j<=1000;j++)
          {
          sum1=sum1+a[j]*Cos(w[j]*t[i])*0.1+b[j]*sin(w[j]*t[i])*0.1;
          }
       ay[i]=sum1;
       }
       */
        public void fourier(bool inverse, double[] A, double[] B, double Frequency, double omegaStart, double omegaStep)
        {
           // double temp = 0;
            // if (tq != 5)
            /*for (int i = 0; i < A.Length / 2; i++)
            {
                temp = A[i];
                A[i] = A[A.Length - i - 1];
                A[A.Length - i - 1] = temp;
                temp = B[i];
                B[i] = B[A.Length - i - 1];
                B[B.Length - i - 1] = temp;
            }*/
            //if (col == 0) col = 1;
            double[] c = new double[A.Length];
            int j = 0, k = 0;
            if (omegaStep == 0) omegaStep = 1;
            int len = (int)((Frequency + 5 - omegaStart) / omegaStep);

            double[] sumCos = new double[len];
            double[] sumSin = new double[len];
            double[] sum = new double[len];
            double total = 0;
            double[] omegaVal = new double[len];
            double[] f = new double[A.Length];
            //for (int kk = 0; kk < col; kk++)
            //{
            for (j = 0; j < len; j++)
            {
                sumCos[j] = 0;
                sumSin[j] = 0;
                sum[j] = 0;
                omegaVal[j] = 0;
            }
            //omegaStart = 0.01     omegaStep = 0.005 frequency = 50
            for (double omega = omegaStart; omega < Frequency + 2; omega += omegaStep)
            {
                omegaVal[k] = omega;
                for (j = 0; j < A.Length - 10; j++)
                {
                    sumCos[k] = sumCos[k] + ((B[j] * Math.Cos(omegaVal[k] * A[j]) * (A[j + 1] - A[j])) / Math.PI);
                    sumSin[k] = sumSin[k] + ((B[j] * Math.Sin(omegaVal[k] * A[j]) * (A[j + 1] - A[j])) / Math.PI);
                }
                sum[k] = Math.Sqrt(Math.Pow(sumCos[k], 2) + Math.Pow(sumSin[k], 2));
                k++;
            }

            for (j = 0; j < A.Length; j++)
            {
                total = 0;
                for (int l = 0; omegaVal[l] < Frequency; l++)
                    if (l < omegaVal.Length)
                        total = total + (omegaStep * sumCos[l] * Math.Cos(omegaVal[l] * A[j])) + (omegaStep * sumSin[l] * Math.Sin(omegaVal[l] * A[j]));
                //sum1=sum1+SumCos[l]*sin(Omega[l]*A[j])*0.1+SumSin[l]*sin(Omega[l]*A[j])*0.1
                if (inverse)
                    f[j] = total * (-1);
                else
                    f[j] = total;
            }
            // }
            AR = A;
            BR = f;
        }

        public void Integral(double[] X, double[] Y, double lowLimit, double highLimit)
        {
            double[] result = new double[X.Length];
            if (X.Length > 2)
            {
                double h = (highLimit - lowLimit) / X.Length;
                double sum = 0;
                for (int i = 0; i < (X.Length); i++)
                {
                    sum = 0;
                    for (int j = 1; j < i - 1; j++)
                        sum = sum + (Y[j - 1] * (X[j] - X[j - 1]) + (((Y[j] - Y[j - 1]) * (X[j] - X[j - 1])) / 2));

                    /*sum += Y[0] + Y[i];
                    sum /= 2;
                    sum *= h;*/
                    result[i] = sum;
                }
            }
            AR = X;
            BR = result;
        }

        public void Derivate(double[] x, double[] y)
        {
            /*for (int i = 0; i < x.Length - 1; i++)
                if (x[i] == x[i + 1])
                {
                    x[i + 1] = x[i + 1] + (i * 0.0000001);
                    y[i + 1] = y[i + 1] + (i * 0.0000001);
                }*/
            double[] result = new double[x.Length];
            if (x.Length > 2)
            {                               // / 2
                if (tq == 5)
                {
                    for (int i = 1; i < (x.Length / 2) - 2; i++)
                        if ((x[i + 1] - x[i]) != 0 && (x[i] - x[i - 1]) != 0)
                            result[i] = (0.5 * ((y[i + 1] - y[i]) / (x[i + 1] - x[i]))) + (0.5 * ((y[i] - y[i - 1]) / (x[i] - x[i - 1])));
                    for (int i = x.Length / 2 - 1; i < x.Length - 2; i++)
                        result[i] = ((0.5 * ((y[i + 1] - y[i]) / (x[i + 1] - x[i]))) + (0.5 * ((y[i] - y[i - 1]) / (x[i] - x[i - 1])))) * (-1);
                }
                else
                    for (int i = 1; i < (x.Length - 2); i++)
                        if ((x[i + 1] - x[i]) != 0 && (x[i] - x[i - 1]) != 0)
                            result[i] = (0.5 * ((y[i + 1] - y[i]) / (x[i + 1] - x[i]))) + (0.5 * ((y[i] - y[i - 1]) / (x[i] - x[i - 1])));
            }
            AR = x;
            BR = result;
        }

        public void PeakFind(double[] X, double[] Y, double PeakHeight, int minu_plus)
        {
            Peak_Points(100, X, Y);

        }

        public void Math_Operation(double[] X, double[] Y, int XorY, string Operation, double opValue)
        {
            double[] resultx = new double[X.Length];
            double[] resulty = new double[Y.Length];
            switch (Operation)
            {
                case "Add": for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) resultx[i] = X[i] + opValue;
                        if (XorY == 1) resulty[i] = Y[i] + opValue;
                        if (XorY == 2) { resultx[i] = X[i] + opValue; resulty[i] = Y[i] + opValue; }
                    }
                    break;
                case "Mul": for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) resultx[i] = X[i] * opValue;
                        if (XorY == 1) resulty[i] = Y[i] * opValue;
                        if (XorY == 2) { resultx[i] = X[i] * opValue; resulty[i] = Y[i] * opValue; }
                    }
                    break;
                case "Sub": for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) resultx[i] = X[i] - opValue;
                        if (XorY == 1) resulty[i] = Y[i] - opValue;
                        if (XorY == 2) { resultx[i] = X[i] - opValue; resulty[i] = Y[i] - opValue; }
                    }
                    break;
                case "Div": if (opValue != 0)
                        for (int i = 0; i < X.Length; i++)
                        {
                            if (XorY == 0)
                                if (opValue != 0) resultx[i] = X[i] / opValue;
                            if (XorY == 1)
                                if (opValue != 0) resulty[i] = Y[i] / opValue;
                            if (XorY == 2)
                                if (opValue != 0)
                                { resultx[i] = X[i] / opValue; resulty[i] = Y[i] / opValue; }
                        }
                    break;
                case "Exp": for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) resultx[i] = Math.Pow(X[i], opValue);
                        if (XorY == 1) resulty[i] = Math.Pow(Y[i], opValue);
                        if (XorY == 2) { resultx[i] = Math.Pow(X[i], opValue); resulty[i] = Math.Pow(Y[i], opValue); }
                    }
                    break;
                case "Log": if (opValue > 0)
                        for (int i = 0; i < X.Length; i++)
                        {
                            if (XorY == 0) if (X[i] != 0) resultx[i] = Math.Log(Math.Abs(X[i]), opValue);
                            if (XorY == 1) if (Y[i] != 0) resulty[i] = Math.Log(Math.Abs(Y[i]), opValue);
                            if (XorY == 2)
                            {
                                if (X[i] != 0) resultx[i] = Math.Log(Math.Abs(X[i]), opValue);
                                if (Y[i] != 0) resulty[i] = Math.Log(Math.Abs(Y[i]), opValue);
                            }
                        }
                    break;
                case "Tafel":
                      
                    for (int i = 0; i < X.Length; i++)
                        {
                            if (Y[i] != 0) 
                                resulty[i] =( Math.Log(Math.Abs(Y[i]),opValue));
                           
                        }
                    break;
                case "Sqr": for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) resultx[i] = X[i] * X[i];
                        if (XorY == 1) resulty[i] = Y[i] * Y[i];
                        if (XorY == 2) { resultx[i] = X[i] * X[i]; resulty[i] = Y[i] * Y[i]; }
                    }
                    break;
                case "Sqrt":
                    for (int i = 0; i < X.Length; i++)
                    {
                        if (XorY == 0) if (X[i] > 0) resultx[i] = Math.Sqrt(Math.Abs(X[i]));
                        if (XorY == 1) if (Y[i] > 0) resulty[i] = Math.Sqrt(Math.Abs(Y[i]));
                        if (XorY == 2) { if (X[i] > 0)  resultx[i] = Math.Sqrt(Math.Abs(X[i])); if (Y[i] != 0) resulty[i] = Math.Sqrt(Math.Abs(Y[i])); }
                    }
                    break;
                default: break;
            }
            if (XorY == 0 || XorY == 2)
                AR = resultx;
            else AR = X;
            if (XorY == 1 || XorY == 2)
                BR = resulty;
            else BR = Y;
        }
    }
}

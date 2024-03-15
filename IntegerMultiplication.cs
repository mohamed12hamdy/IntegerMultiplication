using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>

        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>

        public static byte[] minus(byte[] x, byte[] y)
        {
            int max_size = Math.Max(x.Length, y.Length);
            byte[] A = new byte[max_size];
            if (x.Length < max_size)
            {
                Array.Resize(ref x, max_size);

            }
            else if (y.Length < max_size)
            {
                Array.Resize(ref y, max_size);

            }
            int sum = 0;
            int b = 0;
            for (int i = 0; i < max_size; i++)
            {
                sum = (x[i] - y[i] - b);
                if (sum < 0)
                {
                    sum += 10;
                    b = 1;
                }
                else
                {
                    b = 0;
                }
                A[i] = (byte)sum;

            }
            return A;

        }

        public static byte[] mo(byte[] a, int s)  
        {
            byte[] result = new byte[a.Length + s];
            Array.Copy(a, 0, result, s, a.Length);
            return result;
        }



















        public static byte[] add(byte[] x, byte[] y)
        {

            int length = Math.Max(x.Length, y.Length);
            byte[] res = new byte[length + 1];
            
            int s = 0;
            int c = 0;
            int f = 0;
            int l = 0;     
            if (length > x.Length)
            {
                Array.Resize(ref x, length);
            }
            else if (length > y.Length)
            {
                Array.Resize(ref y, length);
            }
            for (int i = 0; i < x.Length; i++)
            {
                f = x[i]; l = y[i];
                s = (f + l + c);


                res[i] = (byte)(s % 10);
                c = s / 10;

            }
            if (c > 0) 
            {
                res[length] = (byte)c;
            }
            if (res[length] == 0)
            {
                Array.Resize(ref res, length);
            }
            return res;
            ///////////////////////////////////////////////////////////////hidden testCases



        }
        public static string cs(byte[] x)    
        {
            string my = string.Empty;    

            int size = x.Length - 1;     
            while (size >= 0)
            {
                my += x[size];
                size--;
            }
            return my;
        }
        


        public static byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            
            if (N <= 8)  
            {
                string res1 = cs(X);
                string res2 = cs(Y);
                long res3 = long.Parse(res1) * long.Parse(res2);
                byte[] arr = new byte[res3.ToString().Length];
                int index = 0;
                int len = res3.ToString().Length;
                while (len != 0)
                {
                    arr[index++] = (byte)(res3 % 10);
                    res3 /= 10;
                    --len;
                }
                if (len < 2 * N)
                    Array.Resize(ref arr, 2 * N);

                return arr;
            }


            if (N % 2 != 0)
            {

                Array.Resize(ref X, X.Length + 1);
                Array.Resize(ref Y, Y.Length + 1);
                ++N;
            }
            byte[] A = new byte[N / 2];
            byte[] B = new byte[N / 2];
            byte[] c = new byte[N / 2];
            byte[] d = new byte[N / 2];
            for (int i = 0; i < X.Length / 2; i++)
            {
                A[i] = X[i];
                B[i] = X[i + (N / 2)];

                c[i] = Y[i];
                d[i] = Y[i + (N / 2)];
            }
            

            byte[] resofAc = new byte[N];   
            byte[] resofbd = new byte[N];    
            byte[] z;
            byte[] resofsub;
            Parallel.Invoke(() => resofAc = IntegerMultiply(A, c, N / 2),
                               () => resofbd = IntegerMultiply(B, d, N / 2));
           

            

            byte[] resofab = add(A, B);
            byte[] resofcd = add(c, d);
            if (resofab.Length > resofcd.Length)
            {
                Array.Resize(ref resofcd, resofab.Length);
                z = IntegerMultiply(resofab, resofcd, resofab.Length);
            }
            else if (resofab.Length < resofcd.Length)
            {
                Array.Resize(ref resofab, resofcd.Length);
                z = IntegerMultiply(resofab, resofcd, resofab.Length);
            }
            else
            {
                z = IntegerMultiply(resofab, resofcd, (resofab.Length));
            }



            resofsub = minus(minus(z, resofAc), resofbd);
           
            resofsub = mo(resofsub, N / 2);  
            resofbd = mo(resofbd, N);    



            byte[] Final;
            Final = add(add(resofsub, resofbd), resofAc);
            if (Final.Length < 2 * N)
            {
                Array.Resize(ref Final, 2 * N);

            }
            else if (Final.Length > 2 * N)
            {
                Array.Resize(ref Final, 2 * N);

            }

            return Final;
          

        }










    }
    #endregion
}
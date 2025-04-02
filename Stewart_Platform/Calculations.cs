using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stewart_Platform
{
    internal class Calculations
    {
        public double deg2rad(double deg)
        {
            /*
             *Convert degree to radian
             */
            double rad = deg * Math.PI / 180;
            return rad;
            
        }

        public double rad2deg(double rad)
        {
            /*
             *Convert radian to degree
             */
            double deg =rad * 180 / Math.PI;
            return deg;
        }

        public double[,] rotMat(double roll, double pitch, double yaw)
        {
            /*
             * Calculate the rotation matrix for given roll, pitch and yaw angles
             */

            roll = deg2rad(roll);
            pitch = deg2rad(pitch);
            yaw = deg2rad(yaw);

            double[,] rot_mat = { {0,0,0 },{0,0,0 },{0,0,0 } };


            rot_mat[0, 0] =Math.Cos(yaw) * Math.Cos(pitch);
            rot_mat[0, 1] =-Math.Sin(yaw) * Math.Cos(roll) + Math.Cos(yaw) * Math.Sin(pitch) * Math.Sin(roll);
            rot_mat[0, 2] =Math.Sin(yaw) * Math.Sin(roll) + Math.Cos(yaw) * Math.Sin(pitch) * Math.Cos(roll);
            rot_mat[1, 0] =Math.Sin(yaw) * Math.Cos(pitch);
            rot_mat[1, 1] =Math.Cos(yaw) * Math.Cos(roll) + Math.Sin(yaw) * Math.Sin(pitch) * Math.Sin(roll);
            rot_mat[1, 2] =-Math.Cos(yaw) * Math.Sin(roll) + Math.Sin(yaw) * Math.Sin(pitch) * Math.Cos(roll);
            rot_mat[2, 0] =-Math.Sin(pitch);
            rot_mat[2, 1] =Math.Cos(pitch) * Math.Sin(roll);
            rot_mat[2, 2] =Math.Cos(pitch) * Math.Cos(roll);

            return rot_mat;
        }

        public double[,] point2coordinate(double[] angle, double radius, double height)
        {
            /*
             * Convert base and platform connection points to x,y,z coordinates
             */
            double[,] coordinate = { {0,0,0,0,0,0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };

            int angle_size = angle.GetLength(0);

            for (int i = 0; i < angle_size; i++)
            {
                coordinate[0, i] = radius * Math.Cos(deg2rad(angle[i])); //X values of given angles
                coordinate[1, i] = radius * Math.Sin(deg2rad(angle[i])); //Y values of given angles
                coordinate[2, i] = height;
            }
            return coordinate;
        }

        public double[,] Matrix_Multiply(double[,] matrix_1, double[,] matrix_2)
        {
            int matrix1Rows = matrix_1.GetLength(0);
            int matrix1Cols = matrix_1.GetLength(1);
            int matrix2Rows = matrix_2.GetLength(0);
            int matrix2Cols = matrix_2.GetLength(1);

            if(matrix1Cols != matrix2Rows)
            {
                Console.WriteLine("Matrixes can't be multiplied!!");
            }

            double[,] product = new double[matrix1Rows, matrix2Cols];

            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++)
            {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix2_col = 0; matrix2_col < matrix2Cols; matrix2_col++)
                {
                    // loop through matrix 1 columns to calculate the dot product  
                    for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++)
                    {
                        product[matrix1_row, matrix2_col] +=
                          matrix_1[matrix1_row, matrix1_col] *
                          matrix_2[matrix1_col, matrix2_col];
                    }
                }
            }
            return product;
        }

        public double[] Matrix_vector_Multiplication(double[,] matrix_1, double[] vector)
        {
            int matrix1Rows = matrix_1.GetLength(0);
            int matrix1Cols = matrix_1.GetLength(1);
            int vector_length = vector.GetLength(0);

            if (matrix1Cols != vector_length)
            {
                Console.WriteLine("Matrixes can't be multiplied!!");
            }

            double[] product = new double[vector_length];

            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++)
            {
               for(int matrix1_col=0; matrix1_col< matrix1Cols; matrix1_col++)
               {
                    product[matrix1_row]+=matrix_1[matrix1_row , matrix1_col] * vector[matrix1_col];
               }
            }
            return product;
        }

        public double[,] Matrix_Transpose(double[,] matrix)
        {
            int w=matrix.GetLength(0);
            int h=matrix.GetLength(1);
            double[,] Matrix_T = new double[h, w];

            for(int i=0; i<w;i++)
            {
                for(int j=0; j<h; j++)
                {
                    Matrix_T[j,i] = matrix[i,j];
                }
            }
            return Matrix_T;
        }

        public double[] LinkLength (double roll, double pitch, double yaw, double[,] Blist, double[,] Plist, double[] Tvector)
        {
            double[,] l_list = new double[6, 3];
            double[] Link_Length = new double[6];

            for(int i=0; i<Link_Length.Length;i++)
            {
                double[] P_List_i=new double[3];
                double[] B_List_i = new double[3];
                for (int j=0; j< P_List_i.Length; j++)
                {
                    P_List_i[j] = Plist[i, j];
                    B_List_i[j] = Blist[i, j];
                }
                double[] platform_rotation = Matrix_vector_Multiplication(rotMat(roll, pitch, yaw), P_List_i);
                for(int j=0; j< l_list.GetLength(1);j++)
                {
                    l_list[i,j] = platform_rotation[j]+ Tvector[j]- B_List_i[j];
                }

                Link_Length[i]=Math.Sqrt(Math.Pow(l_list[i, 0], 2) + Math.Pow(l_list[i, 1], 2) + Math.Pow(l_list[i, 2], 2));
            }
            return Link_Length;
        }

        public double[,] platformPointRotation(double roll, double pitch, double yaw, double[,] PList)
        {
            double[,] paltfromPoints = new double[6, 3];

            for (int i=0; i< paltfromPoints.GetLength(0);i++)
            {
                double[] P_List_i = new double[3];
                for (int j = 0; j < P_List_i.Length; j++)
                {
                    P_List_i[j] = PList[i, j];
                }
                double[] platform_rotation = Matrix_vector_Multiplication(rotMat(roll, pitch, yaw), P_List_i);
                for (int j = 0; j < paltfromPoints.GetLength(1); j++)
                {
                    paltfromPoints[i, j] = platform_rotation[j];
                }
            }
            return paltfromPoints;
        }

        public double[] Servo_Angle(double[] betha, double roll, double pitch, double yaw, double[,] Plist, double[,] Blist, double servoArmLength, double servoLinkLength, double[] TVector)
        {
            double[] alpha = new double[6];
            double[] beta = new double[6];
            double[] Alpha = new double[6];
            double[] L = new double[6];
            double[] M = new double[6];
            double[] N = new double[6];
            double[,] bi = Blist;
            double[,] pi = platformPointRotation(roll, pitch, yaw, Plist);
            double[] Li = LinkLength(roll, pitch, yaw, Blist, Plist, TVector);

            double s = servoLinkLength;
            double a = servoArmLength;

            for (int i = 0; i < 6; i++)
            {
                beta[i] = deg2rad(betha[i]);
            }

            for (int i = 0; i < 6; i++)
            {
                L[i] = Math.Pow(Li[i], 2) - Math.Pow(s, 2) + Math.Pow(a, 2);
                M[i] = 2 * a * (pi[i, 2] - bi[i, 2]);
                N[i] = 2 * a * ((Math.Cos(beta[i]) * (pi[i, 0] - bi[i, 0])) + (Math.Sin(beta[i]) * (pi[i, 1] - bi[i, 1])));

                alpha[i] = Math.Asin(L[i] / Math.Sqrt(Math.Pow(M[i], 2) + Math.Pow(N[i], 2))) - Math.Atan2(N[i], M[i]);
            }

            for (int i = 0; i < 6; i++)
            {
                Alpha[i] =rad2deg(alpha[i]);
            }

            return Alpha;
        }
 
    }

   
}

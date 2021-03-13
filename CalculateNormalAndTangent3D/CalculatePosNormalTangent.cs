using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D; //PresentationCore.dll

namespace CalculateNormalAndTangent3D
{
    class CalculatePosNormalTangent
    {
        public PosNormalTangent Pos_Normal_Tangent { get; set; }
        public class PosNormalTangent
        {
            public Vector3D Position3D { get; set; }
            public Vector3D Normal3D { get; set; }
            public Vector3D Tangent3D { get; set; }
        }

        public enum RotationMatrixAxis
        {
            XYZ,
            XZY,
            YXZ,
            YZX,
            ZXY,
            ZYX
        }
        
        /// <summary>
        /// 指定された回転(X, Y, Z)を回転行列に変換します
        /// </summary>
        /// <param name="RotationVector">Rotation_Value</param>
        /// <param name="rotationMatrixAxis">Rotation Axis</param>
        /// <param name="MV3D">ModelVisual3D</param>
        /// <returns>Matrix3D</returns>
        public Matrix3D EulrRotation(Vector3D RotationVector, RotationMatrixAxis rotationMatrixAxis = RotationMatrixAxis.XYZ, ModelVisual3D MV3D = null)
        {
            Vector3D? Position3D = null;
            if (MV3D != null)
            {
                Position3D = new Vector3D(Math.Round(MV3D.Transform.Value.OffsetX, 3), Math.Round(MV3D.Transform.Value.OffsetY, 3), Math.Round(MV3D.Transform.Value.OffsetZ, 3));

            }
            if(MV3D == null)
            {
                Position3D = new Vector3D(0, 0, 0);
            }

            Matrix3D RotationEulrX = new Matrix3D
            {
                M11 = 1,
                M12 = 0,
                M13 = 0,
                M14 = 0,

                M21 = 0,
                M22 = Math.Cos(RotationVector.X),
                M23 = -Math.Sin(RotationVector.X),
                M24 = 0,

                M31 = 0,
                M32 = Math.Sin(RotationVector.X),
                M33 = Math.Cos(RotationVector.X),
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D RotationEulrY = new Matrix3D
            {
                M11 = Math.Cos(RotationVector.Y),
                M12 = 0,
                M13 = -Math.Sin(RotationVector.Y),
                M14 = 0,

                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = 0,

                M31 = -Math.Sin(RotationVector.Y),
                M32 = 0,
                M33 = Math.Cos(RotationVector.Y),
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D RotationEulrZ = new Matrix3D
            {
                M11 = Math.Cos(RotationVector.Z),
                M12 = -Math.Sin(RotationVector.Z),
                M13 = 0,
                M14 = 0,

                M21 = Math.Sin(RotationVector.Z),
                M22 = -Math.Cos(RotationVector.Z),
                M23 = 0,
                M24 = 0,

                M31 = 0,
                M32 = 0,
                M33 = 1,
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D m2 = new Matrix3D();
            if(rotationMatrixAxis == RotationMatrixAxis.XYZ)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrX, RotationEulrY), RotationEulrZ);

                m2.OffsetX = Position3D.Value.X;
                m2.OffsetY = Position3D.Value.Y;
                m2.OffsetZ = Position3D.Value.Z;
            }
            if(rotationMatrixAxis == RotationMatrixAxis.XZY)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrX, RotationEulrZ), RotationEulrY);

                m2.OffsetX = Position3D.Value.X;
                m2.OffsetY = Position3D.Value.Z;
                m2.OffsetZ = Position3D.Value.Y;
            }
            if(rotationMatrixAxis == RotationMatrixAxis.YXZ)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrY, RotationEulrX), RotationEulrZ);

                m2.OffsetX = Position3D.Value.Y;
                m2.OffsetY = Position3D.Value.X;
                m2.OffsetZ = Position3D.Value.Z;
            }
            if(rotationMatrixAxis == RotationMatrixAxis.YZX)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrY, RotationEulrZ), RotationEulrX);

                m2.OffsetX = Position3D.Value.Y;
                m2.OffsetY = Position3D.Value.Z;
                m2.OffsetZ = Position3D.Value.X;
            }
            if(rotationMatrixAxis == RotationMatrixAxis.ZXY)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrZ, RotationEulrX), RotationEulrY);

                m2.OffsetX = Position3D.Value.Z;
                m2.OffsetY = Position3D.Value.X;
                m2.OffsetZ = Position3D.Value.Y;
            }
            if(rotationMatrixAxis == RotationMatrixAxis.ZYX)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrZ, RotationEulrY), RotationEulrX);

                m2.OffsetX = Position3D.Value.Z;
                m2.OffsetY = Position3D.Value.Y;
                m2.OffsetZ = Position3D.Value.X;
            }

            return m2;
        }

        /// <summary>
        /// 指定された回転(X, Y, Z)を回転行列に変換します
        /// </summary>
        /// <param name="RotationVector">Rotation Value</param>
        /// <param name="rotationMatrixAxis">Rotation Axis</param>
        /// <param name="M3D">Model3D</param>
        /// <returns>Matrix3D</returns>
        public Matrix3D EulrRotation(Vector3D RotationVector, RotationMatrixAxis rotationMatrixAxis = RotationMatrixAxis.XYZ, Model3D M3D = null)
        {
            Vector3D? Position3D = null;
            if (M3D != null)
            {
                Position3D = new Vector3D(Math.Round(M3D.Transform.Value.OffsetX, 3), Math.Round(M3D.Transform.Value.OffsetY, 3), Math.Round(M3D.Transform.Value.OffsetZ, 3));

            }
            if (M3D == null)
            {
                Position3D = new Vector3D(0, 0, 0);
            }

            Matrix3D RotationEulrX = new Matrix3D
            {
                M11 = 1,
                M12 = 0,
                M13 = 0,
                M14 = 0,

                M21 = 0,
                M22 = Math.Cos(RotationVector.X),
                M23 = -Math.Sin(RotationVector.X),
                M24 = 0,

                M31 = 0,
                M32 = Math.Sin(RotationVector.X),
                M33 = Math.Cos(RotationVector.X),
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D RotationEulrY = new Matrix3D
            {
                M11 = Math.Cos(RotationVector.Y),
                M12 = 0,
                M13 = -Math.Sin(RotationVector.Y),
                M14 = 0,

                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = 0,

                M31 = -Math.Sin(RotationVector.Y),
                M32 = 0,
                M33 = Math.Cos(RotationVector.Y),
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D RotationEulrZ = new Matrix3D
            {
                M11 = Math.Cos(RotationVector.Z),
                M12 = -Math.Sin(RotationVector.Z),
                M13 = 0,
                M14 = 0,

                M21 = Math.Sin(RotationVector.Z),
                M22 = -Math.Cos(RotationVector.Z),
                M23 = 0,
                M24 = 0,

                M31 = 0,
                M32 = 0,
                M33 = 1,
                M34 = 0,

                OffsetX = 0,
                OffsetY = 0,
                OffsetZ = 0,
                M44 = 1
            };

            Matrix3D m2 = new Matrix3D();
            if (rotationMatrixAxis == RotationMatrixAxis.XYZ)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrX, RotationEulrY), RotationEulrZ);

                m2.OffsetX = Position3D.Value.X;
                m2.OffsetY = Position3D.Value.Y;
                m2.OffsetZ = Position3D.Value.Z;
            }
            if (rotationMatrixAxis == RotationMatrixAxis.XZY)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrX, RotationEulrZ), RotationEulrY);

                m2.OffsetX = Position3D.Value.X;
                m2.OffsetY = Position3D.Value.Z;
                m2.OffsetZ = Position3D.Value.Y;
            }
            if (rotationMatrixAxis == RotationMatrixAxis.YXZ)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrY, RotationEulrX), RotationEulrZ);

                m2.OffsetX = Position3D.Value.Y;
                m2.OffsetY = Position3D.Value.X;
                m2.OffsetZ = Position3D.Value.Z;
            }
            if (rotationMatrixAxis == RotationMatrixAxis.YZX)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrY, RotationEulrZ), RotationEulrX);

                m2.OffsetX = Position3D.Value.Y;
                m2.OffsetY = Position3D.Value.Z;
                m2.OffsetZ = Position3D.Value.X;
            }
            if (rotationMatrixAxis == RotationMatrixAxis.ZXY)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrZ, RotationEulrX), RotationEulrY);

                m2.OffsetX = Position3D.Value.Z;
                m2.OffsetY = Position3D.Value.X;
                m2.OffsetZ = Position3D.Value.Y;
            }
            if (rotationMatrixAxis == RotationMatrixAxis.ZYX)
            {
                m2 = Matrix3D.Multiply(Matrix3D.Multiply(RotationEulrZ, RotationEulrY), RotationEulrX);

                m2.OffsetX = Position3D.Value.Z;
                m2.OffsetY = Position3D.Value.Y;
                m2.OffsetZ = Position3D.Value.X;
            }

            return m2;
        }

        public PosNormalTangent GetPosNrmTan3D(Vector3D Rot, RotationMatrixAxis rotationMatrixAxis = RotationMatrixAxis.XYZ, ModelVisual3D MV3D = null, Model3D M3D = null)
        {
            #region Default
            //M11, M12, M13, M14
            //M21, M22, M23, M24
            //M31, M32, M33, M34
            //M41, M42, M43, M44
            #endregion

            #region Blender
            //M11, M21, M31, M41
            //M12, M22, M32, M42
            //M13, M23, M33, M43
            //M14, M24, M34, M44
            #endregion

            Matrix3D m = new Matrix3D();
            if (MV3D != null || M3D != null)
            {
                if(MV3D != null)
                {
                    m = EulrRotation(Rot, rotationMatrixAxis, MV3D);
                }
                if(M3D != null)
                {
                    m = EulrRotation(Rot, rotationMatrixAxis, M3D);
                }
            }
            else
            {
                //Default
                m = EulrRotation(Rot, rotationMatrixAxis, new ModelVisual3D());
            }

            PosNormalTangent PosNrmTan3D = null;
            if (rotationMatrixAxis == RotationMatrixAxis.XYZ)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M21, 3), Math.Round(m.M22, 3), Math.Round(m.M23, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M31, 3), Math.Round(m.M32, 3), Math.Round(m.M33, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetX, 3), Math.Round(m.OffsetY, 3), Math.Round(m.OffsetZ, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }
            if (rotationMatrixAxis == RotationMatrixAxis.XZY)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M21, 3), Math.Round(m.M23, 3), Math.Round(m.M22, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M31, 3), Math.Round(m.M33, 3), Math.Round(m.M32, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetX, 3), Math.Round(m.OffsetZ, 3), Math.Round(m.OffsetY, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }
            if (rotationMatrixAxis == RotationMatrixAxis.YXZ)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M22, 3), Math.Round(m.M21, 3), Math.Round(m.M23, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M32, 3), Math.Round(m.M31, 3), Math.Round(m.M33, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetY, 3), Math.Round(m.OffsetX, 3), Math.Round(m.OffsetZ, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }
            if (rotationMatrixAxis == RotationMatrixAxis.YZX)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M22, 3), Math.Round(m.M23, 3), Math.Round(m.M21, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M32, 3), Math.Round(m.M33, 3), Math.Round(m.M31, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetY, 3), Math.Round(m.OffsetZ, 3), Math.Round(m.OffsetX, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }
            if (rotationMatrixAxis == RotationMatrixAxis.ZXY)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M23, 3), Math.Round(m.M21, 3), Math.Round(m.M22, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M33, 3), Math.Round(m.M31, 3), Math.Round(m.M32, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetZ, 3), Math.Round(m.OffsetX, 3), Math.Round(m.OffsetY, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }
            if (rotationMatrixAxis == RotationMatrixAxis.ZYX)
            {
                Vector3D Tangent3D = new Vector3D(Math.Round(m.M23, 3), Math.Round(m.M22, 3), Math.Round(m.M21, 3));
                Vector3D Normal3D = new Vector3D(Math.Round(m.M33, 3), Math.Round(m.M32, 3), Math.Round(m.M31, 3));
                Vector3D Position3D = new Vector3D(Math.Round(m.OffsetZ, 3), Math.Round(m.OffsetY, 3), Math.Round(m.OffsetX, 3));

                PosNrmTan3D = new PosNormalTangent
                {
                    Tangent3D = Tangent3D,
                    Normal3D = Normal3D,
                    Position3D = Position3D
                };
            }

            return PosNrmTan3D;
        }


        public BINormalTangent BINrmTans { get; set; }
        public class BINormalTangent
        {
            public Vector3 BINormal { get; set; }
            public Vector3 Tangent { get; set; }
        }

        /// <summary>
        /// BI-NormalとTangentを返すメソッド
        /// </summary>
        /// <param name="Normal">Vector3</param>
        /// <returns></returns>
        public BINormalTangent Calculate_BINormalAndTangent(Vector3 Normal)
        {

            BINormalTangent BINrmAndTan = new BINormalTangent
            {
                BINormal = new Vector3(),
                Tangent = new Vector3()
            };

            var t1 = Vector3.Cross(Normal, new Vector3(0.0f, 0.0f, 1.0f));
            var t2 = Vector3.Cross(Normal, new Vector3(0.0f, 1.0f, 0.0f));
            if(t1.Length() > t2.Length())
            {
                BINrmAndTan.Tangent = Vector3.Normalize(t1);
            }
            else
            {
                BINrmAndTan.Tangent = Vector3.Normalize(t2);
            }

            BINrmAndTan.BINormal = Vector3.Normalize(Vector3.Cross(Normal, BINrmAndTan.Tangent));

            return BINrmAndTan;
        }
    }
}

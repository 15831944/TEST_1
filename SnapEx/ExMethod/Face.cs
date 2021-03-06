﻿using NXOpen.UF;
using Snap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  SnapEx
{
    public static partial class ExMethod
    {
        /// <summary>
        /// 获取平面投影面积
        /// </summary>
        public static double GetPlaneProjectArea(this Snap.NX.Face face)
        {
            return GetPlaneProjectArea(face, face.GetFaceDirection());
        }
        /// <summary>
        /// 获取平面投影面积
        /// </summary>
        public static double GetPlaneProjectArea(this Snap.NX.Face face,Snap.Vector baseDir)
        {
            var topFaceDir = -baseDir;
            var topFaceOrientation = new Snap.Orientation(topFaceDir);
            var box = SnapEx.Ex.AcsToWcsBox3d(face.BoxEx(), topFaceOrientation);
            return System.Math.Abs(box.MinX-box.MaxX)* System.Math.Abs(box.MinY - box.MaxY);
        }

        /// <summary>
        /// 获取BOX
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static Snap.Geom.Box3d BoxEx(this Snap.NX.Face face)
        {
            int surfaceType;
            double radius1;
            double radius2;
            int normalFlip;
            double[] point = new double[3];
            double[] dir = new double[3];
            double[] box = new double[6];
            try
            {
                NXOpen.UF.UFSession uFSession = NXOpen.UF.UFSession.GetUFSession();
                uFSession.Modl.AskFaceData(face.NXOpenTag, out surfaceType, point, dir, box, out radius1, out radius2, out normalFlip);
                var faceBox = new Snap.Geom.Box3d(box[0], box[1], box[2], box[3], box[4], box[5]);
                return faceBox;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return face.Box;
            }
        }
        /// <summary>
        /// 获取面的法向
        /// </summary>
        public static Vector GetFaceDirection(this Snap.NX.Face face)
        {
            return Vector.Unit(GetSurfaceAxisVector(face));
        }

        public static Snap.Position GetCenterPointEx(this Snap.NX.Face face)
        {
            var box = face.BoxEx();
            return new Snap.Position((box.MaxX + box.MinX) / 2, (box.MaxY + box.MinY) / 2, (box.MaxZ + box.MinZ) / 2);
        }

        public static Vector GetFaceDirectionByPoint(this Snap.NX.Face face, Snap.Position pos)
        {
            var ufSession = NXOpen.UF.UFSession.GetUFSession();
            double[] param = new double[2], faceOnPoint = pos.Array, u1 = new double[3], v1 = new double[3], u2 = new double[3], v2 = new double[3], unitNorm = new double[3], radii = new double[2];
            ufSession.Modl.AskFaceParm(face.NXOpenTag, faceOnPoint, param, faceOnPoint);
            ufSession.Modl.AskFaceProps(face.NXOpenTag, param, faceOnPoint, u1, v1, u2, v2, unitNorm, radii);
            return Snap.Vector.Unit(new Snap.Vector(unitNorm));
        }

        /// <summary>
        /// 获取拔模角度
        /// </summary>
        public static double GetDraftAngle(this Snap.NX.Face face)
        {
            return face.GetDraftAngle(new Snap.Vector(0, 0, 1));
        }
        
        /// <summary>
        /// 获取拔模角度
        /// </summary>
        public static double GetDraftAngle(this Snap.NX.Face face, Snap.Vector draftVector)
        {
            var result = 0.0;
            var angles = new List<double>();
            var vector = new Snap.Vector(double.NaN,double.NaN,double.NaN);

            switch (face.ObjectSubType)
            {
                case Snap.NX.ObjectTypes.SubType.FacePlane:
                    vector = face.GetFaceDirection();
                    break;
            }

            if (double.IsNaN(vector.X) || double.IsNaN(vector.Y) || double.IsNaN(vector.Z))
            {
                var posLst = SnapEx.Create.GetFacePoints(face);
                posLst.ForEach(u => {
                    angles.Add(90 - Snap.Vector.Angle(draftVector, GetFaceDirectionByPoint(face,u)));
                });
            }
            else
            {
                angles.Add(90 - Snap.Vector.Angle(draftVector, vector));
            }

            if (angles.Count > 0)
            {
                int count = angles.Count;
                angles.ForEach(u => {
                    result += u / count;
                });
            }

            return result;
        }


        private static Vector GetSurfaceAxisVector(Snap.NX.Face face)
        {
            int num;
            Snap.Position position;
            Vector vector;
            double num2;
            double num3;
            int num4;
            GetSurfaceData(face, out num, out position, out vector, out num2, out num3, out num4);
            return vector;
        }

        private static void GetSurfaceData(Snap.NX.Face face, out int surfaceType, out Snap.Position axisPoint, out Vector axisVector, out double radius1, out double radius2, out int normalFlip)
        {
            UFSession uFSession = NXOpen.UF.UFSession.GetUFSession();
            double[] point = new double[3];
            double[] dir = new double[3];
            double[] box = new double[6];
            uFSession.Modl.AskFaceData(face.NXOpenTag, out surfaceType, point, dir, box, out radius1, out radius2, out normalFlip);
            axisPoint = new Snap.Position(point);
            axisVector = new Vector(dir);
        }
    }
    
}

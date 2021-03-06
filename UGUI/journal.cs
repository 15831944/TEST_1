// NX 9.0.0.19
// Journal created by PENGHUI on Mon May 21 11:29:01 2018 中国标准时间
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    Session theSession = Session.GetSession();
    Part workPart = theSession.Parts.Work;
    Part displayPart = theSession.Parts.Display;
    // ----------------------------------------------
    //   菜单：编辑->移动对象...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");
    
    NXOpen.Features.MoveObject nullFeatures_MoveObject = null;
    
    if ( !workPart.Preferences.Modeling.GetHistoryMode() )
    {
        throw new Exception("Create or edit of a Feature was recorded in History Mode but playback is in History-Free Mode.");
    }
    
    NXOpen.Features.MoveObjectBuilder moveObjectBuilder1;
    moveObjectBuilder1 = workPart.BaseFeatures.CreateMoveObjectBuilder(nullFeatures_MoveObject);
    
    moveObjectBuilder1.TransformMotion.DistanceAngle.OrientXpress.AxisOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Axis.Passive;
    
    moveObjectBuilder1.TransformMotion.DistanceAngle.OrientXpress.PlaneOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Plane.Passive;
    
    moveObjectBuilder1.TransformMotion.AlongCurveAngle.AlongCurve.IsPercentUsed = true;
    
    moveObjectBuilder1.TransformMotion.AlongCurveAngle.AlongCurve.Expression.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.AlongCurveAngle.AlongCurve.Expression.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.OrientXpress.AxisOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Axis.Passive;
    
    moveObjectBuilder1.TransformMotion.OrientXpress.PlaneOption = NXOpen.GeometricUtilities.OrientXpressBuilder.Plane.Passive;
    
    moveObjectBuilder1.TransformMotion.Option = NXOpen.GeometricUtilities.ModlMotion.Options.Angle;
    
    moveObjectBuilder1.TransformMotion.DistanceValue.RightHandSide = "90";
    
    moveObjectBuilder1.TransformMotion.DistanceBetweenPointsDistance.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.RadialDistance.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.Angle.RightHandSide = "30";
    
    moveObjectBuilder1.TransformMotion.DistanceAngle.Distance.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.DistanceAngle.Angle.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.DeltaEnum = NXOpen.GeometricUtilities.ModlMotion.Delta.ReferenceWcsWorkPart;
    
    moveObjectBuilder1.TransformMotion.DeltaXc.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.DeltaYc.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.DeltaZc.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.AlongCurveAngle.AlongCurve.Expression.RightHandSide = "0";
    
    moveObjectBuilder1.TransformMotion.AlongCurveAngle.AlongCurveAngle.RightHandSide = "0";
    
    moveObjectBuilder1.Layer = 201;
    
    theSession.SetUndoMarkName(markId1, "移动对象 对话框");
    
    Point3d origin1 = new Point3d(0.0, -200.0, 0.0);
    Vector3d vector1 = new Vector3d(0.0, 1.0, 0.0);
    Direction direction1;
    direction1 = workPart.Directions.CreateDirection(origin1, vector1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    Point nullPoint = null;
    Axis axis1;
    axis1 = workPart.Axes.CreateAxis(nullPoint, direction1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    moveObjectBuilder1.TransformMotion.AngularAxis = axis1;
    
    moveObjectBuilder1.TransformMotion.AngularAxis = axis1;
    
    Line line1 = (Line)workPart.Lines.FindObject("HANDLE R-35944");
    bool added1;
    added1 = moveObjectBuilder1.ObjectToMoveObject.Add(line1);
    
    moveObjectBuilder1.TransformMotion.Option = NXOpen.GeometricUtilities.ModlMotion.Options.CsysToCsys;
    
    Xform xform1;
    xform1 = workPart.Xforms.CreateXform(NXOpen.SmartObject.UpdateOption.WithinModeling, 1.0);
    
    CartesianCoordinateSystem cartesianCoordinateSystem1;
    cartesianCoordinateSystem1 = workPart.CoordinateSystems.CreateCoordinateSystem(xform1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    moveObjectBuilder1.TransformMotion.FromCsys = cartesianCoordinateSystem1;
    
    Point3d origin2 = new Point3d(0.0, 0.0, 0.0);
    Vector3d xDirection1 = new Vector3d(1.0, 0.0, 0.0);
    Vector3d yDirection1 = new Vector3d(0.0, 1.0, 0.0);
    Xform xform2;
    xform2 = workPart.Xforms.CreateXform(origin2, xDirection1, yDirection1, NXOpen.SmartObject.UpdateOption.WithinModeling, 1.0);
    
    CartesianCoordinateSystem cartesianCoordinateSystem2;
    cartesianCoordinateSystem2 = workPart.CoordinateSystems.CreateCoordinateSystem(xform2, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    moveObjectBuilder1.TransformMotion.ToCsys = cartesianCoordinateSystem2;
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "移动对象");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "移动对象");
    
    theSession.UndoToMarkWithStatus(markId3, null);
    
    theSession.DeleteUndoMark(markId3, null);
    
    moveObjectBuilder1.MoveObjectResult = NXOpen.Features.MoveObjectBuilder.MoveObjectResultOptions.CopyOriginal;
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "移动对象");
    
    theSession.DeleteUndoMark(markId4, null);
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "移动对象");
    
    NXObject nXObject1;
    nXObject1 = moveObjectBuilder1.Commit();
    
    NXObject[] objects1;
    objects1 = moveObjectBuilder1.GetCommittedObjects();
    
    theSession.DeleteUndoMark(markId5, null);
    
    theSession.SetUndoMarkName(markId1, "移动对象");
    
    moveObjectBuilder1.Destroy();
    
    // ----------------------------------------------
    //   菜单：工具->操作记录->停止录制
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)Session.LibraryUnloadOption.Immediately; }
}

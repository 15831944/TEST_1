//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\PHEactProject\Source\Project\UGUI\CheckRegionsUI.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: PENGHUI
//              Version: NX 9
//              Date: 12-13-2018  (Format: mm-dd-yyyy)
//              Time: 09:51 (Format: hh-mm)
//
//==============================================================================

//==============================================================================
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using NXOpen;
using NXOpen.BlockStyler;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public class CheckRegionsUI
{
    //class members
    private static Session theSession = null;
    private static UI theUI = null;
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private NXOpen.BlockStyler.TabControl tabControl;// Block type: Tabs Page
    private NXOpen.BlockStyler.Group tabPage;// Block type: Group
    private NXOpen.BlockStyler.Group group5;// Block type: Group
    private NXOpen.BlockStyler.Enumeration enum02;// Block type: Enumeration
    private NXOpen.BlockStyler.Group group1;// Block type: Group
    private NXOpen.BlockStyler.Enumeration enum0;// Block type: Enumeration
    private NXOpen.BlockStyler.Label label01;// Block type: Label
    private NXOpen.BlockStyler.Button button0;// Block type: Button
    private NXOpen.BlockStyler.Group tabPage1;// Block type: Group
    private NXOpen.BlockStyler.Group group4;// Block type: Group
    private NXOpen.BlockStyler.Label label02;// Block type: Label
    private NXOpen.BlockStyler.IntegerBlock integer0;// Block type: Integer
    private NXOpen.BlockStyler.Label label03;// Block type: Label
    private NXOpen.BlockStyler.IntegerBlock integer01;// Block type: Integer
    private NXOpen.BlockStyler.Group group3;// Block type: Group
    private NXOpen.BlockStyler.FaceCollector face_select0;// Block type: Face Collector
    private NXOpen.BlockStyler.Enumeration enum01;// Block type: Enumeration
    private NXOpen.BlockStyler.Button button02;// Block type: Button
    private NXOpen.BlockStyler.Group group;// Block type: Group
    private NXOpen.BlockStyler.SelectObject bodySelect0;// Block type: Selection
    private NXOpen.BlockStyler.SpecifyVector vector0;// Block type: Specify Vector
    private NXOpen.BlockStyler.Group group2;// Block type: Group
    private NXOpen.BlockStyler.DoubleBlock double0;// Block type: Double
    private NXOpen.BlockStyler.Label label0;// Block type: Label
    private NXOpen.BlockStyler.Toggle toggle0;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker0;// Block type: Color Picker
    private NXOpen.BlockStyler.Toggle toggle01;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker01;// Block type: Color Picker
    private NXOpen.BlockStyler.Toggle toggle02;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker02;// Block type: Color Picker
    private NXOpen.BlockStyler.Toggle toggle03;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker03;// Block type: Color Picker
    private NXOpen.BlockStyler.Toggle toggle04;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker04;// Block type: Color Picker
    private NXOpen.BlockStyler.Toggle toggle05;// Block type: Toggle
    private NXOpen.BlockStyler.ObjectColorPicker colorPicker05;// Block type: Color Picker
    private NXOpen.BlockStyler.Button button01;// Block type: Button
    public static readonly int                          EntityType_AllowFaces = (1 << 4);
    public static readonly int                         EntityType_AllowDatums = (1 << 5);
    public static readonly int                         EntityType_AllowBodies = (1 << 6);
    public static readonly int                           FaceRules_SingleFace = (1 << 0);
    public static readonly int                          FaceRules_RegionFaces = (1 << 1);
    public static readonly int                         FaceRules_TangentFaces = (1 << 2);
    public static readonly int                   FaceRules_TangentRegionFaces = (1 << 3);
    public static readonly int                            FaceRules_BodyFaces = (1 << 4);
    public static readonly int                         FaceRules_FeatureFaces = (1 << 5);
    public static readonly int                        FaceRules_AdjacentFaces = (1 << 6);
    public static readonly int                  FaceRules_ConnectedBlendFaces = (1 << 7);
    public static readonly int                        FaceRules_AllBlendFaces = (1 << 8);
    public static readonly int                             FaceRules_RibFaces = (1 << 9);
    public static readonly int                            FaceRules_SlotFaces = (1 <<10);
    public static readonly int                   FaceRules_BossandPocketFaces = (1 <<11);
    public static readonly int                       FaceRules_MergedRibFaces = (1 <<12);
    public static readonly int                  FaceRules_RegionBoundaryFaces = (1 <<13);
    public static readonly int                 FaceRules_FaceandAdjacentFaces = (1 <<14);
    public static readonly int              SnapPointTypesEnabled_UserDefined = (1 << 0);
    public static readonly int                 SnapPointTypesEnabled_Inferred = (1 << 1);
    public static readonly int           SnapPointTypesEnabled_ScreenPosition = (1 << 2);
    public static readonly int                 SnapPointTypesEnabled_EndPoint = (1 << 3);
    public static readonly int                 SnapPointTypesEnabled_MidPoint = (1 << 4);
    public static readonly int             SnapPointTypesEnabled_ControlPoint = (1 << 5);
    public static readonly int             SnapPointTypesEnabled_Intersection = (1 << 6);
    public static readonly int                SnapPointTypesEnabled_ArcCenter = (1 << 7);
    public static readonly int            SnapPointTypesEnabled_QuadrantPoint = (1 << 8);
    public static readonly int            SnapPointTypesEnabled_ExistingPoint = (1 << 9);
    public static readonly int             SnapPointTypesEnabled_PointonCurve = (1 <<10);
    public static readonly int           SnapPointTypesEnabled_PointonSurface = (1 <<11);
    public static readonly int         SnapPointTypesEnabled_PointConstructor = (1 <<12);
    public static readonly int     SnapPointTypesEnabled_TwocurveIntersection = (1 <<13);
    public static readonly int             SnapPointTypesEnabled_TangentPoint = (1 <<14);
    public static readonly int                    SnapPointTypesEnabled_Poles = (1 <<15);
    public static readonly int         SnapPointTypesEnabled_BoundedGridPoint = (1 <<16);
    public static readonly int         SnapPointTypesEnabled_FacetVertexPoint = (1 <<17);
    public static readonly int          SnapPointTypesOnByDefault_UserDefined = (1 << 0);
    public static readonly int             SnapPointTypesOnByDefault_Inferred = (1 << 1);
    public static readonly int       SnapPointTypesOnByDefault_ScreenPosition = (1 << 2);
    public static readonly int             SnapPointTypesOnByDefault_EndPoint = (1 << 3);
    public static readonly int             SnapPointTypesOnByDefault_MidPoint = (1 << 4);
    public static readonly int         SnapPointTypesOnByDefault_ControlPoint = (1 << 5);
    public static readonly int         SnapPointTypesOnByDefault_Intersection = (1 << 6);
    public static readonly int            SnapPointTypesOnByDefault_ArcCenter = (1 << 7);
    public static readonly int        SnapPointTypesOnByDefault_QuadrantPoint = (1 << 8);
    public static readonly int        SnapPointTypesOnByDefault_ExistingPoint = (1 << 9);
    public static readonly int         SnapPointTypesOnByDefault_PointonCurve = (1 <<10);
    public static readonly int       SnapPointTypesOnByDefault_PointonSurface = (1 <<11);
    public static readonly int     SnapPointTypesOnByDefault_PointConstructor = (1 <<12);
    public static readonly int SnapPointTypesOnByDefault_TwocurveIntersection = (1 <<13);
    public static readonly int         SnapPointTypesOnByDefault_TangentPoint = (1 <<14);
    public static readonly int                SnapPointTypesOnByDefault_Poles = (1 <<15);
    public static readonly int     SnapPointTypesOnByDefault_BoundedGridPoint = (1 <<16);
    public static readonly int     SnapPointTypesOnByDefault_FacetVertexPoint = (1 <<17);
    
    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public CheckRegionsUI()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theDlxFileName = "CheckRegionsUI.dlx";
            theDialog = theUI.CreateDialog(theDlxFileName);
            theDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(ok_cb));
            theDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update(update_cb));
            theDialog.AddFilterHandler(new NXOpen.BlockStyler.BlockDialog.Filter(filter_cb));
            theDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(initialize_cb));
            theDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(dialogShown_cb));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void Main()
    {
        CheckRegionsUI theCheckRegionsUI = null;
        try
        {
            theCheckRegionsUI = new CheckRegionsUI();
            theCheckRegionsUI.Show();
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            if(theCheckRegionsUI != null)
                theCheckRegionsUI.Dispose();
                theCheckRegionsUI = null;
        }
    }
     public static int GetUnloadOption(string arg)
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
         return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }
    
    public static void UnloadLibrary(string arg)
    {
        try
        {
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    public NXOpen.UIStyler.DialogResponse Show()
    {
        try
        {
            theDialog.Show();
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        if(theDialog != null)
        {
            theDialog.Dispose();
            theDialog = null;
        }
    }
    
    
    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    public void initialize_cb()
    {
        try
        {
            tabControl = theDialog.TopBlock.FindBlock("tabControl");
            tabPage = theDialog.TopBlock.FindBlock("tabPage");
            group5 = theDialog.TopBlock.FindBlock("group5");
            enum02 = theDialog.TopBlock.FindBlock("enum02");
            group1 = theDialog.TopBlock.FindBlock("group1");
            enum0 = theDialog.TopBlock.FindBlock("enum0");
            label01 = theDialog.TopBlock.FindBlock("label01");
            button0 = theDialog.TopBlock.FindBlock("button0");
            tabPage1 = theDialog.TopBlock.FindBlock("tabPage1");
            group4 = theDialog.TopBlock.FindBlock("group4");
            label02 = theDialog.TopBlock.FindBlock("label02");
            integer0 = theDialog.TopBlock.FindBlock("integer0");
            label03 = theDialog.TopBlock.FindBlock("label03");
            integer01 = theDialog.TopBlock.FindBlock("integer01");
            group3 = theDialog.TopBlock.FindBlock("group3");
            face_select0 = theDialog.TopBlock.FindBlock("face_select0");
            enum01 = theDialog.TopBlock.FindBlock("enum01");
            button02 = theDialog.TopBlock.FindBlock("button02");
            group = theDialog.TopBlock.FindBlock("group");
            bodySelect0 = theDialog.TopBlock.FindBlock("bodySelect0");
            vector0 = theDialog.TopBlock.FindBlock("vector0");
            group2 = theDialog.TopBlock.FindBlock("group2");
            double0 = theDialog.TopBlock.FindBlock("double0");
            label0 = theDialog.TopBlock.FindBlock("label0");
            toggle0 = theDialog.TopBlock.FindBlock("toggle0");
            colorPicker0 = theDialog.TopBlock.FindBlock("colorPicker0");
            toggle01 = theDialog.TopBlock.FindBlock("toggle01");
            colorPicker01 = theDialog.TopBlock.FindBlock("colorPicker01");
            toggle02 = theDialog.TopBlock.FindBlock("toggle02");
            colorPicker02 = theDialog.TopBlock.FindBlock("colorPicker02");
            toggle03 = theDialog.TopBlock.FindBlock("toggle03");
            colorPicker03 = theDialog.TopBlock.FindBlock("colorPicker03");
            toggle04 = theDialog.TopBlock.FindBlock("toggle04");
            colorPicker04 = theDialog.TopBlock.FindBlock("colorPicker04");
            toggle05 = theDialog.TopBlock.FindBlock("toggle05");
            colorPicker05 = theDialog.TopBlock.FindBlock("colorPicker05");
            button01 = theDialog.TopBlock.FindBlock("button01");
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //------------------------------------------------------------------------------
    public void dialogShown_cb()
    {
        try
        {
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    public int update_cb( NXOpen.BlockStyler.UIBlock block)
    {
        try
        {
            if(block == enum02)
            {
            }
            else if(block == enum0)
            {
            }
            else if(block == label01)
            {
            }
            else if(block == button0)
            {
            }
            else if(block == label02)
            {
            }
            else if(block == integer0)
            {
            }
            else if(block == label03)
            {
            }
            else if(block == integer01)
            {
            }
            else if(block == face_select0)
            {
            }
            else if(block == enum01)
            {
            }
            else if(block == button02)
            {
            }
            else if(block == bodySelect0)
            {
            }
            else if(block == vector0)
            {
            }
            else if(block == double0)
            {
            }
            else if(block == label0)
            {
            }
            else if(block == toggle0)
            {
            }
            else if(block == colorPicker0)
            {
            }
            else if(block == toggle01)
            {
            }
            else if(block == colorPicker01)
            {
            }
            else if(block == toggle02)
            {
            }
            else if(block == colorPicker02)
            {
            }
            else if(block == toggle03)
            {
            }
            else if(block == colorPicker03)
            {
            }
            else if(block == toggle04)
            {
            }
            else if(block == colorPicker04)
            {
            }
            else if(block == toggle05)
            {
            }
            else if(block == colorPicker05)
            {
            }
            else if(block == button01)
            {
            }
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    public int ok_cb()
    {
        int errorCode = 0;
        try
        {
        }
        catch (Exception ex)
        {
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: filter_cb
    //------------------------------------------------------------------------------
    public int filter_cb(NXOpen.BlockStyler.UIBlock block, NXOpen.TaggedObject selectedObject)
    {
        return(NXOpen.UF.UFConstants.UF_UI_SEL_ACCEPT);
    }
    
    //------------------------------------------------------------------------------
    //Function Name: GetBlockProperties
    //Returns the propertylist of the specified BlockID
    //------------------------------------------------------------------------------
    public PropertyList GetBlockProperties(string blockID)
    {
        PropertyList plist =null;
        try
        {
            plist = theDialog.GetBlockProperties(blockID);
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return plist;
    }
    
}

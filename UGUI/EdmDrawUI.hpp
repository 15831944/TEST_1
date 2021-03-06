//==============================================================================
//  WARNING!!  This file is overwritten by the Block Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\PHEactProject\Source\Project\UGUI\EdmDrawUI.hpp
//
//        This file was generated by the NX Block Styler
//        Created by: PENGHUI
//              Version: NX 9
//              Date: 12-04-2018  (Format: mm-dd-yyyy)
//              Time: 17:44
//
//==============================================================================

#ifndef EDMDRAWUI_H_INCLUDED
#define EDMDRAWUI_H_INCLUDED

#include <uf_defs.h>
#include <uf_ui_types.h>
#include <iostream>
#include <NXOpen/Session.hxx>
#include <NXOpen/UI.hxx>
#include <NXOpen/NXMessageBox.hxx>
#include <NXOpen/Callback.hxx>
#include <NXOpen/NXException.hxx>
#include <NXOpen/BlockStyler_UIBlock.hxx>
#include <NXOpen/BlockStyler_BlockDialog.hxx>
#include <NXOpen/BlockStyler_PropertyList.hxx>
#define              SnapPointTypesEnabled_UserDefined (1 << 0);
#define                 SnapPointTypesEnabled_Inferred (1 << 1);
#define           SnapPointTypesEnabled_ScreenPosition (1 << 2);
#define                 SnapPointTypesEnabled_EndPoint (1 << 3);
#define                 SnapPointTypesEnabled_MidPoint (1 << 4);
#define             SnapPointTypesEnabled_ControlPoint (1 << 5);
#define             SnapPointTypesEnabled_Intersection (1 << 6);
#define                SnapPointTypesEnabled_ArcCenter (1 << 7);
#define            SnapPointTypesEnabled_QuadrantPoint (1 << 8);
#define            SnapPointTypesEnabled_ExistingPoint (1 << 9);
#define             SnapPointTypesEnabled_PointonCurve (1 <<10);
#define           SnapPointTypesEnabled_PointonSurface (1 <<11);
#define         SnapPointTypesEnabled_PointConstructor (1 <<12);
#define     SnapPointTypesEnabled_TwocurveIntersection (1 <<13);
#define             SnapPointTypesEnabled_TangentPoint (1 <<14);
#define                    SnapPointTypesEnabled_Poles (1 <<15);
#define         SnapPointTypesEnabled_BoundedGridPoint (1 <<16);
#define         SnapPointTypesEnabled_FacetVertexPoint (1 <<17);
#define             SnapPointTypesOnByDefault_EndPoint (1 << 3);
#define             SnapPointTypesOnByDefault_MidPoint (1 << 4);
#define         SnapPointTypesOnByDefault_ControlPoint (1 << 5);
#define         SnapPointTypesOnByDefault_Intersection (1 << 6);
#define            SnapPointTypesOnByDefault_ArcCenter (1 << 7);
#define        SnapPointTypesOnByDefault_QuadrantPoint (1 << 8);
#define        SnapPointTypesOnByDefault_ExistingPoint (1 << 9);
#define         SnapPointTypesOnByDefault_PointonCurve (1 <<10);
#define       SnapPointTypesOnByDefault_PointonSurface (1 <<11);
#define     SnapPointTypesOnByDefault_PointConstructor (1 <<12);
#define     SnapPointTypesOnByDefault_BoundedGridPoint (1 <<16);
using namespace std;
using namespace NXOpen;
using namespace NXOpen::BlockStyler;

class DllExport EdmDrawUI
{
    // class members
public:
    static Session *theSession;
    static UI *theUI;
    EdmDrawUI();
    ~EdmDrawUI();
    int Show();
    
    void initialize_cb();
    void dialogShown_cb();
    int ok_cb();
    int update_cb(NXOpen::BlockStyler::UIBlock* block);
    PropertyList* GetBlockProperties(const char *blockID);
    
private:
    const char* theDlxFileName;
    NXOpen::BlockStyler::BlockDialog* theDialog;
    NXOpen::BlockStyler::Enumeration* selectTemplate0;// Block type: Enumeration
    NXOpen::BlockStyler::Group* group5;// Block type: Group
    NXOpen::BlockStyler::SelectObject* selectSteel;// Block type: Selection
    NXOpen::BlockStyler::SelectObject* selectCuprum;// Block type: Selection
    NXOpen::BlockStyler::IntegerBlock* txtDrfLayer;// Block type: Integer
    NXOpen::BlockStyler::IntegerBlock* txtDrfEndLayer;// Block type: Integer
    NXOpen::BlockStyler::Group* group1;// Block type: Group
    NXOpen::BlockStyler::IntegerBlock* integer03;// Block type: Integer
    NXOpen::BlockStyler::IntegerBlock* integer04;// Block type: Integer
    NXOpen::BlockStyler::StringBlock* string0;// Block type: String
    NXOpen::BlockStyler::Group* group2;// Block type: Group
    NXOpen::BlockStyler::Enumeration* enum02;// Block type: Enumeration
    NXOpen::BlockStyler::Group* group3;// Block type: Group
    NXOpen::BlockStyler::Toggle* toggle01;// Block type: Toggle
    NXOpen::BlockStyler::Toggle* toggle02;// Block type: Toggle
    NXOpen::BlockStyler::Toggle* toggle03;// Block type: Toggle
    
};
#endif //EDMDRAWUI_H_INCLUDED

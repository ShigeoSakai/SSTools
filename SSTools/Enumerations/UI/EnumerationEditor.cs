/* 
    Bitwise Enumeration Editor for PropertyGrid (and, hence, Visual Studio Designer):
    
    BitwiseEnumerationEditor and EnumerationEditor:
        the editors to be used in System.ComponentModel.EditorAttribute
        applied to enumeration types
    
    BitwiseEnumerationTypeConverter and EnumerationTypeConverter:
        type converters to be used in System.ComponentModel.TypeConverterAttribute
        applied to enumeration types
  
    Copyright (C) 2008-2014, 2017 by Sergey A Kryukov
    http://www.SAKryukov.org
    http://www.codeproject.com/Members/SAKryukov

    Original publication: https://www.codeproject.com/Articles/809357/Bitwise-Enumeration-Editor-for-PropertyGrid-and-Vi

*/

namespace SSTools.Enumerations.UI {
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using EnumerationItemBase = SSTools.Enumerations.EnumerationItemBase;
    using IWindowsFormsEditorService = System.Windows.Forms.Design.IWindowsFormsEditorService;
    using Cardinal = System.UInt64;

    public class EnumerationEditor : EnumerationEditorBase {

        protected override object UpdateData(object value, TreeView treeView) {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null)
                return value;
            else
                return ((EnumerationItemBase)selectedNode.Tag).Value;
        } //UpdateData

        protected override bool UseCheckBoxes { get { return false; } }

    } //EnumerationEditor

} //namespace SA.Universal.Enumerations.UI

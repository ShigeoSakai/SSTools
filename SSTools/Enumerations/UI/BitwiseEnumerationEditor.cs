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

    public class BitwiseEnumerationEditor : EnumerationEditorBase {

        protected override object UpdateData(object value, TreeView treeView) {
            Type underlyingType = Enum.GetUnderlyingType(value.GetType());
            object result = EnumerationUtility.MakeZeroObject((Enum)value);
            foreach (TreeNode node in treeView.Nodes)
                if (node.Checked)
                    result = EnumerationUtility.SetBit(((EnumerationItemBase)node.Tag).Value, result);
            return result;
        } //UpdateData

    } //BitwiseEnumerationEditor

} //namespace SA.Universal.Enumerations.UI

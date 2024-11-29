/* 
    Bitwise Enumeration Editor for PropertyGrid (and, hence, Visual Studio Designer):
    
    BitwiseEnumerationEditor and EnumerationEditor:
        the editors to be used in System.ComponentModel.EditorAttribute
        applied to enumeration types
    
    BitwiseEnumerationTypeConverter and EnumerationTypeConverter:
        type converters to be used in System.ComponentModel.TypeConverterAttribute
        applied to enumeration types
  
    Based on SA.Universal.Enumerations 
 
    Copyright (C) 2008-2014, 2017 by Sergey A Kryukov
    http://www.SAKryukov.org
    http://www.codeproject.com/Members/SAKryukov

    Original publication: https://www.codeproject.com/Articles/809357/Bitwise-Enumeration-Editor-for-PropertyGrid-and-Vi

*/

namespace SSTools.Enumerations.UI {
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using IWindowsFormsEditorService = System.Windows.Forms.Design.IWindowsFormsEditorService;
    using EnumerationItemBase = SSTools.Enumerations.EnumerationItemBase;

    public abstract class EnumerationEditorBase : UITypeEditor {

        protected EnumerationEditorBase() {
            treeView.ShowNodeToolTips = true;
            treeView.CheckBoxes = this.UseCheckBoxes;
            treeView.ShowLines = false;
            treeView.ShowPlusMinus = false;
            treeView.ShowRootLines = false;
            treeView.Indent = 0;
        } //EnumerationEditorBase

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            if (value == null) return null;
            Type type = value.GetType();
            if (!type.IsEnum) return null;
            treeView.Nodes.Clear();
            Populate(value);
            edSvc = (IWindowsFormsEditorService)
                provider.GetService(typeof(IWindowsFormsEditorService));
            edSvc.DropDownControl(treeView);
            return UpdateData(value, treeView);
        } //EditValue

        protected void Populate(object value) {
            EnumerationItemBase[] items = EnumerationUtility.ReflectItems(value);
            int selectedIndex = -1;
            for (int index = 0; index < items.Length; ++index) {
                EnumerationItemBase item = items[index];
                TreeNode node = treeView.Nodes.Add(string.Format(DefinitionSet.NodeFormat, item.DisplayName));
                node.ToolTipText = item.Description;
                node.Tag = item;
                if (UseCheckBoxes)
                    treeView.Nodes[index].Checked = EnumerationUtility.IsBitSet(item.Value, value);
                else
                    if (((Enum)value).Equals(item.Value))
                        selectedIndex = index;
            } //loop
            if (selectedIndex >= 0)
                treeView.SelectedNode = treeView.Nodes[selectedIndex];
        } //Populate

        protected abstract object UpdateData(object value, TreeView treeView);
        protected virtual bool UseCheckBoxes { get { return true; } }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.DropDown;
        } //GetEditStyle

        protected IWindowsFormsEditorService edSvc;
        TreeView treeView = new TreeView();

    } //class EnumerationEditor

} //namespace SA.Universal.Enumerations.UI

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

namespace SSTools.Enumerations {
    using Cardinal = System.UInt32;
    using AbbreviationLength = System.Byte;

    public abstract class EnumerationItemBase {

        public override string ToString() {
            return this.FDisplayName;
        } //ToString

        /// <summary>
        /// Name of the static field representing enumeration item.
        /// </summary>
        public string Name { get { return FName; } }

        /// <summary>
        /// Abbreviated name of the static field representing enumeration item; abbreviation is defined by the AbbreviationAttribute
        /// </summary>
        public string AbbreviatedName {
            get {
                if (FAbbreviatedName == null) //lazy
                    FAbbreviatedName = GetAbbreviatedName();
                return FAbbreviatedName;
            } //get AbbreviatedName
        } //AbbreviatedName

        /// <summary>
        /// Name of the item based on DisplayNameAttribute;
        /// the purpose of this member is to provide human-readable name for an item;
        /// it the attribute is not available or does not resolve the name, default value is used: DisplayName = Name 
        /// </summary>
        public string DisplayName { get { return FDisplayName; } }

        /// <summary>
        /// Description of the item based on DescriptionAttribute;
        /// it the attribute is not available or does not resolve the name, default value is used: Description = null
        /// </summary>
        public string Description { get { return FDescription; } }

        /// <summary>
        /// Index of a static field corresponding to present instance of EnumerationItem in the order it appears in the declaration
        /// </summary>
        public Cardinal Index { get { return FIndex; } }

        /// <summary>
        /// Value of a static field corresponding to present instance of EnumerationItem.
        /// The type of Value is always ENUM if ENUM is enumeration type, otherwise it can be of any type.
        /// </summary>
        public object Value { get { return FValue; } }

        #region implementation

        string GetAbbreviatedName() {
            int len = FName.Length;
            if (FAbbreviationLength >= len)
                return FName;
            else
                return FName.Substring(0, FAbbreviationLength);
        } //GetAbbreviatedName

        protected Cardinal FIndex;
        protected string FName, FAbbreviatedName, FDisplayName, FDescription;
        protected AbbreviationLength FAbbreviationLength;
        protected object FValue;

        #endregion implementation

    } //class EnumerationItemBase

} //namespace SA.Universal.Enumerations

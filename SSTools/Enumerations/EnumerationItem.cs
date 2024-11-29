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
    using Debug = System.Diagnostics.Debug;

    /// <summary>
    /// EnumerationItem play the same role in <seealso cref="Enumeration">Enumeration</seealso> class as enum members do in their parent enum types.
    /// EnumerationItem provides more comprehensive information and resolves the situation when different enum members have same integer values.
    /// </summary>
    /// <typeparam name="ENUM">Any type; however, only the set of the public static fields of the type are essential</typeparam>
    public sealed class EnumerationItem<ENUM> : EnumerationItemBase {
        
        private EnumerationItem() { }

        internal EnumerationItem(string name, AbbreviationLength abbreviationLength, string displayName, string description, Cardinal index, object value, ENUM enumValue) {
            this.FName = name;
            this.FDescription = description;
            this.FDisplayName = displayName;
            if (string.IsNullOrEmpty(this.FDisplayName))
                this.FDisplayName = name;
            this.FIndex = index;
            this.FValue = value;
            this.FEnumValue = enumValue;
            this.FAbbreviationLength = abbreviationLength;
            Debug.Assert(FAbbreviationLength > 0, "Abbreviation Length must be greater than zero");
            if (this.FAbbreviationLength < 1)
                this.FAbbreviationLength = 1;
        } //EnumerationItem

        /// <summary>
        /// Value of the static field representing enumeration member.
        /// If ENUM is not an enumeration type, the type of a static field corresponding to present instance of EnumerationItem may be of the type other then ENUN;
        /// in this case EnumValue is assigned to default(ENUM).
        /// </summary>
        public ENUM EnumValue { get { return FEnumValue; } }

        #region implementation

        ENUM FEnumValue;
        #endregion implementation

    } //struct EnumerationItem

} //namespace SA.Universal.Enumerations
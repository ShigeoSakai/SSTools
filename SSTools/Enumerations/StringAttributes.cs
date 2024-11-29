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
    using System;

    public abstract class StringAttribute : Attribute {
        public StringAttribute(string value) { FValue = value; }
        public StringAttribute(Type type) { FType = type; }
        internal string Value { get { return FValue; } }
        internal Type Type { get { return FType; } }
        #region implementation
        string FValue;
        Type FType;
        #endregion implementation
    } //class StringAttribute

    /// <summary>
    /// This attributes provides StringAttributeUtility with data used to generate human-readable Display Name for enumeration members.
    /// The attribute can be applied to enumeration type and/or to individual enumeration members.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class DisplayNameAttribute : StringAttribute {

        /// <summary>
        /// This constructor should be used to specify Display Name for individual enumeration member
        /// </summary>
        /// <param name="value">Display Name for an individual enumeration member</param>
        public DisplayNameAttribute(string value) : base(value) { }

        /// <summary>
        /// This constructor should be used to specify Display Name for all or some members of enumeration or type an individual enumeration member
        /// </summary>
        /// <param name="type">Expects type of the resource class auto-generated when XML resource (.RESX) is created</param>
        public DisplayNameAttribute(Type type) : base(type) { }

    } //class DisplayNameAttribute

    /// <summary>
    /// This attributes provides StringAttributeUtility with data used to generate human-readable Description of enumeration members.
    /// The attribute can be applied to enumeration type and/or to individual enumeration members.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class DescriptionAttribute : StringAttribute {

        /// <summary>
        /// This constructor should be used to specify Description for individual enumeration member
        /// </summary>
        /// <param name="value">Description for an individual enumeration member</param>
        public DescriptionAttribute(string value) : base(value) { }

        /// <summary>
        /// This constructor should be used to specify Description for all or some members of enumeration or type an individual enumeration member
        /// </summary>
        /// <param name="type">Expects type of the resource class auto-generated when XML resource (.RESX) is created</param>
        public DescriptionAttribute(Type type) : base(type) { }

    } //class DisplayNameAttribute

} //namespace SA.Universal.Enumerations

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
    
    static class DefinitionSet {
        internal const string NodeFormat = " {0} ";
        internal const string BitDelimiter = " | ";
		internal const char ParsableEnumOrOperand = ',';
		internal const char EnumOrOperand = '|';
    } //class DefinitionSet

} //namespace SA.Universal.Enumerations.UI

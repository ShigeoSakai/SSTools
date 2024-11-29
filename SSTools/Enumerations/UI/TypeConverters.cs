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
	using CultureInfo = System.Globalization.CultureInfo;

    public class EnumerationTypeConverter<ENUM> : TypeConverter {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            return destinationType == typeof(string);
        } //CanConvertTo
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (value.GetType().IsEnum)
                return EnumerationUtility.EnumerationToString(value, IsBitwise);
            else
                return base.ConvertTo(context, culture, value, destinationType);
        } //ConvertTo
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return sourceType == typeof(string);
		} //CanConvertFrom
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			return Enum.Parse(
				typeof(ENUM),
				((string)value).Replace(DefinitionSet.EnumOrOperand, DefinitionSet.ParsableEnumOrOperand));
		} //ConvertFrom
		protected virtual bool IsBitwise { get { return false; } }
    } //class EnumerationTypeConverter

    public class BitwiseEnumerationTypeConverter<ENUM> : EnumerationTypeConverter<ENUM> {
        protected override bool IsBitwise { get { return true; } }
    } //class BitwiseEnumerationTypeConverter

} //namespace SA.Universal.Enumerations.UI
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
    using System.Reflection;
    using System.Collections;
    using EnumerationItemList = System.Collections.Generic.List<SSTools.Enumerations.EnumerationItemBase>;
    using EnumerationItemBase = SSTools.Enumerations.EnumerationItemBase;
    using StringBuilder = System.Text.StringBuilder;

    internal static class EnumerationUtility {
    
        internal static EnumerationItemBase[] ReflectItems(object value) {
            Type type = typeof(Enumeration<>);
            Type enumerationType = type.MakeGenericType(new Type[] { value.GetType() });
            ConstructorInfo constructor = enumerationType.GetConstructor(Type.EmptyTypes);
            object enumeration = constructor.Invoke(null);
            IEnumerable enumerable = (IEnumerable)enumeration;
            EnumerationItemList list = new EnumerationItemList();
            foreach (object @object in enumerable)
                list.Add((EnumerationItemBase)@object);
            return list.ToArray();
        } //ReflectItems

        internal static bool IsBitSet(object bit, object value) {
            try {
                return ((ulong)Convert.ChangeType(bit, typeof(ulong)) & (ulong)Convert.ChangeType(value, typeof(ulong))) > 0;
            } catch (System.InvalidCastException) {
                return ((long)Convert.ChangeType(bit, typeof(long)) & (long)Convert.ChangeType(value, typeof(long))) > 0;
            } // exception
        } //IsBitSet
        internal static object SetBit(object bit, object value) {
            Type underlyingType = Enum.GetUnderlyingType(bit.GetType());
            try {
                ulong numBit = (ulong)Convert.ChangeType(bit, typeof(ulong));
                ulong numValue = (ulong)Convert.ChangeType(value, typeof(ulong));
                ulong numResult = numValue | numBit;
                return Convert.ChangeType(numResult, underlyingType);
            } catch (System.InvalidCastException) {
                long numBit = (long)Convert.ChangeType(bit, typeof(long));
                long numValue = (long)Convert.ChangeType(value, typeof(long));
                long numResult = numValue | numBit;
                return Convert.ChangeType(numResult, underlyingType);
            } // exception
        } //IsBitSet
        internal static object MakeZeroObject(Enum value) {
            return Convert.ChangeType(0, Enum.GetUnderlyingType(value.GetType()));
        } //MakeZeroObject

        internal static string EnumerationToString(object value, bool bitwise) {
            if (value == null) return null;
            Type type = value.GetType();
            if (!type.IsEnum)
                return null;
            EnumerationItemBase[] items = ReflectItems(value);
            if (items.Length < 1)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (EnumerationItemBase item in items)
                if (bitwise && IsBitSet(item.Value, value)) {
                    if (result.Length > 0)
                        result.Append(DefinitionSet.BitDelimiter);
                    result.Append(item.AbbreviatedName); 
                } else if ( ((Enum)value).Equals(item.Value) )
                    return item.DisplayName;
            return result.ToString();
        } //EnumerationToString

    } //class EnumerationUtility

} // namespace SA.Universal.Enumerations.UI

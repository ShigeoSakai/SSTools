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

    /// <summary>
    /// Generic class EnumerationIndexedArray implements enumeration-indexed arrays
    /// </summary>
    /// <typeparam name="INDEX">Type representing the type for array indices; enum type is recommended, but can be any type with any static fields used for indexing</typeparam>
    /// <typeparam name="ELEMENT">Type representing the type for array values</typeparam>
    public class EnumerationIndexedArray<INDEX, ELEMENT> {

        public EnumerationIndexedArray() {
            InitializeBody(default(ELEMENT), false);
        } //EnumerationIndexedArray

        public EnumerationIndexedArray(ELEMENT sameInitialValue) {
            InitializeBody(sameInitialValue, true);
        } //EnumerationIndexedArray

        /// <summary>
        /// Indexed property used to manipulate array elements.
        /// </summary>
        /// <param name="index">INDEX value used to index the array; must have the same value as one of the static INDEX fields</param>
        /// <returns>Element of the array; may cause out-of-range exception:
        /// Indexing only works if the value passed as index is the same as one of the static INDEX values;
        /// otherwise it returns -1. For example if INDEX is System.Int32, this property works
        /// if index == System.Int32.MaxValue or index == System.Int32.MinValue and cause exception otherwise.
        /// </returns>
        public ELEMENT this[INDEX index] {
            get { return Body[Enumeration<INDEX>.GetIntegerIndexFromEnumValue(index)]; }
            set { Body[Enumeration<INDEX>.GetIntegerIndexFromEnumValue(index)] = value; }
        } //this

        #region implementation
        
        private ELEMENT[] Body;
        private void InitializeBody(ELEMENT sameInitialValue, bool useInitialValue) {
            Body = new ELEMENT[Enumeration<INDEX>.CollectionLength];
            if (!useInitialValue) return;
            for (int jj = 0; jj < Body.Length; jj++)
                Body[jj] = sameInitialValue;
        } //InitializeBody

        #endregion implementation

    } //EnumerationIndexedArray

} //namespace SA.Universal.Enumerations

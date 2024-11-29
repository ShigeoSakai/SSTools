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
    /// Generic class CartesianSquareIndexedArray implements indexed arrays indexed by a Cartesian Square based on an enumeration
    /// </summary>
    /// <typeparam name="INDEX">Type representing the type for array indices; enum type is recommended, but can be any type with any static fields used for indexing</typeparam>
    /// <typeparam name="ELEMENT">Type representing the type for array values</typeparam>
    public class CartesianSquareIndexedArray<INDEX, ELEMENT> {

        public CartesianSquareIndexedArray() {
            InitializeBody(default(ELEMENT), false);
        } //CartesianSquareIndexedArray

        public CartesianSquareIndexedArray(ELEMENT sameInitialValue) {
            InitializeBody(sameInitialValue, true);
        } //CartesianSquareIndexedArray

        /// <summary>
        /// Indexed property used to manipulate array elements.
        /// </summary>
        /// <returns>Element of the array; may cause out-of-range exception:
        /// Indexing only works if the value passed as index is the same as one of the static INDEX values;
        /// otherwise it returns -1. For example if INDEX is System.Int32, this property works
        /// if index == System.Int32.MaxValue or index == System.Int32.MinValue and cause exception otherwise.
        /// </returns>
        public ELEMENT this[INDEX from, INDEX to] {
            get { return Body[Enumeration<INDEX>.GetIntegerIndexFromEnumValue(from), Enumeration<INDEX>.GetIntegerIndexFromEnumValue(to)]; }
            set { Body[Enumeration<INDEX>.GetIntegerIndexFromEnumValue(from), Enumeration<INDEX>.GetIntegerIndexFromEnumValue(to)] = value; }
        } //this

        #region implementation

        private ELEMENT[,] Body;
        private void InitializeBody(ELEMENT sameInitialValue, bool useInitialValue) {
            uint len = Enumeration<INDEX>.CollectionLength;
            Body = new ELEMENT[len, len];
            if (!useInitialValue) return;
            for (int xx = 0; xx < len; xx++)
                for (int yy = 0; yy < Body.Length; yy++)
                    Body[xx, yy] = sameInitialValue;
        } //InitializeBody

        #endregion implementation

    } //CartesianSquareIndexedArray

} //namespace SA.Universal.Enumerations
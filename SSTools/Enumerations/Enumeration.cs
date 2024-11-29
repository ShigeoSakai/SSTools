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
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using ReaderWriterLockSlim = System.Threading.ReaderWriterLockSlim;
    using Cardinal = System.UInt32;
    using AbbreviationLength = System.Byte; //SA???new

    /// <summary>
    /// NonEnumerableAttribute can be applied to an Enum field to exclude it from iteration sequence
    /// when used as a in the generic class <see cref="Enumeration"/>.
    /// This attribute can be applied to any other static field of any type, with the same effect.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NonEnumerableAttribute : Attribute { }

    //SA???new:
    /// <summary>
    /// Abbreviation attribute allows to specify number of characters in the abbreviated string representing enumeration member name used in command line
    /// (<seealso cref="SA.Universal.Utilities.CommandLine<SWITCHES, VALUES>"/>)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class AbbreviationAttribute : Attribute {
        public AbbreviationAttribute() { this.FAbbreviationLength = 1; }
        public AbbreviationAttribute(AbbreviationLength length) { this.FAbbreviationLength = length; }
        public AbbreviationLength AbbreviationLength { get { return FAbbreviationLength; } }
        AbbreviationLength FAbbreviationLength;
    } //class AbbreviationAttribute
    //end SA???new:

    /// <summary>
    /// Class supporting interface IEnumerable
    /// to allow of iterations through the set of public static fields of the type parameter in natural order,
    /// that is, in the order the public static fields appear in the declaration (alrernatively, in reverse order)
    /// </summary>
    /// <typeparam name="ENUM">There is no constraint on this type; for most typical application this is an enumeration type</typeparam>
    public class Enumeration<ENUM> : IEnumerable<EnumerationItem<ENUM>> {

        public Enumeration() {
            BuildEnumerationCollection();
            FEnumeratorInstance = new Enumerator(this);
        } //Enumeration

        public static Cardinal CollectionLength {
            get {
                BuildEnumerationCollection();
                return FCollectionLength;
            } //get CollectionLength
        } //CollectionLength
        public static ENUM First {
            get {
                BuildEnumerationCollection();
                if (FCollectionLength < 1)
                    return default(ENUM);
                else
                    return EnumerationCollection[0].EnumValue;
            } //get First
        } //First
        public static ENUM Last {
            get {
                BuildEnumerationCollection();
                if (FCollectionLength < 1)
                    return default(ENUM);
                else
                    return EnumerationCollection[FCollectionLength - 1].EnumValue;
            } //get Last
        } //Last

        public EnumerationItem<ENUM> this[Cardinal index] { get { return EnumerationCollection[index]; } }
        public bool IsReverse { get { return FReverse; } set { FReverse = value; } }

        public bool Reverse(bool reset) {
            IsReverse = !FReverse;
            if (reset)
                EnumeratorInstance.Reset();
            return FReverse;
        } //Reverse

        #region implementation

        IEnumerator<EnumerationItem<ENUM>> IEnumerable<EnumerationItem<ENUM>>.GetEnumerator() { return EnumeratorInstance; }
        IEnumerator IEnumerable.GetEnumerator() { return EnumeratorInstance; }

        IEnumerator<EnumerationItem<ENUM>> FEnumeratorInstance;
        IEnumerator<EnumerationItem<ENUM>> EnumeratorInstance {
            get {
                FEnumeratorInstance.Reset();
                return FEnumeratorInstance;
            } //get EnumeratorInstance
        } //EnumeratorInstance

        bool FReverse;

        private class Enumerator : IEnumerator, IEnumerator<EnumerationItem<ENUM>> {
            internal Enumerator(Enumeration<ENUM> owner) { this.Owner = owner; }
            object IEnumerator.Current { get { return EnumerationCollection[Owner.CollectionCurrent]; } }
            bool IEnumerator.MoveNext() {
                if (FCollectionLength < 1) return false;
                if (Owner.FReverse) {
                    if (!Owner.BelowZero && Owner.CollectionCurrent > 0) {
                        Owner.CollectionCurrent--;
                        return true;
                    } else {
                        Owner.CollectionCurrent = 0;
                        Owner.BelowZero = true;
                    } //if
                } else { //forward:
                    if (Owner.BelowZero) {
                        Owner.CollectionCurrent = 0;
                        Owner.BelowZero = false;
                        return true;
                    } else if (Owner.CollectionCurrent + 1 < FCollectionLength) {
                        Owner.CollectionCurrent++;
                        return true;
                    } //if
                } //if forward
                return false;
            } //IEnumerator.MoveNext
            void IDisposable.Dispose() { }
            EnumerationItem<ENUM> IEnumerator<EnumerationItem<ENUM>>.Current { get { return EnumerationCollection[Owner.CollectionCurrent]; } }
            void IEnumerator.Reset() {
                Owner.BelowZero = !Owner.FReverse;
                if (Owner.FReverse)
                    Owner.CollectionCurrent = FCollectionLength;
                else
                    Owner.CollectionCurrent = 0;
            } //IEnumerator.Reset
            internal protected Enumeration<ENUM> Owner;
        } //class Enumerator

        delegate void BuildAction();
        static void Build(BuildAction action, object existenceCheck, ReaderWriterLockSlim readwriteLock) { //just a locking pattern
            readwriteLock.EnterUpgradeableReadLock();
            try {
                if (existenceCheck == null) {
                    readwriteLock.EnterWriteLock();
                    try {
                        action();
                    } finally {
                        readwriteLock.ExitWriteLock();
                    } //write lock
                } //if
            } finally {
                readwriteLock.ExitUpgradeableReadLock();
            } //read lock
        } //Build

        static void BuildEnumerationCollection() {
#if THREAD_SAFE_ENUMERATIONS
            Build(BuildEnumerationCollectionCore, EnumerationCollection, EnumerationCollectionLock);
#else
            if (EnumerationCollection != null) return;
            BuildEnumerationCollectionCore();
#endif
        } //BuildEnumerationCollection

        static void BuildEnumerationCollectionCore() {
            Type type = typeof(ENUM);
            bool isEnum = type.IsEnum;
            FieldInfo[] fields = GetStaticFields(type);
            List<EnumerationItem<ENUM>> list = new List<EnumerationItem<ENUM>>();
            Cardinal currentIndex = 0;
            for (Cardinal jj = 0; jj < (Cardinal)fields.Length; jj++) {
                FieldInfo field = fields[jj];
                object[] attributes = field.GetCustomAttributes(typeof(NonEnumerableAttribute), false);
                if (attributes.Length > 0) continue;
                object objValue = field.GetValue(null); //boxed if ENUM is primitive
                if (objValue == null) continue;
                ENUM enumValue = default(ENUM);
                if (isEnum)
                    enumValue = (ENUM)objValue;
                else {
                    if (objValue is ENUM) //this object-oriented dynamic check always works even if ENUM is primitive type because objValue is boxed object
                        enumValue = (ENUM)objValue;
                } //if not enum
                string name = field.Name;
                //SA???new:
                attributes = field.GetCustomAttributes(typeof(AbbreviationAttribute), false);
                AbbreviationLength abbreviationLength = AbbreviationLength.MaxValue;
                if (attributes.Length > 0) {
                    AbbreviationAttribute attr = (AbbreviationAttribute)attributes[0];
                    abbreviationLength = attr.AbbreviationLength;
                } //if Abbreviation works
                list.Add(new EnumerationItem<ENUM>(name, abbreviationLength, GetDisplayName(field), GetDescription(field), currentIndex, objValue, enumValue));
                currentIndex++;
                //end SA???new
            } //loop
            EnumerationCollection = list.ToArray();
            FCollectionLength = (Cardinal)EnumerationCollection.Length;
        } //BuildEnumerationCollectionCore

        static string GetDisplayName(FieldInfo field) {
            string value = StringAttributeUtility.ResolveValue<DisplayNameAttribute>(field);
            if (string.IsNullOrEmpty(value))
                value = field.Name;
            return value;
        } //GetDisplayName
        static string GetDescription(FieldInfo field) {
            return StringAttributeUtility.ResolveValue<DescriptionAttribute>(field);
        } //GetDescription

        /// <summary>
        /// BuildIndexDictionary only used to support EnumerationIndexedArray via GetIntegerIndexFromEnumValue;
            /// If nobody calls GetIntegerIndexFromEnumValue, IndexDictionary remains null
        /// </summary>
        static void BuildIndexDictionary() {
#if THREAD_SAFE_ENUMERATIONS
            Build(BuildIndexDictionaryCore, IndexDictionary, IndexDictionaryLock);
#else
            if (IndexDictionary != null) return;
            BuildIndexDictionaryCore();
#endif
        } //BuildIndexDictionary

        static void BuildIndexDictionaryCore() {
            BuildEnumerationCollection();  //lazy evaluation: does nothing if already built
            IndexDictionary = new Dictionary<ENUM, uint>();
            for (Cardinal jj = 0; jj < FCollectionLength; jj++) {
                ENUM dictionaryKey = EnumerationCollection[jj].EnumValue;
                if (!IndexDictionary.ContainsKey(dictionaryKey))
                    IndexDictionary.Add(EnumerationCollection[jj].EnumValue, jj);
            } //loop
        } //BuildIndexDictionaryCore

        static FieldInfo[] GetStaticFields(Type type) {
            return type.GetFields(BindingFlags.Static | BindingFlags.Public);
        } //GetStaticFields

        Cardinal CollectionCurrent;
        bool BelowZero = true;
        static Cardinal FCollectionLength;
        static EnumerationItem<ENUM>[] EnumerationCollection; //only used to support EnumerationIndexedArray via GetIntegerIndexFromEnumValue
        static Dictionary<ENUM, Cardinal> IndexDictionary;
        static ReaderWriterLockSlim EnumerationCollectionLock = new ReaderWriterLockSlim();
        static ReaderWriterLockSlim IndexDictionaryLock = new ReaderWriterLockSlim();

        #endregion implementation

        #region internal implementation for EnumerationIndexedArray

        /// <summary>
        /// GetIntegerIndexFromEnumValue only used to support EnumerationIndexedArray;
        /// it retrieve integer index of a static INDEX field based on sone INDEX value;
        /// It only works if the value passed as index is the same as one of the static INDEX values;
        /// otherwise it returns -1. For example if INDEX is System.Int32, this function returns 0
        /// if index == System.Int32.MaxValue, 1 if index == System.Int32.MinValue and 0 otherwise.
        /// </summary>
        /// <param name="index">INDEX value used to retrieve the index of a static field in INDEX</param>
        /// <returns>Integer index of INDEX static field</returns>
        internal static int GetIntegerIndexFromEnumValue(ENUM index) {
            BuildIndexDictionary(); //lazy evaluation: does nothing if already built
            Cardinal intIndex;
            if (IndexDictionary.TryGetValue(index, out intIndex))
                return (int)intIndex;
            else
                return -1;
        } //GetIntegerIndexFromEnumValue

        #endregion internal implementation for EnumerationIndexedArray

    } //class Enumeration

} //namespace SA.Universal.Enumerations
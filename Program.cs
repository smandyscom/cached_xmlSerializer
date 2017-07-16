using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace xmlSerializerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
           var __serializer1 =  utilities.getSerializer(typeof(theBaseClass1),new Type[]{typeof(theDerivedClass1)});
           var __serializer2 =  utilities.getSerializer(typeof(theBaseClass1),new Type[]{typeof(theDerivedClass1)});
           Console.WriteLine(__serializer1.Equals(__serializer2)); //check if the cached serializer fetched

           var __serializer3 = utilities.getSerializer(typeof(theBaseClass2),new Type[]{typeof(theDerivedClass2)});
           Console.WriteLine(__serializer3.Equals(__serializer1));
        }
    }
    public class theBaseClass1
    {
        int field1 = 0;
    }
    public class theDerivedClass1
    {
        int field2 = 0;
    }
    public class theBaseClass2
    {
        int field1 = 1;
    }
    public class theDerivedClass2
    {
        int field2 = 1;
    }

    class utilities
    {
        /// <summary>
        /// Used to store key-serializer
        /// </summary>
        /// <returns></returns>
        private static Hashtable __dictionary = new Hashtable(); 
        /// <summary>
        /// Generate key according to contents of type list
        /// </summary>
        /// <param name="typeList"></param>
        /// <returns></returns>
        protected static int generateKey(List<Type> typeList)
        {
            //hash code summarize
           return typeList.Sum(__type => __type.GetHashCode());
        }
        /// <summary>
        /// Always fetch serializer from this function
        /// (Never allocate by yourself)
        /// </summary>
        /// <param name="mainType"></param>
        /// <param name="extraTypes"></param>
        /// <returns></returns>
        public static XmlSerializer getSerializer(Type mainType,Type[] extraTypes){
            
            //merge the list
            List<Type> __typeList = new List<Type>();

            __typeList.Add(mainType);
            __typeList.AddRange(extraTypes);

            var key = generateKey(__typeList);

            if (__dictionary.ContainsKey(key))
            {
                // this kind of serializer had been cached/used before , 
                // get the cached serializer
                return (XmlSerializer)__dictionary[key];
                }
            else
            {
                // never been used , need to create a new one and push into cache
                XmlSerializer __serializer = new XmlSerializer(mainType,extraTypes);
                __dictionary.Add(key,__serializer);
                return __serializer;
             }
        }//getserializer
    }//utilities
}//namspace

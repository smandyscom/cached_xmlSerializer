using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;

namespace xmlSerializerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            utilities.getSerializer(typeof(theBaseClass1),new Type[]{typeof(theDerivedClass1)});
            utilities.getSerializer(typeof(theBaseClass1),new Type[]{typeof(theDerivedClass1)});
        }
    }
    public class theBaseClass1{
        int field1 = 0;
    }
    public class theDerivedClass1{
        int field2 = 0;
    }

    class utilities
    {
        /// <summary>
        /// Used to store typelist-serializer pair
        /// </summary>
        /// <returns></returns>
        
        private static Hashtable __dictionary = new Hashtable(); 
        
        public static XmlSerializer getSerializer(Type mainType,Type[] extraTypes){
            
            //merge the list
            List<Type> __typeList = new List<Type>();

            __typeList.Add(mainType);
            __typeList.AddRange(extraTypes);

            if (__dictionary.ContainsKey(__typeList))
            {
                // this kind of serializer had been cached/used before , 
                // get the cached serializer
                return (XmlSerializer)__dictionary[__typeList];
                }
            else
            {
                // never been used , need to create a new one and push into cache
                XmlSerializer __serializer = new XmlSerializer(mainType,extraTypes);
                __dictionary.Add(__typeList,__serializer);
                return __serializer;
             }
        }//getserializer
    }//utilities
}//namspace

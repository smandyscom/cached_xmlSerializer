using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace xmlSerializerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class theBaseClassBranch1{

    }
    class theDerivedClassBranch1{
        
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

         if (__dictionary.ContainsKey(__typeList)){
             // this kind of serializer had been cached/used before , 
             // get the cached serializer
             return (XmlSerializer)__dictionary[__typeList];
         }
         else{
             // never been used , need to create a new one and push into cache
             XmlSerializer __serializer = new XmlSerializer(mainType,extraTypes);
             __dictionary.Add(__typeList,__serializer);
             return __serializer;
      }
    }
}

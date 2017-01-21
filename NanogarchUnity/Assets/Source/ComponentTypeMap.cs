using Entitas;
using System;
using UnityEngine;
using System.Collections.Generic;

public class ComponentTypeMap {

 
    public readonly string[] componentNames;
    public readonly Type[] componentTypes;
    public readonly int[] componentIds;

    public ComponentTypeMap(Type t, string[] componentNames,Type[] componentTypes) 
    {
        List<Type> filteredTypes = new List<Type>();
        List<string> filteredNames = new List<string>();
        List<int> filteredIds = new List<int>();
        for(int i=0; i<componentTypes.Length; i++)
        {
            // Debug.Log("Checking if: "+componentTypes[i]+" is compatible with : "+t);

            // if(componentTypes[i].IsAssignableFrom(t))
            if(Array.IndexOf(componentTypes[i].GetInterfaces(),t)!=-1)
            {
                filteredTypes.Add(componentTypes[i]);
                filteredNames.Add(componentNames[i]);
                filteredIds.Add(i);
                // Debug.Log("Mapping "+i+" to: "+componentNames[i]+":"+componentTypes[i]);
            }
        }
        this.componentIds = filteredIds.ToArray();
        this.componentNames = filteredNames.ToArray();
        this.componentTypes = filteredTypes.ToArray();
    }
    public IMatcher CreateMatcher()
    {
         var matcher = (Matcher)Matcher.AnyOf(this.componentIds);
         matcher.componentNames =componentNames;
         return matcher;
    }
 
}
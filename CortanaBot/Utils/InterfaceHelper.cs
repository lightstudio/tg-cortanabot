using System;
using System.Collections.Generic;
using System.Linq;

namespace CortanaBot.Utils
{
    public static class InterfaceHelper
    {
        // SEE HERE: http://stackoverflow.com/questions/80247/implementations-of-interface-through-reflection
        /// <summary>
        /// Returns all types in the current AppDomain implementing the interface or inheriting the type. 
        /// </summary>
        public static IEnumerable<Type> TypesImplementingInterface<T>()
        {
            var query = AppDomain
                   .CurrentDomain
                   .GetAssemblies()
                   .SelectMany(assembly => assembly.GetTypes())
                   .Where(typeof(T).IsAssignableFrom);
            return from c in query where c.FullName != typeof (T).FullName select c;
        }
    }
}
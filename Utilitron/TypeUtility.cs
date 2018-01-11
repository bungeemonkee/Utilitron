using System;

namespace Utilitron
{
    public static class TypeUtility
    {
        /// <summary>
        /// Get the depth of one type in another types inheritance tree.
        /// </summary>
        /// <param name="subType">The type who's inheritance tree will be searched.</param>
        /// <param name="baseType">The type to search for in the inheritance tree.</param>
        /// <returns>How many types down in the inheritance tree of 'subType' is 'baseType' or -1 if 'subType' does not inherit from 'baseType'.</returns>
        public static int GetTypeDepth(Type subType, Type baseType)
        {
            if (subType == baseType)
                return 0;

            var depth = 0;
            while (subType != baseType)
            {
                depth += 1;
                subType = subType.BaseType;

                if (subType == null)
                    return -1;
            }
            return depth;
        }
    }
}

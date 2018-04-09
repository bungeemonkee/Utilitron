using System;
using System.ComponentModel;
using System.Reflection;

namespace Utilitron
{
    /// <summary>
    /// Extensions for enumerable values.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get's the value of the <see cref="DescriptionAttribute"/> on an enum value.
        /// Defaults to the string value if there is no <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The string value.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            // Get the "field" for this enumeration value
            var type = value.GetType();
            var name = value.ToString();
            var field = type.GetField(name);
            if (field == null)
            {
                throw new ArgumentException(nameof(value), "Value is not a valid enumeration value.");
            }

            // Get the attribute
            var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            // Return the description if there is one or the name if there is not
            return attribute?.Description
                ?? name;
        }
    }
}

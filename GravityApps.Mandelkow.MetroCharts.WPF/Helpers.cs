using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GravityApps.Mandelkow.MetroCharts
{
    public class Helpers
    {
        /// <summary>
        /// get the setter for a property using the 'based on' styles if needed
        /// styles if needed
        /// </summary>
        /// <param name="style"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static Setter getCalculatedSetter(Style style, string propertyname, bool throwError = true)
        {
            var propertySetter = getSetter(style, propertyname);
            if (propertySetter == null)
            {
                Style currentStyle = style;
                while (currentStyle.BasedOn != null)
                {
                    currentStyle = currentStyle.BasedOn;
                    var tempSetter = getSetter(currentStyle, propertyname);
                    if (tempSetter != null)
                    {
                        propertySetter = tempSetter;
                        break;
                    }

                }

                if (propertySetter == null)
                {
                    if (throwError)
                    {
                        throw new Exception("cant find [" + propertyname + "] setter for target [" + style.TargetType.FullName);
                    }

                }
            }
            return propertySetter;
        }

        /// <summary>
        /// get the style setter of an object
        /// </summary>
        /// <param name="styleName"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static Setter getSetter(Style styleName, string propertyname)
        {
            var setter = styleName.Setters.OfType<Setter>().FirstOrDefault(s => s.Property.Name == propertyname);
            return setter;
        }

  }
}

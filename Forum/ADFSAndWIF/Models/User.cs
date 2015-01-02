using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ADFSAndWIF.Models;

namespace ADFSAndWIF.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AuthenticationOptions SelectedAuthenticationOption { get; set; }
        public static List<SelectListItem> AuthenticationOptionSelectItemList
        {
            get {
                return Enum
                        .GetValues(typeof(AuthenticationOptions))
                        .Cast<AuthenticationOptions>()
                        .Select(x => new SelectListItem { Value = x.ToString(), Text = x.GetDescription() })
                        .ToList();
            }    
        }
    }

    public enum AuthenticationOptions
    {
        [Description("Standard")]
        Standard,

        [Description("Active Directory")]
        ActiveDirectory,

        [Description("ADF And WIF")]
        ADFSAndWif
    }

    public static class EnumResolver
    {
        public static string GetDescription<T>(this T enumerationValue)
                    where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace POD_Async.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
    public class IPAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return IPAddress.TryParse(value.ToString(), out _);
        }
    }
}

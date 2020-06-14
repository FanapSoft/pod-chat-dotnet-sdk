using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace POD_Async.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string other;
        private readonly string other1;
        private readonly string other2;
        private readonly bool doubleCheck;
        private readonly bool tripleCheck;
        private const string ErrorMsg = " - One of these Builder fields are required.";

        public RequiredIfAttribute(string other)
        {
            this.other = other;
        }

        public RequiredIfAttribute(string other, string other1)
        {
            this.other = other;
            this.other1 = other1;
            doubleCheck = true;
        }

        public RequiredIfAttribute(string other, string other1, string other2)
        {
            this.other = other;
            this.other1 = other1;
            this.other2 = other2;
            tripleCheck = true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (tripleCheck) return CheckWithThreeFields(value, validationContext);
            if (doubleCheck) return CheckWithTwoFields(value, validationContext);
            return CheckWithOneField(value, validationContext);
        }

        private ValidationResult CheckWithThreeFields(object value, ValidationContext validationContext)
        {
            var fields = validationContext.ObjectType.GetRuntimeFields().ToList();
            var field = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other));
            var field1 = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other1));
            var field2 = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other2));

            if (field == null || field1 == null || field2 == null)
            {
                return new ValidationResult($"Unknown property");
            }

            var otherValue = field.GetValue(validationContext.ObjectInstance);
            var otherValue1 = field1.GetValue(validationContext.ObjectInstance);
            var otherValue2 = field2.GetValue(validationContext.ObjectInstance);
            if (value == null && otherValue == null && otherValue1 == null && otherValue2 == null)
            {
                return new ValidationResult($"{field.Name} Or {field1.Name} Or {field2.Name}{ErrorMsg}");
            }
            return null;
        }
        private ValidationResult CheckWithTwoFields(object value, ValidationContext validationContext)
        {
            var fields = validationContext.ObjectType.GetRuntimeFields().ToList();
            var field = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other));
            var field1 = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other1));

            if (field == null || field1 == null)
            {
                return new ValidationResult($"Unknown property");
            }

            var otherValue = field.GetValue(validationContext.ObjectInstance);
            var otherValue1 = field1.GetValue(validationContext.ObjectInstance);
            if (value == null && otherValue == null && otherValue1 == null)
            {
                return new ValidationResult($"{field.Name} Or {field1.Name}{ErrorMsg}");
            }
            return null;
        }

        private ValidationResult CheckWithOneField(object value, ValidationContext validationContext)
        {
            var field = GetField(validationContext);
            var property = GetProperty(validationContext);

            if (field == null && property == null)
            {
                return new ValidationResult($"Unknown property");
            }

            var otherValue = field != null ? field.GetValue(validationContext.ObjectInstance) : property.GetValue(validationContext.ObjectInstance);

            if (value == null && otherValue == null)
            {
                return new ValidationResult($"{field?.Name}{property?.Name}{ErrorMsg}");
            }

            return null;
        }

        private FieldInfo GetField(ValidationContext validationContext)
        {
            var fields = validationContext.ObjectType.GetRuntimeFields().ToList();
            var field = fields.FirstOrDefault(fieldInfo => fieldInfo.Name.Equals(other));
            return field;
        }

        private PropertyInfo GetProperty(ValidationContext validationContext)
        {
            var properties = validationContext.ObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            var property = properties.FirstOrDefault(prop => prop.Name.Equals(other));
            return property;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace KonceptSupportLibrary
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public object _targetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            this._dependentProperty = dependentProperty;
            this._targetValue = targetValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var field = validationContext.ObjectType.GetProperty(_dependentProperty);
                if (field != null)
                {
                    var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                    if (dependentValue != null)
                    {
                        if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
                        {
                            if (!_innerAttribute.IsValid(value))
                            {
                                string name = validationContext.DisplayName;
                                return new ValidationResult(ErrorMessage = name + " Is required.");
                            }
                        }
                    }
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(_dependentProperty));
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }


    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }

            return false;
        }
    }

    #region Inventory Range Validator
    public class InventoryRangeAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }

        public InventoryRangeAttribute(string dependentProperty)
        {
            this._dependentProperty = dependentProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if (Convert.ToInt64(dependentValue) == 1)
                {
                    if (long.TryParse(value.ToString(),out long result))
                    {
                        if (Convert.ToInt64(value) < 1 || Convert.ToInt64(value) > 99999999)
                        {
                            if (!_innerAttribute.IsValid(value))
                            {
                                return new ValidationResult(ErrorMessage = "Invalid Range");
                            }
                        }
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage = "Invalid Range");
                    }
                }
                else if (Convert.ToInt64(dependentValue) == 2)
                {
                    if (long.TryParse(value.ToString(), out long result))
                    {
                        if (Convert.ToInt64(value) < 1 || Convert.ToInt64(value) > 9999999999)
                        {
                            if (!_innerAttribute.IsValid(value))
                            {
                                return new ValidationResult(ErrorMessage = "Invalid Range");
                            }
                        }
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage = "Invalid Range");
                    }
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }
    }
    #endregion

    #region Inventory String Length Validator
    public class InventoryStringLengthAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }

        public InventoryStringLengthAttribute(string dependentProperty)
        {
            this._dependentProperty = dependentProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                if (Convert.ToInt64(dependentValue) == 1)
                {
                    if (value.ToString().Length < 7 || value.ToString().Length > 8)
                    {
                        if (!_innerAttribute.IsValid(value))
                        {
                            string name = validationContext.DisplayName;
                            return new ValidationResult(ErrorMessage = "Length should be between 7 or 8");
                        }
                    }
                }
                else if (Convert.ToInt64(dependentValue) == 2)
                {
                    if (value.ToString().Length < 10 || value.ToString().Length > 10)
                    {
                        if (!_innerAttribute.IsValid(value))
                        {
                            string name = validationContext.DisplayName;
                            return new ValidationResult(ErrorMessage = "Length should be 10");
                        }
                    }
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }
    }
    #endregion
}

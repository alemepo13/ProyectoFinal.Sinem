using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sinem
{
    public class validarFecha : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime fecha = (DateTime)value;
            if (DateTime.Now.AddYears(-90).CompareTo(value)<=0 && DateTime.Now.AddYears(-3).CompareTo (value)>=0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("La fecha de nacimiento debe ser razonable");
            }
        }
    }
}
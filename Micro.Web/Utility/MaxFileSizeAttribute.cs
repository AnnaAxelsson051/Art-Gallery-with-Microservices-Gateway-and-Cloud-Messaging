using System;
using System.ComponentModel.DataAnnotations;

namespace Micro.Web.Utility
{
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;
		public MaxFileSizeAttribute(int maxFileSize)
		{
            _maxFileSize = maxFileSize;
		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if(file != null)
			{
				if (file.Length > (_maxFileSize * 1024 * 1024))
				{
					return new ValidationResult($"Maximum allowed filesize is {_maxFileSize} MB");
				}
			}
			return ValidationResult.Success;
		}
		
	}
}


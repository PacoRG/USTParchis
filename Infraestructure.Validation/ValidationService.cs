﻿using Domain.Model;
using Domain.Model.Extensions;
using DomainServices.Services.Interfaces;
using Infraestructure.Reflection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Infraestructure.Validation
{
    public class ValidationService : IValidationService
    {
        Dictionary<Type, string> _errorMessagesMap;

        IStringLocalizer _sharedLocalizer;
        IStringLocalizerFactory _stringLocalizerFactory;

        public ValidationService(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
            _sharedLocalizer = stringLocalizerFactory.Create(null);
            _errorMessagesMap = new Dictionary<Type, string>
            {
                {typeof(RequiredAttribute), "RequiredError" },
                {typeof(MaxLengthAttribute), "MaxLengthError" }
            };
        }

        public List<ValidationResult> Validate(object entity)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var totalResults = new List<ValidationResult>();

            ValidateObject(context, entity, totalResults);
            return totalResults;
        }

        private void ValidateObject(ValidationContext context, object entity, List<ValidationResult> validationResults)
        {
            if (entity == null) return;

            var objectType = entity.GetType();

            foreach (var property in objectType.GetProperties())
            {
                var propertyValue = property.GetValue(entity);

                if (propertyValue.IsDataEntity())
                    this.ValidateObject(context, propertyValue, validationResults);

                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    ValidateAttributeForProperty(context, entity, validationResults, attribute, property);
                }

            }
        }

        private void ValidateAttributeForProperty(
            ValidationContext context,
            object entity,
            List<ValidationResult> validationResults,
            Attribute attribute,
            PropertyInfo property)
        {
            var validationAttribute = attribute as ValidationAttribute;
            var validatAttributes = new ValidationAttribute[] { validationAttribute };

            if (validationAttribute == null) return;

            bool isValid = Validator.TryValidateValue(
                property.GetValue(entity),
                context,
                new List<ValidationResult>(),
                validatAttributes);

            if (isValid) return;

            var errorMessage = validationAttribute.ErrorMessage;
            var dictionaryValue = string.Empty;
            var propertyName = LocalizePropertyName(property, entity);
            var propertiesName = new string[] { propertyName };

            if (IsAttributeGeneric(attribute, dictionaryValue))
            {
                errorMessage = LocalizedPredefinedAttribute(errorMessage, attribute, propertiesName);
            }
            else
            {
                errorMessage = LocalizeEntityAttribute(errorMessage, entity, propertiesName);
            }

            var membersName = new string[] { property.Name };
            var validationResult = new ValidationResult(errorMessage, membersName);
            validationResults.Add(validationResult);

        }

        private bool IsAttributeGeneric(Attribute attribute, string dictionaryValue)
        {
            return _errorMessagesMap.TryGetValue(attribute.GetType(), out dictionaryValue);
        }

        private string LocalizePropertyName(PropertyInfo property, object entity)
        {
            var localizer = _stringLocalizerFactory.Create(entity.GetType());
            return localizer[property.Name];
        }

        private string LocalizedPredefinedAttribute(string errorMessage, Attribute attribute, params object[] memberNames)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                string dictionaryValue;
                _errorMessagesMap.TryGetValue(attribute.GetType(), out dictionaryValue);
                errorMessage = _sharedLocalizer[dictionaryValue, memberNames];
            }
            else
            {
                errorMessage = _sharedLocalizer[errorMessage, memberNames];
            }

            return errorMessage;
        }


        private string LocalizeEntityAttribute(string errorMessage, object entity, params object[] memberNames)
        {
            var localizer = _stringLocalizerFactory.Create(entity.GetType());
            errorMessage = localizer[errorMessage, memberNames];

            return errorMessage;
        }
    }
}

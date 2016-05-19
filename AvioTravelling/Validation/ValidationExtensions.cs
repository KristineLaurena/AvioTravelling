using AvioTravelling.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace AvioTravelling.Validation
{
    public static class ValidationExtensions
    {
        public static void Validate<T>(this T entity, AbstractValidator<T> validator)
            where T : ModelBase
        {
            var results = validator.Validate(entity);

            if (results.Errors.Any())
            {
                var errors = results.Errors.Select(x => new Error { Text = $"{x.PropertyName} : {x.ErrorMessage}", Id = Guid.NewGuid() });
                throw new ValidationException(errors);
            }
        }

        public static T ConvertJSonToObject<T>(this string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings {  });
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
        }
    }
}
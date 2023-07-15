using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            // sample sort query: books?orderBy=title,price desc,id asc
            // sample params: "title", "price desc", "id asc"
            var orderParams = orderByQueryString.Trim().Split(',');

            // Getting the public and non-static properties of the Book class 
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                // getting the property name from param
                // example: {"price", "desc"}[0] = "price"
                var propertyFromQuery = param.Split(' ')[0];

                // getting the property and ignoring case differences
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQuery, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                var direction = param.EndsWith("desc") ? "descending" : "ascending";

                // creating order queries like "price ascending" and adding them by seperating a comma
                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            // removing the last comma 
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}

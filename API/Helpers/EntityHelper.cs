// namespace API.Helpers;
//
// public class EntityHelper
// {
//     public static bool EntityExists<T>(IQueryable<T> query, object keyValue)
//     {
//         var keyProperty = typeof(T).GetProperties()
//             .FirstOrDefault(p => p.IsKeyOrForeignKey()); // Assuming helper method to check for key
//
//         if (keyProperty == null)
//         {
//             throw new ArgumentException("Entity type T does not have a key property.");
//         }
//
//         var parameter = Expression.Parameter(typeof(T), "e");
//         var keyExpression = Expression.Property(parameter, keyProperty.Name);
//         var constantExpression = Expression.Constant(keyValue);
//         var equalExpression = Expression.Equal(keyExpression, constantExpression);
//         var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);
//
//         return query.Any(lambda);
//     }
// }
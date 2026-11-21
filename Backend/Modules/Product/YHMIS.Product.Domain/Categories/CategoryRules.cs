using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.Categories;

public sealed partial record Category
{
    public static class CategoryRules
    {
        /// <summary>
        /// Crea una nueva categoría y valida sus propiedades iniciales.
        /// </summary>
        /// <returns>
        /// Si los parámetros son válidos, devuelve la nueva categoría.
        /// De lo contrario, devuelve un error.
        /// </returns>
        public static Result<Category> Create(int id, string name)
        {
            if (id < 0)
                return Result<Category>.Failure(
                    new Error("Category.Invalid.Id", "Id cannot be negative"));

            var nameRule = NameRule(name);

            if (nameRule.HasValue)
                return Result<Category>.Failure(nameRule.Value);

            return Result<Category>.Success(new Category(id, name));
        }

        /// <summary>
        /// Cambia el nombre de una categoría.
        /// </summary>
        /// <returns>
        /// Si el nuevo nombre es válido, devuelve la misma categoría con el nombre actualizado.
        /// De lo contrario, devuelve un error.
        public static Result<Category> ChangeName(in Category category, string newName)
        {
            var nameRule = NameRule(newName);

            if (nameRule.HasValue)
                return Result<Category>.Failure(nameRule.Value);

            return Result<Category>.Success(
                category with { Name = newName });
        }

        private static Error? NameRule(string name)
        => string.IsNullOrWhiteSpace(name) ? new("Category.Invalid.Name", "Name cannot be empty") :
        null;
    }
}

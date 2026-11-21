using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.Brands;

public sealed partial record Brand
{
    public static class BrandRules
    {
        /// <summary>
        /// Crea una nueva marca y valida sus propiedades iniciales.
        /// </summary>
        /// <returns>
        /// Si los parámetros son válidos, devuelve la nueva marca.
        /// De lo contrario, devuelve un error.
        /// </returns>
        public static Result<Brand> Create(int id, string name)
        {
            if (id < 0)
                return Result<Brand>.Failure(
                    new Error("Brand.Invalid.Id", "Id cannot be negative"));

            var nameRule = BrandNameRules(name);

            if (nameRule.HasValue)
                return Result<Brand>.Failure(nameRule.Value);

            return Result<Brand>.Success(new Brand(id, name));
        }

        /// <summary>
        /// Cambia el nombre de una marca.
        /// </summary>
        /// <returns>
        /// Si el nuevo nombre es válido, devuelve la misma marca con el nombre actualizado.
        /// De lo contrario, devuelve un error.
        /// </returns>
        public static Result<Brand> ChangeName(in Brand brand, string newName)
        {
            var nameRule = BrandNameRules(newName);

            if (nameRule.HasValue)
                return Result<Brand>.Failure(nameRule.Value);

            return Result<Brand>.Success(
                brand with { Name = newName });
        }

        private static Error? BrandNameRules(string name)
        => string.IsNullOrWhiteSpace(name) ? new("Brand.Invalid.Name", "Name cannot be empty") : null; 
    }
}
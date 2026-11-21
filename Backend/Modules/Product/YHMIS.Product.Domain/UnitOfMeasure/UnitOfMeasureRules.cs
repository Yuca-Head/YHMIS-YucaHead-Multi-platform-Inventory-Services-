
using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.UnitsOfMeasure;

public sealed partial record UnitOfMeasure
{
    public static class UnitOfMeasureRules
    {
        /// <summary>
        /// Crea una nueva unidad de medida y valida sus propiedades iniciales.
        /// </summary>
        /// <returns>
        /// Si los parámetros son válidos, devuelve la nueva unidad de medida.
        /// De lo contrario, devuelve un error.
        /// </returns>
        public static Result<UnitOfMeasure> Create(int id, string name)
        {
            if (id < 0)
                return Result<UnitOfMeasure>.Failure(
                    new Error("UnitOfMeasure.Invalid.Id", "Id cannot be negative"));

            if (string.IsNullOrWhiteSpace(name))
                return Result<UnitOfMeasure>.Failure(
                    new Error("UnitOfMeasure.Invalid.Name", "Name cannot be empty"));

            return Result<UnitOfMeasure>.Success(
                new UnitOfMeasure(id, name));
        }
        
        /// <summary>
        /// Cambia el nombre de una unidad de medida.
        /// </summary>
        /// <returns>
        /// Si el nuevo nombre es válido, devuelve la misma unidad de medida con el nombre actualizado.
        /// De lo contrario, devuelve un error.
        /// </returns>
        public static Result<UnitOfMeasure> ChangeName(
            in UnitOfMeasure unitOfMeasure, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return Result<UnitOfMeasure>.Failure(
                    new Error("UnitOfMeasure.Invalid.Name", "Name cannot be empty"));

            return Result<UnitOfMeasure>.Success(
                unitOfMeasure with { Name = newName });
        }
    }
}


using YHMIS.Shared.Results;

namespace YHMIS.Product.Domain.Products;

public partial record class Product
{
    public static class ProductRules
    {


        /// <summary>
        /// Creates a new product and validates its inital properties.
        /// </summary>
        /// <returns>If parameters are valid: New product. Else: Error</returns>
        public static Result<Product> Create(int id, string name, string? description, bool isActive = true)
        {
            if(id < 0)
                return Result<Product>.Failure(new("Product.Invalid.Id", "Id cannot be negative"));
            
            var invalidName = InvalidName(name);

            if(invalidName is not null)
                return Result<Product>.Failure(invalidName.Value);
            
            return Result<Product>.Success(new(id, name, description, isActive));
        }


        /// <summary>
        /// Changes the name of a product.
        /// </summary>
        /// <returns>If newName is valid returns the same product with new name. Else: Error.</returns>
        public static Result<Product> ChangeName(in Product product, string newName)
        {
            var invalidName = InvalidName(newName);
            if(invalidName is not null)
                return Result<Product>.Failure(invalidName.Value);
            
            return Result<Product>.Success(product with {Name = newName});
        }

        public static Product ChangeDescription(in Product product, string newDescription)
        => product with {Description = newDescription};

        public static Product ChangeState(in Product product, bool isActive)
        => product with {IsActive = isActive};

        private static Error? InvalidName(string name)
        => string.IsNullOrWhiteSpace(name) ? new("Product.Invalid.Name", "Name cannot be empty")
        : null;

    }
}
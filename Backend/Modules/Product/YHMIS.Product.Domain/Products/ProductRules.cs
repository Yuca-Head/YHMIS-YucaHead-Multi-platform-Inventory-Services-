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
        public static Result<Product> Create(int id, string name, string sku, string? description, bool isActive = true)
        {
            if(id < 0)
                return Result<Product>.Failure(new("Product.Invalid.Id", "Id cannot be negative"));
            
            var invalidName = Error.FromNameRule(name, nameof(Product));

            if(invalidName.HasValue)
                return Result<Product>.Failure(invalidName.Value);

            var invalidSKU = SkuRules(sku);

            if(invalidSKU.HasValue)
                return Result<Product>.Failure(invalidSKU.Value);
            
            return Result<Product>.Success(new(id, name, sku, description, isActive));
        }


        /// <summary>
        /// Changes the name of a product.
        /// </summary>
        /// <returns>If newName is valid returns the same product with new name. Else: Error.</returns>
        public static Result<Product> ChangeName(in Product product, string newName)
        {
            var invalidName = Error.FromNameRule(newName, nameof(Product));
            if(invalidName.HasValue)
                return Result<Product>.Failure(invalidName.Value);
            
            return Result<Product>.Success(product with {Name = newName});
        }

        public static Product ChangeDescription(in Product product, string newDescription)
        => product with {Description = newDescription};

        public static Product ChangeState(in Product product, bool isActive)
        => product with {IsActive = isActive};

        private static Error? SkuRules(string sku)
        {

            if(string.IsNullOrWhiteSpace(sku))  
                return new Error("Product.Invalid.SKU", "SKU cannot be empty");
                
            if(sku.Length != 8)
                return new Error("Prodcut.Invalid.SkuLength", "SKU length must be 8");
            
            return null;
        }

    }
}
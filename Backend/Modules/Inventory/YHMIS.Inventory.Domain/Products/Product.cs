namespace YHMIS.Inventory.Domain.Products;

    public partial record class Product
    {
        private Product(int id, string name, string? description, bool isActive = true)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
        }

        private Product(){}

        public int Id {get; private init;} = -1;
        public string Name {get; private init => field = value.Trim();} = "";
        public string? Description {get; private init => field = value?.Trim();} = null;
        public bool IsActive {get; private init;} = true;
        

    }
-- TYPE CREATIONS

CREATE TYPE "Purchases"."PurchaseDetailCart" AS
(
    "ProductId" INT,
	"Quantity" NUMERIC(18,4),
	"UnitCost" NUMERIC(18,4)
);

CREATE TYPE "Sales"."SaleDetailCart" AS
(
    "ProductId" INT,
    "Quantity"  NUMERIC(18,4),
    "UnitPrice" NUMERIC(18,4)
);

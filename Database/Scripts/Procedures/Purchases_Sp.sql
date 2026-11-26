/*
    Things to basic understanding
    Stored procedures -> sp 
    Function -> fn
    To make a stored procedure or function
    "Schema"."Abreviation_Name"
*/

CREATE OR REPLACE PROCEDURE "Purchases"."Sp_RegisterPurchase"
(
	"p_Code" VARCHAR(25),
	"p_SupplierId" INT,
	"p_StatusId" INT,
	"p_PurchaseCart" "Purchases"."PurchaseDetailCart"[]
)
LANGUAGE PLPGSQL
AS $$
	DECLARE v_PurchaseId INT;
BEGIN

	IF NOT "Thirdparties"."Fn_ExistingThirdPartyAsSupplier"("p_SupplierId")
	THEN
		RAISE EXCEPTION 'Ingrese un proveedor existente';
	END IF;

	 IF NOT "Transactions"."Fn_ExistingTransactionStatus"("p_StatusId")
	THEN
        RAISE EXCEPTION 'Ingrese un estado valido';
    END IF;

	INSERT INTO "Purchases"."Purchases"("Code", "SupplierId", "StatusId")
    VALUES ("p_Code", "p_SupplierId", "p_StatusId")
    RETURNING "Id" INTO v_PurchaseId;

	IF "p_PurchaseCart" IS NULL OR array_length("p_PurchaseCart", 1) IS NULL 
	THEN
        RAISE EXCEPTION 'El carrito no debe estar vacio.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_PurchaseCart") SC WHERE SC."ProductId" IS NULL OR SC."ProductId" <= 0) 
	THEN
        RAISE EXCEPTION 'Producto seleccionado es invalido.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_PurchaseCart") SC GROUP BY SC."ProductId" HAVING COUNT(*) > 1) 
			   THEN
        RAISE EXCEPTION 'El carrito contiene productos repetidos.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_PurchaseCart") SC WHERE SC."Quantity" IS NULL OR SC."Quantity" <= 0) 
	THEN
        RAISE EXCEPTION 'La cantidad del producto debe ser mayor a cero.';
    END IF;

	-- si o no?
    IF EXISTS (SELECT 1 FROM UNNEST("p_PurchaseCart") SC WHERE SC."UnitCost" IS NULL OR SC."UnitCost" <= 0) 
	THEN
        RAISE EXCEPTION 'El costo debe ser mayor a cero.';
    END IF;
	
	IF EXISTS (SELECT 1 FROM UNNEST("p_PurchaseCart") PC WHERE "Products"."Fn_ExceedsMaxStock"(PC."ProductId",PC."Quantity"))
	THEN
		RAISE EXCEPTION 'La cantidad excede la capacidad maxima del inventario para el producto';
	END IF;

	INSERT INTO "Purchases"."PurchaseDetails"("ProductId","PurchaseId","Quantity","UnitCost")
	SELECT PC."ProductId",v_PurchaseId,PC."Quantity",PC."UnitCost"
	FROM UNNEST("p_PurchaseCart") PC;

	--entiendo que el transactionid es el id de la transaccion segun lo que entiendo
	CALL "Inventory"."Sp_RegisterMovement"(v_PurchaseId,"p_PurchaseCart");
	
END;
$$








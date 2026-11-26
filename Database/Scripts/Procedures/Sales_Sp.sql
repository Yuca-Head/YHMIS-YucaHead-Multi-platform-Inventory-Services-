
CREATE OR REPLACE PROCEDURE "Sales"."Sp_RegisterSale"
(
    "p_ThirdPartyId" INT,
    "p_StatusId"     INT,
    "p_Code"         VARCHAR(25),
    "p_SaleCart"     "Sales"."SaleDetailCart"[]
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_SaleId INT;
BEGIN
    IF NOT "Thirdparties"."Fn_ExistingThirdPartyAsCustomer"("p_ThirdPartyId") 
	THEN
        RAISE EXCEPTION 'Ingrese un cliente existente';
    END IF;

    IF NOT "Transactions"."Fn_ExistingTransactionStatus"("p_StatusId")
	THEN
        RAISE EXCEPTION 'Ingrese un estado valido';
    END IF;

	INSERT INTO "Sales"."Sales"("Code", "CustomerId", "StatusId")
    VALUES ("p_Code", "p_ThirdPartyId", "p_StatusId")
    RETURNING "Id" INTO v_SaleId;
	
    IF "p_SaleCart" IS NULL OR array_length("p_SaleCart", 1) IS NULL 
	THEN
        RAISE EXCEPTION 'El carrito no debe estar vacio.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_SaleCart") SC WHERE SC."ProductId" IS NULL OR SC."ProductId" <= 0) 
	THEN
        RAISE EXCEPTION 'Producto seleccionado es invalido.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_SaleCart") SC GROUP BY SC."ProductId" HAVING COUNT(*) > 1) 
			   THEN
        RAISE EXCEPTION 'El carrito contiene productos repetidos.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_SaleCart") SC WHERE SC."Quantity" IS NULL OR SC."Quantity" <= 0) 
	THEN
        RAISE EXCEPTION 'La cantidad del producto debe ser mayor a cero.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_SaleCart") SC WHERE SC."UnitPrice" IS NULL OR SC."UnitPrice" <= 0) 
	THEN
        RAISE EXCEPTION 'El precio debe ser mayor a cero.';
    END IF;

    IF EXISTS (SELECT 1 FROM UNNEST("p_SaleCart") SC WHERE NOT "Products"."Fn_ExistingEnoughStock"(SC."ProductId", SC."Quantity")) 
	THEN
        RAISE EXCEPTION 'Stock insuficiente para venta';
    END IF;

    INSERT INTO "Sales"."SaleDetails"("SaleId", "ProductId", "Quantity", "UnitPrice")
    SELECT v_SaleId, SC."ProductId", SC."Quantity", SC."UnitPrice"
    FROM UNNEST("p_SaleCart") SC;

	CALL "Inventory"."Sp_RegisterMovement"(v_SaleId,"p_SaleCart");
	
END
$$;

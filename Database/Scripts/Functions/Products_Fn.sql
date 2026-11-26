CREATE OR REPLACE FUNCTION "Products"."Fn_ExistingProduct"
(
	"ProductId" INT
)
RETURNS BOOLEAN
LANGUAGE PLPGSQL
AS $$
BEGIN
	IF NOT EXISTS(SELECT 1 FROM "Products"."Products" WHERE "Id" = "ProductId")
	THEN
		RETURN FALSE;
	ELSE
		RETURN TRUE;
	END IF;
END;
$$

CREATE OR REPLACE FUNCTION "Products"."Fn_ExistingEnoughStock"
(
    "ProductId" INT,
    "Quantity"  NUMERIC(18,4)
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$

DECLARE v_stock NUMERIC(18,4);
DECLARE v_result NUMERIC(18,4);
DECLARE v_minStock NUMERIC(18,4);

BEGIN
    SELECT
        COALESCE(i."Stock", 0),
        COALESCE(i."MinStock", 0)
    INTO
        v_stock,
        v_minStock
    FROM "Products"."Products" AS p
    INNER JOIN "Inventory"."Inventories" AS i
        ON p."Id" = i."ProductId"
    WHERE i."ProductId" = "ProductId";
	
	IF v_stock IS NULL THEN
	   RETURN FALSE;
	END IF;
	
	IF v_stock < "Quantity" THEN
	   RETURN FALSE;
	END IF;
	
	IF(v_stock - "Quantity") < v_minStock THEN
	   RETURN FALSE;
	END IF;
	
	 RETURN TRUE;
END;
$$;

CREATE OR REPLACE FUNCTION "Products"."Fn_ExceedsMaxStock"
(
	"ProductId" INT,
	"Quantity" NUMERIC(18,4)
)
RETURNS BOOLEAN
LANGUAGE PLPGSQL
AS $$
	DECLARE v_Stock NUMERIC(18,4);
	DECLARE v_StockMax NUMERIC(18,4);
BEGIN
	SELECT
        COALESCE(i."Stock", 0),
        COALESCE(i."MaxStock", 0)
    INTO
        v_Stock,
        v_StockMax
    FROM "Products"."Products" AS p
    INNER JOIN "Inventory"."Inventories" AS i
        ON p."Id" = i."ProductId"
    WHERE i."ProductId" = "ProductId";

	IF("Quantity" + v_Stock) > v_StockMax
	THEN
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
END
$$



-- INVENTORY TRIGGERS AND SP v0.1
-- FOR NOW JUST STORED PROCEDURES

CREATE OR REPLACE PROCEDURE "Inventory"."Sp_RegisterPurchaseMovement"
(	
	"p_TransactionId" INT,
	"p_PurchaseCart" "Purchases"."PurchaseDetailCart"[]
)
LANGUAGE PLPGSQL
AS $$
	DECLARE v_InventoryMovementId INT;
BEGIN
	--if we do it like sp, i think validations are not neccesary cause' all validations were made in the other sp
	--id 1 -> purchases
	INSERT INTO "Inventory"."InventoryMovements"("TransactionSourceId","TransactionId")
	VALUES(1,"p_TransactionId")
	RETURNING "Id" INTO v_InventoryMovementId;

	INSERT INTO "Inventory"."InventoryMovementsDetails"("InventoryMovementId","ProductId","UnitCost","Quantity")
	SELECT
	v_InventoryMovementId,PC."ProductId",PC."UnitCost",PC."Quantity"
	FROM UNNEST("p_PurchaseCart") PC;
	
END;
$$

CREATE OR REPLACE PROCEDURE "Inventory"."Sp_RegisterSaleMovement"
(	
	"p_TransactionId" INT,
	"p_PurchaseCart" "Sales"."SaleDetailCart"[]
)
LANGUAGE PLPGSQL
AS $$
	DECLARE v_InventoryMovementId INT;
BEGIN

	--id 2 -> sales
	INSERT INTO "Inventory"."InventoryMovements"("TransactionSourceId","TransactionId")
	VALUES(2,"p_TransactionId")
	RETURNING "Id" INTO v_InventoryMovementId;

	INSERT INTO "Inventory"."InventoryMovementsDetails"("InventoryMovementId","ProductId","UnitCost","Quantity")
	SELECT
	v_InventoryMovementId,PC."ProductId",PC."UnitCost",PC."Quantity"
	FROM UNNEST("p_PurchaseCart") PC;
	
END;
$$




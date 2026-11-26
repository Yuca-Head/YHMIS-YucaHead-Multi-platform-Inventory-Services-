CREATE OR REPLACE FUNCTION "Transactions"."Fn_ExistingTransactionStatus"
(
	"StatusId" INT
)
RETURNS BOOLEAN
LANGUAGE PLPGSQL
AS $$
BEGIN
	IF NOT EXISTS (SELECT 1 FROM "Transactions"."TransactionStatuses" WHERE "Id" = "StatusId")
	THEN
		RETURN FALSE;
	ELSE
		RETURN TRUE;
	END IF;
END;
$$
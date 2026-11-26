--THIRDPARTIES FUNCTIONS 

CREATE OR REPLACE FUNCTION "Thirdparties"."Fn_ExistingThirdPartyAsCustomer"
(
	"ThirdPartyId" INT
)
RETURNS BOOLEAN 
LANGUAGE PLPGSQL
AS $$
BEGIN
	IF NOT EXISTS
	(
		SELECT 1 FROM "Thirdparties"."ThirdParties" AS tp
		INNER JOIN "Thirdparties"."CustomerRoles" AS cr
		ON tp."Id" = cr."ThirdPartyRoleId"
		WHERE tp."Id" = "ThirdPartyId"
	)
	THEN
		RETURN FALSE;
	ELSE
		RETURN TRUE;
	END IF;
END
$$

CREATE OR REPLACE FUNCTION "Thirdparties"."Fn_ExistingThirdPartyAsSupplier"
(
	"ThirdPartyId" INT
)
RETURNS BOOLEAN 
LANGUAGE PLPGSQL
AS $$
BEGIN
	IF NOT EXISTS
	(
		SELECT 1 FROM "Thirdparties"."ThirdParties" AS tp
		INNER JOIN "Thirdparties"."SupplierRoles" AS cr
		ON tp."Id" = cr."ThirdPartyRoleId"
		WHERE tp."Id" = "ThirdPartyId"
	)
	THEN
		RETURN FALSE;
	ELSE
		RETURN TRUE;
	END IF;
END
$$


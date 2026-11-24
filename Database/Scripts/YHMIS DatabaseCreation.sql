--DATABASE CREATE SCRIPT

--TABLES WITH ITS SCHEMAS


/*
	BETTER UNDERSTADING SqlServer -> PostgreeSql
	
	[SCHEMA_NAME].[TABLE_NAME] IS THE SAME AS "SCHEMA_NAME"."TABLE_NAME"
	IDENTITY(1,1) IS THE SAME AS GENERATED ALWAYS AS IDENTITY
	
	NOTATIONS
	SQL COMMANDS ALWAYS IN UPPERCASE
	CONSTRAINT DEFINITIONS 
	PK -> PRIMARY KEY
	FK -> FOREIGN KEY
	UQ -> UNIQUE
	CHK -> CHECK

	PARA DEFINIR 
	AbreviationConstraint_TableName_AtributeName/TableName
	ONLY FOR FOREING KEY
	REFERENCES SCHEMA_NAME/"SHEMA_NAME".Table_Name/"TableName" (PRIMARYKEY_ATRIBUTE/"PRIMARYKEY_ATRIBUTE")
	
	
*/


CREATE TABLE IF NOT EXISTS "Products"."Brands"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,

	--CONSTRAINTS
	CONSTRAINT PK_Brands_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Brands_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Products"."Categories"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,
	
	--CONSTRAINTS
	CONSTRAINT PK_Categories_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Categories_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Products"."UnitsOfMeasure"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,
	"Abbreviation" VARCHAR(5) NULL, 

	--CONSTRAINTS
	CONSTRAINT PK_UnitsOfMeasure_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_UnitsOfMeasure_Name UNIQUE("Name"),
	
	CONSTRAINT UQ_UnitsOfMeasure_Abbreviation UNIQUE("Abbreviation") 
);


CREATE TABLE IF NOT EXISTS "Products"."Products"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"SKU" VARCHAR(8) NOT NULL,
	"Name" VARCHAR(100) NOT NULL,
	"Description" VARCHAR(255) NULL,
	"BrandId" INT NULL, 
	"CategoryId" INT NOT NULL,
	"UnitOfMeasureId" INT NOT NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	--CONSTRAINTS
	CONSTRAINT PK_Products_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Products_SKU UNIQUE("SKU"),

	CONSTRAINT CHK_Products_SKU CHECK (length("SKU") = 8),
	
	CONSTRAINT FK_Products_Brands FOREIGN KEY("BrandId")
	REFERENCES "Products"."Brands" ("Id"),

	CONSTRAINT FK_Products_Categories FOREIGN KEY("CategoryId")
	REFERENCES "Products"."Categories" ("Id"),

	CONSTRAINT FK_Products_UnitsOfMeasure FOREIGN KEY("UnitOfMeasureId")
	REFERENCES "Products"."UnitsOfMeasure" ("Id")
);

CREATE TABLE IF NOT EXISTS "Inventory"."Inventories"
(
	"ProductId" INT NOT NULL,
	"Stock" NUMERIC(18,4) NOT NULL DEFAULT 0,
	"MaxStock" NUMERIC(18,4) NULL,
	"MinStock" NUMERIC(18,4) NOT NULL,
	"UnitCost" NUMERIC(18,4) NOT NULL,

	CONSTRAINT PK_Inventories_ProductId PRIMARY KEY("ProductId"),

	CONSTRAINT FK_Inventories_Products FOREIGN KEY("ProductId")
	REFERENCES "Products"."Products" ("Id"),

	CONSTRAINT CHK_Inventories_Stock CHECK ("Stock" >= 0),

	CONSTRAINT CHK_Inventories_MinStock CHECK ("MinStock" >= 0),

	CONSTRAINT CHK_Inventories_MaxStock CHECK ("MaxStock" IS NULL OR "MaxStock" >= "MinStock"),

	CONSTRAINT CHK_Inventories_Cost CHECK ("UnitCost" >= 0)
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."ThirdParties"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(150) NOT NULL,
	"RUC" VARCHAR(14) NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	CONSTRAINT PK_ThirdParties_Id PRIMARY KEY("Id"),
	CONSTRAINT UQ_ThirdParties_RUC UNIQUE("RUC"),

	CONSTRAINT CHK_ThirdParties_RUC CHECK(length("RUC") = 14)
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."ContactTypes"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_ContactTypes_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_ContactTypes_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."ThirdPartyContacts"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"ThirdPartyId" INT NOT NULL,
	"ContactTypeId" INT NOT NULL,
	"Value" VARCHAR(150) NOT NULL,

	CONSTRAINT PK_ThirdPartyContacts_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_ThirdPartyContacts_ContactTypeId_Value UNIQUE("ContactTypeId", "Value"),

	CONSTRAINT FK_ThirdPartyContacts_ThirdParties FOREIGN KEY("ThirdPartyId")
	REFERENCES "ThirdParties"."ThirdParties" ("Id"),

	CONSTRAINT FK_ThirdPartyContacts_ContactTypes FOREIGN KEY("ContactTypeId")
	REFERENCES "ThirdParties"."ContactTypes" ("Id")
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."ThirdPartyRoles"
(
	"ThirdPartyId" INT NOT NULL,
	"Code" VARCHAR(20) NOT NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	CONSTRAINT PK_ThirdPartyRoles_ThirdPartyId PRIMARY KEY("ThirdPartyId"),

	CONSTRAINT UQ_ThirdPartyRoles_Code UNIQUE("Code"),

	CONSTRAINT FK_ThirdPartyRoles_ThirdParties FOREIGN KEY("ThirdPartyId")
	REFERENCES "ThirdParties"."ThirdParties" ("Id")
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."SupplierRoles"
(
	"ThirdPartyRoleId" INT NOT NULL,

	CONSTRAINT PK_SupplierRoles_ThirdPartyRoleId PRIMARY KEY("ThirdPartyRoleId"),

	CONSTRAINT FK_SupplierRoles_ThirdPartyRoles FOREIGN KEY("ThirdPartyRoleId")
	REFERENCES "ThirdParties"."ThirdPartyRoles" ("ThirdPartyId")
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."CustomerRoles"
(
	"ThirdPartyRoleId" INT NOT NULL,

	CONSTRAINT PK_CustomerRoles_ThirdPartyRoleId PRIMARY KEY("ThirdPartyRoleId"),

	CONSTRAINT FK_CustomerRoles_ThirdPartyRoles FOREIGN KEY("ThirdPartyRoleId")
	REFERENCES "ThirdParties"."ThirdPartyRoles" ("ThirdPartyId")
);

CREATE TABLE IF NOT EXISTS "ThirdParties"."SupplierProducts"
(
	"SupplierId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"ProductCost" NUMERIC(18,4) NOT NULL,

	CONSTRAINT PK_SupplierProducts_SupplierId_ProductId PRIMARY KEY("SupplierId", "ProductId"),

	CONSTRAINT FK_SupplierProducts_SupplierRoles FOREIGN KEY("SupplierId")
	REFERENCES "ThirdParties"."SupplierRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY("ProductId")
	REFERENCES "Products"."Products" ("Id"),

	CONSTRAINT CHK_SupplierProducts_ProductCost CHECK ("ProductCost" >= 0)
);

CREATE TABLE IF NOT EXISTS "Transactions"."TransactionStatuses"
(
	"Id" INT NOT NULL,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_TransactionStatuses_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_TransactionStatuses_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Transactions"."TransactionSources"
(
	"Id" INT NOT NULL,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_TransactionSources_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_TransactionSources_Name UNIQUE("Name")
);

INSERT INTO "Transactions"."TransactionStatuses" ("Id", "Name")
VALUES (1, 'Draft'), (2, 'Confirmed'), (3, 'Cancelled')
ON CONFLICT DO NOTHING;

INSERT INTO "Transactions"."TransactionSources" ("Id", "Name")
VALUES (1, 'Purchase'), (2, 'Sale'), (3, 'Adjustment')
ON CONFLICT DO NOTHING;	

CREATE TABLE IF NOT EXISTS "Inventory"."InventoryMovements"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"TransactionSourceId" INT NOT NULL,
	"TransactionId" INT NOT NULL,
	"CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),

	CONSTRAINT PK_InventoryMovements_Id PRIMARY KEY("Id"),

	CONSTRAINT FK_InventoryMovements_TransactionSources FOREIGN KEY("TransactionSourceId")
	REFERENCES "Transactions"."TransactionSources" ("Id"),

	CONSTRAINT UQ_InventoryMovements_TransactionSourceId_TransactionId
	UNIQUE("TransactionSourceId", "TransactionId")
	
);

CREATE TABLE IF NOT EXISTS "Inventory"."InventoryMovementDetails"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"InventoryMovementId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"OccurredAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	"UnitCost" NUMERIC(18,4) NOT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"Subtotal" NUMERIC(18,4) GENERATED ALWAYS AS ("Quantity" * "UnitCost") STORED,

	CONSTRAINT PK_InventoryMovementDetails_Id PRIMARY KEY("Id"),

	CONSTRAINT FK_InventoryMovementDetails_Inventories FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Inventories" ("ProductId"),
	CONSTRAINT FK_InventoryMovementDetails_Movements FOREIGN KEY("InventoryMovementId") 
	REFERENCES "Inventory"."InventoryMovements" ("Id"),


	CONSTRAINT CHK_InventoryMovementDetails_ProductCost CHECK ("UnitCost" >= 0),
	CONSTRAINT CHK_InventoryMovementDetails_Quantity CHECK ("Quantity" <> 0)
);

CREATE TABLE IF NOT EXISTS "Purchases"."Purchases"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Code" VARCHAR(25) NOT NULL,
	"Date" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	"SupplierId" INT NOT NULL,
	"StatusId" INT NOT NULL DEFAULT 1,

	CONSTRAINT PK_Purchases_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_Purchases_Code UNIQUE("Code"),

	CONSTRAINT FK_Purchases_SupplierRoles FOREIGN KEY("SupplierId")
	REFERENCES "ThirdParties"."SupplierRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_Purchases_TransactionStatuses FOREIGN KEY("StatusId")
	REFERENCES "Transactions"."TransactionStatuses" ("Id")
);

CREATE TABLE IF NOT EXISTS "Purchases"."PurchaseDetails"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"PurchaseId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"UnitCost" NUMERIC(18,4) NOT NULL,
	"Subtotal" NUMERIC(18,4) GENERATED ALWAYS AS ("Quantity" * "UnitCost") STORED,

	CONSTRAINT PK_PurchaseDetails_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_PurchaseDetails_PurchaseId_ProductId UNIQUE("PurchaseId", "ProductId"),

	CONSTRAINT FK_PurchaseDetails_Purchases FOREIGN KEY("PurchaseId")
	REFERENCES "Purchases"."Purchases" ("Id") ON DELETE CASCADE,

	CONSTRAINT FK_PurchaseDetails_Products FOREIGN KEY("ProductId")
	REFERENCES "Products"."Products" ("Id"),

	CONSTRAINT CHK_PurchaseDetails_Quantity CHECK ("Quantity" > 0),

	CONSTRAINT CHK_PurchaseDetails_UnitCost CHECK ("UnitCost" >= 0)
);

CREATE TABLE IF NOT EXISTS "Sales"."Sales"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Code" VARCHAR(25) NOT NULL,
	"Date" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	"CustomerId" INT NOT NULL,
	"StatusId" INT NOT NULL,

	CONSTRAINT PK_Sales_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_Sales_Code UNIQUE("Code"),

	CONSTRAINT FK_Sales_CustomerRoles FOREIGN KEY("CustomerId")
	REFERENCES "ThirdParties"."CustomerRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_Sales_TransactionStatuses FOREIGN KEY("StatusId")
	REFERENCES "Transactions"."TransactionStatuses" ("Id")
);

CREATE TABLE IF NOT EXISTS "Sales"."SaleDetails"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"SaleId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"UnitPrice" NUMERIC(18,4) NOT NULL,
	"Subtotal" NUMERIC(18,4) GENERATED ALWAYS AS ("Quantity" * "UnitPrice") STORED,

	CONSTRAINT PK_SaleDetails_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_SaleDetails_SaleId_ProductId UNIQUE("SaleId", "ProductId"),

	CONSTRAINT FK_SaleDetails_Sales FOREIGN KEY("SaleId")
	REFERENCES "Sales"."Sales" ("Id") ON DELETE CASCADE,

	CONSTRAINT FK_SaleDetails_Products FOREIGN KEY("ProductId")
	REFERENCES "Products"."Products" ("Id"),

	CONSTRAINT CHK_SaleDetails_Quantity CHECK ("Quantity" > 0),

	CONSTRAINT CHK_SaleDetails_UnitPrice CHECK ("UnitPrice" >= 0)
);

--segun tengo entendido los indices de la fk en postgreeSql no se ponen automatico por lo cual para las consultas con join es necesario
CREATE INDEX IF NOT EXISTS IX_Products_BrandId ON "Products"."Products" ("BrandId");
CREATE INDEX IF NOT EXISTS IX_Products_CategoryId ON "Products"."Products" ("CategoryId");
CREATE INDEX IF NOT EXISTS IX_Products_UnitOfMeasureId ON "Products"."Products" ("UnitOfMeasureId");
CREATE INDEX IF NOT EXISTS IX_ThirdPartyContacts_ThirdPartyId ON "ThirdParties"."ThirdPartyContacts" ("ThirdPartyId");
CREATE INDEX IF NOT EXISTS IX_SupplierProducts_ProductId ON "ThirdParties"."SupplierProducts" ("ProductId");
CREATE INDEX IF NOT EXISTS IX_InventoryMovementDetails_ProductId ON "Inventory"."InventoryMovementDetails" ("ProductId");
CREATE INDEX IF NOT EXISTS IX_InventoryMovementDetails_InventoryMovementId ON "Inventory"."InventoryMovementDetails" ("InventoryMovementId");
CREATE INDEX IF NOT EXISTS IX_Purchases_SupplierId ON "Purchases"."Purchases" ("SupplierId");
CREATE INDEX IF NOT EXISTS IX_Sales_CustomerId ON "Sales"."Sales" ("CustomerId");
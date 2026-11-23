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


CREATE TABLE IF NOT EXISTS "Inventory"."Brands"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,

	--CONSTRAINTS
	CONSTRAINT PK_Brands_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Brands_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Inventory"."Categories"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,
	
	--CONSTRAINTS
	CONSTRAINT PK_Categories_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Categories_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Inventory"."UnitsOfMeasure"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,
	"Abreviation" VARCHAR(5) NULL, 

	--CONSTRAINTS
	CONSTRAINT PK_UnitsOfMeasure_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_UnitsOfMeasure_Name UNIQUE("Name"),
	
	CONSTRAINT UQ_UnitsOfMeasure_Abreviation UNIQUE("Abreviation") 
);


CREATE TABLE IF NOT EXISTS "Inventory"."Products"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(100) NOT NULL,
	"Description" VARCHAR(255) NULL,
	"BrandId" INT NULL, 
	"CategoryId" INT NOT NULL,
	"UnitOfMeasureId" INT NOT NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	--CONSTRAINTS
	CONSTRAINT PK_Products_Id PRIMARY KEY("Id"),
	
	CONSTRAINT UQ_Products_Name UNIQUE("Name"),
	
	CONSTRAINT FK_Products_Brands FOREIGN KEY("BrandId")
	REFERENCES "Inventory"."Brands" ("Id"),

	CONSTRAINT FK_Products_Categories FOREIGN KEY("CategoryId")
	REFERENCES "Inventory"."Categories" ("Id"),

	CONSTRAINT FK_Products_UnitsOfMeasure FOREIGN KEY("UnitOfMeasureId")
	REFERENCES "Inventory"."UnitsOfMeasure" ("Id")
);

CREATE TABLE IF NOT EXISTS "Inventory"."Inventories"
(
	"ProductId" INT NOT NULL,
	"Stock" NUMERIC(18,4) NOT NULL DEFAULT 0,
	"MaxStock" NUMERIC(18,4) NULL,
	"MinStock" NUMERIC(18,4) NOT NULL,
	"Cost" NUMERIC(18,4) NOT NULL,

	CONSTRAINT PK_Inventories_ProductId PRIMARY KEY("ProductId"),

	CONSTRAINT FK_Inventories_Products FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Products" ("Id"),

	CONSTRAINT CHK_Inventories_Stock CHECK ("Stock" >= 0),

	CONSTRAINT CHK_Inventories_MinStock CHECK ("MinStock" >= 0),

	CONSTRAINT CHK_Inventories_MaxStock CHECK ("MaxStock" IS NULL OR "MaxStock" >= "MinStock"),

	CONSTRAINT CHK_Inventories_Cost CHECK ("Cost" >= 0)
);

CREATE TABLE IF NOT EXISTS "Parties"."ThirdParties"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(150) NOT NULL,
	"RUC" VARCHAR(20) NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	CONSTRAINT PK_ThirdParties_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_ThirdParties_RUC UNIQUE("RUC")
);

CREATE TABLE IF NOT EXISTS "Parties"."ContactTypes"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_ContactTypes_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_ContactTypes_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Parties"."ThirdPartyContacts"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"ThirdPartyId" INT NOT NULL,
	"ContactTypeId" INT NOT NULL,
	"Value" VARCHAR(150) NOT NULL,

	CONSTRAINT PK_ThirdPartyContacts_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_ThirdPartyContacts_ContactTypeId_Value UNIQUE("ContactTypeId", "Value"),

	CONSTRAINT FK_ThirdPartyContacts_ThirdParties FOREIGN KEY("ThirdPartyId")
	REFERENCES "Parties"."ThirdParties" ("Id"),

	CONSTRAINT FK_ThirdPartyContacts_ContactTypes FOREIGN KEY("ContactTypeId")
	REFERENCES "Parties"."ContactTypes" ("Id")
);

CREATE TABLE IF NOT EXISTS "Parties"."ThirdPartyRoles"
(
	"ThirdPartyId" INT NOT NULL,
	"Code" VARCHAR(20) NOT NULL,
	"Status" BOOLEAN NOT NULL DEFAULT TRUE,

	CONSTRAINT PK_ThirdPartyRoles_ThirdPartyId PRIMARY KEY("ThirdPartyId"),

	CONSTRAINT UQ_ThirdPartyRoles_Code UNIQUE("Code"),

	CONSTRAINT FK_ThirdPartyRoles_ThirdParties FOREIGN KEY("ThirdPartyId")
	REFERENCES "Parties"."ThirdParties" ("Id")
);

CREATE TABLE IF NOT EXISTS "Parties"."SupplierRoles"
(
	"ThirdPartyRoleId" INT NOT NULL,

	CONSTRAINT PK_SupplierRoles_ThirdPartyRoleId PRIMARY KEY("ThirdPartyRoleId"),

	CONSTRAINT FK_SupplierRoles_ThirdPartyRoles FOREIGN KEY("ThirdPartyRoleId")
	REFERENCES "Parties"."ThirdPartyRoles" ("ThirdPartyId")
);

CREATE TABLE IF NOT EXISTS "Parties"."CustomerRoles"
(
	"ThirdPartyRoleId" INT NOT NULL,

	CONSTRAINT PK_CustomerRoles_ThirdPartyRoleId PRIMARY KEY("ThirdPartyRoleId"),

	CONSTRAINT FK_CustomerRoles_ThirdPartyRoles FOREIGN KEY("ThirdPartyRoleId")
	REFERENCES "Parties"."ThirdPartyRoles" ("ThirdPartyId")
);

CREATE TABLE IF NOT EXISTS "Parties"."SupplierProducts"
(
	"SupplierId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"ProductCost" NUMERIC(18,4) NOT NULL,

	CONSTRAINT PK_SupplierProducts_SupplierId_ProductId PRIMARY KEY("SupplierId", "ProductId"),

	CONSTRAINT FK_SupplierProducts_SupplierRoles FOREIGN KEY("SupplierId")
	REFERENCES "Parties"."SupplierRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_SupplierProducts_Products FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Products" ("Id"),

	CONSTRAINT CHK_SupplierProducts_ProductCost CHECK ("ProductCost" >= 0)
);

CREATE TABLE IF NOT EXISTS "Transactions"."TransactionStatuses"
(
	"Id" INT NOT NULL,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_TransactionStatuses_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_TransactionStatuses_Name UNIQUE("Name")
);

CREATE TABLE IF NOT EXISTS "Transactions"."SourceTransactionCodes"
(
	"Id" INT NOT NULL,
	"Name" VARCHAR(50) NOT NULL,

	CONSTRAINT PK_SourceTransactionCodes_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_SourceTransactionCodes_Name UNIQUE("Name")
);

INSERT INTO "Transactions"."TransactionStatuses" ("Id", "Name")
VALUES (1, 'Draft'), (2, 'Confirmed'), (3, 'Cancelled')
ON CONFLICT DO NOTHING;

INSERT INTO "Transactions"."SourceTransactionCodes" ("Id", "Name")
VALUES (1, 'Purchase'), (2, 'Sale'), (3, 'Adjustment')
ON CONFLICT DO NOTHING;

CREATE TABLE IF NOT EXISTS "Inventory"."InventoryMovements"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"ProductId" INT NOT NULL,
	"SourceTransactionCodeId" INT NOT NULL,
	"ProductCost" NUMERIC(18,4) NOT NULL,
	"TransactionCode" VARCHAR(50) NOT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"Date" TIMESTAMPTZ NOT NULL DEFAULT NOW(),

	CONSTRAINT PK_InventoryMovements_Id PRIMARY KEY("Id"),

	CONSTRAINT FK_InventoryMovements_Inventories FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Inventories" ("ProductId"),

	CONSTRAINT FK_InventoryMovements_SourceTransactionCodes FOREIGN KEY("SourceTransactionCodeId")
	REFERENCES "Transactions"."SourceTransactionCodes" ("Id"),

	CONSTRAINT CHK_InventoryMovements_ProductCost CHECK ("ProductCost" >= 0),

	CONSTRAINT CHK_InventoryMovements_Quantity CHECK ("Quantity" <> 0)
);

CREATE TABLE IF NOT EXISTS "Transactions"."Purchases"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Code" VARCHAR(50) NOT NULL,
	"Date" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	"SupplierId" INT NOT NULL,
	"StatusId" INT NOT NULL DEFAULT 1,

	CONSTRAINT PK_Purchases_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_Purchases_Code UNIQUE("Code"),

	CONSTRAINT FK_Purchases_SupplierRoles FOREIGN KEY("SupplierId")
	REFERENCES "Parties"."SupplierRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_Purchases_TransactionStatuses FOREIGN KEY("StatusId")
	REFERENCES "Transactions"."TransactionStatuses" ("Id")
);

CREATE TABLE IF NOT EXISTS "Transactions"."PurchaseDetails"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"PurchaseId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"InventoryMovementId" INT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"UnitCost" NUMERIC(18,4) NOT NULL,
	"Subtotal" NUMERIC(18,4) GENERATED ALWAYS AS ("Quantity" * "UnitCost") STORED,

	CONSTRAINT PK_PurchaseDetails_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_PurchaseDetails_PurchaseId_ProductId UNIQUE("PurchaseId", "ProductId"),

	CONSTRAINT UQ_PurchaseDetails_InventoryMovementId UNIQUE("InventoryMovementId"),

	CONSTRAINT FK_PurchaseDetails_Purchases FOREIGN KEY("PurchaseId")
	REFERENCES "Transactions"."Purchases" ("Id") ON DELETE CASCADE,

	CONSTRAINT FK_PurchaseDetails_Products FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Products" ("Id"),

	CONSTRAINT FK_PurchaseDetails_InventoryMovements FOREIGN KEY("InventoryMovementId")
	REFERENCES "Inventory"."InventoryMovements" ("Id"),

	CONSTRAINT CHK_PurchaseDetails_Quantity CHECK ("Quantity" > 0),

	CONSTRAINT CHK_PurchaseDetails_UnitCost CHECK ("UnitCost" >= 0)
);

CREATE TABLE IF NOT EXISTS "Transactions"."Sales"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"Code" VARCHAR(50) NOT NULL,
	"Date" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	"CustomerId" INT NOT NULL,
	"StatusId" INT NOT NULL,

	CONSTRAINT PK_Sales_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_Sales_Code UNIQUE("Code"),

	CONSTRAINT FK_Sales_CustomerRoles FOREIGN KEY("CustomerId")
	REFERENCES "Parties"."CustomerRoles" ("ThirdPartyRoleId"),

	CONSTRAINT FK_Sales_TransactionStatuses FOREIGN KEY("StatusId")
	REFERENCES "Transactions"."TransactionStatuses" ("Id")
);

CREATE TABLE IF NOT EXISTS "Transactions"."SaleDetails"
(
	"Id" INT GENERATED ALWAYS AS IDENTITY,
	"SaleId" INT NOT NULL,
	"ProductId" INT NOT NULL,
	"InventoryMovementId" INT NULL,
	"Quantity" NUMERIC(18,4) NOT NULL,
	"UnitPrice" NUMERIC(18,4) NOT NULL,
	"Subtotal" NUMERIC(18,4) GENERATED ALWAYS AS ("Quantity" * "UnitPrice") STORED,

	CONSTRAINT PK_SaleDetails_Id PRIMARY KEY("Id"),

	CONSTRAINT UQ_SaleDetails_SaleId_ProductId UNIQUE("SaleId", "ProductId"),

	CONSTRAINT UQ_SaleDetails_InventoryMovementId UNIQUE("InventoryMovementId"),

	CONSTRAINT FK_SaleDetails_Sales FOREIGN KEY("SaleId")
	REFERENCES "Transactions"."Sales" ("Id") ON DELETE CASCADE,

	CONSTRAINT FK_SaleDetails_Products FOREIGN KEY("ProductId")
	REFERENCES "Inventory"."Products" ("Id"),

	CONSTRAINT FK_SaleDetails_InventoryMovements FOREIGN KEY("InventoryMovementId")
	REFERENCES "Inventory"."InventoryMovements" ("Id"),

	CONSTRAINT CHK_SaleDetails_Quantity CHECK ("Quantity" > 0),

	CONSTRAINT CHK_SaleDetails_UnitPrice CHECK ("UnitPrice" >= 0)
);

--segun tengo entendido los indices de la fk en postgreeSql no se ponen automatico por lo cual para las consultas con join es necesario
CREATE INDEX IF NOT EXISTS IX_Products_BrandId ON "Inventory"."Products" ("BrandId");
CREATE INDEX IF NOT EXISTS IX_Products_CategoryId ON "Inventory"."Products" ("CategoryId");
CREATE INDEX IF NOT EXISTS IX_Products_UnitOfMeasureId ON "Inventory"."Products" ("UnitOfMeasureId");
CREATE INDEX IF NOT EXISTS IX_ThirdPartyContacts_ThirdPartyId ON "Parties"."ThirdPartyContacts" ("ThirdPartyId");
CREATE INDEX IF NOT EXISTS IX_SupplierProducts_ProductId ON "Parties"."SupplierProducts" ("ProductId");
CREATE INDEX IF NOT EXISTS IX_InventoryMovements_ProductId ON "Inventory"."InventoryMovements" ("ProductId");
CREATE INDEX IF NOT EXISTS IX_InventoryMovements_TransactionCode ON "Inventory"."InventoryMovements" ("TransactionCode");
CREATE INDEX IF NOT EXISTS IX_Purchases_SupplierId ON "Transactions"."Purchases" ("SupplierId");
CREATE INDEX IF NOT EXISTS IX_PurchaseDetails_ProductId ON "Transactions"."PurchaseDetails" ("ProductId");
CREATE INDEX IF NOT EXISTS IX_Sales_CustomerId ON "Transactions"."Sales" ("CustomerId");
CREATE INDEX IF NOT EXISTS IX_SaleDetails_ProductId ON "Transactions"."SaleDetails" ("ProductId");
--CREATE DATABASE Project0;

--GO
--CREATE SCHEMA Project0;
--GO

CREATE TABLE Project0.Cupcake (
	CupcakeId INT NOT NULL PRIMARY KEY IDENTITY,
	Type NVARCHAR(100) UNIQUE NOT NULL,
	Cost DECIMAL(8,2) NOT NULL DEFAULT(6.00)
);

CREATE TABLE Project0.Ingredient (
	IngredientId INT NOT NULL PRIMARY KEY IDENTITY,
	Type NVARCHAR(100) UNIQUE NOT NULL,
	Units NVARCHAR(50) NOT NULL
);

CREATE TABLE Project0.RecipeItem (
	RecipeItemId INT NOT NULL PRIMARY KEY IDENTITY,
	CupcakeID INT NOT NULL,
	IngredientID INT NOT NULL,
	Amount DECIMAL(10,6) NOT NULL,
	CONSTRAINT CupcakeIngredient UNIQUE (CupcakeID, IngredientID),
	CONSTRAINT FK_Recipe_Cupcake FOREIGN KEY (CupcakeID) REFERENCES Project0.Cupcake (CupcakeId),
	CONSTRAINT FK_Recipe_Ingredient FOREIGN KEY (IngredientID) REFERENCES Project0.Ingredient (IngredientId)
);

CREATE TABLE Project0.Location (
	LocationId INT NOT NULL PRIMARY KEY IDENTITY
);

CREATE TABLE Project0.LocationInventory (
	LocationInventoryId INT NOT NULL PRIMARY KEY IDENTITY,
	LocationID INT NOT NULL,
	IngredientID INT NOT NULL,
	Amount DECIMAL(10,6) NOT NULL,
	CONSTRAINT InventoryIngredient UNIQUE (LocationID, IngredientID),
	CONSTRAINT FK_Location FOREIGN KEY (LocationID) REFERENCES Project0.Location (LocationId),
	CONSTRAINT FK_Ingredient FOREIGN KEY (IngredientID) REFERENCES Project0.Ingredient (IngredientId)
);

CREATE TABLE Project0.Customer (
	CustomerId INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	DefaultLocation INT NOT NULL,
	CONSTRAINT FK_Default_Location FOREIGN KEY (DefaultLocation) REFERENCES Project0.Location (LocationId)
);

CREATE TABLE Project0.CupcakeOrder (
	OrderId INT NOT NULL PRIMARY KEY IDENTITY,
	LocationID INT NOT NULL,
	CustomerID INT NOT NULL,
	OrderTime DATETIME2 NOT NULL,
	CONSTRAINT FK_Order_Location FOREIGN KEY (LocationID) REFERENCES Project0.Location (LocationId),
	CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Project0.Customer (CustomerId),
);

CREATE TABLE Project0.CupcakeOrderItem (
	CupcakeOrderItemId INT NOT NULL PRIMARY KEY IDENTITY,
	OrderID INT NOT NULL,
	CupcakeID INT NOT NULL,
	Quantity INT NOT NULL
	CONSTRAINT OrderToCupcake UNIQUE (OrderID, CupcakeID),
	CONSTRAINT FK_OrderItem_CupcakeOrder FOREIGN KEY (OrderID) REFERENCES Project0.CupcakeOrder (OrderId),
	CONSTRAINT FK_OrderItem_Cupcake FOREIGN KEY (CupcakeID) REFERENCES Project0.Cupcake (CupcakeId)
);

INSERT INTO Project0.Cupcake (Type, Cost) VALUES
	('Vanilla', 3.5),
	('Chocolate', 5.25),
	('ChocPeanutButter', 6.25),
	('RaspAmaretto', 6),
	('ChocPeppermint', 6.5),
	('MintOreo', 6),
	('Coconut', 4),
	('Lemon', 4.7);
	
INSERT INTO Project0.Ingredient (Type, Units) VALUES
	('Flour', 'lbs'),
	('BakingPowder', 'lbs'),
	('Salt', 'lbs'),
	('UnsaltedButter', 'lbs'),
	('GranulatedSugar', 'lbs'),
	('LargeEgg', 'each'),
	('VanillaExtract', 'gallons'),
	('SourCream', 'lbs'),
	('ConfectionerSugar', 'lbs'),
	('HeavyCream', 'gallons'),
	('CocoaPowder', 'lbs'),
	('Raspberry', 'each'),
	('PeanutButter', 'lbs'),
	('Peppermint', 'each'),
	('Oreo', 'lbs'),
	('CoconutMilk', 'gallons'),
	('Lemon', 'each'),
	('Amaretto', 'lbs');

INSERT INTO Project0.RecipeItem 
(CupcakeID, IngredientID, Amount) VALUES
	(1, 1, 0.028),
	(1, 2, 0.00094),
	(1, 3, 0.00045),
	(1, 4, 0.054),
	(1, 5, 0.031),

	(1, 6, 0.14),
	(1, 7, 0.0004),
	(1, 8, 0.019),
	(1, 9, 0.057),
	(1, 10, 0.0045),

	(1, 11, 0),
	(1, 12, 0),
	(1, 13, 0),
	(1, 14, 0),
	(1, 15, 0),

	(1, 16, 0),
	(1, 17, 0),
	(1, 18, 0),
	
	
	(2, 1, 0.028),
	(2, 2, 0.00094),
	(2, 3, 0.00045),
	(2, 4, 0.054),
	(2, 5, 0.031),

	(2, 6, 0.14),
	(2, 7, 0.0004),
	(2, 8, 0.019),
	(2, 9, 0.057),
	(2, 10, 0.0045),

	(2, 11, 0.024),
	(2, 12, 0),
	(2, 13, 0),
	(2, 14, 0),
	(2, 15, 0),

	(2, 16, 0),
	(2, 17, 0),
	(2, 18, 0),
	

	(3, 1, 0.028),
	(3, 2, 0.00094),
	(3, 3, 0.00045),
	(3, 4, 0.054),
	(3, 5, 0.031),

	(3, 6, 0.14),
	(3, 7, 0.0004),
	(3, 8, 0.019),
	(3, 9, 0.057),
	(3, 10, 0.0045),

	(3, 11, 0.024),
	(3, 12, 0),
	(3, 13, 0.011),
	(3, 14, 0),
	(3, 15, 0),

	(3, 16, 0),
	(3, 17, 0),
	(3, 18, 0),


	(4, 1, 0.028),
	(4, 2, 0.00094),
	(4, 3, 0.00045),
	(4, 4, 0.054),
	(4, 5, 0.031),

	(4, 6, 0.14),
	(4, 7, 0.0004),
	(4, 8, 0.019),
	(4, 9, 0.057),
	(4, 10, 0.0045),

	(4, 11, 0),
	(4, 12, 7),
	(4, 13, 0),
	(4, 14, 0),
	(4, 15, 0),

	(4, 16, 0),
	(4, 17, 0),
	(4, 18, 0.014),


	(5, 1, 0.028),
	(5, 2, 0.00094),
	(5, 3, 0.00045),
	(5, 4, 0.054),
	(5, 5, 0.031),

	(5, 6, 0.14),
	(5, 7, 0.0004),
	(5, 8, 0.019),
	(5, 9, 0.057),
	(5, 10, 0.0045),

	(5, 11, 0.024),
	(5, 12, 0),
	(5, 13, 0),
	(5, 14, 1),
	(5, 15, 0),

	(5, 16, 0),
	(5, 17, 0),
	(5, 18, 0),


	(6, 1, 0.028),
	(6, 2, 0.00094),
	(6, 3, 0.00045),
	(6, 4, 0.054),
	(6, 5, 0.031),

	(6, 6, 0.14),
	(6, 7, 0.0004),
	(6, 8, 0.019),
	(6, 9, 0.057),
	(6, 10, 0.0045),

	(6, 11, 0),
	(6, 12, 0),
	(6, 13, 0),
	(6, 14, 0.5),
	(6, 15, 0.02),

	(6, 16, 0),
	(6, 17, 0),
	(6, 18, 0),


	(7, 1, 0.028),
	(7, 2, 0.00094),
	(7, 3, 0.00045),
	(7, 4, 0.054),
	(7, 5, 0.031),

	(7, 6, 0.14),
	(7, 7, 0.0004),
	(7, 8, 0.019),
	(7, 9, 0.057),
	(7, 10, 0.0045),

	(7, 11, 0),
	(7, 12, 0),
	(7, 13, 0),
	(7, 14, 0),
	(7, 15, 0),

	(7, 16, 0.0025),
	(7, 17, 0),
	(7, 18, 0),


	(8, 1, 0.028),
	(8, 2, 0.00094),
	(8, 3, 0.00045),
	(8, 4, 0.054),
	(8, 5, 0.031),

	(8, 6, 0.14),
	(8, 7, 0.0004),
	(8, 8, 0.019),
	(8, 9, 0.057),
	(8, 10, 0.0045),

	(8, 11, 0),
	(8, 12, 0),
	(8, 13, 0),
	(8, 14, 0),
	(8, 15, 0),

	(8, 16, 0),
	(8, 17, 0.15),
	(8, 18, 0);




DROP TABLE IF EXISTS Project1.CupcakeOrderItem
DROP TABLE IF EXISTS Project1.CupcakeOrder
DROP TABLE IF EXISTS Project1.Customer
DROP TABLE IF EXISTS Project1.LocationInventory
DROP TABLE IF EXISTS Project1.RecipeItem
DROP TABLE IF EXISTS Project1.Cupcake
DROP TABLE IF EXISTS Project1.Ingredient
DROP TABLE IF EXISTS Project1.Location

UPDATE Project1.LocationInventory
SET Amount = 120.00
WHERE 1=1;

UPDATE Project1.LocationInventory
SET Amount = 20000.00
WHERE LocationID = 27;

SELECT *
FROM Project1.LocationInventory
WHERE LocationId = 1;

SELECT *
FROM Project1.Location;

SELECT *
FROM Project1.Customer;

SELECT *
FROM Project1.Cupcake;

SELECT *
FROM Project1.RecipeItem;
WHERE CupcakeId = 1;

SELECT *
FROM Project1.CupcakeOrder
WHERE LocationID = 12;

SELECT *
FROM Project1.Ingredient;

DELETE FROM Project1.CupcakeOrder
WHERE 1 = 1;

SELECT *
FROM Project1.CupcakeOrder;

SELECT *
FROM Project1.CupcakeOrderItem;

TRUNCATE TABLE Project1.CupcakeOrderItem;

TRUNCATE TABLE Project1.CupcakeOrder;



SELECT *
FROM Project1.CupcakeOrderItem;

INSERT INTO Project1.Location DEFAULT VALUES;

DELETE FROM Project1.Location
WHERE 1=1;

DELETE FROM Project1.Customer
WHERE 1=1;

DELETE FROM Project1.CupcakeOrder
WHERE 1=1;
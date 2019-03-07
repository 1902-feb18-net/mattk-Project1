DROP TABLE IF EXISTS Project0.CupcakeOrderItem
DROP TABLE IF EXISTS Project0.CupcakeOrder
DROP TABLE IF EXISTS Project0.Customer
DROP TABLE IF EXISTS Project0.LocationInventory
DROP TABLE IF EXISTS Project0.RecipeItem
DROP TABLE IF EXISTS Project0.Cupcake
DROP TABLE IF EXISTS Project0.Ingredient
DROP TABLE IF EXISTS Project0.Location

UPDATE Project0.LocationInventory
SET Amount = 120.00
WHERE 1=1;

UPDATE Project0.LocationInventory
SET Amount = 5000.00
WHERE LocationID = 12;

SELECT *
FROM Project0.LocationInventory
WHERE LocationId = 1;

SELECT *
FROM Project0.Location;

SELECT *
FROM Project0.Customer;

SELECT *
FROM Project0.RecipeItem;
WHERE CupcakeId = 1;

SELECT *
FROM Project0.CupcakeOrder
WHERE LocationID = 12;

SELECT *
FROM Project0.Ingredient;

DELETE FROM Project0.CupcakeOrder
WHERE 1 = 1;

SELECT *
FROM Project0.CupcakeOrder;

SELECT *
FROM Project0.CupcakeOrderItem;

DELETE FROM Project0.Location
WHERE 1 = 1;

TRUNCATE TABLE Project0.CupcakeOrderItem;

TRUNCATE TABLE Project0.CupcakeOrder;

DELETE FROM Project0.CupcakeOrder
WHERE 1=1;

SELECT *
FROM Project0.CupcakeOrderItem;

INSERT INTO Project0.Location DEFAULT VALUES;

DELETE FROM Project0.Location
WHERE LocationId = 11;
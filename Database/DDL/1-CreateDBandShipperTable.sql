CREATE DATABASE IF NOT EXISTS inventory;

USE inventory;

# ---------------------------------------------------------------------- #
# Add table "Shippers"                                                   #
# ---------------------------------------------------------------------- #

CREATE TABLE Shippers (
    ShipperID INTEGER NOT NULL AUTO_INCREMENT,
    CompanyName VARCHAR(40) NOT NULL,
    Phone VARCHAR(24),
    CONSTRAINT PK_Shippers PRIMARY KEY (ShipperID)
);

# ---------------------------------------------------------------------- #
# Add info into "Shippers"                                               #
# ---------------------------------------------------------------------- #

TRUNCATE TABLE Shippers;
INSERT INTO Shippers (ShipperID, CompanyName, Phone)
VALUES(1, 'Speedy Express', '(503) 555-9831');
INSERT INTO Shippers (ShipperID, CompanyName, Phone)
VALUES(2, 'United Package', '(503) 555-3199');
INSERT INTO Shippers (ShipperID, CompanyName, Phone)
VALUES(3, 'Federal Shipping', '(503) 555-9931');
CREATE DATABASE IF NOT EXISTS inventory;

USE inventory;

# ---------------------------------------------------------------------- #
# Add table "Shippers"                                                   #
# ---------------------------------------------------------------------- #

drop table if exists shipper;

CREATE TABLE shipper (
    ShipperID INTEGER NOT NULL AUTO_INCREMENT,
    ShipperName VARCHAR(40) NOT NULL,
    Phone VARCHAR(24),
    ActiveYN Varchar(1),
    CONSTRAINT PK_Shippers PRIMARY KEY (ShipperID)
);

# ---------------------------------------------------------------------- #
# Add info into "Shippers"                                               #
# ---------------------------------------------------------------------- #

TRUNCATE TABLE Shipper;
INSERT INTO Shipper (ShipperID, ShipperName, Phone, ActiveYN)
VALUES(1, 'Speedy Express', '(503) 555-9831', 'Y');
INSERT INTO Shipper (ShipperID, ShipperName, Phone, ActiveYN)
VALUES(2, 'United Package', '(503) 555-3199', 'Y');
INSERT INTO Shipper (ShipperID, ShipperName, Phone, ActiveYN)
VALUES(3, 'Federal Shipping', '(503) 555-9931', 'Y');
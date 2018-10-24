use inventory;

CREATE TABLE Users (
UserID char(20) NOT NULL PRIMARY KEY
, Password char(20) NOT NULL
, First_Name char(25) NOT NULL
, Last_Name char(25) NOT NULL
, Phone_Number char(10) NOT NULL
, Street char(25) NOT NULL
, City char(25) NOT NULL
, Zip_Code char(5) NOT NULL
, Email char(40) NOT NULL
, User_Type char(1) NOT NULL
);


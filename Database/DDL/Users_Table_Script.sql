use inventory;

CREATE TABLE users (
userId char(20) NOT NULL PRIMARY KEY
, password char(20) NOT NULL
, first_name char(20) NOT NULL
, last_name char(30) NOT NULL
, phone_number char(10) NOT NULL
, street char(40) NOT NULL
, city char(40) NOT NULL
, zip_code char(5) NOT NULL
, email char(40) NOT NULL
, user_type char(1) NOT NULL
);


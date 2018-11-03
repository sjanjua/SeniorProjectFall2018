ALTER TABLE user
ADD COLUMN PhoneNumber VARCHAR(10) AFTER LastName;

ALTER TABLE user
ADD COLUMN Street VARCHAR(25) AFTER PhoneNumber;

ALTER TABLE user
ADD COLUMN City VARCHAR(25) AFTER Street;

ALTER TABLE user
ADD COLUMN ZipCode VARCHAR(5) AFTER City;

ALTER TABLE user
ADD COLUMN Email VARCHAR(40) AFTER ZipCode;

Alter table product
Add Column ActiveYN varchar(1) default 'Y';

update product set ActiveYN = 'N' where discontinued = 1;

Alter table product
Drop Column discontinued;
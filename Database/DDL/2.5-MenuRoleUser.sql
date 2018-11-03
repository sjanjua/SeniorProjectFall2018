use inventory;

Drop table if exists role;

Create table role(
`RoleID` INTEGER NOT NULL AUTO_INCREMENT,
`RoleName` Varchar(50),
Constraint `PK_Role` Primary Key (`RoleID`)
);

Insert into role values (null, 'Administrator');
Insert into role values (null, 'All Accesss');
Insert into role values (null, 'Sales');
Insert into role values (null, 'Inventory');


Drop table if exists menu;

Create table menu(
`MenuID` INTEGER NOT NULL AUTO_INCREMENT,
`MenuName` Varchar(50),
constraint `PK_Menu` Primary Key (`MenuID`)
);

Insert into menu values (null, 'Administration');
Insert into menu values (null, 'Maintenance');
Insert into menu values (null, 'Product');
Insert into menu values (null, 'Order');

drop table if exists menuRole;

Create table menuRole(
`MenuID` INTEGER,
`RoleID` Integer,
Constraint `FK_Menu` Foreign Key (`MenuID`) References `menu`(`MenuID`),
Constraint `FK_Role` Foreign key (`RoleID`) References `role`(`RoleID`)
);

Insert into menuRole values (1, 1);
Insert into menuRole values (2, 1);
Insert into menuRole values (3, 1);
Insert into menuRole values (4, 1);

Insert into menuRole values (2, 2);
Insert into menuRole values (3, 2);
Insert into menuRole values (4, 2);

Insert into menuRole values (3, 4);

Insert into menuRole values (4, 3);

Drop table if exists user;

create table user(
	`UserID` INTEGER NOT NULL AUTO_INCREMENT,
    `Username` Varchar(50) not null,
    `Password` blob comment 'salt is seniorproject',
    `FirstName` Varchar(100),
    `LastName` Varchar(100),
    `RoleID` INteger,
    `ActiveYN` varchar(1) DEFAULT 'Y',
    Constraint `PK_User` Primary Key (`UserID`),
    Constraint `UK_Username` Unique key (`Username`)
);

INsert into user values(NULL,'admin', AES_ENCRYPT('admin','seniorproject'), 'Admin', '', 1, 'Y');
INsert into user values(NULL,'cchakka', AES_ENCRYPT('chakka','seniorproject'), 'Chaitanya', 'Chakka',2, 'Y');
INsert into user values(NULL,'achris', AES_ENCRYPT('chris','seniorproject'), 'Christopher', 'Andrews',2, 'Y');
INsert into user values(NULL,'nturner', AES_ENCRYPT('turner','seniorproject'), 'Nick', 'Turner',2, 'Y');
INsert into user values(NULL,'sjanjua', AES_ENCRYPT('janjua','seniorproject'), 'Sarim', 'Janjua',2, 'Y');
INsert into user values(NULL,'kparker', AES_ENCRYPT('parker','seniorproject'), 'Kyle', 'Parker',2, 'Y');
INsert into user values(NULL,'qterry', AES_ENCRYPT('terry','seniorproject'), 'Quentin', 'Terry',2, 'Y');
INsert into user values(NULL,'ewojcik', AES_ENCRYPT('wojcik','seniorproject'), 'Erik', 'Wojcik',3, 'Y');
INsert into user values(NULL,'mahmed', AES_ENCRYPT('ahmed','seniorproject'), 'Mohib', 'Ahmed',3, 'Y');
CREATE DATABASE barbershop;
USE barbershop;

drop table barber;
drop table customers;
drop table shift;

CREATE TABLE barber (
	BarberId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
	UserName VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL UNIQUE,
    Password VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
	Role ENUM("Barber") CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ModifyDate DATETIME,
    UserCreation INT NOT NULL,
    UserMod INT,
    UserDelete INT,
    DeletedDate DATETIME,
    Deleted BIT DEFAULT 0 NOT NULL,
    FOREIGN KEY (UserCreation) REFERENCES barber(BarberId)
)ENGINE = InnoDB default CHARACTER SET latin1 COLLATE latin1_spanish_ci;

CREATE TABLE customers(
	CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
	Phone VARCHAR(20) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL UNIQUE,
    UserName VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL UNIQUE,
    Password VARCHAR(50) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
    Positive INT NOT NULL,
    Negative INT NOT NULL,
    Role ENUM("Customer") CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ModifyDate DATETIME,
    UserCreation INT NOT NULL,
    UserMod INT,
    UserDelete INT,
    DeletedDate DATETIME,
    Deleted BIT DEFAULT 0 NOT NULL,
    FOREIGN KEY (UserCreation) REFERENCES barber(BarberId),
    FOREIGN KEY (UserMod) REFERENCES barber(BarberId),
    FOREIGN KEY (UserDelete) REFERENCES barber(BarberId)
)ENGINE = InnoDB default CHARACTER SET latin1 COLLATE latin1_spanish_ci;

CREATE TABLE shift(
	ShiftId INT AUTO_INCREMENT PRIMARY KEY,
    Date date,
    Time time,
    Position INT NOT NULL,
	CreationDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ModifyDate DATETIME,
    UserCreation INT NOT NULL,
    UserMod INT,
    UserDelete INT,
    DeletedDate DATETIME,
    Deleted BIT DEFAULT 0 NOT NULL,
    FOREIGN KEY (UserCreation) REFERENCES customers(CustomerId),
	FOREIGN KEY (UserMod) REFERENCES customers(CustomerId),
	FOREIGN KEY (UserDelete) REFERENCES customers(CustomerId)
)ENGINE = InnoDB default CHARACTER SET latin1 COLLATE latin1_spanish_ci;
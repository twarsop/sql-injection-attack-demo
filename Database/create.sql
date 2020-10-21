CREATE DATABASE sqlinjectionattackdemo;

CREATE TABLE sqlinjectionattackdemo.public.titles
(
    Id SERIAL NOT NULL PRIMARY KEY,
    Name VARCHAR(4) NOT NULL
);

INSERT INTO sqlinjectionattackdemo.public.titles (Id, Name)
VALUES (1, 'Mr'),
(2, 'Mrs'),
(3, 'Miss'),
(4, 'Dr'),
(5, 'Ms'),
(6, 'Prof');

CREATE TABLE sqlinjectionattackdemo.public.customers
(
    Id SERIAL NOT NULL PRIMARY KEY,
    TitleId INT NOT NULL REFERENCES sqlinjectionattackdemo.public.titles(Id),
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    AddressLine1 VARCHAR(255) NOT NULL,
    AddressPostcode VARCHAR(255) NOT NULL
);

INSERT INTO sqlinjectionattackdemo.public.customers (TitleId, FirstName, LastName, AddressLine1, AddressPostcode)
VALUES (1, 'FirstName', 'LastName', 'AddressLine1', 'AddressPostcode');
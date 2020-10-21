CREATE DATABASE sqlinjectionattackdemo;

CREATE TABLE sqlinjectionattackdemo.public.titles
(
    id SERIAL NOT NULL PRIMARY KEY,
    name VARCHAR(4) NOT NULL
);

INSERT INTO sqlinjectionattackdemo.public.titles (id, name)
VALUES (1, 'Mr'),
(2, 'Mrs'),
(3, 'Miss'),
(4, 'Dr'),
(5, 'Ms'),
(6, 'Prof');

CREATE TABLE sqlinjectionattackdemo.public.customers
(
    id SERIAL NOT NULL PRIMARY KEY,
    titleid INT NOT NULL REFERENCES sqlinjectionattackdemo.public.titles(id),
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL,
    addressline1 VARCHAR(255) NOT NULL,
    addresspostcode VARCHAR(255) NOT NULL
);

INSERT INTO sqlinjectionattackdemo.public.customers (titleid, firstname, lastname, addressline1, addresspostcode)
VALUES (1, 'FirstName', 'LastName', 'AddressLine1', 'AddressPostcode');
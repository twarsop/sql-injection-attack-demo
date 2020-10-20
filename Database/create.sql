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
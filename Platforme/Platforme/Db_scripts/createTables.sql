DROP database  Platforme;
CREATE Database Platforme;
USE Platforme

CREATE table TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR (80),
	Obrisan BIT
);

CREATE TABLE Namestaj(
	Id INT PRIMARY KEY IDENTITY (1,1),
	IdTip INT,
	Sifra VARCHAR(20) ,
	Naziv VARCHAR (100),
	Cena NUMERIC(9,2),
	Kolicina INT,
	Obrisan BIT,
	FOREIGN KEY (IdTip) REFERENCES TipNamestaja(Id)
);
create table Kupac(
	Id int primary key identity(1,1),
	Ime varchar(25),
	Prezime VARCHAR(25),
	Telefon VARCHAR(25),
	Obrisan BIT
);
CREATE TABLE Zaposleni(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Ime VARCHAR(30),
	Prezime VARCHAR(30),
	Enum INT,
	KorisnickoIme VARCHAR(30),
	Lozinka VARCHAR(30)
);
CREATE TABLE StavkaProdajeNamestaja(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Id_Racun INT,
	Id_Namestaj INT,
	Kolicina INT
);
CREATE TABLE Racun(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Id_Kupac INT,
	Id_Zaposleni INT,
	Datum DATETIME
);
CREATE TABLE Akcija(
	Id INT PRIMARY KEY IDENTITY(1,1),
	DatumPocetka DATETIME,
	DatumZavrsetka DATETIME,
	IdNamestaj INT,
	Popust NUMERIC(9,2),
	Obrisan BIT
);
CREATE TABLE StavkaUsluge(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Id_Racun INT,
	Id_Usluga INT
);
CREATE TABLE Usluga(
	Id	INT PRIMARY KEY IDENTITY(1,1),
	Naziv VARCHAR(30),
	Cena NUMERIC(9,2),
	Obrisan BIT
)
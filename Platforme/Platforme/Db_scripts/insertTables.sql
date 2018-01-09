

INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES ('Krevet',0)
INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES ('Ugaona garnitura',0)
INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES ('Kauc',0)

INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (1,'n1', 'Roma',1000,22,0)
INSERT INTO Namestaj(IdTip,Sifra,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (2,'n2', 'Sofija',1200,22,0)
INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (3,'n3', 'Atina',1250,21,0)
INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (3,'n4', 'Napoli',2250,21,0)
INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (3,'n5', 'Paris',3250,41,0)
INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (1,'a1', 'Porto',1550,21,0)
INSERT INTO Namestaj(IdTip,Sifra ,Naziv, Cena ,Kolicina,Obrisan) 
VALUES (3,'a2', 'Avala',5250,21,0)

INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Prevoz do 50 km',1000,0)
INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Prevoz do 100 km',2000,0)
INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Prevoz do 300 km',4000,0)
INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Montaza ormana',500,0)
INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Montaza kuhinje',1500,0)
INSERT INTO Usluga(Naziv,Cena,Obrisan) VALUES('Montaza stola',500,0)

INSERT INTO Zaposleni(Ime,Prezime,Tip,KorisnickoIme,Lozinka,Obrisan)
	VALUES ('a','a','Administrator','a','a',0)
INSERT INTO Zaposleni(Ime,Prezime,Tip,KorisnickoIme,Lozinka,Obrisan)
	VALUES ('b','b','Prodavac','b','b',0)
INSERT INTO Zaposleni(Ime,Prezime,Tip,KorisnickoIme,Lozinka,Obrisan)
	VALUES ('a','a','Administrator','c','c',0)
INSERT INTO Zaposleni(Ime,Prezime,Tip,KorisnickoIme,Lozinka,Obrisan)
	VALUES ('a','a','Prodavac','d','d',0)

INSERT INTO Akcija(DatumPocetka,DatumZavrsetka,IdNamestaj,Popust,Obrisan)
	VALUES('1/7/2018 12:00:00 AM','1/13/2018 12:00:00 AM',1,20,0)
INSERT INTO Akcija(DatumPocetka,DatumZavrsetka,IdNamestaj,Popust,Obrisan)
	VALUES('1/7/2018 12:00:00 AM','1/11/2018 12:00:00 AM',2,25,0)
INSERT INTO Akcija(DatumPocetka,DatumZavrsetka,IdNamestaj,Popust,Obrisan)
	VALUES('1/9/2018 12:00:00 AM','1/15/2018 12:00:00 AM',3,15,0)

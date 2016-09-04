create database referenciaDireta;
use referenciaDireta;

create table usuarios (
	codigo integer primary key auto_increment,
	nome varchar(100) not null,
	username varchar(45) not null,
	senha varchar(45) not null,
	cpf varchar(14) not null
);

insert into usuarios values(0, 'Felipe', 'felipe', '12345', '123.456.789-10');
insert into usuarios values(0, 'Gabriel', 'gabriel', '123', '897.456.457-20');
insert into usuarios values(0, 'José', 'jose', '987654', '987.854.124-25');


select * from usuarios; 

create table produtos (
	codigo integer primary key auto_increment,
	nome varchar(100) not null,
	preco decimal(10, 2) not null,
	imagem varchar(250) not null
);

insert into produtos values
(0, 'Notebook Acer E5-574-592S Intel Core i5 8GB 1TB', 2499.99, 'Content/ImagensProdutos/Notebook Acer E5-574-592S.png'),
(0, 'Impressora Laser Mono P1102w', 825.99, 'Content/ImagensProdutos/Impressora Laser Mono P1102w.jpg');
insert into produtos values(0, 'HD Externo Portátil WD Elements 1TB USB 3.0', 259.99, 
'Content/ImagensProdutos/HD Externo Portátil WD Elements 1TB USB 3.0.png');

select * from produtos;
CREATE TABLE LIVRO (
CodL			INT PRIMARY KEY IDENTITY(1,1),
Titulo			VARCHAR(40) NOT NULL,
Editora			VARCHAR(40) NOT NULL,
Edicao			INT NULL,
AnoPublicacao	VARCHAR(4) NULL,
Valor			DECIMAL NULL
);

CREATE TABLE AUTOR(
CodAu	INT PRIMARY KEY IDENTITY(1,1),
Nome	VARCHAR(40) NOT NULL
);

CREATE TABLE ASSUNTO(
CodAs		INT PRIMARY KEY IDENTITY(1,1),
Descricao	VARCHAR(20) NOT NULL
);

CREATE TABLE LIVRO_AUTOR(
Id			INT PRIMARY KEY IDENTITY(1,1),
Livro_CodL	INT FOREIGN KEY REFERENCES LIVRO(CodL) NOT NULL,
Autor_CodAu	INT FOREIGN KEY REFERENCES AUTOR(CodAu) NOT NULL
);

CREATE TABLE LIVRO_ASSUNTO(
Id				INT PRIMARY KEY IDENTITY(1,1),
Livro_CodL		INT FOREIGN KEY REFERENCES LIVRO(CodL) NOT NULL,
Assunto_CodAs	INT FOREIGN KEY REFERENCES ASSUNTO(CodAs) NOT NULL
);

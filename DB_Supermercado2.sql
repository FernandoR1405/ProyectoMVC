CREATE DATABASE SupermercadoDB
GO
USE SupermercadoDB
GO
--Supermercado

--Gral.tblPersonas:id,identidad,rtn,nombres,primerapelli,segundoapelli,sexo,iddireccion,telefono,correo,auditoria

--Gral.tblCargo:id,descripcion,auditoria

--Gral.tblCargoPersona:id,idcargo,idpersona,auditoria

--x-Acce.tblUsuarios: id,usuarionombre,contraseña,esadmin,idcargopersona,auditoria

--x-Gral.tblDepartamentos:codigo,descripcion,auditoria

--x-Gral.tblCiudades:codigo,descripcion,iddepartamento,auditoria

--x-Gral.tblDirecciones:id,calle,avenida,bloque,idciudad,auditoria

--Gral.tblProveedores:id,nomempresa,nombrerepresentan,apelli1representan,apelli2representan,iddireccion,telefonofij,telefonomov,correo,paginaweb

--Fact.tblCategorias:id,descripcion,auditoria

--Fact.tblProductos:id,codigo,descripcion,idcategoria,idproveedor,stock,preciocompra,precioventa,imagen,auditoria

--Fact.tblventaencabezado:id,numfac,fecha,idpersona,total,auditoria

--Fact.tblventadetalle:id,id,producto,precio,cant,descuento,impuesto,subtotal

--Fact.tblcompras:id,numfac,fecha,idproducto,cant,auditoria
























CREATE SCHEMA Fact
GO
CREATE SCHEMA Gral
GO
CREATE SCHEMA Acce
GO

CREATE TABLE Acce.tblUsuarios(
	Usu_Id							 INT IDENTITY(1,1),
	Usu_UsuarioNombre				 NVARCHAR(100),
	Usu_Contrasena					 NVARCHAR(100),
	Usu_EsAdmin						 BIT,
	CarP_Id							 INT,
	Usu_Activo						 BIT,
	Usu_UsuarioCrea					 INT NOT NULL,
	Usu_FechaCrea					 DATETIME NOT NULL,
	Usu_UsuarioModifica				 INT,
	Usu_FechaModifica				 DATETIME,
	CONSTRAINT PK_tblUsuarios_Usu_Id PRIMARY KEY(Usu_Id)
	)
	INSERT INTO  Acce.tblUsuarios(Usu_UsuarioNombre,Usu_Contrasena,Usu_EsAdmin,Usu_Activo,Usu_UsuarioCrea,Usu_FechaCrea,Usu_UsuarioModifica,Usu_FechaModifica)
	VALUES ('Admin','123',1,1,1,GETDATE(),NULL,NULL)

	ALTER TABLE  Acce.tblUsuarios
	ADD CONSTRAINT FK_tblUsuarios_tblPersonas_CarP_Id					FOREIGN KEY(CarP_Id)			  REFERENCES Gral.tblCargosPersonas(CarP_Id)

	ALTER TABLE  Acce.tblUsuarios
	ADD CONSTRAINT FK_tblUsuarios_Usu_UsuarioCrea_tblUsuarios_Usu_Id	 FOREIGN KEY(Usu_UsuarioCrea)	  REFERENCES  Acce.tblUsuarios(Usu_Id)

	ALTER TABLE  Acce.tblUsuarios
	ADD CONSTRAINT FK_tblUsuarios_Usu_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY(Usu_UsuarioModifica) REFERENCES  Acce.tblUsuarios(Usu_Id)


CREATE TABLE Gral.tblDepartamentos(
	Dep_Codigo			INT,
	Dep_Descripcion		NVARCHAR(50),
	Dep_Activo			BIT,
	Dep_UsuarioCrea		INT,	
	Dep_FechaCrea		DATETIME,
	Dep_UsuarioModifica INT,
	Dep_FechaModifica	DATETIME,
	CONSTRAINT PK_tblDepartamentos_Dep_Codigo								PRIMARY KEY(Dep_Codigo),
	CONSTRAINT FK_tblDepartamentos_Dep_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY(Dep_UsuarioCrea)	 REFERENCES  Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_tblDepartamentos_Dep_UsuarioModifica_tblUsuarios_Usu_Id	FOREIGN KEY(Dep_UsuarioModifica) REFERENCES  Acce.tblUsuarios(Usu_Id)
);

CREATE TABLE Gral.tblCiudades(
	Ciu_Codigo			INT,
	Ciu_Descripcion		NVARCHAR(50),
	Dep_Codigo			INT,
	Ciu_Activo			BIT,
	Ciu_UsuarioCrea		INT,	
	Ciu_FechaCrea		DATETIME,
	Ciu_UsuarioModifica INT,
	Ciu_FechaModifica	DATETIME,
	CONSTRAINT PK_tblCiudades_Ciu_Codigo								PRIMARY KEY(Ciu_Codigo),
	CONSTRAINT FK_tblCiudades_tblDepartamentos_Dep_Codigo				FOREIGN KEY(Dep_Codigo)			 REFERENCES Gral.tblDepartamentos(Dep_Codigo),
	CONSTRAINT FK_tblCiudades_Ciu_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY(Ciu_UsuarioCrea)	 REFERENCES  Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_tblCiudades_Ciu_UsuarioModifica_tblUsuarios_Usu_Id	FOREIGN KEY(Ciu_UsuarioModifica) REFERENCES  Acce.tblUsuarios(Usu_Id)
);

CREATE TABLE Gral.tblDirecciones(
	Dir_Id				INT IDENTITY(1,1),
	Ciu_Codigo			INT,
	Dir_Sector			VARCHAR(50),
	Dir_Calle			NVARCHAR(50),
	Dir_Avenida			NVARCHAR(50),
	Dir_Bloque			NVARCHAR(20),
	Dir_Activo			BIT,
	Dir_UsuarioCrea		INT,	
	Dir_FechaCrea		DATETIME,
	Dir_UsuarioModifica INT,
	Dir_FechaModifica	DATETIME,
	CONSTRAINT PK_tblDirecciones_Dir_Id									PRIMARY KEY(Dir_Id),
	CONSTRAINT FK_tblDirecciones_tblDirecciones_Ciu_Codigo				FOREIGN KEY(Ciu_Codigo)			 REFERENCES Gral.tblCiudades(Ciu_Codigo),
	CONSTRAINT FK_tblDirecciones_Dir_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY(Dir_UsuarioCrea)	 REFERENCES  Acce.tblUsuarios(Usu_Id), 
	CONSTRAINT FK_tblDirecciones_Dir_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY(Dir_UsuarioModifica) REFERENCES  Acce.tblUsuarios(Usu_Id)
)

CREATE TABLE Gral.tblPersonas(
	Per_Id					INT IDENTITY(1,1),
	Per_Identidad			NVARCHAR(13),
	Per_Rtn					NVARCHAR(15) NULL,
	Per_Nombres				NVARCHAR(50),
	Per_PrimerApellido		NVARCHAR(50),
	Per_SegundoApellido		NVARCHAR(100),
	Per_Sexo				CHAR(1),
	Dir_Id					INT,
	Per_Telefono			NVARCHAR(8),
	Per_Correo				NVARCHAR(60),
	Per_EsActivo			INT NOT NULL,
	Per_UsuarioCrea			INT NOT NULL,
	Per_FechaCrea			DATETIME NOT NULL,
	Per_UsuarioModifica		INT NULL,
	Per_FechaModifica		DATETIME NULL
	CONSTRAINT PK_tblPersonas_Per_Id PRIMARY KEY(Per_Id)
	)

	INSERT INTO Gral.tblPersonas(Per_Identidad,Per_Rtn,Per_Nombres,Per_PrimerApellido,Per_SegundoApellido,Per_Sexo,Per_Telefono,Per_EsActivo,Per_UsuarioCrea,Per_FechaCrea)
	VALUES ('0502200302148',NULL,'Josue Antonio','Guerra','Contreras','M','97991325',1,1,'2020-01-16')

	ALTER TABLE Gral.tblPersonas
	ADD CONSTRAINT CK_Per_Sexo											 CHECK (Per_Sexo in ('F','M'))

	ALTER TABLE Gral.tblPersonas
	ADD CONSTRAINT FK_tblPersonas_tblDireccion_Dir_Id					 FOREIGN KEY (Dir_Id)				REFERENCES Gral.tblDirecciones(Dir_Id)
	
	ALTER TABLE Gral.tblPersonas
	ADD CONSTRAINT FK_tblPersonas_Per_UsuarioCrea_tblUsuarios_Usu_Id	 FOREIGN KEY (Per_UsuarioCrea)		REFERENCES Acce.tblUsuarios(Usu_Id)
	
	ALTER TABLE Gral.tblPersonas
	ADD CONSTRAINT FK_tblPersonas_Per_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY (Per_UsuarioModifica)  REFERENCES Acce.tblUsuarios(Usu_Id)


CREATE TABLE Gral.tblCargos(
	Carg_Id					INT IDENTITY(1,1),
	Carg_Descripcion		NVARCHAR(60),
	Carg_EsActivo			INT NOT NULL,
	Carg_UsuarioCrea		INT NOT NULL,
	Carg_FechaCrea			DATETIME NOT NULL,
	Carg_UsuarioModifica	INT NULL,
	Carg_FechaModifica		DATETIME NULL
	CONSTRAINT PK_tblCargo_Carg_Id									PRIMARY KEY(Carg_Id),
	CONSTRAINT FK_tblCargos_Carg_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY (Carg_UsuarioCrea)		REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_tblCargos_Carg_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY (Carg_UsuarioModifica)  REFERENCES Acce.tblUsuarios(Usu_Id)
);

CREATE TABLE Gral.tblCargosPersonas(
	CarP_Id					INT IDENTITY(1,1),
	Carg_Id					INT,
	Per_Id					INT,
	CarP_EsActivo			INT NOT NULL,
	CarP_UsuarioCrea		INT NOT NULL,
	CarP_FechaCrea			DATETIME NOT NULL,
	CarP_UsuarioModifica	INT NULL,
	CarP_FechaModifica		DATETIME NULL
	CONSTRAINT PK_tblCargosPersonas_CarP_Id									PRIMARY KEY(CarP_Id),
	CONSTRAINT FK_tblCargosPersonas_tblCargos_Dir_Id						FOREIGN KEY (Carg_Id)				REFERENCES Gral.tblCargos(Carg_Id),
	CONSTRAINT FK_tblCargosPersonas_tblPersonas_Dir_Id						FOREIGN KEY (Per_Id)				REFERENCES Gral.tblPersonas(Per_Id),
	CONSTRAINT FK_tblCargosPersonas_CarP_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY (CarP_UsuarioCrea)		REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_tblCargosPersonas_CarP_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY (CarP_UsuarioModifica)  REFERENCES Acce.tblUsuarios(Usu_Id)
);



CREATE TABLE Gral.tblProveedores
(
	Pro_Id						INT IDENTITY(1,1),
	Pro_Empresa					NVARCHAR(50),
	Pro_RepNombreS				NVARCHAR(100),
	Pro_RepPriApellido			NVARCHAR(50),
	Pro_RepSegApellido			NVARCHAR(50),
	Dir_Id						INT,
	Pro_TelFijo					NVARCHAR(8),
	Pro_TelMovil				NVARCHAR(8),
	Pro_Email					NVARCHAR(100),
	Pro_PaginaWeb				NVARCHAR(MAX),
	Pro_Estado					BIT NOT NULL,
	Pro_UsuarioCrea				INT NOT NULL,
	Pro_FechaCrea				DATE NOT NULL,
	Pro_UsuarioModifica			INT,
	Pro_FechaModifica			DATE
	CONSTRAINT PK_Gral_tblProveedores_Pro_Id										PRIMARY KEY(Pro_Id),
	CONSTRAINT FK_Gral_tblProveedores_Gral_tblDirecciones_Dir_Id					FOREIGN KEY(Dir_Id)			 REFERENCES Gral.tblDirecciones(Dir_Id),
	CONSTRAINT FK_Gral_tblProveedores_Pro_UsuarioCrea_Acc_tblUsuario_Usu_Id			FOREIGN KEY(Pro_UsuarioCrea) REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_Gral_tblProveedores_Pro_UsuarioModifica_Acc_tblUsuario_Usu_Id		FOREIGN KEY(Pro_UsuarioCrea) REFERENCES Acce.tblUsuarios(Usu_Id)
)


CREATE TABLE Fact.tblCategorias
(
	Cat_Id						INT IDENTITY(1,1),
	Cat_Descripcion				NVARCHAR(100),
	Cat_Estado					BIT NOT NULL,
	Cat_UsuarioCrea				INT NOT NULL,
	Cat_FechaCrea				DATE NOT NULL,
	Cat_UsuarioModifica			INT,
	Cat_FechaModifica			DATE,
	CONSTRAINT PK_tblCategorias_Cat_Id											PRIMARY KEY(Cat_Id),
	CONSTRAINT FK_Fact_tblCategorias_Cat_UsuarioCrea_Acc_tblUsuario_Usu_Id		FOREIGN KEY(Cat_UsuarioCrea)	 REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_Fact_tblCategorias_Cat_UsuarioModifica_Acc_tblUsuario_Usu_Id  FOREIGN KEY(Cat_UsuarioModifica) REFERENCES Acce.tblUsuarios(Usu_Id)
)


CREATE TABLE Fact.tblProductos
(
	Prd_Id						INT IDENTITY(1,1),
	Prd_Codigo					NVARCHAR(20) UNIQUE,
	Prd_Descripcion				NVARCHAR(MAX),
	Cat_Id						INT,
	Pro_Id						INT,
	Prd_Stock					INT,
	Prd_PrecioCompra			NUMERIC(8,2),
	Prd_PrecioVenta				NUMERIC(8,2),
	Prd_Imagen					IMAGE,
	Prd_Estado					BIT NOT NULL,
	Prd_UsuarioCrea				INT NOT NULL,
	Prd_FechaCrea				DATE NOT NULL,
	Prd_UsuarioModifica			INT,
	Prd_FechaModifica			DATE
	CONSTRAINT PK_Fact_tblProductos_Prd_Id										PRIMARY KEY(Prd_Id),
	CONSTRAINT FK_Fact_tblProductos_Fact_tblCategorias_Cat_Id					FOREIGN KEY(Cat_Id)				 REFERENCES Fact.tblCategorias(Cat_Id),
	CONSTRAINT FK_Fact_tblProductos_Gral_tblProveedores_Pro_Id					FOREIGN KEY(Pro_Id)				 REFERENCES Gral.tblProveedores(Pro_Id),
	CONSTRAINT FK_Fact_tblProductos_Prd_UsuarioCrea_Acc_tblUsuario_Usu_Id		FOREIGN KEY(Prd_UsuarioCrea)	 REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_Fact_tblProductos_Prd_UsuarioModifica_Acc_tblUsuario_Usu_Id	FOREIGN KEY(Prd_UsuarioModifica) REFERENCES Acce.tblUsuarios(Usu_Id)
)



--Fact.tblventaencabezado:id,numfac,fecha,idpersona,total,auditoria



CREATE TABLE Fact.tblVentaEncabezado(
	VentEnc_Id				INT IDENTITY(1,1),
	VentEnc_NumFactura		INT , 
	VentEnc_Fecha			DATETIME,
	Per_Id					INT,
	VentEnc_Total			MONEY,
	VentEnc_Activo			BIT,
	VentEnc_UsuarioCrea		INT,	
	VentEnc_FechaCrea		DATETIME,
	VentEnc_UsuarioModifica INT,
	VentEnc_FechaModifica	DATETIME,
	CONSTRAINT PK_tblVentaEncabezado_VentEnc_Id									PRIMARY KEY(VentEnc_Id),
	CONSTRAINT FK_tblVentaEncabezado_tblPersonas_Per_Id							FOREIGN KEY (Per_Id)					REFERENCES Gral.tblPersonas(Per_Id),
	CONSTRAINT FK_tblVentaEncabezado_VentEnc_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY(VentEnc_UsuarioCrea)		REFERENCES  Acce.tblUsuarios(Usu_Id), 
	CONSTRAINT FK_tblVentaEncabezado_VentEnc_UsuarioModifica_tblUsuarios_Usu_Id FOREIGN KEY(VentEnc_UsuarioModifica)	REFERENCES  Acce.tblUsuarios(Usu_Id)
)


--Fact.tblventadetalle:id,id,producto,precio,cant,descuento,impuesto,subtotal

CREATE TABLE Fact.tblventadetalle(
	VentDet_Id				 INT IDENTITY(1,1),
	VentEnc_Id				 INT,
	Prd_Id					 INT,
	VentDet_Precio			 MONEY,
	VentDet_Cantidad		 INT,
	VentDet_Descuento		 MONEY,
	VentDet_Impuesto		 NVARCHAR(10),
	VentDet_Subtotal		 MONEY,
	VentDet_Activo			 BIT,
	VentDet_UsuarioCrea	     INT,	
	VentDet_FechaCrea		 DATETIME,
	VentDet_UsuarioModifica  INT,
	VentDet_FechaModifica	 DATETIME,
	CONSTRAINT PK_tblventadetalle_tblventadetalle_VentDet_Id					PRIMARY KEY(VentDet_Id),
	CONSTRAINT FK_tblventadetalle_tblVentaEncabezado_VentEnc_Id					FOREIGN KEY(VentEnc_Id)					REFERENCES Fact.tblVentaEncabezado(VentEnc_Id),
	CONSTRAINT FK_tblventadetalle_tblProductos_Prd_Id							FOREIGN KEY(Prd_Id)						REFERENCES Fact.tblProductos(Prd_Id),
	CONSTRAINT FK_tblventadetalle_VentDet_UsuarioCrea_tblUsuarios_Usu_Id		FOREIGN KEY(VentDet_UsuarioCrea)		REFERENCES  Acce.tblUsuarios(Usu_Id), 
	CONSTRAINT FK_tblventadetalle_VentDet_UsuarioModifica_tblUsuarios_Usu_Id	FOREIGN KEY(VentDet_UsuarioModifica)	REFERENCES  Acce.tblUsuarios(Usu_Id)
);


CREATE TABLE Fact.tblCompras
(
	Cop_Id						INT IDENTITY(1,1),
	Cop_NumFactura				NVARCHAR(20),
	Cop_Fecha					DATE,
	Prd_Id						INT,
	Cop_Cantidad				INT,
	Cop_Estado					BIT NOT NULL,
	Cop_UsuarioCrea				INT NOT NULL,
	Cop_FechaCrea				DATE NOT NULL,
	Cop_UsuarioModifica			INT,
	Cop_FechaModifica			DATE
	CONSTRAINT PK_Fact_tblCompras_Cop_Id									PRIMARY KEY(Cop_Id),
	CONSTRAINT FK_Fact_tblCompras_Fact_tblProductos_Prd_Id					FOREIGN KEY(Prd_Id)				 REFERENCES Fact.tblProductos(Prd_Id),
	CONSTRAINT FK_Fact_tblCompras_Cop_UsuarioCrea_Acc_tblUsuario_Usu_Id		FOREIGN KEY(Cop_UsuarioCrea)	 REFERENCES Acce.tblUsuarios(Usu_Id),
	CONSTRAINT FK_Fact_tblCompras_Cop_UsuarioModifica_Acc_tblUsuario_Usu_Id FOREIGN KEY(Cop_UsuarioModifica) REFERENCES Acce.tblUsuarios(Usu_Id)
)



INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (1, N'Atalntida', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (2, N'Colon', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (3, N'Comayagua', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (4, N'Copan', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (5, N'Cortes', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (6, N'Choluteca', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (7, N'El Paraiso', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (8, N'Francisco Morazan', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (9, N'Gracias a Dios', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (10, N'Intibuca', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (11, N'Islas de la Bahia', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (12, N'La Paz', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (13, N'Lempira', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (14, N'Ocotepeque', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (15, N'Olancho', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (16, N'Santa Barbara', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (17, N'Valle', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblDepartamentos] (Dep_Codigo, [Dep_Descripcion],Dep_Activo, [Dep_UsuarioCrea], [Dep_FechaCrea], [Dep_UsuarioModifica], [Dep_FechaModifica]) VALUES (18, N'Yoro', 1, 1, CAST(N'2021-01-05T00:00:00.000' AS DateTime), NULL, NULL)

GO



INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (1, N'San Pedro Sula', 5,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (2, N'El Progreso', 18,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (3, N'Comayagua', 3,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (4, N'Danli', 7,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (5, N'Ruinas de Copan', 4,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (6, N'Roatan', 11,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (7, N'Tegucigalpa', 8,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (8, N'Catacamas', 15,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (9, N'Puerto Cortes', 5,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (10, N'San Lorenzo', 17,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (11, N'Trujillo', 2,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (12, N'Tela', 1,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (13, N'Gracias', 13,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (14, N'Santa Barbara', 16,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (15, N'Yuscaran', 7,1, 1, CAST(N'2022-01-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (16, N'La Lima', 5,1, 1, CAST(N'2022-01-08T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (17, N'Villa Nueva', 5,1, 1, CAST(N'2022-01-08T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (18, N'Chamelecon', 5,1, 1, CAST(N'2022-01-08T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (19, N'Choloma', 5,1, 1, CAST(N'2022-01-08T00:00:00.000' AS DateTime), NULL, NULL)
INSERT Gral.[tblCiudades] ([Ciu_Codigo], [Ciu_Descripcion], [Dep_Codigo],Ciu_Activo, [Ciu_UsuarioCrea], [Ciu_FechaCrea], [Ciu_UsuarioModifica], [Ciu_FechaModifica]) VALUES (20, N'Pimienta', 5,1, 1, CAST(N'2022-01-08T00:00:00.000' AS DateTime), NULL, NULL)

GO

SET IDENTITY_INSERT [Gral].[tblDirecciones] ON 

INSERT [Gral].[tblDirecciones] ([Dir_Id], [Ciu_Codigo], [Dir_Sector], [Dir_Calle], [Dir_Avenida], [Dir_Bloque], [Dir_Activo], [Dir_UsuarioCrea], [Dir_FechaCrea], [Dir_UsuarioModifica], [Dir_FechaModifica]) VALUES (1, 1, N'La roca', N'1ra', N'1ra', N'503', 1, 1, CAST(N'2022-02-15T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Gral].[tblDirecciones] ([Dir_Id], [Ciu_Codigo], [Dir_Sector], [Dir_Calle], [Dir_Avenida], [Dir_Bloque], [Dir_Activo], [Dir_UsuarioCrea], [Dir_FechaCrea], [Dir_UsuarioModifica], [Dir_FechaModifica]) VALUES (2, 2, N'Cerro Verde', N'1ra', N'2da', N'3', 1, 1, CAST(N'2022-02-15T00:00:00.000' AS DateTime), NULL, NULL)

SET IDENTITY_INSERT [Gral].[tblDirecciones] OFF

INSERT INTO Gral.tblPersonas(Per_Identidad,Per_Rtn,Per_Nombres,Per_PrimerApellido,Per_SegundoApellido,Per_Sexo,Dir_Id,Per_Telefono,
							 Per_Correo,Per_EsActivo,Per_UsuarioCrea,Per_FechaCrea)
VALUES('0502200302148',NULL,'Josue Antonio','Guerra','Contreras','M',1,'97991325','Josue@gmail.com',1,1,'2020-01-16'),
	  ('0202200302157',NULL,'Maria Luisa','Molina','Zuniga','F',1,'95992547','Maria@gmail.com',1,1,'2020-10-16'),
	  ('0302200302136',NULL,'Marco Pedro','Martinez','Castro','M',1,'88961325','Marco@gmail.com',1,1,'2020-11-16'),
	  ('0402200302125',NULL,'Luis Gerardo','Castro','Posas','M',1,'89591325','Luis@gmail.com',1,1,'2021-12-16'),
	  ('0702200302165',NULL,'Julio Mario','Melgar','Argueta','M',1,'97992525','Julio@gmail.com',1,1,'2022-01-16')

INSERT INTO Gral.tblCargos(Carg_Descripcion,Carg_EsActivo,Carg_UsuarioCrea,Carg_FechaCrea)
VALUES('Conserje',1,1,'2021-12-13'),
	  ('Cajero',1,1,'2019-10-05'),
	  ('Gerente',1,1,'2022-11-04'),
	  ('Empacadores',1,1,'2021-02-20')

INSERT INTO Gral.tblCargosPersonas(Carg_Id,Per_Id,CarP_EsActivo,CarP_UsuarioCrea,CarP_FechaCrea)
VALUES(1,1,1,1,'2021-02-23'),
	  (2,5,1,1,'2022-03-16'),
	  (2,3,1,1,'2022-10-17'),
	  (4,4,1,1,'2023-10-17')

INSERT INTO Gral.tblProveedores
VALUES('Sula','Juan Manuel','Perez','Galvez',2,'22667890','98298329','Sula@gmail.com','SulaHN.com',1,1,GETDATE(),NULL,NULL),
	  ('Coca Cola','Andres','Ramirez','Mendoza',1,'22458631','98653118','CocaCola@HN.com','CocaCola.com',1,1,GETDATE(),NULL,NULL),
	  ('CosmeticosSA','Marta Lorena','Ramos','Martinez',1,'21255648','95230179','CosmeticosHN@gmail.com','CosmeticosSA.com',1,1,GETDATE(),NULL,NULL),
	  ('Pollo Norteño','Armando Misael','Nuñez','Savedra',2,'77888510','30214598','PolloNorteño@gmail.com','PolloNorteñoHN.com',1,1,GETDATE(),NULL,NULL),
	  ('Sarita','Arnoldo','Sanchez','Vasquez',2,'45443652','32789516','HSarita@gmail.com','SaritaHN.com',1,1,GETDATE(),NULL,NULL)


INSERT INTO Fact.tblCategorias
VALUES('Bebidas',1,1,GETDATE(),NULL,NULL),
	  ('Carne',1,1,GETDATE(),NULL,NULL),
	  ('Cuidado Personal',1,1,GETDATE(),NULL,NULL),
	  ('Cereales',1,1,GETDATE(),NULL,NULL),
	  ('Verduras',1,1,GETDATE(),NULL,NULL)


INSERT INTO Fact.tblProductos
VALUES('021356','Talco Jhonsson',3,3,30,100.00,150.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('024578','Coca Cola 1L',1,2,50,18.00,25.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('514826','Pollo Entero',2,4,41,50.00,75.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('865465','Desodorante Nivea',3,3,70,120.00,140.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('852145','Banana 3L',1,2,50,50.00,65.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('256532','Acrilico Rojo Para Uñas',3,3,30,40.00,70.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('685353','Sprite 2L',1,2,50,30.00,45.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('514825','Pechuga Fresca',2,4,41,70.00,100.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('825465','Gelatina EGO',3,3,70,20.00,50.00,NULL,1,1,GETDATE(),NULL,NULL),
	  ('852245','Uva 2.5L',1,2,50,30.00,40.00,NULL,1,1,GETDATE(),NULL,NULL)



INSERT INTO Fact.tblCompras
VALUES('656876','2022/01/24',2,20,1,1,GETDATE(),NULL,NULL),
	  ('898665','2022/01/25',7,10,1,1,GETDATE(),NULL,NULL),
	  ('986565','2022/01/27',9,15,1,1,GETDATE(),NULL,NULL),
	  ('755352','2022/01/30',8,11,1,1,GETDATE(),NULL,NULL),
	  ('351655','2022/01/30',3,30,1,1,GETDATE(),NULL,NULL)
		
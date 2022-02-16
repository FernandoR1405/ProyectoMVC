
--=============================================================================
								--LOGIN
--=============================================================================
CREATE PROCEDURE Acce.UDP_Inicio_Sesion
	@Usu_UsuarioNombre	 NVARCHAR(100),
	@Usu_Contrasena		 NVARCHAR(100)

AS
BEGIN
	SELECT *
	FROM [Acce].[tblUsuarios]
	WHERE Usu_UsuarioNombre=@Usu_UsuarioNombre AND Usu_Contrasena=@Usu_Contrasena
END

EXEC Acce.UDP_Inicio_Sesion 'admin','123'

--=============================================================================
								--USUARIOS
--=============================================================================
CREATE PROCEDURE Acce.UDP_Usuario_INSERT
		
	@Usu_UsuarioNombre	 NVARCHAR(100),
	@Usu_Contrasena		 NVARCHAR(100),
	@Usu_EsAdmin		 BIT,
	@Usu_UsuarioCrea	 INT

		
AS
BEGIN
	BEGIN TRAN
		BEGIN TRY
			INSERT INTO [Acce].[tblUsuarios](Usu_UsuarioNombre, Usu_Contrasena, Usu_EsAdmin, Usu_Activo, Usu_UsuarioCrea, Usu_FechaCrea)
			VALUES(@Usu_UsuarioNombre,@Usu_Contrasena,@Usu_EsAdmin,'1',@Usu_UsuarioCrea,GETDATE())		
		COMMIT TRAN
	END TRY
	BEGIN CATCH
			SELECT ERROR_MESSAGE()
			ROLLBACK
	END CATCH
END

CREATE PROCEDURE Acce.UDP_Usuario_UPDATE
	@Usu_Id				 INT,	
	@Usu_UsuarioNombre	 NVARCHAR(100),
	@Usu_Contrasena		 NVARCHAR(100),
	@Usu_EsAdmin		 BIT,
	@Usu_UsuarioModifica INT
		
AS
BEGIN
			UPDATE [Acce].[tblUsuarios]
			SET		
				Usu_UsuarioNombre	 = @Usu_UsuarioNombre,	 
				Usu_Contrasena		 = @Usu_Contrasena,		 
				Usu_EsAdmin		 	 = @Usu_EsAdmin,		 		 
				Usu_UsuarioModifica  = @Usu_UsuarioModifica, 
				Usu_FechaModifica	 = GETDATE()	 

			WHERE Usu_Id=@Usu_Id
END


CREATE PROCEDURE Gral.UDP_Usuario_DELETE
	@Usu_Id				 INT,
	@Usu_UsuarioModifica INT
AS
BEGIN
	
			UPDATE [Acce].[tblUsuarios]
			SET 			
				Usu_Activo = 0,
				Usu_UsuarioModifica = @Usu_UsuarioModifica
			WHERE Usu_Id=@Usu_Id

END


CREATE VIEW Acce.VW_Usuario
AS
SELECT Usu_Id,
	   Usu_UsuarioNombre,	
	   Usu_Contrasena,		
	   CASE Usu_EsAdmin
	   WHEN 1 THEN 'SI'
	   WHEN 0 THEN 'NO'
	   END Usu_EsAdmin
	   
FROM Acce.tblUsuarios

--=============================================================================
								--PERSONAS
--=============================================================================
CREATE PROCEDURE Gral.UDP_tblPersonas_Insert
	@Per_Identidad			NVARCHAR(13),
	@Per_Rtn				NVARCHAR(15),
	@Per_Nombres			NVARCHAR(50),
	@Per_PrimerApellido		NVARCHAR(50),
	@Per_SegundoApellido	NVARCHAR(100),
	@Per_Sexo				CHAR(1),
	@Dir_Id					INT,
	@Per_Telefono			NVARCHAR(8),
	@Per_Correo				NVARCHAR(60),
	@Per_UsuarioCrea		INT
AS
	BEGIN
		INSERT INTO  Gral.tblPersonas
		VALUES(@Per_Identidad,@Per_Rtn,@Per_Nombres,@Per_PrimerApellido,@Per_SegundoApellido,@Per_Sexo,@Dir_Id,@Per_Telefono,@Per_Correo,'1',@Per_UsuarioCrea,GETDATE(),NULL,NULL)
END

EXEC Gral.UDP_tblPersonas_Insert '0502200303208',NULL,'Gloria Dayanne', 'Medina', 'Sanchez','F',2,'97991325','dayanne@gamil.com',1

----------------UPDATE------------------
CREATE PROCEDURE Gral.UDP_tblPersonas_Update
	@Per_Id					INT,
	@Per_Identidad			NVARCHAR(13),
	@Per_Rtn				NVARCHAR(15),
	@Per_Nombres			NVARCHAR(50),
	@Per_PrimerApellido		NVARCHAR(50),
	@Per_SegundoApellido	NVARCHAR(100),
	@Per_Sexo				CHAR(1),
	@Dir_Id					INT,
	@Per_Telefono			NVARCHAR(8),
	@Per_Correo				NVARCHAR(60),
	@Per_UsuarioModifica	INT
AS
	BEGIN
		UPDATE  Gral.tblPersonas 
		SET Per_Identidad		 = @Per_Identidad,		
			Per_Rtn				 = @Per_Rtn,			
			Per_Nombres			 = @Per_Nombres,		
			Per_PrimerApellido	 = @Per_PrimerApellido,	
			Per_SegundoApellido  = @Per_SegundoApellido,
			Per_Sexo			 = @Per_Sexo,			
			Dir_Id				 = @Dir_Id,				
			Per_Telefono		 = @Per_Telefono,		
			Per_Correo			 = @Per_Correo,				
			Per_UsuarioModifica  = @Per_UsuarioModifica,
			Per_FechaModifica	 = GETDATE()	
		
	WHERE Per_Id = @Per_Id
END

EXEC Gral.UDP_tblPersonas_Update 1,'0502200303208',NULL,'Gloria', 'Medina', 'Sanchez','F',2,'97991325','dayanne@gamil.com',1


----------------DELELE------------------
CREATE PROCEDURE Gral.UDP_tblPersonas_Delete
	@Per_Id					INT,
	@Per_UsuarioModifica	INT
AS
	BEGIN
		UPDATE  Gral.tblPersonas
		SET	Per_EsActivo		 = 0,
			Per_UsuarioModifica  = @Per_UsuarioModifica,
			Per_FechaModifica	 = GETDATE()
		WHERE Per_Id = @Per_Id
END

EXEC Gral.UDP_tblPersonas_Delete 2,1


--=============================================================================
								--PROVEEDORES
--=============================================================================
CREATE PROCEDURE UDP_Proveedores_INSERT
	@Pro_Empresa				NVARCHAR(50),
	@Pro_RepNombreS				NVARCHAR(100),
	@Pro_RepPriApellido			NVARCHAR(50),
	@Pro_RepSegApellido			NVARCHAR(50),
	@Dir_Id						INT,
	@Pro_TelFijo				NVARCHAR(8),
	@Pro_TelMovil				NVARCHAR(8),
	@Pro_Email					NVARCHAR(100),
	@Pro_PaginaWeb				NVARCHAR(MAX),
	@Pro_UsuarioCrea			INT
AS
	BEGIN
		INSERT INTO Gral.tblProveedores
		(
			Pro_Empresa,
			Pro_RepNombreS,
			Pro_RepPriApellido,
			Pro_RepSegApellido,
			Dir_Id,Pro_TelFijo,
			Pro_TelMovil,
			Pro_Email,
			Pro_PaginaWeb,
			Pro_Estado,
			Pro_UsuarioCrea,
			Pro_FechaCrea
		)
		VALUES	
		(
			@Pro_Empresa,
			@Pro_RepNombreS,
			@Pro_RepPriApellido,
			@Pro_RepSegApellido,
			@Dir_Id,@Pro_TelFijo,
			@Pro_TelMovil,
			@Pro_Email,
			@Pro_PaginaWeb,
			1,
			@Pro_UsuarioCrea,
			GETDATE()
		)
	END

----------------UPDATE------------------
CREATE PROCEDURE UDP_Proveedores_UPDATE
	@Pro_Id						INT,
	@Pro_Empresa				NVARCHAR(50),
	@Pro_RepNombreS				NVARCHAR(100),
	@Pro_RepPriApellido			NVARCHAR(50),
	@Pro_RepSegApellido			NVARCHAR(50),
	@Dir_Id						INT,
	@Pro_TelFijo				NVARCHAR(8),
	@Pro_TelMovil				NVARCHAR(8),
	@Pro_Email					NVARCHAR(100),
	@Pro_PaginaWeb				NVARCHAR(MAX),
	@Pro_UsuarioModifica		INT
AS
	BEGIN
		UPDATE Gral.tblProveedores
		SET		Pro_Empresa			=	@Pro_Empresa,
				Pro_RepNombreS		=	@Pro_RepNombreS,
				Pro_RepPriApellido	=	@Pro_RepPriApellido,
				Pro_RepSegApellido	=	@Pro_RepSegApellido,
				Dir_Id				=	@Dir_Id,
				Pro_TelFijo			=	@Pro_TelFijo,
				Pro_TelMovil		=	@Pro_TelMovil,
				Pro_Email			=	@Pro_Email,
				Pro_PaginaWeb		=	@Pro_PaginaWeb,
				Pro_UsuarioModifica	=	@Pro_UsuarioModifica,
				Pro_FechaModifica	=	GETDATE()
		WHERE	Pro_Id = @Pro_Id
	END

----------------DELELE------------------
CREATE PROCEDURE UDP_Proveedores_DELETE
	@Pro_Id						INT,
	@Pro_UsuarioModifica		INT
AS
	BEGIN
		UPDATE	Gral.tblProveedores
		SET		Pro_Estado			=	0,
				Pro_UsuarioModifica	=	@Pro_UsuarioModifica,
				Pro_FechaModifica	=	GETDATE()
		WHERE	Pro_Id = @Pro_Id
	END


--=============================================================================
								--CATEGORIAS
--=============================================================================
CREATE PROCEDURE UDP_Categoria_INSERT
	@Cat_Descripcion			NVARCHAR(100),
	@Cat_UsuarioCrea			INT
AS
	BEGIN
		INSERT INTO Fact.tblCategorias
		(
			Cat_Descripcion,
			Cat_Estado,
			Cat_UsuarioCrea,
			Cat_FechaCrea
		)
		VALUES	
		(
			@Cat_Descripcion,
			1,
			@Cat_UsuarioCrea,
			GETDATE()
		)
	END

----------------UPDATE------------------
CREATE PROCEDURE UDP_Categoria_UPDATE
	@Cat_Id						INT,
	@Cat_Descripcion			NVARCHAR(100),
	@Cat_UsuarioModifica		INT
AS
	BEGIN
		UPDATE Fact.tblCategorias
		SET		Cat_Descripcion		= @Cat_Descripcion,
				Cat_UsuarioModifica = @Cat_UsuarioModifica,
				Cat_FechaModifica	= GETDATE()
		WHERE	Cat_Id = @Cat_Id
	END

----------------DELELE------------------
CREATE PROCEDURE UDP_Categoria_DELETE
	@Cat_Id						INT,
	@Cat_UsuarioModifica		INT
AS
	BEGIN
		UPDATE	Fact.tblCategorias
		SET		Cat_Estado			= 0,
				Cat_UsuarioModifica = @Cat_UsuarioModifica,
				Cat_FechaModifica	= GETDATE()
		WHERE	Cat_Id				= @Cat_Id
	END


--=============================================================================
								--PRODCUTOS
--=============================================================================
CREATE PROCEDURE UDP_Productos_INSERT
	@Prd_Codigo					NVARCHAR(20),
	@Prd_Descripcion			NVARCHAR(MAX),
	@Cat_Id						INT,
	@Pro_Id						INT,
	@Prd_Stock					INT,
	@Prd_PrecioCompra			NUMERIC(8,2),
	@Prd_PrecioVenta			NUMERIC(8,2),
	@Prd_Imagen					NVARCHAR(MAX),
	@Prd_UsuarioCrea			INT
AS
	BEGIN
		INSERT INTO Fact.tblProductos
		(
			Prd_Codigo,
			Prd_Descripcion,
			Cat_Id,
			Pro_Id,
			Prd_Stock,
			Prd_PrecioCompra,
			Prd_PrecioVenta,
			Prd_Imagen,
			Prd_Estado,
			Prd_UsuarioCrea,
			Prd_FechaCrea
		)
		VALUES	
		(
			@Prd_Codigo,
			@Prd_Descripcion,
			@Cat_Id,
			@Pro_Id,
			@Prd_Stock,
			@Prd_PrecioCompra,
			@Prd_PrecioVenta,
			@Prd_Imagen,
			1,
			@Prd_UsuarioCrea,
			GETDATE()
		)
	END

----------------UPDATE------------------
CREATE PROCEDURE UDP_Productos_UPDATE
	@Prd_Id						INT,
	@Prd_Codigo					NVARCHAR(20),
	@Prd_Descripcion			NVARCHAR(MAX),
	@Cat_Id						INT,
	@Pro_Id						INT,
	@Prd_Stock					INT,
	@Prd_PrecioCompra			NUMERIC(8,2),
	@Prd_PrecioVenta			NUMERIC(8,2),
	@Prd_Imagen					NVARCHAR(MAX),
	@Prd_UsuarioModifica		INT
AS
	BEGIN
		UPDATE Fact.tblProductos
		SET		Prd_Codigo				=  @Prd_Codigo,
				Prd_Descripcion			=  @Prd_Descripcion,
				Cat_Id					=  @Cat_Id,
				Pro_Id					=  @Pro_Id,
				Prd_Stock				=  @Prd_Stock,
				Prd_PrecioCompra		=  @Prd_PrecioCompra,
				Prd_PrecioVenta			=  @Prd_PrecioVenta,
				Prd_Imagen				=  @Prd_Imagen,
				Prd_UsuarioModifica		=  @Prd_UsuarioModifica,
				Prd_FechaModifica		= GETDATE()
			
		WHERE	Prd_Id = @Prd_Id
	END

----------------DELELE------------------
CREATE PROCEDURE UDP_Productos_DELETE
	@Prd_Id						INT,
	@Prd_UsuarioModifica		INT
AS
	BEGIN
		UPDATE	Fact.tblProductos
		SET		Prd_Estado			= 0,
				Prd_UsuarioModifica = @Prd_UsuarioModifica,
				Prd_FechaModifica	= GETDATE()
		WHERE	Prd_Id				= @Prd_Id
	END

--=============================================================================
								--COMPRAS
--=============================================================================
CREATE PROCEDURE UDP_Compras_INSERT
	@Cop_NumFactura				NVARCHAR(20),
	@Cop_Fecha					DATE,
	@Prd_Id						INT,
	@Cop_Cantidad				INT,
	@Cop_UsuarioCrea			INT
AS
	BEGIN
		BEGIN TRAN
			UPDATE  Fact.tblProductos
			SET		Prd_Stock = Prd_Stock + @Cop_Cantidad
			WHERE	Prd_Id = @Prd_Id


			INSERT INTO Fact.tblCompras
			(
				Cop_NumFactura,	
				Cop_Fecha,		
				Prd_Id,		
				Cop_Cantidad,
				Cop_Estado,
				Cop_UsuarioCrea,
				Cop_FechaCrea
			)
			VALUES	
			(
				@Cop_NumFactura,	
				@Cop_Fecha,	
				@Prd_Id,			
				@Cop_Cantidad,
				1,
				@Cop_UsuarioCrea,
				GETDATE()
			)
		COMMIT TRAN
	END

----------------UPDATE------------------
CREATE PROCEDURE UDP_Compras_UPDATE
	@Cop_Id						INT,
	@Cop_NumFactura				NVARCHAR(20),
	@Cop_Fecha					DATE,
	@Prd_Id						INT,
	@Cop_Cantidad				INT,
	@Cop_UsuarioModifica		INT,
--VARIABLES PRODUCTO
	@Prd_Stock					INT
AS
	BEGIN
		BEGIN TRAN
			UPDATE  Fact.tblProductos
			SET		Prd_Stock = @Prd_Stock
			WHERE	Prd_Id = @Prd_Id

			UPDATE Fact.tblCompras
			SET		Cop_NumFactura		=  @Cop_NumFactura,		
					Cop_Fecha			=  @Cop_Fecha,			
					Prd_Id				=  @Prd_Id,				
					Cop_Cantidad		=  @Cop_Cantidad,		
					Cop_UsuarioModifica	=  @Cop_UsuarioModifica,
					Cop_FechaModifica	=  GETDATE()
			WHERE	Cop_Id = @Cop_Id
		COMMIT TRAN
	END

----------------DELELE------------------
CREATE PROCEDURE UDP_Compras_DELETE
	@Cop_Id						INT,
	@Cop_UsuarioModifica		INT
AS
	BEGIN
		UPDATE	Fact.tblCompras
		SET		Cop_Estado			= 0,
				Cop_UsuarioModifica = @Cop_UsuarioModifica,
				Cop_FechaModifica	= GETDATE()
		WHERE	Cop_Id				= @Cop_Id
	END

--=============================================================================
								--VENTAS
--=============================================================================
--INSERT
CREATE PROCEDURE Fact.UDP_Venta_INSERT
	--VARIABLES ENCABEZADO
	@VentEnc_NumFactura			INT , 
	@VentEnc_Fecha				DATETIME,
	@VentEnc_Total				MONEY,
	@Ven_UsuarioCrea			INT,	
	--VARIABLES DETALLE
	@Prd_Id						INT,
	@VentDet_Precio				MONEY,
	@VentDet_Cantidad			INT,
	@VentDet_Descuento			MONEY,
	@VentDet_Impuesto			NVARCHAR(10),
--VARIABLES CLIENTE QUE SE REGISTRAN EN PERSONAS
	@Per_Identidad				NVARCHAR(13),
	@Per_Rtn					NVARCHAR(15),
	@Per_Nombres				NVARCHAR(50),
	@Per_PrimerApellido			NVARCHAR(50),
	@Per_SegundoApellido		NVARCHAR(100),
	@Per_Sexo					CHAR(1),
	@Dir_Id						INT,
	@Per_Telefono				NVARCHAR(8),
	@Per_Correo					NVARCHAR(60),
	@cont						INT
AS
	BEGIN
		BEGIN TRAN
			DECLARE @i			INT
			DECLARE @Cliente_Id INT
			IF @cont = 0
				BEGIN
					UPDATE  Fact.tblProductos
					SET		Prd_Stock = Prd_Stock - @VentDet_Cantidad
					WHERE	Prd_Id = @Prd_Id
					SET @Cliente_Id = (SELECT MAX(Per_Id) FROM Gral.tblPersonas)
					INSERT INTO Fact.tblVentaEncabezado
						(
							VentEnc_NumFactura,	
							VentEnc_Fecha,		
							Per_Id,				
							VentEnc_Total,
							VentEnc_Activo,
							VentEnc_UsuarioCrea,
							VentEnc_FechaCrea
						)
					VALUES
						(
							@VentEnc_NumFactura,	
							@VentEnc_Fecha,		
							@Cliente_Id,				
							@VentEnc_Total,
							1,
							@Ven_UsuarioCrea,
							GETDATE()
						)
					SET @i = @@IDENTITY

					INSERT INTO Fact.tblventadetalle
						(
							VentEnc_Id,
							Prd_Id,			
							VentDet_Precio,		
							VentDet_Cantidad,	
							VentDet_Descuento,	
							VentDet_Impuesto,	
							VentDet_Subtotal,
							VentDet_Activo,
							VentDet_UsuarioCrea,
							VentDet_FechaCrea
						)
					VALUES
						(
							@i,
							@Prd_Id,				
							@VentDet_Precio,		
							@VentDet_Cantidad,	
							@VentDet_Descuento,	
							@VentDet_Impuesto,	
							--Subtotal
							@VentDet_Precio * @VentDet_Cantidad - @VentDet_Descuento,
							1,
							@Ven_UsuarioCrea,
							GETDATE()
						)
				END
			ELSE
				BEGIN
					SET @i = (SELECT MAX(VentEnc_Id) FROM Fact.tblVentaEncabezado)
					INSERT INTO Fact.tblventadetalle
						(
							VentEnc_Id,
							Prd_Id,			
							VentDet_Precio,		
							VentDet_Cantidad,	
							VentDet_Descuento,	
							VentDet_Impuesto,	
							VentDet_Subtotal,
							VentDet_Activo,
							VentDet_UsuarioCrea,
							VentDet_FechaCrea
						)
					VALUES
						(
							@i,
							@Prd_Id,				
							@VentDet_Precio,		
							@VentDet_Cantidad,	
							@VentDet_Descuento,	
							@VentDet_Impuesto,
							--Subtotal
							@VentDet_Precio * @VentDet_Cantidad - @VentDet_Descuento,
							1,
							@Ven_UsuarioCrea,
							GETDATE()
						)
				END
		COMMIT TRAN
	END



--UPDATE
CREATE PROCEDURE Fact.UDP_Venta_UPDATE
	--VARIABLES ENCABEZADO
	@VentEnc_Id					INT,
	@VentEnc_NumFactura			INT, 
	@VentEnc_Fecha				DATETIME,
	@Per_Id						INT,
	@VentEnc_Total				MONEY,
	@Ven_UsuarioModifica		INT,	
	--VARIABLES DETALLE
	@VentDet_Id					INT,
	@Prd_Id						INT,
	@VentDet_Precio				MONEY,
	@VentDet_Cantidad			INT,
	@VentDet_Descuento			MONEY,
	@VentDet_Impuesto			NVARCHAR(10),
--VARIABLES PRODUCTO
	@Prd_Stock					INT,
	@cont						INT
AS
	BEGIN
		BEGIN TRAN
			IF @cont = 0
				BEGIN
					UPDATE  Fact.tblProductos
					SET		Prd_Stock = @Prd_Stock
					WHERE	Prd_Id = @Prd_Id

					UPDATE	Fact.tblVentaEncabezado
					SET		VentEnc_NumFactura			= @VentEnc_NumFactura,
							VentEnc_Fecha				= @VentEnc_Fecha,
							Per_Id						= @Per_Id,		
							VentEnc_Total				= @VentEnc_Total,
							VentEnc_UsuarioModifica		= @Ven_UsuarioModifica,
							VentEnc_FechaModifica		= GETDATE()
					WHERE	VentEnc_Id					= @VentEnc_Id

					
				END
			ELSE
				BEGIN
					UPDATE	Fact.tblventadetalle
					SET		Prd_Id					= @Prd_Id,				
							VentDet_Precio			= @VentDet_Precio,		
							VentDet_Cantidad		= @VentDet_Cantidad,
							VentDet_Descuento		= @VentDet_Descuento,	
							VentDet_Impuesto		= @VentDet_Impuesto,
							VentDet_UsuarioModifica = @Ven_UsuarioModifica,
							VentDet_FechaModifica	= GETDATE()
					WHERE	VentDet_Id				= @VentDet_Id
							
				END
		COMMIT TRAN
	END


CREATE PROCEDURE Fact.UDP_Venta_DELETE
	--VARIABLES ENCABEZADO
	@VentEnc_Id					INT,
	@Ven_UsuarioModifica		INT
AS
	BEGIN
		BEGIN TRAN
					UPDATE	Fact.tblVentaEncabezado
					SET		VentEnc_Activo				= 0,
							VentEnc_UsuarioModifica		= @Ven_UsuarioModifica,
							VentEnc_FechaModifica		= GETDATE()
					WHERE	VentEnc_Id					= @VentEnc_Id

					
					UPDATE	Fact.tblventadetalle
					SET		VentDet_Activo			= 0,
							VentDet_UsuarioModifica = @Ven_UsuarioModifica,
							VentDet_FechaModifica	= GETDATE()
					WHERE	VentEnc_Id				= @VentEnc_Id
						
		COMMIT TRAN
	END

--=============================================================================
						--DIRECCION-PERSONA
--=============================================================================
--INSERT
CREATE PROCEDURE Fact.UDP_Personas_INSERT_1
--VARIABLES DEPARTAMENTO
	@Dep_Codigo					NVARCHAR(2),
	@Dep_Descripcion			NVARCHAR(50),

--VARIABLES CIUDAD
	@Ciu_Codigo					NVARCHAR(4),
	@Ciu_Descripcion			NVARCHAR(50),
	--@@IDENTITY Dep_Id	
	
--VARIABLES DIRECCION
	--@@IDENTITY Ciu_Id		
	@Dir_Sector					VARCHAR(50),
	@Dir_Calle					NVARCHAR(50),
	@Dir_Avenida				NVARCHAR(50),
	@Dir_Bloque					NVARCHAR(20),

--VARIABLES PERSONA
	@Per_Identidad				NVARCHAR(13),
	@Per_Rtn					NVARCHAR(15),
	@Per_Nombres				NVARCHAR(50),
	@Per_PrimerApellido			NVARCHAR(50),
	@Per_SegundoApellido		NVARCHAR(100),
	@Per_Sexo					CHAR(1),
	--@@IDENTITY Dir_Id
	@Per_Telefono				NVARCHAR(8),
	@Per_Correo					NVARCHAR(60),
	@UsuarioCrea				INT
AS
	BEGIN
		BEGIN TRAN
			DECLARE @Dep_Id		INT
			DECLARE @Ciu_Id		INT
			DECLARE @Dir_Id		INT

					INSERT INTO Gral.tblDepartamentos
						(
							Dep_Codigo,
							Dep_Descripcion, 
							Dep_Activo, 
							Dep_UsuarioCrea, 
							Dep_FechaCrea
						)
						VALUES
							(
								@Dep_Codigo,
								@Dep_Descripcion,
								1,
								@UsuarioCrea,
								GETDATE()
							)
							SET @Dep_Id = @@IDENTITY


					INSERT INTO Gral.tblCiudades
						(
							Ciu_Codigo,
							Ciu_Descripcion, 
							Dep_Id, 
							Ciu_Activo, 
							Ciu_UsuarioCrea, 
							Ciu_FechaCrea
						)
						VALUES
							(
								@Ciu_Codigo,
								@Ciu_Descripcion,
								@Dep_Id,
								1,
								@UsuarioCrea,
								GETDATE()
							)
							SET @Ciu_Id = @@IDENTITY


					INSERT INTO Gral.tblDirecciones
						(
							Ciu_Id, 
							Dir_Sector, 
							Dir_Calle, 
							Dir_Avenida, 
							Dir_Bloque, 
							Dir_Activo, 
							Dir_UsuarioCrea, 
							Dir_FechaCrea
						)
						VALUES
							(
								@Ciu_Id,
								@Dir_Sector,
								@Dir_Calle, 
								@Dir_Avenida, 
								@Dir_Bloque,
								1,
								@UsuarioCrea,
								GETDATE()
							)
							SET @Dir_Id = @@IDENTITY


					INSERT INTO Gral.tblPersonas
						(
							Per_Identidad, 
							Per_Rtn, 
							Per_Nombres, 
							Per_PrimerApellido, 
							Per_SegundoApellido, 
							Per_Sexo, 
							Dir_Id, 
							Per_Telefono, 
							Per_Correo, 
							Per_EsActivo, 
							Per_UsuarioCrea, 
							Per_FechaCrea
						)
						VALUES
							(
								@Per_Identidad, 
								@Per_Rtn, 
								@Per_Nombres, 
								@Per_PrimerApellido, 
								@Per_SegundoApellido, 
								@Per_Sexo, 
								@Dir_Id, 
								@Per_Telefono, 
								@Per_Correo,
								1,
								@UsuarioCrea,
								GETDATE()
							)
		COMMIT TRAN
	END



	--INSERT 1.2
CREATE PROCEDURE Fact.UDP_Personas_INSERT
--VARIABLES DIRECCION
	@Ciu_Id						INT,	
	@Dir_Sector					VARCHAR(50),
	@Dir_Calle					NVARCHAR(50),
	@Dir_Avenida				NVARCHAR(50),
	@Dir_Bloque					NVARCHAR(20),

--VARIABLES PERSONA
	@Per_Identidad				NVARCHAR(13),
	@Per_Rtn					NVARCHAR(15),
	@Per_Nombres				NVARCHAR(50),
	@Per_PrimerApellido			NVARCHAR(50),
	@Per_SegundoApellido		NVARCHAR(100),
	@Per_Sexo					CHAR(1),
	--@@IDENTITY Dir_Id
	@Per_Telefono				NVARCHAR(8),
	@Per_Correo					NVARCHAR(60),
	@UsuarioCrea				INT
AS
	BEGIN
		BEGIN TRAN
			DECLARE @Dir_Id		INT

					INSERT INTO Gral.tblDirecciones
						(
							Ciu_Id, 
							Dir_Sector, 
							Dir_Calle, 
							Dir_Avenida, 
							Dir_Bloque, 
							Dir_Activo, 
							Dir_UsuarioCrea, 
							Dir_FechaCrea
						)
						VALUES
							(
								@Ciu_Id,
								@Dir_Sector,
								@Dir_Calle, 
								@Dir_Avenida, 
								@Dir_Bloque,
								1,
								@UsuarioCrea,
								GETDATE()
							)
							SET @Dir_Id = @@IDENTITY


					INSERT INTO Gral.tblPersonas
						(
							Per_Identidad, 
							Per_Rtn, 
							Per_Nombres, 
							Per_PrimerApellido, 
							Per_SegundoApellido, 
							Per_Sexo, 
							Dir_Id, 
							Per_Telefono, 
							Per_Correo, 
							Per_EsActivo, 
							Per_UsuarioCrea, 
							Per_FechaCrea
						)
						VALUES
							(
								@Per_Identidad, 
								@Per_Rtn, 
								@Per_Nombres, 
								@Per_PrimerApellido, 
								@Per_SegundoApellido, 
								@Per_Sexo, 
								@Dir_Id, 
								@Per_Telefono, 
								@Per_Correo,
								1,
								@UsuarioCrea,
								GETDATE()
							)
		COMMIT TRAN
	END











--UPDATE
CREATE PROCEDURE Fact.UDP_Personas_UPDATE
	--VARIABLES DIRECCION
	@Dir_Id						INT,
	@Ciu_Id						INT,	
	@Dir_Sector					VARCHAR(50),
	@Dir_Calle					NVARCHAR(50),
	@Dir_Avenida				NVARCHAR(50),
	@Dir_Bloque					NVARCHAR(20),

--VARIABLES PERSONA
	@Per_Id						INT,
	@Per_Identidad				NVARCHAR(13),
	@Per_Rtn					NVARCHAR(15),
	@Per_Nombres				NVARCHAR(50),
	@Per_PrimerApellido			NVARCHAR(50),
	@Per_SegundoApellido		NVARCHAR(100),
	@Per_Sexo					CHAR(1),
	@Per_Telefono				NVARCHAR(8),
	@Per_Correo					NVARCHAR(60),
	@UsuarioModifica			INT
AS
	BEGIN
		BEGIN TRAN
					UPDATE	Gral.tblDirecciones
					SET		Ciu_Id				= @Ciu_Id,		
							Dir_Sector			= @Dir_Sector,	
							Dir_Calle			= @Dir_Calle,	
							Dir_Avenida			= @Dir_Avenida,
							Dir_Bloque			= @Dir_Bloque,
							Dir_UsuarioModifica = @UsuarioModifica,
							Dir_FechaModifica	= GETDATE()
					WHERE	@Dir_Id				= @Dir_Id

					
					UPDATE	Gral.tblPersonas
					SET		Per_Identidad		= @Per_Identidad,			
							Per_Rtn				= @Per_Rtn,				
							Per_Nombres			= @Per_Nombres,		
							Per_PrimerApellido	= @Per_PrimerApellido,		
							Per_SegundoApellido	= @Per_SegundoApellido,
							Per_Sexo			= @Per_Sexo,
							Dir_Id				= @Dir_Id,
							Per_Telefono		= @Per_Telefono,		
							Per_Correo			= @Per_Correo,			
							Per_UsuarioModifica	= @UsuarioModifica,
							Per_FechaModifica   = GETDATE()
					WHERE	Per_Id				= @Per_Id
						
		COMMIT TRAN
	END


CREATE PROCEDURE Fact.UDP_Personas_DELETE
	--VARIABLES PERSONA
	@Per_Id						INT,
	@UsuarioModifica			INT,
	--VARIABLES DIRECCION
	@Dir_Id						INT
	
AS
	BEGIN
		BEGIN TRAN
					UPDATE	Gral.tblPersonas
					SET		Per_EsActivo				= 0,
							Per_UsuarioModifica			= @UsuarioModifica,
							Per_FechaModifica			= GETDATE()
					WHERE	Per_Id						= @Per_Id

					
					UPDATE	Gral.tblDirecciones
					SET		Dir_Activo				= 0,
							Dir_UsuarioModifica		= @UsuarioModifica,
							Dir_FechaModifica		= GETDATE()
					WHERE	Dir_Id					= @Dir_Id
						
		COMMIT TRAN
	END

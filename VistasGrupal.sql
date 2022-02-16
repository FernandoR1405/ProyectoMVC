
--		VISTA USUARIO
--====================================
CREATE VIEW Acce.VW_Usuario
AS
SELECT Usu_Id				AS Id_Usuario,
	   Usu_UsuarioNombre,	
	   Usu_Contrasena,		
	   CASE Usu_EsAdmin
	   WHEN 1 THEN 'SI'
	   WHEN 0 THEN 'NO'
	   END Usu_EsAdmin
	   
FROM Acce.tblUsuarios

--		VISTA PERSONA
--====================================
CREATE VIEW Gral.VW.Personas
AS
SELECT	 per.Per_Id														AS Id,
		 per.Per_Identidad												AS Identidad,
		 per.Per_Rtn													AS RTN,
		 per.Per_Nombres+' '+Per_PrimerApellido+' '+Per_SegundoApellido	AS NombresPersona,
		CASE per.Per_Sexo
			WHEN 'F' THEN 'Femenino'
			WHEN 'M' THEN 'Masculino'
	   END Sexo,
		 dire.Dir_Id														AS IdDireccion,
		 dire.Dir_Calle+''+dire.Dir_Avenida+''+dire.Dir_Bloque			AS Direccion,
		 per.Per_Telefono													AS Telefono,
		 per.Per_Correo													AS Correo,
		CASE per.Per_EsActivo
			WHEN 1 THEN 'Activo'
			WHEN 0 THEN 'Inactivo'
	   END Estado													
FROM Gral.tblPersonas		   AS per
INNER JOIN Gral.tblDirecciones AS dire ON per.Dir_Id = dire.Dir_Id

SELECT * FROM  Gral.tblPersonas
	--		VISTA CARGO
--====================================
CREATE VIEW Gral.VW_Cargo
AS
SELECT Carg_Id			AS Id_Cargo,
	   Carg_Descripcion AS Cargo,
	   	CASE Carg_EsActivo
			WHEN 1 THEN 'Activo'
			WHEN 0 THEN 'Inactivo'
	   END Estado
FROM Gral.tblCargos

	--VISTA CARGOSPERSONAS
--====================================
CREATE VIEW Gral.VW_CargoPersona
AS
SELECT carP.CarP_Id														AS Id_CargoPersona,
	   carg.Carg_Descripcion											AS Cargo,
	   per.Per_Nombres+' '+Per_PrimerApellido+' '+Per_SegundoApellido	AS NombresPersona
FROM Gral.tblCargosPersonas AS carP
INNER JOIN Gral.tblCargos   AS carg ON carP.Carg_Id = carg.Carg_Id
INNER JOIN Gral.tblPersonas AS per ON carP.Per_Id = per.Per_Id
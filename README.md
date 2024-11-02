Este repositorio es la respuesta de una prueba, a continuación se detallan los pasos para poder compilar el proyecto, dentro del proyecto existe una carpeta llamada "Documentacion" con un archivo llamado "Documentación.docx", el cuál tiene detalladas algunas partes del examen<br/>
1.- Al descargar este proyecto, favor de cambiar la cadena de conexión en el archivo appsettings.json, actualmente es:<br/>
  "cnn": "Server=SQLEXPRESS; Encrypt=False; TrustServerCertificate=True; Database=PruebaTecnica_AARCO; Integrated Security=True"<br/>
  Se debe de cambiar el valor del "Server" y de "Database" según lo tenga en su BD<br/>
2.- Al abrir el "SLN" de la carpeta se verá la estructura del proyecto, especificando un poco lo que tiene<br/>
  2.1.- La carpeta Configuration contiene archivos de configuración del proyecto<br/>
  2.2.- Se usaron Paquetes nuggets para mejor funcionamiento: <br/>
    2.2.1.- AutoMapper<br/>
    2.2.2.- FluentValidator<br/>
    2.2.3.- MediatR<br/>
    2.2.4.- Entity Framework<br/>
    2.2.5.- Swagger (Al compilar notará que el título es "Prueba técnica" y su versión es "LuisCarbajal-PreV1-2224384055")<br/>
3.- Al compilar, se ven 3 EndPoints, 2 de Dashboard (Uno que es el que se pidió (Con paginado por default) y otro con número de página (Por el tamaño de registros), y el EndPoint de "PostToken", el cual es para generar el token JWT para autenticación (Se conecta a la tabla "Usuario" de la BD)<br/>
4.- En la carpeta "Documentacion" se adjuntan 2 archivos de extensión "json", los cuales son el "Collection" y "Environment" de Postman para el consumo, el EndPoint "PostToken" guarda automáticamente el token generado en la llave "token" del Enviroment<br/>
5.- Para que los EndPoint "dashboard" puedan obtener las llaves del Environment debe ser seleccionado en el ComboList de la parte superior derecha<br/>
6.- Para el correcto funcionamiento del EndPoint "Dashboard" se requiere el SP en la BD que se cambió en el paso, a continuación se agrega el código del SP para que lo pueda agregar en su BD<br/>
    
CREATE PROCEDURE [dbo].[uspRequest]
(
	  @vOpcion int
	, @vString varchar(150)
	, @vPagina int
)
AS
BEGIN
	declare @FilasPorPagina int = 1000;
	if(@vOpcion = 1)
	begin
	select m.nombre		
		, s.nombre as submarca
		, convert(varchar(5), ms.anio) as modelo
		, REPLACE(REPLACE(REPLACE(d.descripcionTexto, '&#160;', ''), '&#39;', ''), '&amp;', '') as descripcion
	from marca m
	inner join submarca s on m.marcaId = s.marcaId
	inner join ModeloPorSubmarca ms on ms.submarcaId = s.submarcaId
	inner join Descripcion d on d.modeloId = ms.modeloId
	where m.nombre = @vString	  
	end
	if(@vOpcion = 2)
	begin
	select m.nombre		
		, s.nombre as submarca
		, convert(varchar(5), ms.anio) as modelo
		, REPLACE(REPLACE(REPLACE(d.descripcionTexto, '&#160;', ''), '&#39;', ''), '&amp;', '') as descripcion
	from marca m
	inner join submarca s on m.marcaId = s.marcaId
	inner join ModeloPorSubmarca ms on ms.submarcaId = s.submarcaId
	inner join Descripcion d on d.modeloId = ms.modeloId
	where s.nombre = @vString	  
	end
	if(@vOpcion not in (1,2))
	begin


SELECT *
FROM (
		select row_number() over (order by m.nombre asc) as numero
			, m.nombre		
			, s.nombre as submarca
			, convert(varchar(5), ms.anio) as modelo
			, REPLACE(REPLACE(REPLACE(d.descripcionTexto, '&#160;', ''), '&#39;', ''), '&amp;', '') as descripcion
		from marca m
			inner join submarca s on m.marcaId = s.marcaId
			inner join ModeloPorSubmarca ms on ms.submarcaId = s.submarcaId
			inner join Descripcion d on d.modeloId = ms.modeloId	
		) as t1
ORDER BY numero asc
OFFSET @vPagina * @FilasPorPagina ROWS
FETCH NEXT @FilasPorPagina ROWS ONLY;
	end
END

<br/>
<br/>
NOTA: Se agregaron funcionalidades extras ya que no se realizó nada de la parte de FrontEnd.

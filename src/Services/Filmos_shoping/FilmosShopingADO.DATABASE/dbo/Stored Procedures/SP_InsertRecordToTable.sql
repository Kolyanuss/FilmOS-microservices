-- =============================================
-- Author:		<0xKolyanus,Nikolay>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_InsertRecordToTable]
	-- Add the parameters for the stored procedure here
	@P_tableName nvarchar(50) = null,
	@P_columnsString nvarchar(MAX) = null, 
	@P_propertiesString nvarchar(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @V_table nvarchar(50) = null
	if (@P_tableName is not null)
		select @V_table = QUOTENAME( TABLE_NAME )
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_NAME = @P_tableName

	declare @V_sql as nvarchar(MAX) = null
	if (@V_table is not null and @P_columnsString is not null and @P_propertiesString is not null)
		SET @V_sql = 'INSERT INTO ' + @V_table + ' (' + @P_columnsString + ') VALUES (' + @P_propertiesString + ');'
	
	if(@V_sql is not null)
		SELECT @V_sql;
	else
		SELECT -1;
END

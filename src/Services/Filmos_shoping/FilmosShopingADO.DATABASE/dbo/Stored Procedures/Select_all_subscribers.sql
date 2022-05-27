-- =============================================
-- Author:		<0xKolyanus,Nikolay>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Select_all_subscribers
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From dbo.Users
	Where Users.id_purchased_subscription != 0
END

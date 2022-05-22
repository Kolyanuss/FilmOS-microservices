-- =============================================
-- Author:		<Author,,Name>
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
	SELECT Users.id, Users.user_name,Type_subscriptions.name_subscribe From dbo.Users, dbo.List_subscriptions,dbo.Type_subscriptions
	Where List_subscriptions.id_user = Users.id AND List_subscriptions.id_subscriptions = Type_subscriptions.id
END

CREATE TABLE [dbo].[Basket_subscriptions] (
    [id_user]          INT NOT NULL,
    [id_subscriptions] INT NOT NULL,
    CONSTRAINT [PK_Basket_subscriptions] PRIMARY KEY CLUSTERED ([id_user] ASC, [id_subscriptions] ASC),
    CONSTRAINT [FK_Basket_subscriptions_Subscriptions] FOREIGN KEY ([id_subscriptions]) REFERENCES [dbo].[Type_subscriptions] ([id]),
    CONSTRAINT [FK_Basket_subscriptions_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([id])
);


CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [user_name] NVARCHAR (MAX) NOT NULL,
    [password]  NVARCHAR (MAX) NOT NULL,
    [is_admin]  BIT            NOT NULL,
    [id_purchased_subscription] INT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Subscriptions] FOREIGN KEY ([id_purchased_subscription]) REFERENCES [dbo].[Type_subscriptions] ([id])
);


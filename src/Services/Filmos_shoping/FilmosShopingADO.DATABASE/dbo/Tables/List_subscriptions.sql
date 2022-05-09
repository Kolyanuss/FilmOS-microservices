CREATE TABLE [dbo].[List_subscriptions] (
    [id_user]          INT NOT NULL,
    [id_subscriptions] INT NOT NULL,
    CONSTRAINT [PK_List_subscriptions] PRIMARY KEY CLUSTERED ([id_user] ASC, [id_subscriptions] ASC),
    CONSTRAINT [FK_List_subscriptions_Type_subscriptions] FOREIGN KEY ([id_subscriptions]) REFERENCES [dbo].[Type_subscriptions] ([id]),
    CONSTRAINT [FK_List_subscriptions_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([id])
);


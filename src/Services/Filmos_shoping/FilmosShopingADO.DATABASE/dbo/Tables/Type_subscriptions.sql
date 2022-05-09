CREATE TABLE [dbo].[Type_subscriptions] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [name_subscribe] NVARCHAR (50) NOT NULL,
    [price]          FLOAT (53)    NOT NULL,
    [duration_month] INT           NOT NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED ([id] ASC)
);


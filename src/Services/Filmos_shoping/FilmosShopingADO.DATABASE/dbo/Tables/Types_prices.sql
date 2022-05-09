CREATE TABLE [dbo].[Types_prices] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [type] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Price_badge] PRIMARY KEY CLUSTERED ([id] ASC)
);


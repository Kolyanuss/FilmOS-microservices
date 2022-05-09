CREATE TABLE [dbo].[Films] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [name_film]     NVARCHAR (MAX) NOT NULL,
    [release_data]  DATETIME2 (7)  NOT NULL,
    [country]       NVARCHAR (MAX) NULL,
    [type_price_id] INT            NOT NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Films_Types_prices] FOREIGN KEY ([type_price_id]) REFERENCES [dbo].[Types_prices] ([id])
);


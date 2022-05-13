CREATE TABLE [dbo].[Basket_films] (
    [id_film] INT NOT NULL,
    [id_user] INT NOT NULL,
    CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED ([id_film] ASC, [id_user] ASC),
    CONSTRAINT [FK_Basket_films_Films] FOREIGN KEY ([id_film]) REFERENCES [dbo].[Films] ([Id]),
    CONSTRAINT [FK_Basket_films_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([Id])
);


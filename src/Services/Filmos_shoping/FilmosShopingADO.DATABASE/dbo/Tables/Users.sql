CREATE TABLE [dbo].[Users] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [user_name] NVARCHAR (MAX) NOT NULL,
    [password]  NVARCHAR (MAX) NOT NULL,
    [is_admin]  BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([id] ASC)
);


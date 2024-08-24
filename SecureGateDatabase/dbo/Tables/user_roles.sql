CREATE TABLE [dbo].[user_roles] (
    [RoleId]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [RoleDescription] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Description of the role', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_roles', @level2type = N'COLUMN', @level2name = N'RoleDescription';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for each role', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_roles', @level2type = N'COLUMN', @level2name = N'RoleId';


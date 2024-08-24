CREATE TABLE [dbo].[permissions] (
    [PermissionId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [PermissionDescri] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([PermissionId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Description of the permission', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'permissions', @level2type = N'COLUMN', @level2name = N'PermissionDescri';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for each permission', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'permissions', @level2type = N'COLUMN', @level2name = N'PermissionId';


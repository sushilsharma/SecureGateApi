CREATE TABLE [dbo].[granted_permissions] (
    [RoleId]       BIGINT         NOT NULL,
    [PermissionId] BIGINT         NOT NULL,
    [TanentCode]   NVARCHAR (250) NULL,
    [IsActive]     BIT            NULL,
    [CreatedDate]  DATETIME       NULL,
    [CreatedBy]    BIGINT         NULL,
    [UpdatedDate]  DATETIME       NULL,
    [UpdatedBy]    BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC),
    FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[permissions] ([PermissionId]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[user_roles] ([RoleId])
);




GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Identifier for the permission', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'granted_permissions', @level2type = N'COLUMN', @level2name = N'PermissionId';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Identifier for the role', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'granted_permissions', @level2type = N'COLUMN', @level2name = N'RoleId';


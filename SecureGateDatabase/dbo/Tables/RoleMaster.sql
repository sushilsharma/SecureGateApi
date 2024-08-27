CREATE TABLE [dbo].[RoleMaster] (
    [RoleMasterId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [RoleName]     NVARCHAR (50)  NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [RoleParentId] BIGINT         NULL,
    [PageUrl]      NVARCHAR (500) NULL,
    [PageName]     NVARCHAR (500) NULL,
    [TanentCode]   NVARCHAR (250) NULL,
    [IsActive]     BIT            NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF_RoleMaster_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [CreatedBy]    BIGINT         NOT NULL,
    [UpdatedDate]  DATETIME       NULL,
    [UpdatedBy]    BIGINT         NULL,
    CONSTRAINT [PK_RoleMaster] PRIMARY KEY CLUSTERED ([RoleMasterId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the updater.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the role was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the creator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the role was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the role is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the page associated with the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'PageName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL associated with the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'PageUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the parent role,  if applicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'RoleParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'RoleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleMaster', @level2type = N'COLUMN', @level2name = N'RoleMasterId';


CREATE TABLE [dbo].[RoleWiseFieldAccess] (
    [RoleWiseFieldAccessId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [RoleId]                BIGINT         NOT NULL,
    [LoginId]               BIGINT         NULL,
    [ResourceId]            BIGINT         NULL,
    [PageId]                BIGINT         NOT NULL,
    [ObjectPropertiesId]    BIGINT         NULL,
    [PageControlId]         BIGINT         NULL,
    [IsMandatory]           BIT            NULL,
    [IsVisible]             BIT            NULL,
    [ValidationExpression]  NVARCHAR (50)  NULL,
    [Description]           NVARCHAR (500) NULL,
    [AccessId]              INT            NULL,
    [IsActive]              BIT            NULL,
    [CreatedBy]             BIGINT         NULL,
    [CreatedDate]           DATETIME       CONSTRAINT [DF_RoleWiseFieldAccess_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [UpdatedBy]             BIGINT         NULL,
    [UpdatedDate]           DATETIME       NULL,
    [IPAddress]             NVARCHAR (20)  NULL,
    [CompanyId]             BIGINT         NULL,
    [CompanyCode]           NVARCHAR (200) NULL,
    [TanentCode]            NVARCHAR (250) NULL,
    CONSTRAINT [PK_RoleWiseFieldAccess] PRIMARY KEY CLUSTERED ([RoleWiseFieldAccessId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the tenant.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP address.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'IPAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of update,  if applicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the updater,  if applicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of creation.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the creator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the record is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the access.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'AccessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the field access.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expression used for validation.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'ValidationExpression';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the field is visible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'IsVisible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the field is mandatory.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'IsMandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the page control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'PageControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the object properties.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'ObjectPropertiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'PageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the resource.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'ResourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the login.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'LoginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the role.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'RoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the role-wise field access.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWiseFieldAccess', @level2type = N'COLUMN', @level2name = N'RoleWiseFieldAccessId';


CREATE TABLE [dbo].[RoleWisePageMapping] (
    [RoleWisePageMappingId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [PageId]                BIGINT   NULL,
    [RoleMasterId]          BIGINT   NULL,
    [LoginId]               BIGINT   NULL,
    [AccessId]              INT      NULL,
    [CreatedBy]             BIGINT   NOT NULL,
    [CreatedDate]           DATETIME CONSTRAINT [DF_RoleWisePageMapping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [ModifiedBy]            BIGINT   NULL,
    [ModifiedDate]          DATETIME CONSTRAINT [DF_RoleWisePageMapping_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [IsActive]              BIT      NOT NULL,
    [TanentCode]            BIGINT   NULL,
    CONSTRAINT [PK_RoleWisePageMapping] PRIMARY KEY CLUSTERED ([RoleWisePageMappingId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the tenant.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the record is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of modification,  if applicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the modifier,  if applicable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time of creation.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User ID of the creator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the access.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'AccessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the login.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'LoginId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the role master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'RoleMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'PageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the role-wise page mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleWisePageMapping', @level2type = N'COLUMN', @level2name = N'RoleWisePageMappingId';


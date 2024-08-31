CREATE TABLE [dbo].[PageWiseConfiguration] (
    [PageWiseConfigurationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [PageId]                  BIGINT         NULL,
    [RoleId]                  BIGINT         NULL,
    [UserId]                  BIGINT         NULL,
    [CompanyId]               BIGINT         NULL,
    [SettingName]             NVARCHAR (150) NULL,
    [SettingValue]            NVARCHAR (150) NULL,
    [IsActive]                BIT            NOT NULL,
    [CreatedBy]               BIGINT         NOT NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF_PageWiseConfiguration_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [ModifiedBy]              BIGINT         NULL,
    [ModifiedDate]            DATETIME       CONSTRAINT [DF_PageWiseConfiguration_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CompanyCode]             NVARCHAR (200) NULL,
    [TanentCode]              NVARCHAR (250) NULL,
    CONSTRAINT [PK_PageWiseConfiguration] PRIMARY KEY CLUSTERED ([PageWiseConfigurationId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the tenant associated with the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the company associated with the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the configuration was last modified.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last modified the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the configuration was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the configuration is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Value of the setting.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'SettingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the setting.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'SettingName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the company associated with the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user associated with the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the role associated with the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'RoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'PageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageWiseConfiguration', @level2type = N'COLUMN', @level2name = N'PageWiseConfigurationId';


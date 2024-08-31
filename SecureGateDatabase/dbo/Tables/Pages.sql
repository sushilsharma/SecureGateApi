CREATE TABLE [dbo].[Pages] (
    [PageId]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ModuleId]                 BIGINT         NOT NULL,
    [PageName]                 NVARCHAR (100) NULL,
    [ResourceKey]              NVARCHAR (50)  NULL,
    [ParentPageId]             BIGINT         NULL,
    [ControllerName]           NVARCHAR (100) NULL,
    [ActionName]               NVARCHAR (100) NULL,
    [IsReport]                 BIT            NULL,
    [Description]              NVARCHAR (500) NULL,
    [PageIcon]                 NVARCHAR (100) NULL,
    [SequenceNumber]           INT            NULL,
    [IsActive]                 BIT            NOT NULL,
    [CreatedBy]                BIGINT         NOT NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_Pages_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]                BIGINT         NULL,
    [UpdatedDate]              DATETIME       NULL,
    [IPAddress]                NVARCHAR (20)  NULL,
    [IsInnerPage]              BIT            NULL,
    [ConfigurationAvailable]   INT            NULL,
    [PageType]                 BIGINT         NULL,
    [IsCommingSoonIndicator]   BIT            NULL,
    [IsPhoneNo]                BIT            NULL,
    [UserPersonaMasterId]      INT            NULL,
    [IsHomePageForUserPersona] BIT            NULL,
    [IsVisibleInMenu]          BIT            NULL,
    [IsReloadForcefully]       BIT            NULL,
    [IsRedirectToNewPortal]    BIT            NULL,
    [ApplicationName]          NVARCHAR (500) NULL,
    [Field1]                   NVARCHAR (500) NULL,
    [Field2]                   NVARCHAR (500) NULL,
    [Field3]                   NVARCHAR (500) NULL,
    [Field4]                   NVARCHAR (500) NULL,
    [Field5]                   NVARCHAR (500) NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([PageId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Pages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Field5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Pages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Field4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Pages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Field3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Pages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Field2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional Information For Pages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Field1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the application associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ApplicationName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page redirects to a new portal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsRedirectToNewPortal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page reloads forcefully.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsReloadForcefully';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is visible in the menu.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsVisibleInMenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is the home page for a user persona.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsHomePageForUserPersona';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user persona master associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'UserPersonaMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is a phone number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsPhoneNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is a coming soon indicator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsCommingSoonIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'PageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Availability of configuration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ConfigurationAvailable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is an inner page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsInnerPage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP address associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IPAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the page was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last updated the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the page was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sequence number for ordering pages.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'SequenceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Icon associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'PageIcon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Description of the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates if the page is a report.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'IsReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the action associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ActionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the controller associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ControllerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the parent page if it''s a sub-page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ParentPageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Key of the resource associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ResourceKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'PageName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the module associated with the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'ModuleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the page.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pages', @level2type = N'COLUMN', @level2name = N'PageId';


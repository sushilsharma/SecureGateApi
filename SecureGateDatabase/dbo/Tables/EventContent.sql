CREATE TABLE [dbo].[EventContent] (
    [EventContentId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventMasterId]            BIGINT         NULL,
    [EventCode]                NVARCHAR (250) NOT NULL,
    [NotificationTypeMasterId] BIGINT         NULL,
    [NotificationType]         NVARCHAR (250) NOT NULL,
    [Title]                    NVARCHAR (250) NULL,
    [BodyContent]              NVARCHAR (MAX) NULL,
    [PriorityTypeMasterId]     BIT            NULL,
    [PriorityTypeCode]         NVARCHAR (250) NULL,
    [IsActive]                 BIT            NOT NULL,
    [CreatedBy]                BIGINT         NOT NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_EventContent_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]                BIGINT         NULL,
    [UpdatedDate]              DATETIME       NULL,
    [CompanyId]                BIGINT         NULL,
    [CompanyCode]              NVARCHAR (200) NULL,
    [TanentCode]               BIGINT         NULL,
    CONSTRAINT [PK_EventContent] PRIMARY KEY CLUSTERED ([EventContentId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tenant code.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event content was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event content was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event content is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the priority type.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'PriorityTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the priority type master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'PriorityTypeMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Body content of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'BodyContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Title of the event content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'NotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the notification type master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'NotificationTypeMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the event master associated with the content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'EventMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventContent', @level2type = N'COLUMN', @level2name = N'EventContentId';


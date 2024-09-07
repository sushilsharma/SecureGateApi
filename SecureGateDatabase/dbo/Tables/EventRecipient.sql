CREATE TABLE [dbo].[EventRecipient] (
    [EventRecipientId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventContentId]           BIGINT         NULL,
    [EventMasterId]            BIGINT         NULL,
    [EventCode]                NVARCHAR (250) NOT NULL,
    [NotificationTypeMasterId] BIGINT         NULL,
    [NotificationType]         NVARCHAR (250) NOT NULL,
    [RecipientType]            NVARCHAR (250) NULL,
    [Recipient]                NVARCHAR (250) NULL,
    [RoleMasterId]             BIGINT         NULL,
    [UserId]                   BIGINT         NULL,
    [IsSpecific]               BIT            NULL,
    [IsActive]                 BIT            NOT NULL,
    [CreatedBy]                BIGINT         NOT NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_EventRecipient_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]                BIGINT         NULL,
    [UpdatedDate]              DATETIME       NULL,
    [CompanyId]                BIGINT         NULL,
    [CompanyCode]              NVARCHAR (200) NULL,
    [TanentCode]               BIGINT         NULL,
    CONSTRAINT [PK_EventRecipient] PRIMARY KEY CLUSTERED ([EventRecipientId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tenant code.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event recipient was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event recipient was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event recipient is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the recipient is specific.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'IsSpecific';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the role master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'RoleMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recipient of the event notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'Recipient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'RecipientType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'NotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the notification type master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'NotificationTypeMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'EventMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event content.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'EventContentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRecipient', @level2type = N'COLUMN', @level2name = N'EventRecipientId';


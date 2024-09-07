CREATE TABLE [dbo].[EventRetrySettings] (
    [EventRetrySettingsId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventMasterId]            BIGINT         NULL,
    [NotificationTypeMasterId] BIGINT         NULL,
    [NotificationType]         NVARCHAR (MAX) NULL,
    [RetryCount]               BIGINT         NULL,
    [RetryInterval]            BIGINT         NULL,
    [IsActive]                 BIT            NOT NULL,
    [Createdby]                BIGINT         NOT NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_EventRetrySettings_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NOT NULL,
    [UpdatedBy]                BIGINT         NULL,
    [UpdatedDate]              DATETIME       NULL,
    [TanentCode]               BIGINT         NULL,
    CONSTRAINT [PK_EventRetrySettings] PRIMARY KEY CLUSTERED ([EventRetrySettingsId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tenant code.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event retry settings were last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event retry settings.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event retry settings were created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event retry settings.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'Createdby';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event retry settings are active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interval between retry attempts.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'RetryInterval';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of retry attempts.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'RetryCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'NotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the notification type master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'NotificationTypeMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'EventMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event retry settings.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventRetrySettings', @level2type = N'COLUMN', @level2name = N'EventRetrySettingsId';


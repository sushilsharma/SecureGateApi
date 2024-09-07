CREATE TABLE [dbo].[EventNotification] (
    [EventNotificationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventMasterId]       BIGINT         NULL,
    [EventCode]           NVARCHAR (250) NULL,
    [ObjectId]            BIGINT         NULL,
    [ObjectType]          NVARCHAR (250) NULL,
    [IsCreated]           BIT            CONSTRAINT [DF_EventNotification_IsCreated] DEFAULT ((0)) NULL,
    [IsActive]            BIT            CONSTRAINT [DF_EventNotification_IsActive] DEFAULT ((1)) NULL,
    [CreatedBy]           BIGINT         NULL,
    [CreatedDate]         DATETIME       CONSTRAINT [DF_EventNotification_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [UpdatedDate]         DATETIME       NULL,
    [UpdatedBy]           BIGINT         NULL,
    [CompanyId]           BIGINT         NULL,
    [CompanyCode]         NVARCHAR (200) NULL,
    [TanentCode]          BIGINT         NULL,
    CONSTRAINT [PK_EventDetail_1] PRIMARY KEY CLUSTERED ([EventNotificationId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tenant code.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'TanentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the company.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who last updated the event notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event notification was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date when the event notification was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'User who created the event notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event notification is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag indicating whether the event notification has been created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'IsCreated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the object related to the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'ObjectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the object related to the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'ObjectId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Code of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'EventCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the associated event master.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'EventMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for the event notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EventNotification', @level2type = N'COLUMN', @level2name = N'EventNotificationId';


CREATE TABLE [dbo].[UserDeviceTokenMapping] (
    [UserDeviceTokenMappingId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]                   BIGINT         NOT NULL,
    [DeviceType]               NVARCHAR (150) NOT NULL,
    [DeviceToken]              NVARCHAR (550) NOT NULL,
    [IsExpired]                BIT            NULL,
    [IsActive]                 BIT            NULL,
    [VendorId]                 NVARCHAR (250) NULL,
    [PushNotificationType]     NVARCHAR (250) NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_UserDeviceTokenMapping_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CreatedBy]                BIGINT         NULL,
    [UpdatedDate]              DATETIME       NULL,
    [UpdatedBy]                BIGINT         NULL,
    CONSTRAINT [PK_UserDeviceTokenMapping] PRIMARY KEY CLUSTERED ([UserDeviceTokenMappingId] ASC) WITH (FILLFACTOR = 80)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last modified the record.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'UpdatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the record.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the record was created.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the push notification.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'PushNotificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the vendor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'VendorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the mapping is active.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the token is expired.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'IsExpired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token associated with the device.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'DeviceToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Type of the device.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'DeviceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier for the user.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each user detail device token mapping.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UserDeviceTokenMapping', @level2type = N'COLUMN', @level2name = N'UserDeviceTokenMappingId';

